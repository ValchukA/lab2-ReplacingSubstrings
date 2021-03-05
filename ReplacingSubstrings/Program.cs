using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ReplacingSubstrings
{
    class Program
    {
        static void Main()
        {
            List<string> data = new List<string>();

            foreach (var line in File.ReadAllLines("Inlet.in.txt"))
            {
                data.Add(line);
            }

            int wordLength = Convert.ToInt32(data[^1]);

            data.RemoveAt(data.Count - 1);

            string pattern = $@"\b\w{{{wordLength}}}\b";

            int insertIndex;

            for (int strIndex = 0; strIndex < data.Count; strIndex++)
            {
                insertIndex = 0;

                var matches = Regex.Matches(data[strIndex], pattern);

                for (int matchIndex = 0; matchIndex < matches.Count; matchIndex++)
                {
                    data[strIndex] = data[strIndex].Remove(matches[matchIndex].Index + matchIndex, matches[matchIndex].Length);
                    data[strIndex] = data[strIndex].Insert(insertIndex, $"{matches[matchIndex].Value} ");

                    insertIndex += matches[matchIndex].Length + 1;
                }

                data[strIndex] = Regex.Replace(data[strIndex].ToString(), @"\s{2,}", " ");
            }

            File.WriteAllLines("Outlet.out.txt", data);
        }
    }
}
