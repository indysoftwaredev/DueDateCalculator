using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DueDateCalculator
{
    public class DueDateCalculator
    {
        public static DateTime CalculateDueDate(DateTime submitDate, int turnaroundTime)
        {
            DateTime resolveDate = submitDate;

            int daysTurnaroundTime = turnaroundTime / 8;
            int hoursTurnaroundTime = turnaroundTime % 8;

            resolveDate = AddDays(resolveDate, daysTurnaroundTime);
            resolveDate = AddHours(resolveDate, hoursTurnaroundTime);

            return resolveDate;            
        }

        public static DateTime AddDays(DateTime date, int days)
        {            
            for(int i = days; days > 0; days--)
            {
                date = date.AddDays(1);
                if(date.DayOfWeek == DayOfWeek.Saturday)
                {
                    date = date.AddDays(2); //skip to monday
                }
            }
            return date;
        }

        public static DateTime AddHours(DateTime date, int hours)
        {
            for(int i = hours; hours > 0; hours--)
            {
                date = date.AddHours(1);
                if(date.Hour > 17)
                {
                    date = AddDays(date, 1); //go to next available day
                    date = date.AddHours(-8); //go to the beginning of the day
                }
            }
            return date;
        }
    }
}
