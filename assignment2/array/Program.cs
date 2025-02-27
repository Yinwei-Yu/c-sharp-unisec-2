using System.ComponentModel.DataAnnotations;

namespace array
{
    internal class Program
    {
        public static void analys(int[] array, out int max, out int min, out double average, out int sum)
        {
            max = 0;
            min = Int32.MaxValue;
            average = 0;
            sum = 0;
            foreach (int num in array)
            {
                max = num > max ? num : max;
                min = num < min ? num : min;
                sum += num;
            }
            average = sum / array.Length;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int[] array = { 1, 3, 4, 5, 6, 7, 8, 9, 11, 235 };
            int max, min, sum;
            double average;
            analys(array, out max, out min, out average, out sum);
            Console.WriteLine("Max: " + max + "\nMin: " + min + "\nSum: " + sum + "\nAverage: " + average);
            Console.ReadKey();
        }
    }
}