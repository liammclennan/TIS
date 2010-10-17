using System;
using NUnit.Framework;

namespace TodayIShall.Test.Today
{
    [TestFixture]
    public class TimeZones
    {
        [Test]
        public void Zones()
        {
            foreach (TimeZoneInfo tz in TimeZoneInfo.GetSystemTimeZones())
            {
                Console.WriteLine(tz.BaseUtcOffset);
                Console.WriteLine(tz.Id);
                Console.WriteLine(tz.DisplayName);

            }
        }
    }
}
