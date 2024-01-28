using System;
using System.Collections.Generic;
using NepaliDateConverter.Interfaces;

namespace NepaliDateConverter
{
    public class NepaliDateConverter : INepaliDateConverter
    {
        private static readonly Dictionary<string, DateParts> CachedConvertedDates = new();

        public static readonly Dictionary<long, string> CachedNepaliMonths = new Dictionary<long, string>
        {
            { 1, "Baisakh" },
            { 2, "Jestha" },
            { 3, "Ashadh" },
            {
                4, "Shrawn"
            },
            {
                5, "Bhadra"
            },
            {
                6, "Ashoj"
            },
            {
                7, "Kartik"
            },
            {
                8, "Mangsir"
            },
            {
                9, "Poush"
            },
            {
                10, "Magh"
            },
            {
                11, "Falgun"
            },
            {
                12, "Chaitra"
            },
        };

        public DateParts GetBsDatePartsFromAdDate(DateTime adDate)
        {
            var calendar = DateConverter.ConvertToNepali(adDate.Year, adDate.Month, adDate.Day);
            return new DateParts(calendar.Year, calendar.Month, calendar.Day);
        }

        public string GetBsDateFromAdDate(int yy, int mm, int dd, char separator = '-', string format = "yyyy-mm-dd")
        {
            var key = GetKey(yy, mm, dd);
            if (CachedConvertedDates.TryGetValue(key, out var convertedDate))
            {
                if (format != "yyyy-mm-dd")
                {
                    return format.Replace("yyyy", convertedDate.Year.ToString())
                        .Replace("mm", convertedDate.Month.ToString()).Replace("dd", convertedDate.Day.ToString());
                }

                return GetFinalDate(convertedDate);
            }

            var calendar = DateConverter.ConvertToNepali(yy, mm, dd);
            CachedConvertedDates.TryAdd(key, new DateParts(calendar.Year, calendar.Month, calendar.Day));

            return GetBsDateFromAdDate(yy, mm, dd, separator, format);

            string GetFinalDate(DateParts calendar)
            {
                return
                    $"{calendar.Year}{separator}{calendar.Month.ToString().PadLeft(2, '0')}{separator}{calendar.Day.ToString().PadLeft(2, '0')}";
            }
        }

        public string GetBsDateFromAdDate(DateTime dateTime, char separator = '-', string format = "yyyy-mm-dd")
        {
            var year = dateTime.Year;
            var month = dateTime.Month;
            var day = dateTime.Day;
            return GetBsDateFromAdDate(year, month, day, separator, format);
        }

        public DateTime GetAdDateFromBsDate(string bsDate, char separator = '-')
        {
            var parts = bsDate.Split(separator);
            var year = Convert.ToInt32(parts[0]);
            var month = Convert.ToInt32(parts[1]);
            var day = Convert.ToInt32(parts[2]);
            var calendar = DateConverter.ConvertToEnglish(year, month, day);
            return new DateTime(calendar.Year, calendar.Month, calendar.Day);
        }

        public string GetNepaliMonth(long month)
        {
            return CachedNepaliMonths.GetValueOrDefault(month);
        }

        public (string From, string To) GetMonthRange(DateTime dateTime, char separator = '-')
        {
            var bsDate = GetBsDateFromAdDate(dateTime, separator);
            var parts = bsDate.Split(separator);
            var year = Convert.ToInt32(parts[0]);
            var month = Convert.ToInt32(parts[1]);
            var calendar = new Calendar();
            var entry = calendar.BSCalendar[year - 2000]; // Year entry start from 2000 with zero index.
            var maxDays = entry[month];
            return ($"{year}-{month}-{01}", $"{year}-{month}-{maxDays}");
        }

        public (DateTime From, DateTime To) GetBsMonthRangeInAd(DateTime dateTime)
        {
            var range = GetMonthRange(dateTime);
            return (GetAdDateFromBsDate(range.From), GetAdDateFromBsDate(range.To));
        }

        private string GetKey(int yy, int mm, int dd)
            => $"{yy}-{mm}-{dd}";
    }
}