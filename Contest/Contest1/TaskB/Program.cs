using System;
public class Program
{
    public static void Main(string[] args)
    {
        int sum = 0;
        bool pr = int.TryParse(Console.ReadLine(), out int a);
        while(pr && a !=0)
        {
            sum += a % 2 != 0 ? a : 0;
            pr = int.TryParse(Console.ReadLine(), out a);
        }
        if (!pr)
        {
            Console.WriteLine("Incorrect input");
        }
        else
        {
            Console.WriteLine(sum);
        }
    }
}