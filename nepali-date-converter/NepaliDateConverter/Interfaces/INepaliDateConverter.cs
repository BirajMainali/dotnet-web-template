using System;

namespace NepaliDateConverter.Interfaces
{
    public interface INepaliDateConverter
    {
        DateParts GetBsDatePartsFromAdDate(DateTime adDate);
        string GetBsDateFromAdDate(int yy, int mm, int dd, char separator = '-', string format = "yyyy-mm-dd");

        string GetBsDateFromAdDate(DateTime dateTime, char separator = '-', string format = "yyyy-mm-dd");

        (string From, string To) GetMonthRange(DateTime dateTime, char separator = '-');
        DateTime GetAdDateFromBsDate(string bsDate, char separator = '-');
        string GetNepaliMonth(long month);

        (DateTime From, DateTime To) GetBsMonthRangeInAd(DateTime dateTime);
    }
}