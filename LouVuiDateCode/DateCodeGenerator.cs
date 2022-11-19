using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace LouVuiDateCode
{
    public static class DateCodeGenerator
    {
        /// Generates a date code using rules from early 1980s
        public static string GenerateEarly1980Code(uint manufacturingYear, uint manufacturingMonth)
        {
           if (manufacturingYear > 1989 || manufacturingYear < 1980 || manufacturingMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingYear));
            }

           return manufacturingYear.ToString()[^2..^0] + manufacturingMonth.ToString();
        }

        /// Generates a date code using rules from early 1980s.
        public static string GenerateEarly1980Code(DateTime manufacturingDate)
        {
            string t = manufacturingDate.ToString("yyyy-MM-dd");
            if (manufacturingDate.Year > 1989 || manufacturingDate.Year < 1980 || manufacturingDate.Month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingDate));
            }

            return t[2..4] + (t[5..6] == "0" ? t[6..7] : t[5..7]);
        }

        /// Generates a date code using rules from late 1980s.
        public static string GenerateLate1980Code(string factoryLocationCode, uint manufacturingYear, uint manufacturingMonth)
        {
            if (string.IsNullOrEmpty(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            if (manufacturingYear > 1989 || manufacturingYear < 1980 || manufacturingMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingYear));
            }

            return factoryLocationCode.All(r => char.IsLetter(r)) && factoryLocationCode.Length == 2 ?
                manufacturingYear.ToString()[^2..^0] + manufacturingMonth.ToString() + factoryLocationCode.ToUpper() : throw new ArgumentException();
        }

        /// Generates a date code using rules from late 1980s.
        public static string GenerateLate1980Code(string factoryLocationCode, DateTime manufacturingDate)
        {
            if (string.IsNullOrEmpty(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            string t = manufacturingDate.ToString(format: "yyyy-MM-dd");
            if (manufacturingDate.Year > 1989 || manufacturingDate.Year < 1980 || manufacturingDate.Month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingDate));
            }

            string countryCode = factoryLocationCode.All(r => char.IsLetter(r)) && factoryLocationCode.Length == 2 ?
                factoryLocationCode.ToUpper() : throw new ArgumentException($"wrong format of {factoryLocationCode}");

            return t[2..4] + (t[5..6] == "0" ? t[6..7] + countryCode : t[5..7] + countryCode);
        }

        /// Generates a date code using rules from 1990 to 2006 period.
        public static string Generate1990Code(string factoryLocationCode, uint manufacturingYear, uint manufacturingMonth)
        {
            if (string.IsNullOrEmpty(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            if (manufacturingYear > 2006 || manufacturingYear < 1990 || manufacturingMonth > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingYear));
            }

            factoryLocationCode = factoryLocationCode.All(r => char.IsLetter(r)) && factoryLocationCode.Length == 2 ?
                factoryLocationCode.ToUpper() : throw new ArgumentException($"wrong format{factoryLocationCode}");

            string month = manufacturingMonth.ToString().Length == 1 ? "0" + manufacturingMonth.ToString() : manufacturingMonth.ToString();

            return factoryLocationCode + month.ToString()[0] + manufacturingYear.ToString()[2] + month.ToString()[1] + manufacturingYear.ToString()[3];
        }

        /// Generates a date code using rules from 1990 to 2006 period.
        public static string Generate1990Code(string factoryLocationCode, DateTime manufacturingDate)
        {
            if (string.IsNullOrEmpty(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            string t = manufacturingDate.ToString("yyyy-MM-dd");
            if (manufacturingDate.Year > 2006 || manufacturingDate.Year < 1990 || manufacturingDate.Month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingDate));
            }

            string countryCode = factoryLocationCode.All(r => char.IsLetter(r)) && factoryLocationCode.Length == 2 ?
                factoryLocationCode.ToUpper() : throw new ArgumentException($"wrong format of {factoryLocationCode}");
            string firstNumMonth = t[5..6] == "0" ? "0" : t[5..6];
            string lastNumMonth = t[6..7];

            return countryCode + firstNumMonth + t[2..3] + lastNumMonth + t[3..4];
        }

        /// Generates a date code using rules from post 2007 period.
        public static string Generate2007Code(string factoryLocationCode, uint manufacturingYear, uint manufacturingWeek)
        {
            if (string.IsNullOrEmpty(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            if (manufacturingYear > DateTime.Now.Year || manufacturingYear < 2007 || manufacturingWeek > ISOWeek.GetWeeksInYear((int)manufacturingYear) || manufacturingWeek < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingYear));
            }

            string countryCode = factoryLocationCode.All(r => char.IsLetter(r)) && factoryLocationCode.Length == 2 ?
                factoryLocationCode.ToUpper() : throw new ArgumentException($"wrong format of {factoryLocationCode}");
            string week = manufacturingWeek.ToString().Length == 1 ? "0" + manufacturingWeek.ToString() : manufacturingWeek.ToString();

            return countryCode + week[0] + manufacturingYear.ToString()[2] + week[1] + manufacturingYear.ToString()[3];
        }

            /// Generates a date code using rules from post 2007 period.
        public static string Generate2007Code(string factoryLocationCode, DateTime manufacturingDate)
            {
            if (string.IsNullOrEmpty(factoryLocationCode))
            {
                throw new ArgumentNullException(nameof(factoryLocationCode));
            }

            if (manufacturingDate.Year > DateTime.Now.Year || manufacturingDate.Year < 2007)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturingDate));
            }

            string countryCode = factoryLocationCode.All(r => char.IsLetter(r)) && factoryLocationCode.Length == 2 ?
                factoryLocationCode.ToUpper() : throw new ArgumentException($"wrong format of {factoryLocationCode}");

            string week = ISOWeek.GetWeekOfYear(manufacturingDate).ToString();
            week = week.Length == 1 ? "0" + week : week;

            string year = ISOWeek.GetYear(manufacturingDate).ToString();

            return countryCode + week[0] + year[2] + week[1] + year[3];
        }
    }
}
