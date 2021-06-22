using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace New_folder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            int min = 0, max = 100;
            int parts = 4;
            Task<List<int>>[] primes =new Task<List<int>>[parts];
            for(int i=0; i<parts; i++)
            {
                primes[i] = GetPrimeNumbers(i==0 ? 2 :i *min+1, (parts-i) *max, i+1);
            }
            var results = await Task.WhenAll(primes);
            

        }
        static async Task<List<int>> GetPrimeNumbers(int min, int max, int index)
        {
            List<int> results = new List<int>();
            var list = await Task.Factory.StartNew(() =>
            {
                for (var i = min; i<=max; i++)
                {
                    if (IsPrimeNumber(i))
                    {
                        results.Add(i);
                    }
                }
                return results;
            });
            Console.WriteLine($"{index}Total prime numbers: {list.Count}");
            return list;
        }
        static bool IsPrimeNumber(int number)
        {
            if (number <2) return false;
            var boundary = (int)Math.Floor(Math.Sqrt(number));
            for(var i=2; i<=boundary; i++)
            {
                if(number % i==0)
                {
                    return false;
                }
            }
            return true;

        }
    }
}
