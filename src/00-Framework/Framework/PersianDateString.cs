using System.Globalization;

namespace Framework;

public static class PersianDateString
{
    public static DateTime ToGregorianDate(this string persianDate)
    {
        PersianCalendar persianCalendar = new PersianCalendar();

        // تبدیل اعداد فارسی به اعداد عربی
        string numericPersianDate = ConvertPersianNumbersToEnglish(persianDate);

        string[] parts = numericPersianDate.Split('/');

        // بررسی طول ورودی
        if (parts.Length != 3)
        {
            throw new FormatException("فرمت تاریخ نامعتبر است.");
        }

        // تبدیل رشته‌ها به اعداد
        int year = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int day = int.Parse(parts[2]);

        // تبدیل تاریخ شمسی به میلادی
        DateTime gregorianDate = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);

        return gregorianDate;
    }

    // تابع تبدیل اعداد فارسی به اعداد عربی
    private static string ConvertPersianNumbersToEnglish(string input)
    {
        char[] persianNumbers = { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
        char[] englishNumbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        for (int i = 0; i < persianNumbers.Length; i++)
        {
            input = input.Replace(persianNumbers[i], englishNumbers[i]);
        }

        return input;
    }
    public static bool IsValidPersianDateAndFuture(this string persianDate)
    {
        try
        {
            // بررسی فرمت تاریخ
            string[] parts = persianDate.Split('/');
            if (parts.Length != 3)
                return false;

            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            // بررسی محدوده‌های معتبر برای سال، ماه و روز
            if (year < 1 || month < 1 || month > 12 || day < 1 || day > 31)
                return false;

            // تبدیل تاریخ شمسی به میلادی
            PersianCalendar pc = new PersianCalendar();
            DateTime gregorianDate = pc.ToDateTime(year, month, day, 0, 0, 0, 0);

            // بررسی معتبر بودن تاریخ
            if (gregorianDate == DateTime.MinValue)
                return false;

            // بررسی بزرگ‌تر بودن از تاریخ فعلی
            DateTime now = DateTime.Now;
            return gregorianDate > now;
        }
        catch
        {
            return false;
        }
    }
}
