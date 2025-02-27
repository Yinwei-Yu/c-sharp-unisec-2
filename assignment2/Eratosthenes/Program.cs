namespace Eratosthenes
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] num = new int[99];
            int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
            for (int i = 2; i <= 100; i++)//初始化
            {
                num[i - 2] = i;
            }
            foreach (int singlePri in primes)
            {
                for (int i = 0; i < 99; i++)
                {
                    if (num[i] % singlePri == 0 && num[i] != singlePri)
                    {
                        num[i] = 0;
                    }
                }
            }
            foreach (int number in num)
            {
                if (number != 0)
                {
                    Console.WriteLine(number);
                }
            }
        }
    }
}