using FsCheck;
using FsCheck.Xunit;
using System;
using System.Linq;
using Xunit;

namespace DiamondKata
{
    public class DiamondTest
    {
        [DiamondProperty]
        public void Diamond_is_not_empty(char letter)
        {
            string actual = Diamond.Make(letter);

            Assert.NotEmpty(actual);
        }

        [DiamondProperty]
        public void First_Row_Is_A(char letter)
        {
            string actual = Diamond.Make(letter);

            string expected = "A";

            string[] rows = actual.Split('\n');

            string firstCaracter = rows.First().Trim();

            Assert.Equal(expected, firstCaracter);
        }

        [DiamondProperty]
        public void All_Rows_Have_Symetric_Contour(char letter)
        {
            string actual = Diamond.Make(letter);

            string[] rows = actual.Split('\n');

            char[] letters = Enumerable.Range('A', letter - 'A' + 1).Select(x => (char)x).ToArray();

            foreach (string row in rows)
            {
                string rightContour = row.Substring(0, row.IndexOfAny(letters));
                string leftContour = row.Substring(row.LastIndexOfAny(letters) + 1);

                Assert.Equal(rightContour, leftContour);
            }
        }

        [DiamondProperty]
        public void Top_Of_letters_Is_In_Correct_Order(char letter)
        {
            string actual = Diamond.Make(letter);

            string[] rows = actual.Split('\n');

            char[] expectedLetters = Enumerable.Range('A', letter - 'A' + 1).Select(x => (char)x).ToArray();

            var letters = rows.Take(expectedLetters.Length).Select(x => x.Trim()[0]);

            Assert.Equal(expectedLetters, letters);
        }

        [DiamondProperty]
        public void Figure_Is_Symetric_around_the_horizontal_axis(char letter)
        {
            string actual = Diamond.Make(letter);

            string[] rows = actual.Split('\n');

            var top = rows.TakeWhile(x => !x.Contains(letter));
            var bottom = rows.Reverse().TakeWhile(x => !x.Contains(letter));

            Assert.Equal(top, bottom);
        }
    }

    public class DiamondPropertyAttribute : PropertyAttribute
    {
        public DiamondPropertyAttribute()
        {
            Arbitrary = new[] { typeof(LettersOnlyStringArbitrary) };
        }
    }

    public static class LettersOnlyStringArbitrary
    {
        public static Arbitrary<char> Chars()
        {
            return Arb.Default.Char().Filter(x => x >= 'A' && x <= 'Z');
        }
    }
}
