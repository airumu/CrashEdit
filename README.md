# CrashEdit: Re

This is a fork of [CrashEdit](https://github.com/cbhacks/CrashEdit) with some extra features and improvements.

## Changes

* **Old Model**
  
  * Added Polygon Editor.
  * Added Texture Editor.

* **Model / Compressed Model**
  
  * Added Polygon Editor.
  * Added Color Editor.
  * Added Texture Editor.
  * (Compressed Model) Added Extended Texture Editor.
  * (Compressed Model) Added Position Editor.
  * Made scales editable in the General tab.
  * Added support for decompressing compressed models.

* **Animation**
  
  * Added sync editing, which applies edits in sync with other frames when enabled.
  * Added Frame Editor; vertices for compressed ones are read-only.
  * Added vertex picking.
  * Added ability to toggle animation in the viewer.
  * Added support for rotating animations.

* **Scenery**
  
  - Added Color Editor.
  - Added Texture Editor.
  - Added Extended Texture Editor.
  - Added vertex picking.
  * Added a node to Scenery for entering the editor.

* **Zone**
  
  * Added Header Editor.
  * Added a "Change Collision Type" menu.
  * Camera entities are labeled as Camera[0], Camera[1], Camera[2], etc.
  * Entity editor now hides unnecessary tabs (e.g., the Camera tab for non-camera entities).
  * Renamed "Interpolate" to "Edit Path", added more features.
  * Added "Sync" to positions.
  * Added "Sync Entities" to positions.
  * Setting property can be copied and pasted.
  * Victims, load lists, and draw lists use list views; copy and paste are supported with shortcut keys.
  * Added a "Verify draw lists" feature.
  * Added Property Field Editor; properties can be saved to an external file and read from it (saved to `CrashEdit.exe.savedentityproperties.json`).
  * Added highlighting of the selected entity in the viewer.

* **GOOL**
  
  * Syntax highlighting is now available.
  * Double-clicking on a specific line jumps to a specific line (e.g., go to state #, call subroutine #, move # instructions, [Code|Trans|Event]: #).
  * Right-click to copy the selected line’s offset as hex.
  * Ctrl + G to go to the specified line.
  * Added Frame Groups Editor.
  * Tries to patch frame groups when GOOL is imported from Crash 3 to Crash 2.

* **Texture**
  
  * Can load external image files and replace textures with them.
  * Supports copying and pasting textures using a buffer, with shortcut keys available.
  * Allows moving or resizing the selection area by clicking or using drag-and-drop on the picture box.
  * Added a label to assist with CLUT calculations.
  * Added CLUT Editor.
  * Recalculates the checksum whenever changes are made.

* **Sound**
  
  * Can use a text box to set the frequency.
  * Sound and voice entries now use different default frequencies.
  * Supports loop audio playback.
  * Supports importing .VAG files (automatically detects and removes headers).
  * Supports importing .WAV files.

* **Music**
  
  * Added Music Entry Editor.
  * Added a SEQ player.
  * Added SEQTool.
  * Added VABTool.
  * Supports importing .MID files.
  * Fixed SEQ to MIDI export.

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
  
  * Added a "Rebuild" (c2export) button.
  * Added a "Reload" button.
  * Added search filters.
  * Added Node List Viewer.
  * Added a "Default game version" setting; use it to skip the game version select form.
  * Added a "Make BIN" form; settings are saved for next use.
  * Added "Entry Converter".
  * Added a "Generate EID" menu.
  * Supports drag&drap .NSF, .nsentry, and .nschunk files.
  * Patch NSD now shows fewer confirmation dialogs.

* **NSF Controller**
  
  * Added a "Add Texture Chunk" menu.
  * Added a "Import Entries Into New Chunks" menu.
  * Added a "Import and Replace Chunk" menu.
  * Added a "Import and Replace Entry" menu.
  * Added a "Analyze level" menu.
  * Added a "Search entities" menu.
  * Added a "Entity Editor" menu.
  * Added a "Scenery Editor" menu.
  * "Fix Nitro Detonators" and "Fix Box Count" can use an external list for calculations (saved to `CrashEdit.exe.externaldata.json`).

* **Chunk Controller**
  
  * Added a "Reload" menu.
  * Added a "Import and Replace Entry" menu.

* **Entry Controller**
  
  * Added a "Duplicate Entry" menu.
  * Added a "Reload" menu.
  * Added a "Replace Entry" menu.

* **Others**
  
  * Supports Dark mode.
  * Merged PR [Support exporting animation frames and scenery to OBJ with textures](https://github.com/cbhacks/CrashEdit/pull/158).
  * Added T21 Entry Editor.
  * Added NSDBox; added "Show GOOL Map" and "Generate Spawn Point" menus.
  * Added more icons.

## Third-Party Libraries

This project uses third-party libraries.
See THIRD_PARTY_LICENSES.txt for details.