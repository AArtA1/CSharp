using System;
/// <summary>
/// Основное пространство имен программы.
/// </summary>
namespace CinemaManager
{
    /// <summary>
    /// Основной класс программы.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Основной метод программы.
        /// </summary>
        internal static Movie[] movies;
        static void Main()
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Greetings();
            while (true)
            {
                CompleteOperation(MainMenu());
            }
        }
        /// <summary>
        /// Метод приветствующий пользователя и принимающий входные данные.
        /// </summary>
        static void Greetings()
        {
            int count = 1;
            Console.WriteLine("Привет, дорогой покупатель! ");
            Console.WriteLine("Программа принимает только натуральные числа 0,следуйте инструкциям при работе.\nПри некорректном вводе программа попросит повторный ввод.");
            movies = new Movie[AdditionalMethods.CorrectIntInput("Введите количество фильмов:")];
            Console.Clear();
            Console.WriteLine($"В следующих {movies.Length} строках укажите название фильма, возрастной рейтинг и количество залов.");
            string[] names = new string[movies.Length];
            string[] ages = new string[movies.Length];
            int[] halls = new int[movies.Length];
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine($"\nВведите данные для {i + 1} фильма.");
                movies[i] = Movie.Parse();
            }
            
            for (int i = 0; i < movies.Length; i++)
            {
                for (int j = 0; j < movies[i].halls.Length; j++)
                {
                    Console.WriteLine($"\nВведите данные для {j + 1} зала {i + 1} фильма.");
                    movies[i][j] = Hall.Parse(movies[i].name, movies[i].age, count++);
                }
            }
            AdditionalMethods.SetBudget();
            Console.Clear();
            Console.WriteLine("По умолчанию на всех сеансах одного зала одна цена билетов,но ее можно изменить самому.");
            Console.WriteLine("Для покупки билетов нужно самому добавить сеансы с помощью пункта в меню,выбрав время.");
        }
        /// <summary>
        /// Главное меню,где пользователь выбирает дальнейшее действие программы.
        /// </summary>
        /// <returns>Возвращает ответ пользователя для дальнейшего метода CompleteOperation </returns>
        static int MainMenu()
        {
            Console.WriteLine("\nДля посетителей: ");
            Console.WriteLine("1 - просмотреть список доступных сеансов");
            Console.WriteLine("2 - просмотреть свободные места на сеансе");
            Console.WriteLine("3 - просмотреть стоимость билетов для каждого места на сеансе");
            Console.WriteLine("4 - купить билеты на сеанс");
            Console.WriteLine("5 - пополнить баланс");
            Console.WriteLine("6 - показать список купленных билетов");
            Console.WriteLine("Для работников кинотеатра: ");
            Console.WriteLine("7 - вывести прибыль с проданных билетов,количество свободных и занятых мест на сеансе");
            Console.WriteLine("8 - изменить стоимость билетов ");
            Console.WriteLine("9 - добавить сеанс");
            Console.WriteLine("10 - выход");
            int answer;
            do
            {
                Console.WriteLine("\nВыберите один из предложенных пунктов: ");
            }
            while (!int.TryParse(Console.ReadLine(), out answer) || answer < 1 || answer > 10);
            return answer;
        }
        /// <summary>
        /// Метод switch для дальнейшего распределения работы методов.
        /// </summary>
        /// <param name="answer">Параметр принимающий целое число для switch из метода MainMenu </param>
        static void CompleteOperation(int answer)
        {
            switch (answer)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Список доступных сеансов:");
                    AdditionalMethods.PrintSessions();
                    break;
                case 2:
                    AdditionalMethods.PrintFreeSeats();
                    break;
                case 3:
                    AdditionalMethods.PrintTicketsCost();
                    break;
                case 4:
                    AdditionalMethods.BuyTickets();
                    break;
                case 5:
                    AdditionalMethods.UpdateBudget(AdditionalMethods.user);
                    break;
                case 6:
                    AdditionalMethods.PrintTickets();
                    break;
                case 7:
                    break;
                case 9:
                    AdditionalMethods.AddSession();
                    break;
                case 10:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nТакого варианта ответа нет,попробуйте еще раз.");
                    break;
            }
        }
    }
}
