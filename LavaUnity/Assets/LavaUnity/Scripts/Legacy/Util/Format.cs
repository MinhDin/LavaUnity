using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Lava
{
    public static class Format
    {
        public static int MinDateTimeInInt = 10000101;
        public static int ConvertDateTimeToIntFormat(DateTime time)
        {
            return time.Year * 10000 + time.Month * 100 + time.Day;
        }

        public static DateTime ConvertIntFormatToDateTime(int time)
        {
            return new DateTime(time / 10000, (time / 100) % 100, time % 100);
        }

        public static string DayToString(int year, int month, int day)
    {
        return string.Format("{0}{1}{2}", year.ToString("D4"),month.ToString("D2"),day.ToString("D2"));
    }

    public static string DayToString(DateTime day)
    {
        return DayToString(day.Year, day.Month, day.Day);
    }
    }
}
