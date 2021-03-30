using System;

namespace SpaceClock
{
    public class Calculations
    {
        //src https://faculty.eng.ufl.edu/jonathan-scheffe/wp-content/uploads/sites/100/2020/08/Solar-Time1419.pdf
        
        //Gonna need a dayOfYear calculation and 366 for leap years
        public static double GetSolarPosition(double longitude, double latitude)
        {
            double dayOfYear =  Convert.ToDouble(DateTime.Now.DayOfYear);
            double hour = Convert.ToDouble(DateTime.Now.Hour);
            double fractionalYear = ((2 * Math.PI) / 365) * ((dayOfYear - 1) + ((hour - 12) / 24));

            //double fractionalYear = (dayOfYear - 1.0) * (360.00 / 365.00);
            double equationOfTime = 229.18 * (0.000075 + 0.001868 * Math.Cos(fractionalYear) - 0.032077 * Math.Sin(fractionalYear)
                                 - 0.014615 * Math.Cos(2.0 * fractionalYear) - 0.040849 * Math.Sin(2.0 * fractionalYear)); //Off a bit, but correct?

            //double degreesB = (360/365 * (dayOfYear - 81));
            //double equationOfTime = 9.87 * Math.Sin(2 * degreesB) - 7.53 * Math.Cos(degreesB) - 1.5 * Math.Sin(degreesB); //src: https://www.pveducation.org/pvcdrom/properties-of-sunlight/solar-time#HRA
            
            
            double declination =  0.006918 - 0.399912 * Math.Cos(fractionalYear) + 0.070257 * Math.Sin(fractionalYear) 
                                    - 0.006758 * Math.Cos(2 * fractionalYear) + 0.000907 * Math.Sin(2 * fractionalYear)
                                    - 0.002697 * Math.Cos(3 * fractionalYear) + 0.00148 * Math.Sin(3 * fractionalYear); //probably correct

            double timeOffset = equationOfTime + 4 * longitude - 60 * Convert.ToDouble(TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).TotalHours);
            TimeSpan currTime = DateTime.Now.TimeOfDay;

            double standardMeridian = Math.Abs(Convert.ToDouble(TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).TotalHours)) * 15;
            TimeSpan solarTime = currTime + TimeSpan.FromMinutes(4 * (standardMeridian - Math.Abs(longitude)) + equationOfTime);
            // double solarTime = Convert.ToDouble(currTime.Hour) * 60 + Convert.ToDouble(currTime.Minute) 
            //                  + Convert.ToDouble(currTime.Second) / 60 + timeOffset;
            
            Console.WriteLine(TimeSpan.FromMinutes(4 * (standardMeridian - longitude) + equationOfTime));
            Console.WriteLine(currTime);
            Console.WriteLine(solarTime);
            //double hourAngle = (solarTime / 4) - 180;

            // double zenithAngle = Math.Sin(latitude) * Math.Sin(declination) + Math.Cos(latitude) 
            //                    * Math.Cos(declination) * Math.Cos(hourAngle); //Wrong
            
            // double azimuthAngle = -1 * ((Math.Sin(latitude) * Math.Cos(zenithAngle) - Math.Sin(declination))
            //                             / Math.Cos(latitude) * Math.Sin(zenithAngle)); //Wrong
            return equationOfTime;
        }
        
    }
}