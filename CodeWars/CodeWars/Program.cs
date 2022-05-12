using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;

namespace CodeWars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result1 = KataKyu7.ToCamelCase("The-Stealth-Warrior");
            var result2 = KataKyu7.ToCamelCase("the_stealth_warrior");
            var res3 = KataKyu7.bouncingBall(3, 0.66, 1.5);
            var res4 = KataKyu7.bouncingBall(30, 0.66, 1.5);
            var res5 = KataKyu7.bouncingBall(3, 1, 1.5);
            var res6 = KataKyu7.Number(new List<int[]>() { new[] { 10, 0 }, new[] { 3, 5 }, new[] { 5, 8 } });
            var res7 = KataKyu7.FindEvenIndexMine(new int[] { 1, 2, 3, 4, 3, 2, 1 });
            var res8 = KataKyu7.GetUnique(new[] { 1, 2, 2, 2 });
            var res9 = KataKyu7.ArrayDiff(new int[] { 2 }, new int[] { 1, 2 });
            var res10 = KataKyu7.CreatePhoneNumber(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            var res11 = KataKyu7.GetReadableTime(359999);
            var res12 = KataKyu7.IsValidString("{[]}");
            var res13 = KataKyu7.IsValidString("()[]]{}");
            var res14 = KataKyu7.IsValidString("([)]");
            var res15 = KataKyu7.IsValidString("((");
            var res17 = KataKyu7.Alphanumeric("");
            var res18 = KataKyu7.UInt32ToIP(2154959208);
            var res19 = KataKyu7.MoveZeroes(new int[] { 1, 2, 0, 1, 0, 1, 0, 3, 0, 1 });
            var res20 = KataKyu7.Add("123456789", "987654322");
        }
    }

    [TestFixture]
    public class KataKyu7
    {
        //https://www.codewars.com/kata/525f4206b73515bffb000b21
        public static string Add(string a, string b)
        {
            return (BigInteger.Parse(a) + BigInteger.Parse(b)).ToString(); // Fix this!
        }

        //https://www.codewars.com/kata/52597aa56021e91c93000cb0
        public static int[] MoveZeroes(int[] arr) =>
            arr.OrderBy(x => x == 0).ToArray();

        //https://www.codewars.com/kata/52e88b39ffb6ac53a400022e
        public static string UInt32ToIP(uint ip) =>
              IPAddress.Parse(ip.ToString()).ToString();

        //https://www.codewars.com/kata/526dbd6c8c0eb53254000110
        //Checks if string only contains alphabets numbers and not empty
        //With regex
        //public static bool Alphanumeric(string str) =>
        //    new Regex("^[a-zA-Z0-9]+$").Match(str).Success;
        public static bool Alphanumeric(string str) =>
            str.All(c => Char.IsLetterOrDigit(c)) && !string.IsNullOrEmpty(str);

        //https://leetcode.com/problems/valid-parentheses/
        public static bool IsValidString(string s)
        {
            if (s.Length % 2 != 0)
                return false;

            var charStack = new Stack<char>();

            foreach (var actualChar in s)
            {
                if ("([{".Contains(actualChar))
                {
                    charStack.Push(actualChar);
                    continue;
                }
                else
                {
                    if (charStack.Count > 0)
                    {
                        var expectedEnding = charStack.Peek() switch
                        {
                            '(' => ')',
                            '[' => ']',
                            '{' => '}',
                            _ => '\0'
                        };

                        if (expectedEnding == '\0' || actualChar != expectedEnding)
                            return false;

                        charStack.Pop();
                    }
                    else
                        return false;
                }
            }
            return !charStack.Any();
        }

    /// <summary>
    /// https://www.codewars.com/kata/52685f7382004e774f0001f7
    /// D2 means pad to 2 places
    /// </summary>
    public static string GetReadableTime(int seconds) =>
            string.Format("{0:d2}:{1:d2}:{2:d2}", seconds / 3600, seconds / 60 % 60, seconds % 60);
        public static string GetReadableTimeMine(int seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Days > 0 ? t.Hours + t.Days * 24 : t.Hours,
                            t.Minutes,
                            t.Seconds);
        }
       

        ///https://www.codewars.com/kata/525f50e3b73515a6db000b83
        /// string.Concat(numbers) puts the numbers together as string already
        /// 
        public static string CreatePhoneNumber(int[] numbers) =>
            long.Parse(string.Concat(numbers)).ToString("(000) 000-0000");

        public static string CreatePhoneNumberMine(int[] numbers)
        {
            var formatted =  string.Join("", numbers);
            var parenthesis = string.Join("",formatted.Take(3));
            var firsts = string.Join("", formatted.Skip(3).Take(3));
            var remainder = string.Join("", formatted.Skip(6));

            return $"({parenthesis}) {firsts}-{remainder}";
        }

        /// https://www.codewars.com/kata/523f5d21c841566fde000009
        /// Filter those that are in a, but not in b
        public static int[] ArrayDiff(int[] a, int[] b) =>
            a.Where(n => !b.Contains(n)).ToArray();


        /// https://www.codewars.com/kata/585d7d5adb20cf33cb000235/solutions/csharp
        /// Group on unique numbers, select where count is only 1 (single) and return key
        public static int GetUnique(IEnumerable<int> numbers) =>
                numbers.GroupBy(x => x).Where(group => group.Count() == 1).Single().Key;

        /// <summary>
        /// https://www.codewars.com/kata/5679aa472b8f57fb8c000047
        /// Get range from 0 to max length, where left part (from 0 to i)
        /// and right part (i + 1 ...) sums are equal
        /// DefaultIfEmpty(-1) if there is none, if there is more, return the first
        /// NICE!
        /// </summary>
        public static int FindEvenIndex(int[] arr) =>
            Enumerable.Range(0, arr.Length)
            .Where(i => arr[..i].Sum() == 
            arr[(i + 1)..].Sum())
            .DefaultIfEmpty(-1).First();

        public static int FindEvenIndexMine(int[] arr)
        {
            int currentIndex, leftResult = 0, rightResult = 0;

            for (currentIndex = 0; currentIndex < arr.Length; currentIndex++)
            {
                leftResult = arr[..currentIndex].Sum();
                rightResult = arr[(currentIndex + 1)..].Sum();
                //for (int i = 0; i < currentIndex; i++)
                //{
                //    leftResult += arr[i];
                //}
                //for (int j = currentIndex + 1; j < arr.Length; j++)
                //{
                //    rightResult += arr[j];
                //}

                if (rightResult == leftResult)
                    return currentIndex;

                rightResult = leftResult = 0;
            }

            return -1;
        }

        /// <summary>
        /// https://www.codewars.com/kata/5648b12ce68d9daa6b000099
        /// 
        /// </summary>
        public static int Number(List<int[]> peopleListInOut)
        {
            return peopleListInOut.Sum(Item => Item[0] - Item[1]);
        }
        public static int NumberMine(List<int[]> peopleListInOut)
        {
            return peopleListInOut
                .Select(stop => stop)
                .Select(e => e.First() - e.Skip(1).First())
                .Sum();
        }

        ///https://www.codewars.com/kata/56541980fa08ab47a0000040
        ///checksh wether the abcd.. string contains the char, return the count of them
        ///and "divides" with the original length
        public static string PrinterError(string s) =>
            $"{s.Where(c => !"abcdefghijklm".Contains(c)).Count()}/{s.Length}";

        /// <summary>
        ///    https://www.codewars.com/kata/517abf86da9663f1d2000003/csharp
        ///    Splits the incoming string by - and _ chars (-> string array),
        ///    Select selects each string, i is a "iteration" variable
        ///    If it's the first element in the split array (i > 0), skips ( :s)
        ///    else make the first char upper and add the substring from index 1
        ///    concat the result
        /// </summary>
        public static string ToCamelCase(string str) =>
            string.Concat(str.Split('-', '_')
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
        /// <summary>
        /// https://www.codewars.com/kata/5544c7a5cb454edb3c000047/csharp
        /// Math log calculates the no of bounces, bounce is the base, window/h is the value
        /// Math ceiling rounds up everything (3.33 -> 4)
        /// </summary>
        public static int bouncingBall(double h, double bounce, double window)
        {
            if (h <= 0 || bounce <= 0 || bounce >= 1 || window >= h)
            {
                return -1;
            }
            return (int)Math.Ceiling(Math.Log(window / h, bounce)) * 2 - 1;
        }
        public static int bouncingBallMine(double h, double bounce, double window)
        {
            if (h <= 0 || bounce <= 0 || bounce >= 1 || window >= h)
                return -1;

            int noOfBounces = 1;

            while (h * bounce > window)
            {
                h *= bounce;
                noOfBounces += 2;
            }

            return noOfBounces;
        }

        ///https://www.codewars.com/kata/54da5a58ea159efa38000836/csharp
        ///Group on each integer in the array
        ///Select the single one, that has odd elements (only 1 always)
        ///and return the key of that group
        public static int find_it(int[] seq)
        {
            return seq.GroupBy(x => x).Single(g => g.Count() % 2 == 1).Key;
        }

        /// <summary>
        /// https://www.codewars.com/kata/54ff3102c1bad923760001f3/csharp
        /// Returns the count and checks wether aeiou contains the given char
        /// </summary>
        public static int GetVowelCount(string str)
        {
            return str.Count(i => "aeiou".Contains(i));
        }

        ///https://www.codewars.com/kata/51f2d1cafc9c0f745c00037d
        ///easy solution
        public static bool Solution(string str, string ending)
        {
            return str.EndsWith(ending);
        }

        ///https://www.codewars.com/kata/578553c3a1b8d5c40300037c
        ///Joins the ints together (0,1 only) and converts them to int
        ///2 specifies the conversion (binary)
        public static int binaryArrayToNumber(int[] BinaryArray)
        {
            return Convert.ToInt32(string.Join("", BinaryArray), 2);
        }

        ///https://www.codewars.com/kata/551f37452ff852b7bd000139
        ///ToString overload, 2 means binary representation
        public static string AddBinary(int a, int b) =>
            Convert.ToString(a + b, 2);
    }
}

