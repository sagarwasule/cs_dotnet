using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Common
{
    public static class RangeExtensions
    {
        public static Boolean isInRange(this Decimal dec, Decimal min, Decimal max, bool includesMin = true, bool includesMax = true)
        {
            return (includesMin ? (dec >= min) : (dec > min)) && (includesMax ? (dec <= max) : (dec < max));
        }

        public static Boolean isInRange(this Int32 dec, Int32 min, Int32 max, bool includesMin = true, bool includesMax = true)
        {
            return (includesMin ? (dec >= min) : (dec > min)) && (includesMax ? (dec <= max) : (dec < max));
        }
    }
}
