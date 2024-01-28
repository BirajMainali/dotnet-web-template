﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NepaliDateConverter
{
    /// <summary>
    /// Calendar calendar = new Calendar();
    /// </summary>
    public class Calendar : ICalendar
    {
        private IDictionary<int, string> WeekNames;
        private IDictionary<int, string> EnglishMonthNames;
        public IDictionary<int, string> NepaliMonthNames { get; private set; }

        /// <summary>
        /// List of BS calendar months in each year
        /// </summary>
        public IDictionary<int, int[]> BSCalendar;

        /// <summary>
        /// Calendar calendar = new Calendar();
        /// </summary>
        public Calendar()
        {
            SetWeekNames();
            SetEnglishMonthNames();
            SetNepaliMonthNames();
            SetBSCalendar();
        }

        /// <summary>
        /// Will return Day of week for given day index
        /// </summary>
        /// <param name="day">1</param>
        /// <returns>Sunday</returns>
        public string GetDayOfWeek(int day)
        {
            if (day < 1 || day > 7)
                day = 1;
            return WeekNames[day];
        }

        /// <summary>
        /// Will return English month name for given month index
        /// </summary>
        /// <param name="month">2</param>
        /// <returns>February</returns>
        public string GetEnglishMonth(int month)
        {
            if (month < 1 || month > 12)
                month = 1;
            return EnglishMonthNames[month];
        }

        /// <summary>
        /// Will return Nepali month name for given month index
        /// </summary>
        /// <param name="month">12</param>
        /// <returns>Chaitra</returns>
        public string GetNepaliMonth(int month)
        {
            if (month < 1 || month > 12)
                month = 1;
            return NepaliMonthNames[month];
        }

        /// <summary>
        /// Check if given year is Leap Year or not
        /// </summary>
        /// <param name="year">2016</param>
        /// <returns>True</returns>
        public bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }

        /// <summary>
        /// Check if given date is in valid English Date range or not.
        /// Only supports Date range between 1944 To 2033
        /// </summary>
        /// <param name="year">2017</param>
        /// <param name="month">5</param>
        /// <param name="day">8</param>
        /// <returns>True</returns>
        public bool ValidEnglishDate(int year, int month, int day)
        {
            if (year < 1944 || year > 2033)
            {
                Debug.WriteLine("Year should be between 1944 - 2033");
                return false;
            }

            if (month < 1 || month > 12)
            {
                Debug.WriteLine("Month should be between 1 - 12");
                return false;
            }

            if (day < 1 || day > 31)
            {
                Debug.WriteLine("Day should be between 1 - 31");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if Given date is in Valid Nepali date range or not.
        /// Only supports Date range Between 2000 To 2089
        /// </summary>
        /// <param name="year">2073</param>
        /// <param name="month">1</param>
        /// <param name="day">1</param>
        /// <returns>True</returns>
        public bool ValidNepaliDate(int year, int month, int day)
        {
            if (year < 2000 || year > 2089)
            {
                Debug.WriteLine("Year should be between 1944 - 2033");
                return false;
            }

            if (month < 1 || month > 12)
            {
                Debug.WriteLine("Month should be between 1 - 12");
                return false;
            }

            if (day < 1 || day > 32)
            {
                Debug.WriteLine("Day should be between 1 - 31");
                return false;
            }

            return true;
        }

        private void SetWeekNames()
        {
            WeekNames = new Dictionary<int, string>
            {
                { 1, "Sunday" },
                { 2, "Monday" },
                { 3, "Tuesday" },
                { 4, "Wednesday" },
                { 5, "Thursday" },
                { 6, "Friday" },
                { 7, "Saturday" }
            };
        }

        private void SetEnglishMonthNames()
        {
            EnglishMonthNames = new Dictionary<int, string>
            {
                { 1, "January" },
                { 2, "February" },
                { 3, "March" },
                { 4, "April" },
                { 5, "May" },
                { 6, "June" },
                { 7, "July" },
                { 8, "August" },
                { 9, "September" },
                { 10, "October" },
                { 11, "November" },
                { 12, "December" }
            };
        }

        private void SetNepaliMonthNames()
        {
            NepaliMonthNames = new Dictionary<int, string>
            {
                { 1, "Baishakh" },
                { 2, "Jestha" },
                { 3, "Ashad" },
                { 4, "Shrawan" },
                { 5, "Bhadra" },
                { 6, "Aswin" },
                { 7, "Kartik" },
                { 8, "Mangshir" },
                { 9, "Poush" },
                { 10, "Magh" },
                { 11, "Falgun" },
                { 12, "Chaitra" }
            };
        }

        private void SetBSCalendar()
        {
            BSCalendar = new Dictionary<int, int[]>
            {
                { 0, new int[] { 2000, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 1, new int[] { 2001, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 2, new int[] { 2002, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 3, new int[] { 2003, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 4, new int[] { 2004, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 5, new int[] { 2005, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 6, new int[] { 2006, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 7, new int[] { 2007, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 8, new int[] { 2008, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31 } },
                { 9, new int[] { 2009, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 10, new int[] { 2010, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 11, new int[] { 2011, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 12, new int[] { 2012, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
                { 13, new int[] { 2013, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 14, new int[] { 2014, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 15, new int[] { 2015, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 16, new int[] { 2016, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
                { 17, new int[] { 2017, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 18, new int[] { 2018, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 19, new int[] { 2019, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 20, new int[] { 2020, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 21, new int[] { 2021, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 22, new int[] { 2022, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
                { 23, new int[] { 2023, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 24, new int[] { 2024, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 25, new int[] { 2025, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 26, new int[] { 2026, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 27, new int[] { 2027, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 28, new int[] { 2028, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 29, new int[] { 2029, 31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30 } },
                { 30, new int[] { 2030, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 31, new int[] { 2031, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 32, new int[] { 2032, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 33, new int[] { 2033, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 34, new int[] { 2034, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 35, new int[] { 2035, 30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31 } },
                { 36, new int[] { 2036, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 37, new int[] { 2037, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 38, new int[] { 2038, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 39, new int[] { 2039, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
                { 40, new int[] { 2040, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 41, new int[] { 2041, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 42, new int[] { 2042, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 43, new int[] { 2043, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
                { 44, new int[] { 2044, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 45, new int[] { 2045, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 46, new int[] { 2046, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 47, new int[] { 2047, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 48, new int[] { 2048, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 49, new int[] { 2049, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
                { 50, new int[] { 2050, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 51, new int[] { 2051, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 52, new int[] { 2052, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 53, new int[] { 2053, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
                { 54, new int[] { 2054, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 55, new int[] { 2055, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 56, new int[] { 2056, 31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30 } },
                { 57, new int[] { 2057, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 58, new int[] { 2058, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 59, new int[] { 2059, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 60, new int[] { 2060, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 61, new int[] { 2061, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 62, new int[] { 2062, 30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31 } },
                { 63, new int[] { 2063, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 64, new int[] { 2064, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 65, new int[] { 2065, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 66, new int[] { 2066, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31 } },
                { 67, new int[] { 2067, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 68, new int[] { 2068, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 69, new int[] { 2069, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 70, new int[] { 2070, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
                { 71, new int[] { 2071, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 72, new int[] { 2072, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
                { 73, new int[] { 2073, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
                { 74, new int[] { 2074, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 75, new int[] { 2075, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 76, new int[] { 2076, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
                { 77, new int[] { 2077, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
                { 78, new int[] { 2078, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 79, new int[] { 2079, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
                { 80, new int[] { 2080, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
                { 81, new int[] { 2081, 31, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30 } },
                { 82, new int[] { 2082, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 } },
                { 83, new int[] { 2083, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30 } },
                { 84, new int[] { 2084, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30 } },
                { 85, new int[] { 2085, 31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30 } },
                { 86, new int[] { 2086, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 } },
                { 87, new int[] { 2087, 31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30 } },
                { 88, new int[] { 2088, 30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30 } },
                { 89, new int[] { 2089, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 } },
                { 90, new int[] { 2090, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 } }
            };
        }
    }
}
