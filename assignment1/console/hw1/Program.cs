using System;

namespace hw1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s;
            Console.WriteLine("Hello,please give me your equation\n" +
                "Your equation should be like this:25 + 35\n" +
                "Note that you can only enter integer");
            Console.WriteLine("You can now using \"+,-,*,/,%,^\"\n");
            while (true)
            {
                s = Console.ReadLine();
                if (s == null)
                {
                    Console.WriteLine("You do not enter anything!\n");
                    continue;
                }
                string[] words;
                try
                {
                    words = s.Split(' ');
                    if (words.Length != 3)
                    {
                        Console.WriteLine("You do not enter a right equation!\n" +
                            "Note that there is an space bwteen numbers and operator.\n");
                        continue;
                    }
                    int num1 = Int32.Parse(words[0]);
                    int num2 = Int32.Parse(words[2]);
                    char ch = char.Parse(words[1]);
                    double res = 0;
                    switch (ch)
                    {
                        case '+': res = num1 + num2; break;
                        case '-': res = num1 - num2; break;
                        case '*': res = num1 * num2; break;
                        case '/': res = num1 / num2; break;
                        case '%': res = num1 % num2; break;
                        case '^': res = Math.Pow(num1,num2); break;
                    }
                    Console.WriteLine("Result:{0}\n" +
                        "Now you can enter a next equation.\n" +
                        "Press Ctrl^C to exit the program.\n", res);
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"There is the err:\n" +
                        $"{e}\nPlease try again\n");
                    continue;
                }
                
                
            }
        }
    }
}
