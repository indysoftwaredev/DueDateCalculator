using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DueDateCalculator
{
    public class DueDateCalculator
    {
        private const int WORKDAY_END_HOUR = 17;
        private const int WORKING_HOURS_PER_DAY = 8;
        private const int WORKDAY_START_HOUR = 9;

        public static DateTime CalculateDueDate(DateTime submitDate, int turnaroundTime)
        {
            if (turnaroundTime < 0)
                throw new ArgumentException("Turnaround time must be non-negative", nameof(turnaroundTime));

            DateTime resolveDate = submitDate;

            int daysTurnaroundTime = turnaroundTime / WORKING_HOURS_PER_DAY;
            int hoursTurnaroundTime = turnaroundTime % WORKING_HOURS_PER_DAY;

            resolveDate = NormalizeStartTime(resolveDate);
            resolveDate = AddWorkingDays(resolveDate, daysTurnaroundTime);
            resolveDate = AddWorkingHours(resolveDate, hoursTurnaroundTime);

            return resolveDate;
        }

        private static DateTime NormalizeStartTime(DateTime date)
        {
            date = AdjustForWeekend(date);
            date = AdjustStartHour(date);
            return date;
        }

        private static DateTime AdjustForWeekend(DateTime date) =>
            date.DayOfWeek switch
            {
                DayOfWeek.Saturday => SetToWorkdayStart(date.AddDays(2)),
                DayOfWeek.Sunday => SetToWorkdayStart(date.AddDays(1)),
                _ => date
            };

        private static DateTime SetToWorkdayStart(DateTime date) =>        
            date.Date.AddHours(WORKDAY_START_HOUR);

        private static DateTime AdjustStartHour(DateTime date) =>
            date.Hour switch
            {
                < WORKDAY_START_HOUR => SetToWorkdayStart(date),
                > WORKDAY_END_HOUR => SetToWorkdayStart(AddWorkingDays(date, 1)),
                _ => date
            };

        public static DateTime AddWorkingDays(DateTime date, int days)
        {            
            for(int i = days; i > 0; i--)
            {
                date = date.AddDays(1);
                if(date.DayOfWeek == DayOfWeek.Saturday)
                {
                    date = date.AddDays(2); 
                } else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = date.AddDays(1);
                }
            }
            return date;
        }

        public static DateTime AddWorkingHours(DateTime date, int hours)
        {
            for(int i = hours; i > 0; i--)
            {
                date = date.AddHours(1);
                if(date.Hour > WORKDAY_END_HOUR)
                {
                    date = AddWorkingDays(date, 1); //go to next available day
                    date = SetToWorkdayStart(date);
                    date = date.AddHours(1);
                } 
            }
            return date;
        }
    }
}
