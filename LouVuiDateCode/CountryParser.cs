using System;
using System.Collections.Generic;
using System.Linq;

namespace LouVuiDateCode
{
    public static class CountryParser
    {
        // Countries codes: https://www.yoogiscloset.com/authenticate/louis-vuitton/

        private static readonly Dictionary<Country, string[]> CountryName = new Dictionary<Country, string[]>()
        {
            { Country.France, new string[] { "A0", "A1", "A2", "AA", "AAS", "AH", "AN", "AR", "AS", "BA", "BJ", "BU", "DR", "DU", "DT", "CO", "CT", "CX", "ET", "FL", "LA", "LW", "MB", "MI", "NO", "RA", "RI", "SA", "SD", "SF", "SL", "SN", "SP", "SR", "TA", "TJ", "TH", "TN", "TR", "TS", "VI", "VX" } },
            { Country.Germany, new string[] { "LP", "OL" } },
            { Country.Italy, new string[] { "BC", "BO", "CE", "FN", "FO", "MA", "NZ", "OB", "PL", "RC", "RE", "SA", "TD" } },
            { Country.Spain, new string[] { "BC", "CA", "LO", "LB", "LM", "LW", "GI", "UB" } },
            { Country.Switzerland, new string[] { "DI", "FA" } },
            { Country.USA, new string[] { "FC", "FH", "LA", "OS", "SD", "FL", "TX" } },
        };

        // Gets a an array of enumeration values for a specified factory location code.
        // One location code can belong to many countries.
        public static Country[] GetCountry(string factoryLocationCode)
        {
            if (string.IsNullOrEmpty(factoryLocationCode)) throw new ArgumentNullException(nameof(factoryLocationCode));
            Country[] array = CountryName.Where(f => f.Value.Contains(factoryLocationCode)).Select(s => s.Key).ToArray();
            if (array.Length < 1) throw new ArgumentException();
            return array;
        }
    }
}
