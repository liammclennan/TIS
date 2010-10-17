using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodayIShall.Core.Domain
{
    public class CalendarDay
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }

        public CalendarDay()
        {
        }

        public CalendarDay(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
        }

        public CalendarDay(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public override bool Equals(object obj)
        {
            CalendarDay toCompare = obj as CalendarDay;
            if (toCompare == null) return false;
            return Year == toCompare.Year
                   && Month == toCompare.Month
                   && Day == toCompare.Day;
        }

        public override int GetHashCode()
        {
            return (Year + Month + Day) * 37;
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day);
        }

        public CalendarDay AddDays(int i)
        {
            return new CalendarDay(ToDateTime().AddDays(i));
        }
    }
}
