namespace NepaliDateConverter
{
    public class DateParts
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public DateParts(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
    }
}