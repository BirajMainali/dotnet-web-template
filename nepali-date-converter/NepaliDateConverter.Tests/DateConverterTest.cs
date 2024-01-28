using NepaliDateConverter;
using NUnit.Framework;

namespace Tests
{
    public class DateConverterTests
    {
        DateConverter converter;
        [SetUp]
        public void Setup()
        {
            converter = new DateConverter();
        }

        [TearDown]
        public void Dispose()
        {
            converter = null;
        }

        [Test()]
        public void ConvertToEnglishShouldReturn2016_4_13()
        {
            converter = DateConverter.ConvertToEnglish(2073, 1, 1);
            Assert.AreEqual(2016, converter.Year);
            Assert.AreEqual(4, converter.Month, null, "Month doesn't matched");
            Assert.AreEqual(13, converter.Day);
            Assert.AreEqual("Wednesday", converter.WeekDayName);
            Assert.AreEqual("April", converter.MonthName);
            Assert.AreEqual(4, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2073_1_1()
        {
            converter = DateConverter.ConvertToNepali(2016, 4, 13);
            Assert.AreEqual(2073, converter.Year);
            Assert.AreEqual(1, converter.Month);
            Assert.AreEqual(1, converter.Day);
            Assert.AreEqual("Wednesday", converter.WeekDayName);
            Assert.AreEqual("Baishakh", converter.MonthName);
            Assert.AreEqual(4, converter.WeekDay);
        }

        [Test()]
        public void ConvertToEnglishShouldReturn2016_4_14()
        {
            converter = DateConverter.ConvertToEnglish(2073, 1, 2);
            Assert.AreEqual(2016, converter.Year);
            Assert.AreEqual(4, converter.Month);
            Assert.AreEqual(14, converter.Day);
            Assert.AreEqual("Thursday", converter.WeekDayName);
            Assert.AreEqual("April", converter.MonthName);
            Assert.AreEqual(5, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2073_1_2()
        {
            converter = DateConverter.ConvertToNepali(2016, 4, 14);
            Assert.AreEqual(2073, converter.Year);
            Assert.AreEqual(1, converter.Month);
            Assert.AreEqual(2, converter.Day);
            Assert.AreEqual("Thursday", converter.WeekDayName);
            Assert.AreEqual("Baishakh", converter.MonthName);
            Assert.AreEqual(5, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2047_5_11()
        {
            converter = DateConverter.ConvertToNepali(1990, 8, 27);
            Assert.AreEqual(2047, converter.Year);
            Assert.AreEqual(5, converter.Month);
            Assert.AreEqual(11, converter.Day);
            Assert.AreEqual("Monday", converter.WeekDayName);
            Assert.AreEqual("Bhadra", converter.MonthName);
            Assert.AreEqual(2, converter.WeekDay);
        }

        [Test]
        public void ConvertToEnglishShouldReturn1990_8_27()
        {
            converter = DateConverter.ConvertToEnglish(2047, 5, 11);
            Assert.AreEqual(1990, converter.Year);
            Assert.AreEqual(8, converter.Month);
            Assert.AreEqual(27, converter.Day);
            Assert.AreEqual("Monday", converter.WeekDayName);
            Assert.AreEqual("August", converter.MonthName);
            Assert.AreEqual(2, converter.WeekDay);
        }

        [Test()]
        public void ConvertToEnglishShouldReturn2016_4_27()
        {
            converter = DateConverter.ConvertToEnglish(2073, 1, 15);
            Assert.AreEqual(2016, converter.Year);
            Assert.AreEqual(4, converter.Month);
            Assert.AreEqual(27, converter.Day);
            Assert.AreEqual("Wednesday", converter.WeekDayName);
            Assert.AreEqual("April", converter.MonthName);
            Assert.AreEqual(4, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2073_1_15()
        {
            converter = DateConverter.ConvertToNepali(2016, 4, 27);
            Assert.AreEqual(2073, converter.Year);
            Assert.AreEqual(1, converter.Month);
            Assert.AreEqual(15, converter.Day);
            Assert.AreEqual("Wednesday", converter.WeekDayName);
            Assert.AreEqual("Baishakh", converter.MonthName);
            Assert.AreEqual(4, converter.WeekDay);
        }

        [Test()]
        public void ConvertToEnglishShouldReturn2016_5_12()
        {
            converter = DateConverter.ConvertToEnglish(2073, 1, 30);
            Assert.AreEqual(2016, converter.Year);
            Assert.AreEqual(5, converter.Month);
            Assert.AreEqual(12, converter.Day);
            Assert.AreEqual("Thursday", converter.WeekDayName);
            Assert.AreEqual("May", converter.MonthName);
            Assert.AreEqual(5, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2073_1_30()
        {
            converter = DateConverter.ConvertToNepali(2016, 5, 12);
            Assert.AreEqual(2073, converter.Year);
            Assert.AreEqual(1, converter.Month);
            Assert.AreEqual(30, converter.Day);
            Assert.AreEqual("Thursday", converter.WeekDayName);
            Assert.AreEqual("Baishakh", converter.MonthName);
            Assert.AreEqual(5, converter.WeekDay);
        }

        [Test()]
        public void ConvertToEnglishShouldReturn2016_5_13()
        {
            converter = DateConverter.ConvertToEnglish(2073, 1, 31);
            Assert.AreEqual(2016, converter.Year);
            Assert.AreEqual(5, converter.Month);
            Assert.AreEqual(13, converter.Day);
            Assert.AreEqual("Friday", converter.WeekDayName);
            Assert.AreEqual("May", converter.MonthName);
            Assert.AreEqual(6, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2073_1_31()
        {
            converter = DateConverter.ConvertToNepali(2016, 5, 13);
            Assert.AreEqual(2073, converter.Year);
            Assert.AreEqual(1, converter.Month);
            Assert.AreEqual(31, converter.Day);
            Assert.AreEqual("Friday", converter.WeekDayName);
            Assert.AreEqual("Baishakh", converter.MonthName);
            Assert.AreEqual(6, converter.WeekDay);
        }



        [Test()]
        public void ConvertToEnglishShouldReturn2017_1_6()
        {
            converter = DateConverter.ConvertToEnglish(2073, 9, 22);
            Assert.AreEqual(2017, converter.Year);
            Assert.AreEqual(1, converter.Month);
            Assert.AreEqual(6, converter.Day);
            Assert.AreEqual("Friday", converter.WeekDayName);
            Assert.AreEqual("January", converter.MonthName);
            Assert.AreEqual(6, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2073_9_22()
        {
            converter = DateConverter.ConvertToNepali(2017, 1, 6);
            Assert.AreEqual(2073, converter.Year);
            Assert.AreEqual(9, converter.Month);
            Assert.AreEqual(22, converter.Day);
            Assert.AreEqual("Friday", converter.WeekDayName);
            Assert.AreEqual("Poush", converter.MonthName);
            Assert.AreEqual(6, converter.WeekDay);
        }

        [Test()]
        public void ConvertToEnglishShouldReturn2017_4_14()
        {
            converter = DateConverter.ConvertToEnglish(2074, 1, 1);
            Assert.AreEqual(2017, converter.Year);
            Assert.AreEqual(4, converter.Month);
            Assert.AreEqual(14, converter.Day);
            Assert.AreEqual("Friday", converter.WeekDayName);
            Assert.AreEqual("April", converter.MonthName);
            Assert.AreEqual(6, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2074_1_1()
        {
            converter = DateConverter.ConvertToNepali(2017, 4, 14);
            Assert.AreEqual(2074, converter.Year);
            Assert.AreEqual(1, converter.Month);
            Assert.AreEqual(1, converter.Day);
            Assert.AreEqual("Friday", converter.WeekDayName);
            Assert.AreEqual("Baishakh", converter.MonthName);
            Assert.AreEqual(6, converter.WeekDay);
        }

        [Test]
        public void ConvertToEnglishShouldReturn2017_4_28()
        {
            converter = DateConverter.ConvertToEnglish(2074, 1, 15);
            Assert.AreEqual(2017, converter.Year);
            Assert.AreEqual(4, converter.Month);
            Assert.AreEqual(28, converter.Day);
            Assert.AreEqual("Friday", converter.WeekDayName);
            Assert.AreEqual("April", converter.MonthName);
            Assert.AreEqual(6, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2074_1_15()
        {
            converter = DateConverter.ConvertToNepali(2017, 4, 28);
            Assert.AreEqual(2074, converter.Year);
            Assert.AreEqual(1, converter.Month);
            Assert.AreEqual(15, converter.Day);
            Assert.AreEqual("Friday", converter.WeekDayName);
            Assert.AreEqual("Baishakh", converter.MonthName);
            Assert.AreEqual(6, converter.WeekDay);
        }

        [Test]
        public void ConvertToNepaliShouldReturn2077_12_19()
        {
            converter = DateConverter.ConvertToNepali(2021, 4, 01);
            Assert.AreEqual(2077, converter.Year);
            Assert.AreEqual(12, converter.Month);
            Assert.AreEqual(19, converter.Day);
        }
        
        [Test]
        public void ConvertToNepaliShouldReturn2077_12_31()
        {
            converter = DateConverter.ConvertToNepali(2021, 4, 13);
            Assert.AreEqual(2077, converter.Year);
            Assert.AreEqual(12, converter.Month);
            Assert.AreEqual(31, converter.Day);
        }
        
        [Test]
        public void ConvertToNepaliShouldReturn2078_01_01()
        {
            converter = DateConverter.ConvertToNepali(2021, 4, 14);
            Assert.AreEqual(2078, converter.Year);
            Assert.AreEqual(1, converter.Month);
            Assert.AreEqual(1, converter.Day);
        }
    }
}