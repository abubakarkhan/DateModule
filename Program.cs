using System;
using System.Collections.Generic;

namespace Date_Module
{
    public enum Interval
    {
        Daily = 1,
        Monthly = 2,
        Quarterly = 3,
        Yearly = 4
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Update parameters here
            var start = new DateTime(year: 2018, month: 2, day: 1);
            var end = new DateTime(year: 2018, month: 3, day: 2);
            var interval = Interval.Monthly;
            var includeStart = false;
            var includeEnd = false;
            
            //Print parameters here
            Console.WriteLine("Start Date: {0}", start.ToShortDateString());
            Console.WriteLine("End Date: {0}", end.ToShortDateString());
            Console.WriteLine("Interval: {0}", interval.ToString());
            Console.WriteLine("Include Start: {0}", includeStart);
            Console.WriteLine("Include End: {0}", includeEnd);
            Console.WriteLine("\n=====================  Dates ======================\n");

            //Generate dates
            var datesAll = GenerateDates(
                startDate: start,
                endDate: end,
                interval: interval,
                includeStartDate: includeStart,
                includeEndDate: includeEnd);


            //Print all generated dates
            var index = 0;

            if (datesAll.Count != 0)
            {
                foreach (var d in datesAll)
                {
                    index++;
                    Console.WriteLine("{1} - Date: {0}", d.ToShortDateString(), index);
                }
            }
            else
            {
                Console.WriteLine("* Dates range and interval criteria yeilded no results." +
                                  "\n* Thank you come again.");
            }
            Console.ReadLine();
        }

        public static List<DateTime> GenerateDates(
            DateTime startDate,
            DateTime endDate,
            Interval interval,
            bool includeStartDate,
            bool includeEndDate)
        {
            var dates = new List<DateTime>();

            //Return empty is same start and end date
            if (startDate.Date == endDate.Date)
            {
                return dates;
            }

            //Add start date only if it is the first day on the month
            if (interval != Interval.Daily && (includeStartDate && startDate.Day == 1))
            {
                dates.Add(startDate);
            }

            switch (interval)
            {
                case Interval.Daily:
                    dates.AddRange(GetDailyDates(startDate, endDate, includeStartDate));
                    break;
                case Interval.Quarterly:
                case Interval.Monthly:
                    dates.AddRange(GetMonthlyOrQuarterlyDates(startDate, endDate, interval));                
                    break;
                case Interval.Yearly:
                    dates.AddRange(GetYearlyDates(startDate, endDate));
                    break;
            }

            //Add end date 
            if (includeEndDate)
            {
                dates.Add(endDate);
            }
            return dates;
        }

        public static List<DateTime> GetYearlyDates(DateTime startDate,DateTime endDate)
        {
            var dates = new List<DateTime>();
            
            for (var dt = startDate.Date.AddYears(1); dt < endDate.Date; dt = dt.AddYears(1))
            {
                var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
                dates.Add(firstDayOfMonth);
            }
            return dates;
        }
        
        public static List<DateTime> GetMonthlyOrQuarterlyDates(DateTime startDate, DateTime endDate, Interval interval)
        {
            var monthOffset = 1;

            if (interval == Interval.Quarterly)
            {
                monthOffset = 3;
            }

            var dates = new List<DateTime>();
            
            for (var dt = startDate.AddMonths(monthOffset).Date; dt < endDate.Date; dt = dt.AddMonths(monthOffset))
            {
                var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
                dates.Add(firstDayOfMonth);
            }            
            return dates;
        }

        public static List<DateTime> GetDailyDates(DateTime startDate, DateTime endDate, bool includeStartDate)
        {
            var startDateOffset = includeStartDate ? 0 : 1;
            var dates = new List<DateTime>();
            
            for (var dt = startDate.AddDays(startDateOffset).Date; dt < endDate.Date; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }            
            return dates;
        }        
    }
}
