using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CinemaManager
{
    class Movie
    {
        internal Hall[] halls;
        internal readonly string name;
        internal readonly string age;
        public Movie(int count,string name, string age)
        {
            halls = new Hall[count];
            this.name = name;
            this.age = age;
        }
        internal static Movie Parse()
        {
            string[] str;
            do
            {
                Console.WriteLine($"Информация о фильме:");
                str = Console.ReadLine().Split();
            }
            while (str.Length != 3 || !int.TryParse(str[2], out int count) || count <= 0);
            return new Movie(int.Parse(str[2]), str[0], str[1]);
        }
        public Hall this[int index]
        {
            get
            {
                return halls[index];
            }
            set
            {
                halls[index] = value;
            }
        }
        public override string ToString()
        {
            return $"Movie: Name = {name}, Age = {age}";
        }
    }
}
