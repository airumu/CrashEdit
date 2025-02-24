using System.Windows.Media.Media3D;
using AltUI.Controls;
using AltUI.Forms;
using CrashEdit.Crash;
using MetroSet_UI.Controls;

namespace CrashEdit.CE
{
    public partial class InterpolatorForm : DarkForm
    {
        public static Dictionary<string, MathCalc> MathFuncs = new Dictionary<string, MathCalc>()
        {
            { "Linear", MathFunctionLinear },
            { "Inverse Linear", MathFunctionInverseLinear },
            { "Quadratic", MathFunctionDouble },
            { "Inverse Quadratic", MathFunctionInverseDouble }
        };

        private readonly List<Position> positions;
        private int positionindex;

        public InterpolatorForm(ICollection<Position> positions)
        {
            Icon = Embeds.GetIcon("ThingViolet");

            if (positions.Count < 2)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            this.positions = [.. positions];
            NewPositions = Array.Empty<Position>();

            InitializeComponent();

            // Tab1
            foreach (string name in MathFuncs.Keys)
                dpdFunc.Items.Add(name);
            dpdFunc.SelectedIndex = 0;
            numAmount.Maximum = short.MaxValue - positions.Count;
            numEnd.Maximum = positions.Count;
            numEnd_ValueChanged(null, null);
            UpdatePosition();

            // Tab2
            numAmount2.Value = positions.Count - 1;

            numAmount.MouseWheel += ScrollHandlerFunction;
            numAmount2.MouseWheel += ScrollHandlerFunction;

            Text = Properties.Resources.InterpolatorForm;
            cmdCancel.Text = Properties.Resources.InterpolatorForm_cmdCancel;
            cmdFirst.Text = Properties.Resources.InterpolatorForm_cmdFirst;
            cmdLast.Text = Properties.Resources.InterpolatorForm_cmdLast;
            cmdNext.Text = Properties.Resources.InterpolatorForm_cmdNext;
            cmdOK.Text = Properties.Resources.InterpolatorForm_cmdOK;
            cmdPrev.Text = Properties.Resources.InterpolatorForm_cmdPrev;
            fraAmount.Text = Properties.Resources.InterpolatorForm_fraAmount;
            fraBound.Text = Properties.Resources.InterpolatorForm_fraBound;
            fraFunction.Text = Properties.Resources.InterpolatorForm_fraFunction;
            fraOrder.Text = Properties.Resources.InterpolatorForm_fraOrder;
            fraPosition.Text = Properties.Resources.InterpolatorForm_fraPosition;
            fraTension.Text = Properties.Resources.InterpolatorForm_fraTension;
        }

        private double Tension => (double)numTension.Value;

        public Position[] NewPositions { get; private set; }
        public int Start => (int)numStart.Value;
        public int End => (int)numEnd.Value;
        public int Amount
        {
            get => (int)numAmount.Value;
            set => numAmount.Value = value;
        }
        public string Func => (string)dpdFunc.SelectedItem;
        public double Order => (double)numOrder.Value;

        public int Mode { get; private set; }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            CalcInterp();

            Mode = 0;
            DialogResult = DialogResult.OK;
        }

        private void cmdOK2_Click(object sender, EventArgs e)
        {
            GenerateCirclePoints();

            Mode = 2;
            DialogResult = DialogResult.OK;
        }

