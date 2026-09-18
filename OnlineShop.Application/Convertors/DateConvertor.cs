using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OnlineShop.Application.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" +
                pc.GetMonth(date).ToString("00") + "/" +
                pc.GetDayOfMonth(date).ToString("00");
        }
        public static string ToShamsiWhitTime(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" +
                pc.GetMonth(date).ToString("00") + "/" +
                pc.GetDayOfMonth(date).ToString("00") + "-" +
                pc.GetHour(date).ToString("00") + ":" +
                pc.GetMinute(date).ToString("00");
        }
    }
}
