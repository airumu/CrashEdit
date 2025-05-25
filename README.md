# CrashEdit: Re

This is a fork of [CrashEdit](https://github.com/cbhacks/CrashEdit) with some extra features and improvements.

## Improvements

* **Old Model**
  
  * Added polygon editor.
  * Added texture editor.

* **Model / Compressed Model**
  
  * Added polygon editor.
  * Added color editor.
  * Added texture editor.
  * (Compressed Model) Added extended texture editor.
  * (Compressed Model) Added position editor.
  * Scales are editable in the General tab.

* **Animation**
  
  * Supports synchronized editing, which applies edits in sync with other frames when enabled.
  * Added frame editor; vertices for compressed ones are read-only.

* **Scenery**
  
  - Added color editor.
  - Added texture editor.
  - Added extended texture editor.
  * Scenery now has a node, to enter the editor.

* **Zone**
  
  * Added header editor.
  * Added a menu "Change Collision Type".
  * Camera entities are labeled as Camera[0], Camera[1], Camera[2], etc.
  * Entity editor now hides unnecessary tabs (e.g., Camera tab in non-camera entities).
  * Renamed "Interpolate" to "Edit Path", added more features.
  * Added "Sync" to positions.
  * Added "Sync Entities" to positions.
  * Setting property can be copied and pasted.
  * Victims, load lists, and draw lists use list views; copy and paste are supported with shortcut keys.
  * Added 'Verify draw lists' feature.
  * Added property fields editor; properties can be saved to an external file and read from it (saved to `CrashEdit.exe.savedentityproperties.json`).

* **GOOL**
  
  * Syntax highlighting is now available.
  * Double-clicking on a specific line jumps to a specific line (e.g., go to state #, call subroutine #, move # instructions, [Code|Trans|Event]: #).
  * Right-click to copy the selected line’s offset as hex.
  * Ctrl + G to go to the specified line.
  * Added frame groups editor.
  * Tries to patch frame groups when GOOL is imported from Crash 3 to Crash 2.

* **Texture**
  
  * Can load external image files and replace textures with them.
  * Supports copying and pasting textures using a buffer, with shortcut keys available.
  * Allows moving or resizing the selection area by clicking or using drag-and-drop on the picture box.
  * Added a label to assist with CLUT calculations.
  * Added CLUT editor.
  * Recalculates the checksum whenever changes are made.

* **Sound**
  
  * Can use a text box to set the frequency.
  * Sound and voice entries now use different default frequencies.
  * Supports importing .VAG files (automatically detects and removes headers).

* **Music**
  
  * Added music entry editor.
  * Added SEQ player.
  * Added a button to open VABTool with the current VAB.
  * Fixed exporting SEQ as MIDI.

* **3D Viewer**
  
  * Hold right-click to increase movement speed.
  * Use the Z key to toggle aligned movement.
  * Added shortcut key (G) to toggle 3D entity display.
  * Supports more Crash 2 entities.

* **Hex Viewer**
  
  * Supports multi-cell selection.
  * Supports both copy, cut, and paste as bytes and as EID.
  * Use the +/- keys to change line width.
  * Ctrl+Space to clear the selected chunk.
  * Added "Import" and "Export" buttons.
  * Added a column header.
  * Added a "Goto" feature.
  * Displays position info.

* **MainForm / OldMainForm**
  
  * Added search filters.
  * Added node list viewer.
  * Added a "Default game version" setting; use it to skip the game version select form.
  * Added a "Make BIN" form; settings are saved for next use.
  * Added EntryConverterForm.
  * Added VABTool.
  * Added a "Generate EID" menu.
  * Patch NSD now shows fewer confirmation dialogs.

## Other Changes

* Supports Dark mode.
* Merged PR [Support exporting animation frames and scenery to OBJ with textures](https://github.com/cbhacks/CrashEdit/pull/158).
* Added NSDBox; added "Show GOOL Map" and "Generate Spawn Point" menus.
* Added a "Add Texture Chunk" menu to NSFController.
* Added a "Analyze level" menu to NSFController.
* Added a "Search entities" menu to NSFController.
* Added a "Edit Scenery" menu to NSFController.
* "Fix Nitro Detonators" and "Fix Box Count" can use an external list for calculations (saved to `CrashEdit.exe.externaldata.json`).
* Added more icons.
