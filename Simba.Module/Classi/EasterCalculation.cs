using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAMS.Module.Classi
{
    //class EasterCalculation
    //{
    //}
    public class EasterCalculation
    {
        private class CalendarConstants
        {
            public int? minDate { get; set; }
            public int maxDate { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }
        private static IEnumerable<CalendarConstants> Costanti = new List<CalendarConstants>()
        {
           new CalendarConstants() { minDate = null , maxDate = 1582, x = 15, y= 6 },
	       new CalendarConstants()  { minDate = 1583 , maxDate = 1699, x = 22, y= 2 },
	       new CalendarConstants()  { minDate = 1700 , maxDate = 1799, x = 23, y= 3 },
	       new CalendarConstants()  { minDate = 1800 , maxDate = 1899, x = 23, y= 4 },
	       new CalendarConstants()  { minDate = 1900 , maxDate = 2099, x = 24, y= 5 },
	       new CalendarConstants()  { minDate = 2100 , maxDate = 2199, x = 24, y= 6 },
	       new CalendarConstants()  { minDate = 2200 , maxDate = 2299, x = 25, y= 7 },
           new CalendarConstants()  { minDate = 2300 , maxDate = 2399, x = 26, y= 1 },
           new CalendarConstants()  { minDate = 2400 , maxDate = 2499, x = 25, y= 1 }
        };
        public static DateTime? GetEasterDate(int year)
        {
            var constant = Costanti.First(cx =>
                (!cx.minDate.HasValue || year >= cx.minDate.Value) &&
                (year <= cx.maxDate));
            var x = constant.x;
            var y = constant.y;
            var a = year % 19;
            var b = year % 4;
            var c = year % 7;
            var d = (19 * a + x) % 30;
            var e = (2 * b + 4 * c + 6 * d + y) % 7;
            var sum = 22 + d + e;
            if (sum <= 31) return new DateTime(year, 3, sum);
            else if (((sum - 31) != 26 && (sum - 31) != 25) ||
                        ((sum - 31) == 25 && (d != 28 || a <= 10)))
                return new DateTime(year, 4, sum - 31);
            else if ((sum - 31) == 25 && d == 28 && a > 10) return new DateTime(year, 4, 18);
            else return new DateTime(year, 4, 19);

        }
    }

}