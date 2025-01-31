﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LostArkRenamer.Classes {

    internal static class Utils {

        public static string Version {
            get => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        public static string Root {
            get => Environment.CurrentDirectory;
        }

        public static string ErrorOutput {
            get => Path.Combine(Root, "error.txt");
        }
        public static string ExportInput {
            get => Path.Combine(Root, "list.txt");
        }
        public static string ExportOutput {
            get => Path.Combine(Root, "export.txt");
        }

        public static bool IsFile(string source) {
            try {
                var attributes = File.GetAttributes(source);
                if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    return false;
                return true;
            }
            catch {
                if (string.IsNullOrEmpty(Path.GetExtension(source)))
                    return false;
                return true;
            }
        }

        public static void SetInfo(string result, bool title = true) {
            Console.ForegroundColor = ConsoleColor.Green;
            if (title) {
                Console.WriteLine($"[Info]: {result}");
            }
            else
                Console.WriteLine($"{result}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void SetOK(string result) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void SetError(string result, bool title = true) {
            Console.ForegroundColor = ConsoleColor.Red;
            if (title) {
                Console.WriteLine($"[Error]: {result}");
            }
            else
                Console.WriteLine($"{result}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void SetWarning(string result, bool title = true) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (title) {
                Console.WriteLine($"[Warning]: {result}");
            }
            else
                Console.WriteLine($"{result}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void SaveException(Exception result) {
            try {

                SetError(result.ToString());

                using (var stream = File.OpenWrite(ErrorOutput))
                using (var writer = new StreamWriter(stream)) {
                    writer.Write(result.ToString());
                }

            }
            catch (Exception ex) {
                SetError(ex.ToString());
            }
        }

        public static IEnumerable<string> ReadFiles(string source) {
            foreach (var entry in Directory.GetFiles(source, "*", SearchOption.AllDirectories)) {
                yield return entry;
            }
        }

    }

}