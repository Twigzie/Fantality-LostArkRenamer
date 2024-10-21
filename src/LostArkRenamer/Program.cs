using LostArkRenamer.Classes;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LostArkRenamer {

    internal class Program {

        private static readonly string OPT_MATCH = "^[A-Z0-9]*[0-9][A-Z0-9]*$";

        [STAThread]
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
                //
                //    //Debug purposes, just ignore this..
                //    args = new string[] {
                //        @"C:\Users\parad\Desktop\New folder",
                //        //@"C:\Program Files (x86)\Steam\steamapps\common\Lost Ark\EFGame\ReleasePC\1V7NU6V7N5L6TCDAER3YA.u",
                //    };
                //
                //#endif

                if (args.Length <= 0) {

                    Console.WriteLine("[Usage]");
                    Console.WriteLine();
                    Console.WriteLine("\t1) LostArkRenamer.exe [ source_file ]");
                    Console.WriteLine("\t\t[ source_file ]: The source file to decrypt");
                    Console.WriteLine();
                    Console.WriteLine("\t2) LostArkRenamer.exe [ source_folder ]");
                    Console.WriteLine("\t\t[ source_folder ]: The source folder to decrypt");
                    Console.WriteLine();

                }
                else {

                    if (Utils.IsFile(args[0])) {
                        ProcessFile(args[0]);
                    }
                    else {
                        ProcessFolder(args[0]);
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

                Console.ReadLine();

            }

        }

        public static void ProcessFile(string source) {

            try {

                Utils.SetOK("[Processing File]");
                Console.WriteLine();
                Console.WriteLine($"\t> Source: {source}");
                Console.WriteLine();

                var inputSource = Path.GetFileNameWithoutExtension(source);
                if (Regex.IsMatch(inputSource, OPT_MATCH) == false || inputSource.Length < 20) {

                    Utils.SetWarning($"\t> Skipped: '{source}' is not obfuscated.");
                    Console.WriteLine();

                    return;

                }
                else {

                    var inputName = Decryptor.Decrypt(inputSource);
                    using (var SFD = new SaveFileDialog()) {

                        SFD.Title = "Select a save location";
                        SFD.FileName = $"{inputName}{Path.GetExtension(source)}";

                        if (SFD.ShowDialog() != DialogResult.OK)
                            return;

                        try {

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\t\t> Input: {inputSource}");

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\t\t> Output: {SFD.FileName}");
                            Console.ForegroundColor = ConsoleColor.White;

                            File.Copy(source, SFD.FileName, true);

                        }
                        catch (Exception ex) {
                            Utils.SaveException(ex);
                            return;
                        }

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[Complete]");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();

                    }
                }

            }
            catch (Exception ex) {
                Utils.SaveException(ex);
            }

        }
        public static void ProcessFolder(string source) {

            try {

                Utils.SetOK("[Processing Folder]");
                Console.WriteLine();
                Console.WriteLine($"\t> Source: {source}");
                Console.WriteLine();

                using (var FBD = new FolderBrowserDialog()) {

                    FBD.Description = "Select a save location.";
                    FBD.ShowNewFolderButton = true;

                    if (FBD.ShowDialog() != DialogResult.OK)
                        return;

                    if (MessageBox.Show("Are you sure you want to process all files found? This could take some time depending on how many files are found. Continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    foreach (var sourceFile in Utils.ReadFiles(source)) {

                        var inputName = Path.GetFileNameWithoutExtension(sourceFile);
                        if (Regex.IsMatch(inputName, OPT_MATCH) == false || inputName.Length < 20) {

                            Utils.SetWarning($"\t> Skipped: '{source}' is not obfuscated.");
                            Console.WriteLine();

                            continue;

                        }
                        else {                        

                            var inputSource = Decryptor.Decrypt(inputName);
                            var inputTarget = $"{FBD.SelectedPath}\\{inputSource}{Path.GetExtension(sourceFile)}";

                            try {

                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"\t\t> Input: {inputName}");

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\t\t> Output: {inputTarget}");

                                Console.ForegroundColor = ConsoleColor.White;

                                File.Copy(sourceFile, inputTarget, true);

                            }
                            catch (Exception ex) {
                                Utils.SaveException(ex);
                                return;
                            }

                        }

                    }

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[Complete]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                }

            }
            catch (Exception ex) {
                Utils.SaveException(ex);
            }

        }

    }

}