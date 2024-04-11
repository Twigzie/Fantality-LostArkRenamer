
# Fantality-LostArkRenamer
[](https://github.com/Twigzie/Fantality-LostArkRenamer#fantality-lostarkrenamer)

A tool that renames obfuscated Lost Ark 'File\Folder' names into readable values.

# Usage

**LostArkRenamer.exe** [ *source_file* | *source_folder* | *source_array* ]
> [ **source_file** ] *The source file to be decrypted*
> -  **Example:** `LostArkRenamer.exe "(FOLDER)\0G1MAN0P84NX8I1MZZESXGZ.upk"`
>
<<<<<<< Updated upstream
> -  **Example:** `LostArkRenamer.exe "(PATH)\0G1MAN0P84NX8I1MZZESXGZ.upk"`
> > ![Example_1](https://github.com/Twigzie/Fantality-LostArkRenamer/blob/main/images/arg_1.png)

**LostArkRenamer.exe** [ *source_file* *-r* ]
> [ **source_file** ] *The source file to decrypt*
> [ **-r** ] *The specified source contains an encrypted ([example](https://github.com/Twigzie/Fantality-LostArkRenamer/blob/main/files/testList.txt)) list of values that will be decrypted*
>
> -  **Example:** `LostArkRenamer.exe "(FOLDER)\0G1MAN0P84NX8I1MZZESXGZ.upk" -r`
> > ![Example_2](https://github.com/Twigzie/Fantality-LostArkRenamer/blob/main/images/arg_2.png)

---
=======
> [ **source_folder** ] *The source folder to be decrypted*
> -  **Example:** `LostArkRenamer.exe "(FOLDER)"`
>
> [ **source_array** ] *an array of source_files or source_folders to be decrypted*
> -  **Example:** `LostArkRenamer.exe "(FOLDER)\0G1MAN0P84NX8I1MZZESXGZ.upk" "(FOLDER)"`
>
> **NOTE**: *Files and folders can also be dropped onto the executable*
>>>>>>> Stashed changes

**LostArkRenamer.exe** [ *l* *source_list*  ]
> [ **l** ] *The specified source contains an encrypted list of values that will be decrypted*
> [ **source_list** ] *The source list to read from*
> -  **Example:** `LostArkRenamer.exe l "(FOLDER)\testList.txt"`
>
> **NOTE:** *([Heres](https://github.com/Twigzie/Fantality-LostArkRenamer/blob/main/files/testList.txt)) an example list*
>
> **NOTE**: *Only files that meet requirements are decrypted. As the above image shows, regular files and folders are shown as errors. Folders are errors cause the game only encrypts the files. I'll add support for folders later.**
>
> **NOTE**: *This will create a txt file in the programs directory called **export.txt** with all the decrypted names (if any)*

---

# Not supported, but working on

- Folders
- Folders (recursive, including files included)
