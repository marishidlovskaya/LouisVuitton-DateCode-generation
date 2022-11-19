using System;
using System.Globalization;

namespace LouVuiDateCode
{
    public static class DateCodeParser
    {//
        public static void ParseEarly1980Code(string dateCode, out uint manufacturingYear, out uint manufacturingMonth)
        {
            if (string.IsNullOrEmpty(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            if (dateCode.Length < 3 || dateCode.Length > 4)
            {
                throw new ArgumentException();
            }

            manufacturingYear = uint.Parse(dateCode[0..2]);
            manufacturingYear = manufacturingYear < 80 || manufacturingYear > 89 ? throw new ArgumentException() : 1900 + manufacturingYear;

            if (dateCode.Length == 3 && dateCode[2..3] != "0")
                {
                    manufacturingMonth = uint.Parse(dateCode[2..3]);
                }
            else if (dateCode.Length == 4 && uint.Parse(dateCode[2..4]) > 0 && uint.Parse(dateCode[2..4]) < 13)
                {
                    manufacturingMonth = uint.Parse(dateCode[2..4]);
                }
            else
            {
                throw new ArgumentException();
            }
        }

        public static void ParseLate1980Code(string dateCode, out Country[] factoryLocationCountry, out string factoryLocationCode, out uint manufacturingYear, out uint manufacturingMonth)
        {
            if (string.IsNullOrEmpty(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            if (dateCode.Length < 4 || dateCode.Length > 6)
            {
                throw new ArgumentException();
            }

            factoryLocationCode = dateCode[^2..^0].ToUpper();
            factoryLocationCountry = CountryParser.GetCountry(factoryLocationCode);

            manufacturingYear = uint.Parse(dateCode[0..2]);
            manufacturingYear = manufacturingYear < 80 || manufacturingYear > 89 ? throw new ArgumentException() : 1900 + manufacturingYear;

            if (dateCode.Length == 5 && dateCode[2..3] != "0")
            {
                manufacturingMonth = uint.Parse(dateCode[2..3]);
            }
            else if (dateCode.Length == 6 && uint.Parse(dateCode[2..4]) > 0 && uint.Parse(dateCode[2..4]) < 13)
            {
                manufacturingMonth = uint.Parse(dateCode[2..4]);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static void Parse1990Code(string dateCode, out Country[] factoryLocationCountry, out string factoryLocationCode, out uint manufacturingYear, out uint manufacturingMonth)
        {
            if (string.IsNullOrEmpty(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            if (dateCode.Length != 6)
            {
                throw new ArgumentException();
            }

            factoryLocationCode = dateCode[0..2].ToUpper();
            factoryLocationCountry = CountryParser.GetCountry(factoryLocationCode);
            string year = dateCode[3..4] + dateCode[5..6];
            manufacturingYear = uint.Parse(year);

            if (dateCode[3..4] == "0")
            {
                manufacturingYear = manufacturingYear < 7 ? manufacturingYear + 2000 : throw new ArgumentException();
            }
            else if (dateCode[3..4] == "9")
            {
                manufacturingYear = manufacturingYear < 90 || manufacturingYear > 99 ? throw new ArgumentException() : manufacturingYear + 1900;
            }
            else
            {
                throw new ArgumentException();
            }

            string month = dateCode[2..3] + dateCode[4..5];
            manufacturingMonth = uint.Parse(month);
            manufacturingMonth = manufacturingMonth > 12 || manufacturingMonth < 1 ? throw new ArgumentException() : manufacturingMonth;
        }

        public static void Parse2007Code(string dateCode, out Country[] factoryLocationCountry, out string factoryLocationCode, out uint manufacturingYear, out uint manufacturingWeek)
        {
            if (string.IsNullOrEmpty(dateCode))
            {
                throw new ArgumentNullException(nameof(dateCode));
            }

            if (dateCode.Length != 6)
            {
                throw new ArgumentException();
            }

            factoryLocationCode = dateCode[0..2].ToUpper();
            factoryLocationCountry = CountryParser.GetCountry(factoryLocationCode);

            string year = dateCode[3..4] + dateCode[5..6];
            manufacturingYear = uint.Parse(year);
            manufacturingYear = manufacturingYear < 7 || manufacturingYear > (uint)DateTime.Now.Year - 2000 ? throw new ArgumentException() : manufacturingYear + 2000;

            int weeks = ISOWeek.GetWeeksInYear((int)manufacturingYear);
            string week = dateCode[2..3] + dateCode[4..5];
            manufacturingWeek = uint.Parse(week);
            manufacturingWeek = manufacturingWeek > weeks || manufacturingWeek < 1 ? throw new ArgumentException() : manufacturingWeek;
        }
    }
}
