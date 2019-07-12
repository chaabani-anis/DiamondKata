using System;
using System.Linq;

namespace DiamondKata
{
    internal class Diamond
    {
        public Diamond()
        {
        }


        internal string Print(char input)
        {
            var num = input - 'A';
            var range = num + 1;
            var width = 2 * num + 1;

            var upper = Enumerable.Range('A', range)
                .Select(c =>
                {
                    var item = (char)c;
                    var val = c - 'A';
                    var core = item == 'A' ? $"{item}" : $"{item}{new string(' ', 2 * val - 1)}{item}";
                    return core.PadLeft(range + val, ' ').PadRight(width, ' ');
                }).ToList();

            return string.Join("\n", upper.Concat(upper.Take(range - 1).Reverse()));
        }

        internal static string Make(char letter)
        {
            if (letter == 'A') return "A";
            string[] rows = Enumerable.Range('A', letter - 'A' + 1).Select(x => ((char)x).ToString()).ToArray();

            for (int i = 0; i < rows.Length; i++)
            {
                char currentLetter = rows[i][0];

                if (currentLetter != 'A')
                    rows[i] = currentLetter + new string(' ', 2 * (currentLetter - 'A') - 1) + currentLetter;

                rows[i] = string.Concat(new string(' ', (letter - currentLetter)), rows[i], new string(' ', (letter - currentLetter)));
            }

            var top = string.Join('\n', rows);
            var bottom = string.Join('\n', rows.TakeWhile(x => !x.Contains(letter)).Reverse());


            return top + '\n' + bottom;
        }
    }
}