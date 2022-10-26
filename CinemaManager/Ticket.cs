using System;

namespace CinemaManager
{
    record Ticket(string name, string age, int row, int site, int cost, DateTime time) : IComparable
    {
        public override string ToString()
        {
            return $"Name = {name},Age = {age}, Row = {row}, Site = {site}, Cost = {cost}, Time = {time}";
        }
        int IComparable.CompareTo(object ticket)
        {
            
            if (this.time > (ticket as Ticket).time)
            {
                return 1;
            }
            if (this.time == (ticket as Ticket).time)
            {
                return 0;
            }
            return -1;
        }
    }
}
