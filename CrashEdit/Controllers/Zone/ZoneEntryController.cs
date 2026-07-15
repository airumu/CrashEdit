using AltUI.Forms;
using CrashEdit.CE.Forms;
using CrashEdit.Crash;
using System.Text.RegularExpressions;

namespace CrashEdit.CE
{
    [OrphanLegacyController(typeof(ZoneEntry))]
    public sealed class ZoneEntryController : EntryController
    {
        public ZoneEntryController(ZoneEntry zoneentry, SubcontrollerGroup parentGroup) : base(zoneentry, parentGroup)
        {
            ZoneEntry = zoneentry;
            AddMenuSeparator();
            AddMenu(CrashUI.Properties.Resources.ZoneEntryController_AcAddEntity, "Add", Menu_AddEntity);
            AddMenu(CrashUI.Properties.Resources.ZoneEntryController_AcChangeCollisionType, "Wrench", Menu_ChangeCollisionType);

            if (GameVersion == GameVersion.Crash2 && ZoneEntry.Entities.Count !=0) {
                AddMenu(CrashUI.Properties.Resources.ZoneEntryController_AcChangeEnvironmentType, "Wrench", Menu_ChangeEnvironmentType);

            }

        }

        public override bool EditorAvailable => true;

        public override Control CreateEditor()
        {
            return new ZoneEntryViewer(GetNSF(), Entry.EID);
        }

        public ZoneEntry ZoneEntry { get; }



        void Menu_AddEntity()
        {
            short id = 10;
            while (true)
            {
                foreach (ZoneEntry zone in GetEntries<ZoneEntry>())
                {
                    foreach (Entity otherentity in zone.Entities)
                    {
                        if (otherentity.ID == id)
                        {
                            goto FOUND_ID;
                        }
                    }
                }
                break;
            FOUND_ID:
                ++id;
                continue;
            }
            Entity newentity = Entity.Load(new Entity(new Dictionary<short, EntityProperty>()).Save());
            newentity.ID = id;
            ZoneEntry.Entities.Add(newentity);
            ++ZoneEntry.EntityCount;
        }

        void Menu_ChangeCollisionType()
        {
            try
            {
                byte[] searchPattern = null!;
                byte[] replacementPattern = null!;
                using (InputWindow inputWindows = new InputWindow(CrashUI.Properties.Resources.ZoneEntryController_AcChangeCollisionType, "Wrench",
                    "Enter collision type to replace (as a literal):", string.Empty, 4,
                    "Enter new collision type (as a literal):", string.Empty, 4))
                {
                    if (inputWindows.ShowDialog() == DialogResult.OK)
                    {
                        string input = inputWindows.Input;
                        string input2 = inputWindows.Input2;
                        if (input.Length != 4 || input2.Length != 4)
                        {
                            throw new ArgumentException("The input must be specified as a 4-digit hexadecimal number.");
                        }

                        searchPattern = BitConverter.GetBytes(Convert.ToUInt16(input, 16));
                        replacementPattern = BitConverter.GetBytes(Convert.ToUInt16(input2, 16));
                    }
                    else return;
                }

                byte[] layout = ZoneEntry.Layout;
                for (int i = 0x24; i <= layout.Length - searchPattern.Length; i += 2)
                {
                    bool isMatch = true;

                    for (int j = 0; j < searchPattern.Length; j++)
                    {
                        if (layout[i + j] != searchPattern[j])
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        for (int j = 0; j < replacementPattern.Length; j++)
                        {
                            layout[i + j] = replacementPattern[j];
                        }
                    }
                }

                ZoneEntry.Layout = layout;
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"Error: {ex.Message}", CrashUI.Properties.Resources.ZoneEntryController_AcChangeCollisionType);
                return;
            }
        }



