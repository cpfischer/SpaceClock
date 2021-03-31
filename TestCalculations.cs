using System;

namespace SpaceClock
{
    class TestCalculations
    {
        static void Main()
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            TimeSpan timeOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            int dayOfYear =  DateTime.Now.DayOfYear;
            Console.WriteLine(Calculations.GetSolarPosition(-92.3341, 38.949931)); //Find way to auto get these
        }
    }
}