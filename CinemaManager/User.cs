using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    class User
    {
        internal string name,surname;
        internal int budget, sum;
        internal List<Ticket> tickets = new (0);
        public User(string surname,string name,int budget)
        {
            this.name = name;
            this.surname = surname;
            this.budget = budget;
        }
        internal static User Parse()
        {
            string[] str;
            int answer;
            do
            {
                Console.WriteLine("Введите фамилию и имя через пробел: ");
                str = Console.ReadLine().Split();
            }
            while (str.Length!=2);
            do
            {
                Console.WriteLine("\nЗадайте ваш бюджет кошелька: ");
            }
            while (!int.TryParse(Console.ReadLine(), out answer) || answer < 1);
            return new User(str[0], str[1],answer);
        }
        public override string ToString()
        {
            return $"Купленные билеты пользователя: {name} {surname}";
        }
    }
}
