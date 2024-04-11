using LostArkRenamer.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LostArkRenamer {

    internal class Program {

        private static readonly string OPT_MATCH = "^[A-Z0-9]*[0-9][A-Z0-9]*$";

        static void Main(string[] args) {

            try {

                Console.Clear();
                Console.WriteLine(@"--------------------------------------------------------------------------------------------------");
                Console.WriteLine(@"                   ___           ___           ___           ___           ___                    ");
                Console.WriteLine(@"                  /\__\         /\  \         /\__\         /\  \         /\  \                   ");
                Console.WriteLine(@"                 /::|  |       /::\  \       /::|  |       /::\  \       /::\  \                  ");
                Console.WriteLine(@"                /:|:|  |      /:/\:\  \     /:|:|  |      /:/\:\  \     /:/\:\  \                 ");
                Console.WriteLine(@"               /:/|:|__|__   /::\~\:\  \   /:/|:|  |__   /:/  \:\  \   /:/  \:\  \                ");
                Console.WriteLine(@"              /:/ |::::\__\ /:/\:\ \:\__\ /:/ |:| /\__\ /:/__/_\:\__\ /:/__/ \:\__\               ");
                Console.WriteLine(@"              \/__/~~/:/  / \/__\:\/:/  / \/__|:|/:/  / \:\  /\ \/__/ \:\  \ /:/  /               ");
                Console.WriteLine(@"                    /:/  /       \::/  /      |:/:/  /   \:\ \:\__\    \:\  /:/  /                ");
                Console.WriteLine(@"                   /:/  /        /:/  /       |::/  /     \:\/:/  /     \:\/:/  /                 ");
                Console.WriteLine(@"                  /:/  /        /:/  /        /:/  /       \::/  /       \::/  /                  ");
                Console.WriteLine(@"                  \/__/         \/__/         \/__/         \/__/         \/__/                   ");
                Console.WriteLine(@"                                                                                                  ");
                Console.WriteLine(@"                             -- A name De-obfuscator for Lost Ark --                              ");
                Console.WriteLine(@"                                                                                                  ");
                Console.WriteLine(@"--------------------------------------------------------------------------------------------------");
                Console.WriteLine($"> TheDeadNorth | v{Utils.Version}");
                Console.WriteLine($"> GitHub | https://github.com/Twigzie/Fantality-LostArkRenamer");
                Console.WriteLine($"> Updates | https://github.com/Twigzie/Fantality-LostArkRenamer/releases");
                Console.WriteLine(@"--------------------------------------------------------------------------------------------------");
                Console.WriteLine();

//#if DEBUG
//                //Debug purposes, just ignore this..
//                args = new string[] {
//                    //"l",
//                    //"J:\\GitHub\\Fantality-LostArkRenamer\\files\\testList.txt",
//                };
//#endif

                if (args.Length <= 0) {

                    Console.WriteLine("[Usage]");
                    Console.WriteLine();
                    Console.WriteLine("\t1) LostArkRenamer.exe [ source_file ]");
                    Console.WriteLine();
                    Console.WriteLine("\t\t[ source_file ]: The source file to decrypt");
                    Console.WriteLine();
                    Console.WriteLine("\t2) LostArkRenamer.exe [ source_folder ]");
                    Console.WriteLine();
                    Console.WriteLine("\t\t[ source_folder ]: The source folder to decrypt");
                    Console.WriteLine();
                    Console.WriteLine("\t3) LostArkRenamer.exe [ source_array ]");
                    Console.WriteLine();
                    Console.WriteLine("\t\t[ source_array ]: An array of files or folders to decrypt");
                    Console.WriteLine();
                    Console.WriteLine("\t4) LostArkRenamer.exe [ l ] [ source_file ]");
                    Console.WriteLine();
                    Console.WriteLine("\t\t[ l ]:\t\t The specified source contains an encrypted list of values that will be decrypted");
                    Console.WriteLine("\t\t[ source_file ]: The source file containing a list of encrypted file or folder names.");
                    Console.WriteLine();

                }
                else {

                    if (args.Length == 1) {

                        if (Utils.IsArg(args[0]) || args[0].Length == 1) {

                            Utils.SetError("Invalid argument specified!");
                            Utils.SetWarning($"\t> Type: 'Unknown'", false);
                            Utils.SetWarning($"\t> Argument: '{args[0]}'", false);
                            Utils.SetWarning($"\t> Error: The specified argument is not valid", false);

                        }
                        else {

                            if (Utils.IsFile(args[0])) {
                                ProcessName(args[0], true);
                            }
                            else {
                                ProcessName(args[0], false);
                            }

                        }

                    }
                    else {

                        if (args.Length == 2) {

                            switch (args[0].ToLower()) {
                                case "l":

                                    if (Utils.IsFile(args[1]) == false) {

                                        Utils.SetError("Invalid argument specified!");
                                        Utils.SetWarning($"\t> Type: 'list'", false);
                                        Utils.SetWarning($"\t> Argument: 'l'", false);
                                        Utils.SetWarning($"\t> Value: '{args[1]}'", false);
                                        Utils.SetWarning($"\t> Error: The specified value is not a file", false);

                                        break;

                                    }
                                    else {
                                        ProcessNames(args[1]);
                                    }

                                    break;

                                case "r":

                                    Utils.SetError("Invalid argument specified!");
                                    Utils.SetWarning($"\t> Type: 'folder'", false);
                                    Utils.SetWarning($"\t> Argument: 'r'", false);
                                    Utils.SetWarning($"\t> Value: '{args[1]}'", false);
                                    Utils.SetWarning($"\t> Error: The specified argument is not yet supported", false);

                                    break;

                                default:

                                    Utils.SetError("Invalid argument specified!");
                                    Utils.SetWarning($"\t> Type: 'Unknown'", false);
                                    Utils.SetWarning($"\t> Argument: '{args[0]}'", false);
                                    Utils.SetWarning($"\t> Error: The specified argument is not valid", false);

                                    break;

                                    //if (Utils.IsFile(args[1])) {

                                    //    Utils.SetError("Invalid argument specified!");
                                    //    Utils.SetWarning($"\t> Type: 'list'", false);
                                    //    Utils.SetWarning($"\t> Argument: 'l'", false);
                                    //    Utils.SetWarning($"\t> Value: '{args[1]}'", false);
                                    //    Utils.SetWarning($"\t> Error: The specified value is not a folder", false);

                                    //    break;

                                    //}
                                    //else {
                                    //    ProcessFolders(args[1]);
                                    //}

                                    //break;
                            }
                        }
                        else {

                            if (MessageBox.Show("Multiple assets were detected! Are you sure you want to process the specified files?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) {

                                //Provide information on the issue.
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Process canceled by the user.");
                                Console.ForegroundColor = ConsoleColor.White;

                                return;

                            }

                            foreach (string arg in args) {

                                if (Utils.IsArg(arg))
                                    continue;

                                else {

                                    if (Utils.IsFile(arg)) {
                                        ProcessName(arg, true);
                                    }
                                    else {
                                        ProcessName(arg, false);
                                    }

                                }

                            }

                        }

                    }

                }

            }
            catch (Exception ex) {
                Utils.SaveException(ex);
            }
            finally {

                Console.WriteLine();
                Console.WriteLine("Done!");
                Console.WriteLine();

            }

        }

        public static void ProcessName(string source, bool file = true) {

            //TODO:

            try {

                var type = file ? "File" : "Folder";

                Utils.SetOK("[Processing]");
                Console.WriteLine();
                Console.WriteLine($"\t> Type: '{type}'");
                Console.WriteLine($"\t> Source: {source}");

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"\t> [Verifying]...");
                Console.ForegroundColor = ConsoleColor.White;

                var sourceName = file ? Path.GetFileNameWithoutExtension(source) : new DirectoryInfo(source).Name;
                if (Regex.IsMatch(sourceName, OPT_MATCH) == false || sourceName.Length < 20) {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Failed!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                    Utils.SetWarning($"\t> Skipped: Source '{source}' is a filename that's not obfuscated.");

                }
                else {

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Pass!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                    var inputSource = Decryptor.Decrypt(sourceName);

                    Console.WriteLine();
                    Console.WriteLine($"\t\t> Input: {sourceName}");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\t\t> Output: {inputSource}");
                    Console.ForegroundColor = ConsoleColor.White;

                }

            }
            catch (Exception ex) {
                Utils.SaveException(ex);
            }

        }
        public static void ProcessNames(string source) {

            //TODO: Remove writing entries that are not encrypted.

            try {

                var sourceSkip = new List<string>();
                var sourceDecrypted = new List<string>();

                Utils.SetOK("[Processing]");
                Console.WriteLine();
                Console.WriteLine($"\t> Type: 'List'");
                Console.WriteLine($"\t> Source: {source}");
                Console.WriteLine();

                //Remove existing output files
                if (File.Exists(Utils.ExportOutput))
                    File.Delete(Utils.ExportOutput);

                using (var inputStream = File.Create(Utils.ExportOutput))
                using (var inputWriter = new StreamWriter(inputStream, Encoding.UTF8)) {

                    foreach (var entry in Utils.ReadStrings(source)) {

                        //If its a directory name, skip it. Only filenames are currently obfuscated but might change.
                        if (Utils.IsFile(entry) == false) {

                            //Just write it to the output regardless (for now). Might remove this later, IDK...
                            inputWriter.WriteLine(entry);

                            //Provide information on the issue.
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Skipped: '{entry}' Only filenames are currently encrypted.");
                            Console.ForegroundColor = ConsoleColor.White;

                            if (sourceSkip.Contains(entry) == false)
                                sourceSkip.Add(entry);

                            continue;

                        }
                        else {

                            var sourceName = Path.GetFileNameWithoutExtension(entry);
                            if (Regex.IsMatch(sourceName, OPT_MATCH) == false || sourceName.Length < 20) {

                                //Again, just write it to the output regardless
                                inputWriter.WriteLine(entry);

                                //Provide information on the issue.
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Skipped: '{entry}' is not encrypted or did not meet the requirements of an encrypted file.");
                                Console.ForegroundColor = ConsoleColor.White;

                                if (sourceSkip.Contains(entry) == false)
                                    sourceSkip.Add(entry);

                                continue;

                            }
                            else {

                                var inputSource = Decryptor.Decrypt(sourceName);
                                var inputTarget = entry.Replace(sourceName, inputSource);

                                inputWriter.WriteLine(Environment.NewLine);
                                inputWriter.WriteLine($"Source: {source}");
                                inputWriter.WriteLine($"Decrypted: {inputTarget}");

                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Decrypting: '{entry}'");
                                Console.ForegroundColor = ConsoleColor.White;

                                Console.WriteLine();
                                Console.WriteLine($"\t> Input: {sourceName}");

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\t> Output: {inputSource}");
                                Console.ForegroundColor = ConsoleColor.White;

                                if (sourceDecrypted.Contains(entry) == false)
                                    sourceDecrypted.Add(entry);

                            }

                        }

                    }

                    Console.WriteLine();
                    Console.WriteLine($"[Processing Complete] > '{sourceSkip.Count + sourceDecrypted.Count}' object(s) processed.");
                    Console.WriteLine();

                    if (sourceSkip.Count >= 1)
                        Utils.SetError($"\t> Entries Skipped: {sourceSkip.Count}", false);

                    if (sourceDecrypted.Count >= 1)
                        Utils.SetOK($"\t> Entries Decrypted: {sourceDecrypted.Count}");

                    if (File.Exists(Utils.ExportOutput)) {
                        Console.WriteLine();
                        Utils.SetWarning($"\t> Created File", false);
                        Utils.SetOK($"\t\t> {Utils.ExportOutput}");
                        Process.Start("explorer.exe", $"/select, {Utils.ExportOutput}");
                    }

                }

            }
            catch (Exception ex) {
                Utils.SaveException(ex);
            }

        }
        public static void ProcessFolders(string source, bool rename = false) {

            //TODO:

            try {

                //If the rename argument is passed, we need to ensure the user wants to do exactly that.
                if (rename) {

                    //Ensure the user wants to continue...I'll give them 3 chances to confirm their choice. I don't want to be responsible for anything that goes wrong.
                    if (MessageBox.Show($"Warning: You have selected to search the specified directory (see below) and rename 'ANY' folders that meet the encryption pattern! This WILL rename ANY folder(s) within the specified directory and\\or ANY folder(s) it finds. This cannot be undone, please make sure you want to do this. I will NOT be responsible for any problems that come with choosing to continue. Continue?\r\rSource: {source}", "First Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes &&
                        MessageBox.Show($"Again, I will NOT be responsible, make sure the specified directory (see below) is correct and that you really want to continue!\r\r" +
                                        $"Source: {source}\r\rIs the specified directory above correct? if not, click NO", "Second Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes &&
                        MessageBox.Show($"Final warning! Any folder that meets the pattern will be renamed!\r" +
                                        $"Please ensure the specified directory (see below) is correct!\r\r" +
                                        $"Source: {source}\r\r" +
                                        $"Continue?", "Last Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.No)


                    //Provide information on the issue.
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Process canceled by the user.");
                    Console.ForegroundColor = ConsoleColor.White;

                    //Exit the application.
                    return;

                }

                var sourceSkip = new List<string>();
                var sourceDecrypted = new List<string>();

                Utils.SetOK("[Processing]");
                Console.WriteLine();
                Console.WriteLine($"\t> Type: 'Recursive Directory'");
                Console.WriteLine($"\t> Source: {source}");
                Console.WriteLine();

                //Remove existing output files
                if (File.Exists(Utils.ExportOutput))
                    File.Delete(Utils.ExportOutput);

                using (var inputStream = File.Create(Utils.ExportOutput))
                using (var inputWriter = new StreamWriter(inputStream, Encoding.UTF8)) {

                    foreach (var entry in Utils.ReadStrings(source)) {

                        //If its a directory name, skip it. Only filenames are currently obfuscated but might change.
                        if (Utils.IsFile(entry) == false) {

                            //Just write it to the output regardless (for now). Might remove this later, IDK...
                            inputWriter.WriteLine(entry);

                            //Provide information on the issue.
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Skipped: '{entry}' Only filenames are currently encrypted.");
                            Console.ForegroundColor = ConsoleColor.White;

                            if (sourceSkip.Contains(entry) == false)
                                sourceSkip.Add(entry);

                            continue;

                        }
                        else {

                            var sourceName = Path.GetFileNameWithoutExtension(entry);
                            if (Regex.IsMatch(sourceName, OPT_MATCH) == false || sourceName.Length < 20) {

                                //Again, just write it to the output regardless
                                inputWriter.WriteLine(entry);

                                //Provide information on the issue.
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Skipped: '{entry}' is not encrypted or did not meet the requirements of an encrypted file.");
                                Console.ForegroundColor = ConsoleColor.White;

                                if (sourceSkip.Contains(entry) == false)
                                    sourceSkip.Add(entry);

                                continue;

                            }
                            else {

                                var inputSource = Decryptor.Decrypt(sourceName);
                                var inputTarget = entry.Replace(sourceName, inputSource);

                                inputWriter.WriteLine(Environment.NewLine);
                                inputWriter.WriteLine($"Source: {source}");
                                inputWriter.WriteLine($"Decrypted: {inputTarget}");

                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Decrypting: '{entry}'");
                                Console.ForegroundColor = ConsoleColor.White;

                                Console.WriteLine();
                                Console.WriteLine($"\t> Input: {sourceName}");

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\t> Output: {inputSource}");
                                Console.ForegroundColor = ConsoleColor.White;

                                if (sourceDecrypted.Contains(entry) == false)
                                    sourceDecrypted.Add(entry);

                            }

                        }

                    }

                    Console.WriteLine();
                    Console.WriteLine($"[Processing Complete] > '{sourceSkip.Count + sourceDecrypted.Count}' object(s) processed.");
                    Console.WriteLine();

                    if (sourceSkip.Count >= 1)
                        Utils.SetError($"\t> Entries Skipped: {sourceSkip.Count}", false);

                    if (sourceDecrypted.Count >= 1)
                        Utils.SetOK($"\t> Entries Decrypted: {sourceDecrypted.Count}");

                    if (File.Exists(Utils.ExportOutput)) {
                        Console.WriteLine();
                        Utils.SetWarning($"\t> Created File", false);
                        Utils.SetOK($"\t\t> {Utils.ExportOutput}");
                        Process.Start("explorer.exe", $"/select, {Utils.ExportOutput}");
                    }

                }

            }
            catch (Exception ex) {
                Utils.SaveException(ex);
            }

        }

    }

}