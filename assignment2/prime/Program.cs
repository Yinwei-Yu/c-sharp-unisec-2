using System;
using System.Collections;

namespace assignment2
{
    internal class Program
    {
        public static ArrayList prime(int num)
        {
            ArrayList primes = new ArrayList();
            for (int i = 2; i < num; i++)
            {
                while (num % i == 0)
                {
                    if (!primes.Contains(i))
                    {
                        primes.Add(i);
                    }
                    num /= i;
                }
            }
            return primes;
        }

        private static void Main(string[] args)
        {
            int num = Int32.Parse(Console.ReadLine());
            ArrayList primes = prime(num);
            Console.WriteLine($"数字{num}的所有质因数为:");
            foreach (int i in primes)
            {
                Console.Write(i + " ");
            }
        }
    }
}