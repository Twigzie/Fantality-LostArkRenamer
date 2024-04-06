using LostArkRenamer.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

#if DEBUG
                args = new string[] {
                    Path.Combine(Utils.Root, "1599341.txt"),
                    "-r"
                };
#endif

                if (args.Length == 0) {

                    Console.WriteLine("[Usage]");
                    Console.WriteLine();
                    Console.WriteLine("\t1) LostArkRenamer.exe [ source_file ]");
                    Console.WriteLine();
                    Console.WriteLine("\t\t[source_file]: The source file to decrypt");
                    Console.WriteLine();
                    Console.WriteLine("\t2) LostArkRenamer.exe [ source_file -r ]");
                    Console.WriteLine();
                    Console.WriteLine("\t\t[ source_file ]: The source file containing a list of encrypted file or folder names.");
                    Console.WriteLine("\t\t[ -r ]:\t\t The specified source contains an encrypted list of values that will be decrypted");
                    Console.WriteLine();
                    Console.WriteLine("\t3) LostArkRenamer.exe [ source_folder ]");
                    Console.WriteLine();
                    Console.WriteLine("\t\t[ source_folder ]: The source folder to decrypt");
                    Console.WriteLine();
                    Console.WriteLine("\t4) LostArkRenamer.exe [ source_folder -r ]");
                    Console.WriteLine();
                    Console.WriteLine("\t\t[ source_folder ]: The source folder to decrypt");
                    Console.WriteLine("\t\t[ -r ]:\t\t Performs a recursive search for the specified directory");
                    Console.WriteLine();
                    Console.WriteLine("\t5) LostArkRenamer.exe [ source_folder -r -f ]");
                    Console.WriteLine();
                    Console.WriteLine("\t\t[ source_folder ]: The source folder to search and rename both folders and files.");
                    Console.WriteLine("\t\t[ -r ]:\t\t Performs a recursive search for the specified directory");
                    Console.WriteLine("\t\t[ -f ]:\t\t Performs a recursive search and also decrypts files");
                    Console.WriteLine();

                }
                else {

                    //Check if the specified source is a file or a folder
                    if (Utils.IsFile(args[0])) {

                        //Check if we have any arguments and if they're valid
                        if (args.Length >= 2) {

                            //Specified argument is not one we support, error...
                            if (args[1].ToLower() != "-r") {
                                Utils.SetError("Invalid argument was specified");
                                Utils.SetWarning($"\t> Source: {args[0]}", false);
                                Utils.SetWarning($"\t> Argument: {args[1]}", false);
                            }
                            else {      
                                //Supported argument, set it for processing.
                                ProcessNames(args[0]);
                            }

                        }
                        else {
                            //No arguments found, process as a single encrypted file.
                            ProcessFile(args[0]);
                        }

                    }
                    else {

                    }

                }

            }
            catch (Exception ex) {
                Utils.SaveException(ex);
            }
            finally {

                Console.WriteLine();
                Console.WriteLine("Done!");
                Console.ReadLine();

            }

        }

        public static void ProcessFile(string source) {

            //TODO:

            try {

                Utils.SetOK("[Processing]");
                Console.WriteLine();
                Console.WriteLine($"\t> Type: 'File'");
                Console.WriteLine($"\t> Source: {source}");

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"\t> [Verifying]...");
                Console.ForegroundColor = ConsoleColor.White;

                var sourceName = Path.GetFileNameWithoutExtension(source);
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
        public static void ProcessFolder(string source, bool recursive, bool files) {

            try {

            }
            catch (Exception ex) {
                Utils.SaveException(ex);
            }

        }

    }

}