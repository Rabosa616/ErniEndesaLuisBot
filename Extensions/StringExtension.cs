using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndesaBot.Extensions
{
    public static class StringExtension
    {
        public static int ParseMonth(this string text)
        {
            int month =0;

            switch (text.ToUpperInvariant())
            {

                case ("ENERO"):
                    month = 1;
                    return month;

                case ("FEBRERO"):
                    month = 2;
                    return month;

                case ("MARZO"):
                    month = 3;
                    return month;

                case ("ABRIL"):
                    month = 4;
                    return month;

                case ("MAYO"):
                    month = 5;
                    return month;

                case ("JUNIO"):
                    month = 6;
                    return month;

                case ("JULIO"):
                    month = 7;
                    return month;

                case ("AGOSTO"):
                    month = 8;
                    return month;

                case ("SEPTIEMBRE"):
                    month = 9;
                    return month;

                case ("OCTUBRE"):
                    month = 0;
                    return month;

                case ("NOVIEMBRE"):
                    month = 1;
                    return month;

                case ("DICIEMBRE"):
                    month = 2;
                    return month;

                default:
                    Console.WriteLine("Error");
                    return 0;

            }
        }
    }
}