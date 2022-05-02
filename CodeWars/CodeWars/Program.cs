using NUnit.Framework;
using System;
using System.Linq;

namespace CodeWars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result1 = KataKyu7.ToCamelCase("The-Stealth-Warrior");
            var result2 = KataKyu7.ToCamelCase("the_stealth_warrior");
        }
    }

    public class KataKyu7
    {
        /// <summary>
        ///    https://www.codewars.com/kata/517abf86da9663f1d2000003/csharp
        ///    Splits the incoming string by - and _ chars (-> string array),
        ///    Select selects each string, i is a "iteration" variable
        ///    If it's the first element in the split array (i > 0), skips ( :s)
        ///    else make the first char upper and add the substring from index 1
        ///    concat the result
        /// </summary>
        [Test]
        [TestCase("The-Stealth-Warrior")]
        [TestCase("the_stealth_warrior")]
        public static string ToCamelCase(string str) => 
            string.Concat(str.Split('-','_')
                .Select((s, i) => i > 0 ? char.ToUpper(s[0]) + s.Substring(1) : s));
        /// <summary>
        /// https://www.codewars.com/kata/544aed4c4a30184e960010f4/csharp
        /// Get divisors, enumerable creates a range from 2 to n-2 (bc 1 and the number itself is not counted)
        /// where clause filters where the input number (n) MOD nbr remainder is 0 or not
        /// if there is no remainder it has to return null (checks for any if there is any)
        /// </summary>
        public static int[] Divisors(int n)
        {
            var div =
            Enumerable.Range(2, (n - 2))
            .Where(nbr => n % nbr == 0)
            .ToArray();

            return div.Any() ? div : null;
        }
    }
}
