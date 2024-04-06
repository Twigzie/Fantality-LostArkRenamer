# Fantality-LostArkRenamer
[](https://github.com/Twigzie/Fantality-LostArkRenamer#fantality-lostarkrenamer)

A tool that renames obfuscated Lost Ark 'File\Folder' names into readable values.

# Usage

 - LostArkRenamer.exe [ source_file ]
	 > [**source_file**]: The source file to decrypt
	 >> Example: **LostArkRenamer.exe "C:\Program Files (x86)\Steam\steamapps\common\Lost Ark\EFGame\ReleasePC\Packages\0G1MAN0P84NX8I1MZZESXGZ.upk"**
	 >![](https://github.com/Twigzie/Fantality-LostArkRenamer/blob/main/images/arg_1.png)
                    
 - LostArkRenamer.exe [ source_file -r ]
	> [ **source_file** ]: The source file containing a list of encrypted file or folder names.
	> [ **-r** ]:\t\t The specified source contains an encrypted list of values that will be decrypted

 - LostArkRenamer.exe [ source_folder ]
	 > [ **source_folder** ]: The source folder to decrypt                    

 - LostArkRenamer.exe [ source_folder -r ]
	 > [ **source_folder** ]: The source folder to decrypt
	 > [ **-r** ]:  Performs a recursive search for the specified directory
                    
 - LostArkRenamer.exe [ source_folder -r -f ]
	 > [ **source_folder** ]: The source folder to search and rename both folders and files.
	 > [ **-r** ]: Performs a recursive search for the specified directory
	 > [ **-f** ]: Performs a recursive search and also decrypts files
