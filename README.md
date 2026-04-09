To run tests, the randomiser requires certain folders and relevant FF7 files to be placed in the following location:
\Godo\Godo\bin\Debug

The folders that need to be produced are listed below:

*) Default Files

*) Default French Files

*) Default German Files

*) Default Spanish Files

*) Kernel Strings

*) Kernel2 Strings

*) Output Files

Some of these folders are optional if you aren't intending to make use of the language support features or the PS1 compatibility (MiscInput & Misc Output Folders).


To use the randomiser, you will need game files from Final Fantasy VII to use as a 'base', preferably default files but modded files may also work.
Into the Default Files folder, the following is needed:

*) Scene.bin (enemy data)

*) Kernel.bin (character data)

*) Kernel2.bin (text strings for character data on the PC version)

These can be located in the following places of an FF7 install:

PC98 Version: data\battle and data\kernel folder

Steam 2013: data\lang-en\battle and data\lang-en\kernel folder

Steam Remaster: ff7\workingdir\data\lang-en\battle and ff7\workingdir\data\lang-en\kernel

For other languages, the lang-xx folder will contain the same files (for example, lang-de for German). Currently, Japanese is not supported.


For the PS1 version, the files can be extracted using a tool like CDMage (preferably the 1.02 1B5 Beta version), opening the disc as an M2 track.
The scene.bin for the PS1 version can be found in BATTLE and the Kernel.bin can be found in INIT; bear in mind there is no Kernel2.bin for the PS1
version as the text strings are baked into the Kernel.bin itself.


When running the randomiser, a seed can be input and options can be toggled. The randomised files will be generated in the Output Files folder.
The uncompressed text strings will also be printed out as generic files that can be opened in Notepad++ in the Kernel & Kernel2 Strings folder
(the reason Kernel.bin has strings, despite going unused in the PC version, is because they are used in the PS1 version so the output of both
these folders is likely to be the same).

This program is still a work in progress and many of its original features were deactivated pending stability testing. If you opt to reactivate these
features then bear in mind that they will likely cause crashes in-game, or bizarre glitches.
