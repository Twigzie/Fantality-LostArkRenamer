# Fantality-LostArkRenamer
[](https://github.com/Twigzie/Fantality-LostArkRenamer#fantality-lostarkrenamer)

A tool that renames obfuscated Lost Ark 'File\Folder' names into readable values.

# Usage

 - *LostArkRenamer.exe* [ source_file ]
	 > [**source_file**]: The source file to decrypt
	 >> ***Example***: **LostArkRenamer.exe "(STEAM PATH)\Lost Ark\EFGame\ReleasePC\Packages\0G1MAN0P84NX8I1MZZESXGZ.upk"**
	 >***Output***: 
	 >![](https://github.com/Twigzie/Fantality-LostArkRenamer/blob/main/images/arg_1.png)
                    
 - LostArkRenamer.exe [ source_file -r ]
	 > [ **source_file** ]: The source file containing a list of encrypted file or folder names.
	 >> [ **-r** ]: The specified source contains an encrypted [list](https://github.com/Twigzie/Fantality-LostArkRenamer/blob/main/files/testList.txt) of values that will be decrypted
	 
	 > ***Example***: **LostArkRenamer.exe "(Path)/list.txt" -r**
	 >***Output***: 
	 >![](https://github.com/Twigzie/Fantality-LostArkRenamer/blob/main/images/arg_2.png)
	 > NOTE: Only files that meet requirements are decrypted. As the above image shows, regular files and folders are shown as errors. Folders are errors cause the game only encrypts the files. I'll add support for folders later

# Not supported yet but working on it

 - LostArkRenamer.exe [ source_folder ]
	 > [ **source_folder** ]: The source folder to decrypt

 - LostArkRenamer.exe [ source_folder -r ]
	 > [ **source_folder** ]: The source folder to decrypt
	 >> [ **-r** ]:  Performs a recursive search for the specified directory
                    
 - LostArkRenamer.exe [ source_folder -r -f ]
	 > [ **source_folder** ]: The source folder to search and rename both folders and files.
	 >> [ **-r** ]: Performs a recursive search for the specified directory
	 >> [ **-f** ]: Performs a recursive search and also decrypts files
