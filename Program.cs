using System.Diagnostics;

namespace Damerau_Levenshtein_distance
{
    class Program
    {
        static int Minimum(int a, int b) => a < b ? a : b;
        static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

        static int DamerauLevenshteinDistance(string firstText, string secondText)
        {
            var n = firstText.Length + 1;
            var m = secondText.Length + 1;
            var arrayD = new int[n, m];

            for (var i = 0; i < n; i++)
                arrayD[i, 0] = i;

            for (var j = 0; j < m; j++)
                arrayD[0, j] = j;

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var cost = firstText[i - 1] == secondText[j - 1] ? 0 : 1;

                    arrayD[i, j] = Minimum(arrayD[i - 1, j] + 1, // delete
                                                            arrayD[i, j - 1] + 1, // insert
                                                            arrayD[i - 1, j - 1] + cost); // replacement

                    if (i > 1 && j > 1
                       && firstText[i - 1] == secondText[j - 2]
                       && firstText[i - 2] == secondText[j - 1])
                    {
                        arrayD[i, j] = Minimum(arrayD[i, j],
                        arrayD[i - 2, j - 2] + cost); // permutation
                    }
                }
            }

            return arrayD[n - 1, m - 1];
        }

        static void Main(string[] args)
        {
            Console.Write("First word:");
            var w1 = Console.ReadLine();
            Console.Write("Second Word:");
            var w2 = Console.ReadLine();

            Stopwatch stopwatch = new();
            stopwatch.Start();
            Console.WriteLine("Damerau-Levenshtein Distance: {0}", DamerauLevenshteinDistance(w1, w2));
            stopwatch.Stop();
            Console.WriteLine("Time past: "+ stopwatch.Elapsed.TotalSeconds);
            Console.ReadLine();
        }
    }
}