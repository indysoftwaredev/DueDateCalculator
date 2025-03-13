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

            if(daysTurnaroundTime > 0)
            {
                //need to skip weekend days                
                resolveDate = resolveDate.AddDays(daysTurnaroundTime);
                if (resolveDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    resolveDate = resolveDate.AddDays(2);
                } else if (resolveDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    resolveDate = resolveDate.AddDays(1);
                }                
            }
            
            if(hoursTurnaroundTime > 0)
            {
                //hours can be no more than 7, so we can never add more than a single day.
                if(resolveDate.Hour + hoursTurnaroundTime > 17)
                {
                    resolveDate = resolveDate.AddHours(-8);
                    resolveDate = resolveDate.AddDays(1);
                    if (resolveDate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        resolveDate = resolveDate.AddDays(2);
                    }
                    else if (resolveDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        resolveDate = resolveDate.AddDays(1);
                    }

                } 
                resolveDate = resolveDate.AddHours(hoursTurnaroundTime);
            }

            return resolveDate;
            
        }
    }
}
