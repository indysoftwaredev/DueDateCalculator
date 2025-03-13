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

        private static DateTime SetToWorkdayStart(DateTime date) 
        {
            int minutes = date.Minute;
            int seconds = date.Second;
            return date.Date
                .AddHours(WORKDAY_START_HOUR)
                .AddMinutes(minutes)
                .AddSeconds(seconds);
        }

        private static DateTime AdjustStartHour(DateTime date) =>
            date.Hour switch
            {
                < WORKDAY_START_HOUR => SetToWorkdayStart(date),
                > WORKDAY_END_HOUR => SetToWorkdayStart(AddWorkingDays(date, 1)),
                _ => date
            };

        public static DateTime AddWorkingDays(DateTime date, int days)
        {            
            for(int i = 0; i < days; i++)
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
            for(int i = 0; i < hours; i++)
            {
                bool isExactlyEndOfDay = IsExactlyEndOfDay(date);
                date = date.AddHours(1);

                if (BeyondEndOfDay(date))
                {
                    date = AddWorkingDays(date, 1);

                    int startHour = isExactlyEndOfDay ? WORKDAY_START_HOUR + 1 : WORKDAY_START_HOUR;

                    date = date.Date
                        .AddHours(startHour)
                        .AddMinutes(date.Minute)
                        .AddSeconds(date.Second);
                }
            }
            return date;
        }

        private static bool IsExactlyEndOfDay(DateTime date)
        {
            return date.Hour == WORKDAY_END_HOUR && date.Minute == 0 && date.Second == 0;
        }

        private static bool BeyondEndOfDay(DateTime date)
        {
            return (date.Hour == WORKDAY_END_HOUR && (date.Minute > 0 || date.Second > 0))
           || date.Hour > WORKDAY_END_HOUR;
        }
    }
}
