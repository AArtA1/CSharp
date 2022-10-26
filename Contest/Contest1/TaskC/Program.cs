using System;
public class Program
{
    public static void Main(string[] args)
    {
        int sum = 0;
        bool pr1 = int.TryParse(Console.ReadLine(), out int a);
        bool pr2 = int.TryParse(Console.ReadLine(), out int b);
        if(!pr1 || !pr2 ||a>=b)
        {
            Console.WriteLine("Incorrect input");
        }
        else
        {
            for(int i = a; i < b; i++)
            {
                sum += i % 2 == 0 ? i : 0;
            }

            if (sum != 0)
            {
                Console.WriteLine(sum);
            }
        }
    }
}