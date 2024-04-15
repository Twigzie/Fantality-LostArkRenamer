
# Fantality-LostArkRenamer
[](https://github.com/Twigzie/Fantality-LostArkRenamer#fantality-lostarkrenamer)

A tool that renames obfuscated Lost Ark 'File\Folder' names into readable values.

# Usage

**LostArkRenamer.exe** [ *source_file* | *source_folder* | *source_array* ]
> [ **source_file** ] *The source file to be decrypted*
> -  **Example:** `LostArkRenamer.exe "(FOLDER)\0G1MAN0P84NX8I1MZZESXGZ.upk"`
>
> [ **source_folder** ] *The source folder to be decrypted*
> -  **Example:** `LostArkRenamer.exe "(FOLDER)"`
>
> [ **source_array** ] *an array of source_files or source_folders to be decrypted*
> -  **Example:** `LostArkRenamer.exe "(FOLDER)\0G1MAN0P84NX8I1MZZESXGZ.upk" "(FOLDER)"`
>

> [!note]
> *Files and folders can also be dropped onto the executable*

**LostArkRenamer.exe** [ *l* *source_list*  ]
> [ **l** ] *The specified source contains an encrypted list of values that will be decrypted*
> [ **source_list** ] *The source list to read from*
> -  **Example:** `LostArkRenamer.exe l "(FOLDER)\testList.txt"`

> [!note]
> *([Heres](https://github.com/Twigzie/Fantality-LostArkRenamer/blob/main/files/testList.txt)) an example list*
> 
> *Only files that meet requirements are decrypted. Regular files and folders are shown as errors. Folders are errors cause the game only encrypts the files. I'll add support for folders later.**
> 
> *This will create a txt file in the program's directory called **export.txt** with all the decrypted names (if any)*

---

# Not supported, but working on

- Folders
- Folders (recursive, including files included)