        void Menu_ChangeEnvironmentType()
        {
            try
            {

                int numberOfCamera = ZoneEntry.Zoneheader.CameraCount;

                int targetCamera = 1;

                if (numberOfCamera < 1) { return; }

                if (numberOfCamera % 3 != 0) {
                    DarkMessageBox.ShowError($"Cameras should be in groups of 3", "Cameras Error");
                    return;
                }

                if (numberOfCamera > 3)
                {

                    int maxIndexCam = (numberOfCamera / 3) - 1;

                using (InputWindow inputWindow = new InputWindow($"Multiple Camera Detected In {ZoneEntry.Title}", "Select Camera Number", $"Enter Cam Number: [0-{maxIndexCam}]", string.Empty, 1))
                    {

                        if (inputWindow.ShowDialog() == DialogResult.OK)
                        {
                            if (inputWindow.Input.Length == 0)
                            {
                                DarkMessageBox.ShowError("Please input camera number", "Error empty");
                                return;
                            }


                            if (Regex.IsMatch(inputWindow.Input, @"^\d$"))
                            {

                                int intInput = int.Parse(inputWindow.Input);
                                if (0 <= intInput && intInput <= maxIndexCam)
                                {
                                    targetCamera = 1 + (intInput * 3);
                                }
                                else {
                                    DarkMessageBox.ShowError("Index out of range, please enter a valid camera number", "Error bad input");
                                    return;

                                }

                            }
                            else {

                                DarkMessageBox.ShowError("Please input camera number", "Error bad input");
                                return;
                            }
                        }
                        else {
                            return;
                        }
                    }
                }


                Entity cameraProperty = ZoneEntry.Entities[targetCamera];

                if (cameraProperty.CameraIndex == null || cameraProperty.CameraSubIndex == null)
                {
                    DarkMessageBox.ShowError("Error", "Error");
                    return;
                }



                using (EnvironmentEditor inputWindows = new EnvironmentEditor())
                {


                    short flagsID = 0x185;
                    short FogDistanceID = 0x1DE;
                    short particles1ID = 0x1B5;
                    short particles2ID = 0x1B6;
                    short lastValueForPropertyParticle = 0x770;
                    uint particleActivation = 0x00000010;
                    uint fogValueRange = 0x00000040u;
                    uint flagPropFogEnable = 0x00200000;
                    uint particleVisibilityRange = 0xE1000A00;




                    if (inputWindows.ShowDialog() == DialogResult.OK)
                    {

                        uint flagPropFog = 0;


                        if (inputWindows.UseFog)
                        {
                            flagPropFog = flagPropFogEnable;


                            byte resultFogDistance = (byte)inputWindows.FogValue;
                            uint fogValueEx = fogValueRange | ((uint)resultFogDistance << 8);

                            var propFogDistance = CreatePropWithRows();
                            propFogDistance = AddValueToProp([0, 1, fogValueEx], 0, propFogDistance);

                            cameraProperty.FogDistance = propFogDistance;
                            cameraProperty.KnownProperties[FogDistanceID] = propFogDistance;

                        }
                        else
                        {
                                cameraProperty.FogDistance = null!;
                                cameraProperty.KnownProperties.Remove(FogDistanceID);
                        }


                        if (inputWindows.UseRecolor) {

                            short bgColorID = 0x1FA;

                            var bgColorProp = CreatePropWithRows();
                            bgColorProp = AddValueToProp([1, inputWindows.BackgroundTextureGapColor, 0], 0, bgColorProp);

                            cameraProperty.Backgrounds = bgColorProp;
                            cameraProperty.KnownProperties[bgColorID] = bgColorProp;

                        }



                        if (!inputWindows.ParticleEffectIsActive)
                        {

                            cameraProperty.Particles1 = null!;

                            cameraProperty.KnownProperties.Remove(particles1ID);

                            cameraProperty.Particles2 = null!;

                            cameraProperty.KnownProperties.Remove(particles2ID);


                        }
                        else 
                        {

                            flagPropFog = flagPropFog | particleActivation;


                            var particles1Prop = new EntityVictimProperty();
                            particles1Prop.Rows.Add(new EntityPropertyRow<EntityVictim>());
                            particles1Prop.Rows[0].MetaValue = 0;


                            short[] values = [(short)inputWindows.VelocityParticleValue[0], (short)inputWindows.VelocityParticleValue[1], (short)inputWindows.VelocityParticleValue[2], (short)inputWindows.ParticleAmountValue, 0, lastValueForPropertyParticle];
                            particles1Prop = AddValueToEntityVictimProp(values, 0, particles1Prop);


                            cameraProperty.Particles1 = particles1Prop;
                            cameraProperty.KnownProperties[particles1ID] = particles1Prop;


                            var particles2Prop = CreatePropWithRows();



                            uint lowerParticle = inputWindows.LowerParticleColor;
                            uint upperParticle = inputWindows.UpperParticleColor;
                            uint particleVisibility = particleVisibilityRange | (uint)inputWindows.ParticleVisibilityValue;

                            if (inputWindows.UseParticleOneColor)
                            {
                                lowerParticle = inputWindows.UpperParticleColor; 
                            }


                            particles2Prop = AddValueToProp([upperParticle,lowerParticle, particleVisibility], 0, particles2Prop);

                            cameraProperty.Particles2 = particles2Prop;
                            cameraProperty.KnownProperties[particles2ID] = particles2Prop;



                        }


                        var flagsProp = CreatePropWithRows();

                        flagsProp.Rows[0].Values.Add(flagPropFog);

                        cameraProperty.Flags = flagsProp;
                        cameraProperty.KnownProperties[flagsID] = flagsProp;

                            

                    }



                }

                return;




            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError($"Error: {ex.Message}", "");
            }
        }


        static EntityUInt32Property AddValueToProp(uint[] values, int rowIndex, EntityUInt32Property prop)
        {

            for (int i = 0; i < values.Length; i++) {

                prop.Rows[rowIndex].Values.Add(values[i]);

            }
            return prop;
        }


        static EntityVictimProperty AddValueToEntityVictimProp(short[] values, int rowIndex, EntityVictimProperty prop)
        {

            for (int i = 0; i < values.Length; i++)
            {

                prop.Rows[rowIndex].Values.Add(new EntityVictim((short)values[i]));

            }
            return prop;
        }


        static EntityUInt32Property CreatePropWithRows() {


            var prop = new EntityUInt32Property();
            prop.Rows.Add(new EntityPropertyRow<uint>());
            prop.Rows[0].MetaValue = 0;

            return prop;


        }




    }
}



