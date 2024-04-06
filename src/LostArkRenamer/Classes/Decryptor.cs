using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostArkRenamer.Classes {

    //Credit to the author below. He created the original in rust, I just converted it over to C#
    //https://www.gildor.org/smf/index.php/topic,3055.msg46444.html#msg46444
    internal static class Decryptor {

        private static readonly Tuple<string, char, int>[] OPT_KEY_TABLE = new Tuple<string, char, int>[] {
            // Q
            Tuple.Create("QP", 'Q', 0),
            Tuple.Create("QD", 'Q', 1),
            Tuple.Create("QW", 'Q', 2),
            Tuple.Create("Q4", 'Q', 3),
            // -
            Tuple.Create("QL", '-', 0),
            Tuple.Create("QB", '-', 1),
            Tuple.Create("QO", '-', 2),
            Tuple.Create("Q5", '-', 3),
            // _
            Tuple.Create("QC", '_', 0),
            Tuple.Create("QN", '_', 1),
            Tuple.Create("QT", '_', 2),
            Tuple.Create("Q9", '_', 3),
            // X
            Tuple.Create("XU", 'X', 0),
            Tuple.Create("XN", 'X', 1),
            Tuple.Create("XH", 'X', 2),
            Tuple.Create("X3", 'X', 3),
            // !
            Tuple.Create("XW", '!', 0),
            Tuple.Create("XS", '!', 1),
            Tuple.Create("XZ", '!', 2),
            Tuple.Create("X0", '!', 3),
        };

        private static string Clean(string source) {
            source = source.ToUpper();
            var outStr = new System.Text.StringBuilder();
            int i = 0;
            while (i < source.Length) {
                var subst = OPT_KEY_TABLE.FirstOrDefault(t => source.Substring(i).StartsWith(t.Item1));
                if (subst != null && i % 4 == subst.Item3) {
                    outStr.Append(subst.Item2);
                    i += subst.Item1.Length;
                }
                else {
                    outStr.Append(source[i]);
                    i++;
                }
            }
            return outStr.ToString();
        }
        public static string Decrypt(string source) {
            source = source.ToUpper();
            var outStr = new System.Text.StringBuilder();
            foreach (char c in source) {
                int x = c;
                if (c >= '0' && c <= '9') {
                    x += 43;
                }
                int i = (31 * (x - source.Length - 65) % 36 + 36) % 36 + 65;
                if (i >= 91) {
                    i -= 43;
                }
                outStr.Append((char)i);
            }
            string unescaped = Clean(outStr.ToString());
            if (unescaped.Contains("!")) {
                return unescaped.Split('!')[0];
            }
            else {
                return unescaped;
            }
        }

    }

}