using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BootCamp.Chapter
{
    public static class FileCleaner
    {
        /// <summary>
        /// Cleans up dirtyFileName 
        /// </summary>
        /// <param name="dirtyFile">Dirty file with "_" placed in random places.</param>
        /// <param name="cleanedFile">Cleaned up file without any "_".</param>
        public static void Clean(string dirtyFile, string cleanedFile)
        {
            var dirtyContents = File.ReadAllText(dirtyFile);

            ValidateBalances(dirtyContents);

            File.WriteAllText(cleanedFile, dirtyContents.Replace("_", ""));
        }

        private static void ValidateBalances(string balances)
        {
            if (string.IsNullOrEmpty(balances)) return;

            var peopleBalances = balances.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            for (int i = 0; i < peopleBalances.Length; i++)
            {
                var balance = peopleBalances[i].Split(",");

                var name = balance[0].Any(s=> char.IsDigit(s) || s == '.');
                if (name)
                    throw new InvalidBalancesException(nameof(name), null);

                for (int j = 1; j < balance.Length; j++)
                {
                    var digit = balance[j].Any(char.IsLetter);
                    if (digit)
                        throw new InvalidBalancesException(nameof(digit), null);
                }
            }
        }
    }
}
