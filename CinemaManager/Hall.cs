using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    class Hall
    {
        // Статическое поле для хранения цен билетов и свободных мест.
        readonly int[][] hall, freeSeats;
        internal List<Session> sessions = new List<Session>(0); 
        internal string name,age;
        internal int id, size;
        static internal int profit = 0;
        public Hall(int[][] hall, int[][] freeSeats,int size,string name,string age,int id)
        {
            this.hall = hall;
            this.freeSeats = freeSeats;
            this.size= size;
            this.name = name;
            this.age = age;
            this.id = id;
        }
        internal void AddSession()
        {
            sessions.Add(new Session(name,age,size,hall,freeSeats,AdditionalMethods.CorrectDateTimeInput()));
        }
        internal int ReturnSumOfSessions()
        {
            int sum = 0;
            foreach (var x in sessions)
            {
                sum += x.profit;
            }
            return sum;
        }
        public static Hall Parse(string name,string age,int id)
        {
            int[] hallSize;
            int[][] hall;
            int[][] freeSeats;
            Console.WriteLine("Задайте размер зала через пробел(N M),где е N – количество рядов в зале, а M – количество мест в одном ряду 2\nДля удобства работы программа принимает натуральные числа от 1 до 10 включительно.");
            do
            {
                hallSize = AdditionalMethods.CorrectInput(2);
            }
            while (hallSize[0] > 10 || hallSize[1] > 10);
            hall = new int[hallSize[0]][];
            freeSeats = new int[hallSize[0]][];
            for (int i = 0; i < hallSize[0]; i++)
            {
                Console.WriteLine($"\nВведите через пробел стоимость билетов {i + 1} ряда. Напоминаю,что в ряду {hallSize[1]} мест(а/о): ");
                hall[i] = AdditionalMethods.CorrectInput(hallSize[1]);
                freeSeats[i] = new int[hallSize[1]];

            }
            Console.Clear();
            Console.WriteLine("Заданная вами матрица цен: ");
            AdditionalMethods.PrintMatrix(hall);
            return new Hall(hall, freeSeats,hallSize[0]*hallSize[1],name,age,id);
        }
        public override string ToString()
        {
            return $"Id = {id}, Name = {name}, Age = {age}, Seats = {string.Format("{0:d3}",size)}";
        }
    }
}
