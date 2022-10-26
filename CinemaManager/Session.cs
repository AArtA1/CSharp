using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    class Session
    {
        internal int[][] hall, freeSeats;
        internal string name,age;
        internal int size;
        internal DateTime time;
        // Статическое поле для хранения информации о занятости тех или иных мест в зале.
        // Статическое поле для хранения информации о прибыли.
        internal int profit = 0;
        public Session(string name, string age, int size, int[][] hall,int[][] freeSeats,DateTime time)
        {
            this.name = name;
            this.age = age;
            this.size = size;
            this.hall = hall;
            this.freeSeats = freeSeats;
            this.time = time;
        }
        internal void BuyTickets()
        {
            (int[][] selectedSeats, int sum) = ChooseSeats();
            if (sum > AdditionalMethods.user.budget)
            {
                Console.WriteLine("На вашем счете недостаточно средств(");
                ReturnSeats(selectedSeats);
            }
            else
            {
                AdditionalMethods.user.budget -= sum;
                profit += sum;
                Console.WriteLine($"\nБилеты успешно приобретены на сумму: {sum}");
                Console.WriteLine($"Ваш остаток на счете:{AdditionalMethods.user.budget}");
                for (int i = 0; i < selectedSeats.Length; i++)
                {
                    AdditionalMethods.user.tickets.Add(new Ticket(name,age,selectedSeats[i][0],selectedSeats[i][1],hall[selectedSeats[i][0]][selectedSeats[i][1]], time));
                }
            }
        }
        /// <summary>
        /// Вовзвращает все свободные места,если у пользователя было недостаточно средств на счете и транзакция не произошла.
        /// </summary>
        /// <param name="selectedSeats">Зубчатый массив,который в себе хранит выбранные пользователем места</param>
        internal void ReturnSeats(int[][] selectedSeats)
        {
            for (int i = 0; i < selectedSeats.Length; i++)
            {
                freeSeats[selectedSeats[i][0]][selectedSeats[i][1]]--;
            }
        }
        /// <summary>
        /// Принимает все точки от пользователя,проверят их корректность и возвращает в качестве зубчатого массива и целого числа.
        /// </summary>
        /// <returns>Возвращает сумму билетов выбранных пользоваталем и выбранные ими места в зале</returns>
        internal (int[][], int) ChooseSeats()
        {
            Console.WriteLine("\nЗадайте ряд и место через пробел. Ряды нумеруются начиная с левого верхнего угла. \nПри некорректном вводе программа заново попросит ввести данные.\nДля остановки введите:0 0");
            int[][] selectedSeats = new int[0][];
            int[] seatNumber = { 1, 1 };
            int sum = 0;
            while (true)
            {
                seatNumber = AdditionalMethods.CorrectInput();
                if (seatNumber[0] == -1 || seatNumber[1] == -1)
                {
                    break;
                }
                if (seatNumber[0] >= hall.Length || seatNumber[1] >= hall[^1].Length)
                {
                    Console.WriteLine("Такого места нет в зале, попробуйте еще раз: ");
                    continue;
                }
                if (freeSeats[seatNumber[0]][seatNumber[1]] == 1)
                {
                    Console.WriteLine("Данное место уже занято,забронируйте другое:");
                    continue;
                }
                freeSeats[seatNumber[0]][seatNumber[1]]++;
                sum += hall[seatNumber[0]][seatNumber[1]];
                Array.Resize(ref selectedSeats, selectedSeats.Length + 1);
                selectedSeats[^1] = seatNumber;
                Console.WriteLine($"\nСумма выбранных билетов:{sum}\nВаш баланс:{AdditionalMethods.user.budget}");
            }
            return (selectedSeats, sum);
        }
        internal void ChangePrices()
        {
            if (AdditionalMethods.Access())
            {
                AdditionalMethods.PrintMatrix(hall);
                Console.WriteLine("\nЗадайте ряд и место через пробел. Ряды нумеруются начиная с левого верхнего угла. \nПри некорректном вводе программа заново попросит ввести данные после окончания\nДля остановки введите:0 0");
                int[] seatNumber = { 1, 1 };
                while (true)
                {
                    Console.WriteLine("Выберите место: ");
                    seatNumber = AdditionalMethods.CorrectInput();
                    if (seatNumber[0] == -1 || seatNumber[1] == -1)
                    {
                        break;
                    }
                    if (seatNumber[0] >= hall.Length || seatNumber[1] >= hall[^1].Length)
                    {
                        Console.WriteLine("Такого места нет в зале, попробуйте еще раз. ");
                        continue;
                    }
                    Console.WriteLine($"\nТекущая стоимость билета:{hall[seatNumber[0]][seatNumber[1]]}");
                    hall[seatNumber[0]][seatNumber[1]] = AdditionalMethods.CorrectIntInput("Задайте новую стоимость билета:");
                    Console.WriteLine("\nСтоимость билета успешно изменена, обновленная стоимость билетов в зале: ");
                    AdditionalMethods.PrintMatrix(hall);
                    Console.WriteLine();
                }
            }
        }
        public override string ToString()
        {
            return $": Name = {name}, Age = {age}, Size = {size}, Time = {time}";
        }
    }
}