        private void cmdOK3_Click(object sender, EventArgs e)
        {
            ShiftPoints();

            Mode = 3;
            DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdatePosition()
        {
            lblPosition.Text = $"{positionindex + 1} / {positions.Count}";
            numX.Value = (decimal)positions[positionindex].X;
            numY.Value = (decimal)positions[positionindex].Y;
            numZ.Value = (decimal)positions[positionindex].Z;
            cmdPrev.Enabled = positionindex > 0;
            cmdNext.Enabled = positionindex < positions.Count - 1;
        }

        #region Tab1

        private void cmdPrev_Click(object sender, EventArgs e)
        {
            --positionindex;
            UpdatePosition();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            ++positionindex;
            UpdatePosition();
        }

        private void cmdFirst_Click(object sender, EventArgs e)
        {
            positionindex = 0;
            UpdatePosition();
        }

        private void cmdLast_Click(object sender, EventArgs e)
        {
            positionindex = positions.Count - 1;
            UpdatePosition();
        }

        private void numEnd_ValueChanged(object sender, EventArgs e)
        {
            numStart.Value = Math.Min(numEnd.Value - 1, numStart.Value);
            numStart.Maximum = numEnd.Value - 1;
            CalcInterp();
        }

        private void numAmount_ValueChanged(object sender, EventArgs e)
        {
            CalcInterp();
        }

        private void CalcInterp()
        {
            if (End - Start == 1)
            {
                Position start = positions[Start - 1];
                Position end = positions[End - 1];
                Position delta = end - start;
                NewPositions = new Position[Amount + 2];
                NewPositions[0] = start;
                NewPositions[NewPositions.Length - 1] = end;
                for (int i = 1, s = NewPositions.Length - 1; i < s + 1; ++i)
                {
                    NewPositions[i] = delta * (float)MathFuncs[Func].Invoke((double)i / s, Order) + start;
                }
                delta /= Amount + 1;
                lblAverage.Text = $"Average Point Distance: {(int)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y + delta.Z * delta.Z)}";
            }
            else
            {
                List<Position> subpositions = positions.GetRange(Start - 1, End - Start + 1);
                double[] weights = new double[subpositions.Count];
                weights[0] = weights[weights.Length - 1] = 1;
                for (int i = 1; i < weights.Length - 1; ++i)
                    weights[i] = Tension;
                Position start = positions[Start - 1];
                Position end = positions[End - 1];
                NewPositions = new Position[Amount + 2];
                NewPositions[0] = start;
                NewPositions[NewPositions.Length - 1] = end;
                Position[] oldpositions = new Position[NewPositions.Length * 2];
                oldpositions[0] = start;
                oldpositions[oldpositions.Length - 1] = end;
                double[] arclen = new double[oldpositions.Length];
                arclen[0] = 0;
                double dist = 0;
                Position distpos;
                for (int i = 1, s = oldpositions.Length - 1; i < s + 1; ++i)
                {
                    oldpositions[i] = GetBezierPoint(subpositions, weights, MathFuncs[Func].Invoke((double)i / s, Order));
                    distpos = oldpositions[i] - oldpositions[i - 1];
                    dist += Math.Sqrt(distpos.X * distpos.X + distpos.Y * distpos.Y + distpos.Z * distpos.Z);
                    arclen[i] = dist;
                }
                dist /= NewPositions.Length - 1;
                lblAverage.Text = $"Average Point Distance: {(int)dist}";
                // recalculate points for equidistance
                for (int i = 1, s = NewPositions.Length - 1; i < s; ++i)
                {
                    NewPositions[i] = FindPointByDistance(oldpositions, arclen, MathFuncs[Func].Invoke((double)i / s, Order));
                }
            }
        }

        private static Position FindPointByDistance(Position[] positions, double[] arclen, double t)
        {
            double targetlen = t * arclen[arclen.Length - 1];
            for (int i = 0; i < arclen.Length; ++i)
            {
                if (targetlen == arclen[i])
                    return positions[i];
                else if (targetlen < arclen[i])
                    return positions[i - 1] + (positions[i] - positions[i - 1]) * (float)((targetlen - arclen[i - 1]) / (arclen[i] - arclen[i - 1]));
            }
            return positions[positions.Length - 1];
        }

        private static readonly List<long[]> Binomials = new List<long[]>();
        private static long GetBinomial(int n, int o)
        {
            while (n >= Binomials.Count)
            {
                int m = Binomials.Count;
                long[] binomial = new long[m + 1];
                binomial[0] = 1;
                for (int i = 1; i < m; ++i)
                {
                    binomial[i] = Binomials[m - 1][i - 1] + Binomials[m - 1][i];
                }
                binomial[m] = 1;
                Binomials.Add(binomial);
            }
            return Binomials[n][o];
        }

        private static Position GetBezierBasisPoint(int controlcount, double[] weights, double t)
        {
            Position newpos = new Position(0, 0, 0);
            int n = controlcount - 1;
            for (int i = 0; i < controlcount; ++i)
            {
                newpos += (float)(GetBinomial(n, i) * Math.Pow(1.0 - t, n - i) * Math.Pow(t, i) * weights[i]) * Position.Unit;
            }
            return newpos;
        }

        private static Position GetBezierPoint(IList<Position> control, double[] weights, double t)
        {
            Position newpos = new Position(0, 0, 0);
            int n = control.Count - 1;
            for (int i = 0; i < control.Count; ++i)
            {
                newpos += (float)(GetBinomial(n, i) * Math.Pow(1.0 - t, n - i) * Math.Pow(t, i) * weights[i]) * control[i] / GetBezierBasisPoint(control.Count, weights, t);
            }
            return newpos;
        }

        public delegate double MathCalc(double x, double o);

        private static double MathFunctionLinear(double x, double o)
        {
            return Math.Pow(x, o);
        }

        private static double MathFunctionInverseLinear(double x, double o)
        {
            return 1 - MathFunctionLinear(-x + 1, o);
        }

        internal static double MathFuncQuadrPolinomial1(double x, double o)
        {
            return Math.Min(Math.Pow(2 * Math.Max(x, 0), o) / 2, 0.5);
        }

        internal static double MathFuncQuadrPolinomial2(double x, double o)
        {
            return 0.5 - MathFuncQuadrPolinomial1(1 - x, o);
        }

        private static double MathFunctionDouble(double x, double o)
        {
            return MathFuncQuadrPolinomial1(x, o) + MathFuncQuadrPolinomial2(x, o);
        }

        private static double MathFunctionInverseDouble(double x, double o)
        {
            return MathFuncQuadrPolinomial1(x - 0.5, o) + MathFuncQuadrPolinomial2(x + 0.5, o);
        }

        private void numStart_ValueChanged(object sender, EventArgs e)
        {
            CalcInterp();
        }

        private void numTension_ValueChanged(object sender, EventArgs e)
        {
            CalcInterp();
        }

        private void numOrder_ValueChanged(object sender, EventArgs e)
        {
            CalcInterp();
        }

        #endregion

        #region Tab2

        public double DegToRad(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public Point3D RotatePoint(Point3D point, double angleX, double angleY, double angleZ)
        {
            double cosX = Math.Cos(angleX), sinX = Math.Sin(angleX);
            double y1 = point.Y * cosX - point.Z * sinX;
            double z1 = point.Y * sinX + point.Z * cosX;
            double x1 = point.X;

            double cosY = Math.Cos(angleY), sinY = Math.Sin(angleY);
            double x2 = x1 * cosY + z1 * sinY;
            double z2 = -x1 * sinY + z1 * cosY;
            double y2 = y1;

            double cosZ = Math.Cos(angleZ), sinZ = Math.Sin(angleZ);
            double x3 = x2 * cosZ - y2 * sinZ;
            double y3 = x2 * sinZ + y2 * cosZ;
            double z3 = z2;

            return new Point3D((float)x3, (float)y3, (float)z3);
        }

        private void GenerateCirclePoints()
        {
            Amount = (int)numAmount2.Value;
            int vertexCount = Amount;
            double radius = (int)numRadius.Value;

            Position start;
            if (rdbPosition0.Checked)
            {
                start = positions[0];
            }
            else
            {
                List<Point3D> pts = new List<Point3D>();
                for (int i = 0; i < positions.Count; i++)
                {
                    pts.Add(new Point3D(positions[i].X, positions[i].Y, positions[i].Z));
                }
                // Remove the last point if it's the same as the first point (closed circle).
                if (pts.Count > 1 &&
                   pts[0].X == pts[pts.Count - 1].X &&
                   pts[0].Y == pts[pts.Count - 1].Y &&
                   pts[0].Z == pts[pts.Count - 1].Z)
                {
                    pts = pts.Take(pts.Count - 1).ToList();
                }

                double sumX = 0;
                double sumY = 0;
                double sumZ = 0;
                foreach (var pt in pts)
                {
                    sumX += pt.X;
                    sumY += pt.Y;
                    sumZ += pt.Z;
                }

                int count = pts.Count;
                start = new Position((float)(sumX / count), (float)(sumY / count), (float)(sumZ / count));
            }

            double centerX = start.X;
            double centerY = start.Y;
            double centerZ = start.Z;
            double angleStep = 2 * Math.PI / vertexCount;

            double angleX = DegToRad((int)numDegreeX.Value);
            double angleY = DegToRad((int)numDegreeY.Value);
            double angleZ = DegToRad((int)numDegreeZ.Value);

            double startAngle = DegToRad((int)numStartAngle.Value);

            List<Point3D> points = new List<Point3D>();
            for (int i = 0; i < vertexCount; i++)
            {
                double theta = startAngle + i * angleStep;
                double x = radius * Math.Cos(theta);
                double z = radius * Math.Sin(theta);
                double y = 0;
                Point3D point = new Point3D(x, y, z);

                Point3D rotatedPoint = RotatePoint(point, angleX, angleY, angleZ);
                rotatedPoint.X += centerX;
                rotatedPoint.Y += centerY;
                rotatedPoint.Z += centerZ;
                points.Add(rotatedPoint);
            }

            // Close the circle.
            points.Add(points[0]);

            NewPositions = new Position[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                NewPositions[i] = new Position((float)points[i].X, (float)points[i].Y, (float)points[i].Z);
            }
        }

        private void cmdDegXAdd45_Click(object sender, EventArgs e)
        {
            numDegreeX.Value = Math.Min(numDegreeX.Value + 45, 360);
        }

        private void cmdDegXSub45_Click(object sender, EventArgs e)
        {
            numDegreeX.Value = Math.Max(numDegreeX.Value - 45, -360);
        }

        private void cmdDegYAdd45_Click(object sender, EventArgs e)
        {
            numDegreeY.Value = Math.Min(numDegreeY.Value + 45, 360);
        }

        private void cmdDegYSub45_Click(object sender, EventArgs e)
        {
            numDegreeY.Value = Math.Max(numDegreeY.Value - 45, -360);
        }

        private void cmdDegZAdd45_Click(object sender, EventArgs e)
        {
            numDegreeZ.Value = Math.Min(numDegreeZ.Value + 45, 360);
        }

        private void cmdDegZSub45_Click(object sender, EventArgs e)
        {
            numDegreeZ.Value = Math.Max(numDegreeZ.Value - 45, -360);
        }

        private void cmdStartAngleAdd45_Click(object sender, EventArgs e)
        {
            numStartAngle.Value = Math.Min(numStartAngle.Value + 45, 360);
        }

        private void cmdStartAngleSub45_Click(object sender, EventArgs e)
        {
            numStartAngle.Value = Math.Max(numStartAngle.Value - 45, -360);
        }

        #endregion

        #region Tab3

        private void ShiftPoints()
        {
            Amount = positions.Count - 1;

            List<Point3D> pts = new List<Point3D>();
            for (int i = 0; i < positions.Count; i++)
            {
                pts.Add(new Point3D(positions[i].X, positions[i].Y, positions[i].Z));
            }

            List<Point3D> points = new List<Point3D>();
            for (int i = 0; i < pts.Count; i++)
            {
                double x = pts[i].X + (short)numShiftX.Value;
                double y = pts[i].Y + (short)numShiftY.Value;
                double z = pts[i].Z + (short)numShiftZ.Value;
                x = Math.Clamp(x, short.MinValue, short.MaxValue);
                y = Math.Clamp(y, short.MinValue, short.MaxValue);
                z = Math.Clamp(z, short.MinValue, short.MaxValue);

                points.Add(new Point3D(x, y, z));
            }

            NewPositions = new Position[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                NewPositions[i] = new Position((float)points[i].X, (float)points[i].Y, (float)points[i].Z);
            }
        }

        #endregion


        private void ScrollHandlerFunction(object sender, MouseEventArgs e)
        {
            if (sender is DarkNumericUpDown num)
            {
                HandledMouseEventArgs handledArgs = e as HandledMouseEventArgs;
                if (handledArgs != null)
                    handledArgs.Handled = true;

                int newValue = (int)num.Value;
                int increment = 1;
                if (e.Delta > 0)
                    newValue = (int)Math.Min(newValue += increment, num.Maximum);

                else if (e.Delta < 0)
                    newValue = (int)Math.Max(newValue -= increment, num.Minimum);

                num.Value = newValue;
            }
        }
    }
}
