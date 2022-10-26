using System;
public class Program
{
    public static void Main(string[] args)
    {
        uint sum = 0;
        bool pr = uint.TryParse(Console.ReadLine(), out uint a);
        if (!pr)
        {
            Console.WriteLine("Incorrect input");
        }
        else
        {
            while (a != 0)
            { 
                sum += a % 10;
                a /= 10;
            }
            Console.WriteLine(sum);
        }
    }
}