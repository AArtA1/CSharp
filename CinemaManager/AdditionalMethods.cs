using System;
using System.Linq;

namespace CinemaManager
{
    static class AdditionalMethods
    {
        // Статическое поле только для чтения,хранящее в себе пароль от функций администратора. 
        internal static string password = "h342";
        internal static User user;
        internal static User SetBudget()
        {
            user = User.Parse();
            return user;
        }
        internal static void UpdateBudget(User user)
        {
            user.budget += CorrectIntInput("\nЗадайте сумму на которую хотите пополнить: ");
        }
        internal static int[] CorrectInput()
        {
            string[] input;
            do
            {
                input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }
            while (input.Length != 2 || !input.All(s => (int.TryParse(s, out int s1) && s1 > -1)));
            return input.Select(x => int.Parse(x) - 1).ToArray();
        }
        /// <summary>
        /// Перегрузка метода для проверки корректности ввода состоящего из count цифр,разделенных пробелом.
        /// </summary>
        /// <param name="count">Количество цифр разделенных пробелом</param>
        /// <returns>Одномерный массив корректного ввода,состоящего из count элементов</returns>
        internal static int[] CorrectInput(int count)
        {
            string[] input;
            do
            {
                Console.WriteLine("Ваш ответ: ");
                input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }
            while (input.Length != count || !input.All(s => (int.TryParse(s, out int s1) && s1 > 0)));
            return input.Select(x => int.Parse(x)).ToArray();
        }
        /// <summary>
        /// Одновременно 1 и 2 пункты из метода MainMenu и вспомогательный метод для вывода зубчатого массива.
        /// </summary>
        /// <param name="matrix">Зубчатый массив,который нужно вывести</param>
        internal static void PrintMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write("{0}\t", matrix[i][j]);
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Проверка корректности ввода пользователя.
        /// </summary>
        /// <returns>Корректный ввод пользователя</returns>
        internal static int CorrectIntInput(string message)
        {
            int answer;
            do
            {
                Console.WriteLine($"{message}");
            }
            while (!int.TryParse(Console.ReadLine(), out answer) || answer <= 0);
            return answer;
        }
        /// <summary>
        /// Проверка корректности ввода пользователя.
        /// </summary>
        /// <returns>Корректный ввод пользователя</returns>
        internal static int CorrectIntInput(string message, int max)
        {
            int answer;
            do
            {
                Console.WriteLine($"{message}");
            }
            while (!int.TryParse(Console.ReadLine(), out answer) || answer <= 0 || answer > max);
            return answer;
        }
        /// <summary>
        /// Доступ пользователя в функции администратора
        /// </summary>
        /// <returns>Вовзвращает доступ в виде bool</returns>
        internal static bool Access()
        {
            Console.WriteLine("Введите пароль для входа: ");
            if (Console.ReadLine() == password)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать!");
                return true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Неправильный пароль,попробуйте еще раз(");
                return false;
            }
        }
        internal static DateTime CorrectDateTimeInput()
        {
            Console.WriteLine("Введите дату сеанса через пробел,чтобы добавить(месяц день час минута): ");
            string[] str = Console.ReadLine().Split();
            DateTime datetime = new(2022, int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]), int.Parse(str[3]), 0);
            return datetime;
        }
        internal static int PrintSessions()
        {
            int id = 1;
            for (int i = 0; i < Program.movies.Length; i++)
            {
                for (int j = 0; j < Program.movies[i].halls.Length; j++)
                {
                    for (int c = 0; c < Program.movies[i].halls[j].sessions.Count; c++)
                    {
                        if (Program.movies[i].halls[j].sessions[c].time > DateTime.Now)
                        {
                            Console.WriteLine($"{id++} {Program.movies[i].halls[j].sessions[c]}");
                        }
                    }
                }
            }
            return id;
        }
        internal static void PrintFreeSeats()
        {
            Console.Clear();
            Console.WriteLine("Список доступных мест на сеансе:");
            int id = PrintSessions();
            int answer = CorrectIntInput("\nВыберите сеанс:", id);
            id = 1;
            for (int i = 0; i < Program.movies.Length; i++)
            {
                for (int j = 0; j < Program.movies[i].halls.Length; j++)
                {
                    for (int c = 0; c < Program.movies[i].halls[j].sessions.Count; c++)
                    {
                        if (Program.movies[i].halls[j].sessions[c].time > DateTime.Now)
                        {
                            if (id == answer)
                            {
                                Console.WriteLine("\nCвободные места - 0,а купленные - 1\n");
                                PrintMatrix(Program.movies[i].halls[j].sessions[c].freeSeats);
                            }
                        }
                    }
                }
            }
        }
        internal static void PrintTicketsCost()
        {
            Console.WriteLine("Список стоимости билетов:");
            int id = PrintSessions();
            int answer = CorrectIntInput("Выберите сеанс:", id);
            id = 1;
            for (int i = 0; i < Program.movies.Length; i++)
            {
                for (int j = 0; j < Program.movies[i].halls.Length; j++)
                {
                    for (int c = 0; c < Program.movies[i].halls[j].sessions.Count; c++)
                    {
                        if (id == answer)
                        {
                            Console.WriteLine("\nСтоимость билетов:");
                            PrintMatrix(Program.movies[i].halls[j].sessions[c].hall);
                        }
                    }
                }
            }
        }
        internal static void BuyTickets()
        {
            Console.WriteLine("\nСписок доступных мест на сеансе:");
            int id = PrintSessions();
            int answer = CorrectIntInput("Выберите сеанс:", id);
            id = 1;
            for (int i = 0; i < Program.movies.Length; i++)
            {
                for (int j = 0; j < Program.movies[i].halls.Length; j++)
                {
                    for (int c = 0; c < Program.movies[i].halls[j].sessions.Count; c++)
                    {
                        if (Program.movies[i].halls[j].sessions[c].time > DateTime.Now)
                        {
                            if (id == answer)
                            {
                                Program.movies[i].halls[j].sessions[c].BuyTickets();
                            }
                        }
                    }
                }
            }
        }
        internal static void PrintTickets()
        {
            Console.WriteLine(user);
            Ticket[] tickets = user.tickets.ToArray();
            Array.Sort(tickets);
            foreach (var x in tickets)
            {
                Console.WriteLine(x);
            }
        }
        internal static void AddSession()
        {
            int max = 0;
            Console.WriteLine("Список доступных залов:");
            for (int i = 0; i < Program.movies.Length; i++)
            {
                for (int j = 0; j < Program.movies[i].halls.Length; j++)
                {
                    Console.WriteLine(Program.movies[i].halls[j]);
                    max = Program.movies[i].halls[j].id;
                }
            }
            int answer = CorrectIntInput("Выберите зал:", max);
            for (int i = 0; i < Program.movies.Length; i++)
            {
                for (int j = 0; j < Program.movies[i].halls.Length; j++)
                {
                    if (Program.movies[i].halls[j].id == answer)
                    {
                        Program.movies[i].halls[j].AddSession();
                    }
                }
            }
        }
    }
}
