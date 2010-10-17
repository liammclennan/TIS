using NUnit.Framework;
using TodayIShall.Core.Domain;

namespace TodayIShall.Test.Today
{
    [TestFixture]
    public class CalendarDayTests
    {
        [Test]
        public void ConvertToDateTime()
        {
            var day = new CalendarDay(2010, 8, 1);
            Assert.AreEqual(day.ToDateTime().Year, 2010);
            Assert.AreEqual(day.ToDateTime().Month, 8);
            Assert.AreEqual(day.ToDateTime().Day, 1);
        }

        [Test]
        public void Equality_SameObject()
        {
            var day = new CalendarDay(2010, 8, 1);
            var secondReference = day;
            Assert.IsTrue(day.Equals(secondReference));
        }

        [Test]
        public void Equality_DifferentObject()
        {
            var day = new CalendarDay(2010, 8, 1);
            var day2 = new CalendarDay(2010, 8, 1);
            Assert.IsTrue(day.Equals(day2));
        }

        [Test]
        public void Equality_NotEqual()
        {
            var day = new CalendarDay(2010, 8, 1);
            var day2 = new CalendarDay(2020, 8, 1);
            Assert.IsFalse(day.Equals(day2));
        }

        [Test]
        public void AddDays_BackADay()
        {
            var day = new CalendarDay(2010, 8, 1);
            day = day.AddDays(-1);
            Assert.AreEqual(day, new CalendarDay(2010,7,31));
        }

        [Test]
        public void AddDays_Forward7()
        {
            var day = new CalendarDay(2010, 8, 1);
            day = day.AddDays(7);
            Assert.AreEqual(day, new CalendarDay(2010, 8, 8));
        }
        
        [Test]
        public void AddDays_ForwardNewMonth()
        {
            var day = new CalendarDay(2010, 8, 30);
            day = day.AddDays(2);
            Assert.AreEqual(day, new CalendarDay(2010, 9, 1));
        }


    }
}
