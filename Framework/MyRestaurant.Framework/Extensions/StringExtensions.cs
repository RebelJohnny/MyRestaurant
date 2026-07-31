namespace MyRestaurant.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string? ToPersian(this string? input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return input
                .Replace("ي", "ی")
                .Replace("ى", "ی")
                .Replace("ك", "ک")
                .Replace("ة", "ه")
                // Persian digits → English
                .Replace("۰", "0").Replace("۱", "1").Replace("۲", "2")
                .Replace("۳", "3").Replace("۴", "4").Replace("۵", "5")
                .Replace("۶", "6").Replace("۷", "7").Replace("۸", "8")
                .Replace("۹", "9")
                // Arabic-Indic digits → English
                .Replace("٠", "0").Replace("١", "1").Replace("٢", "2")
                .Replace("٣", "3").Replace("٤", "4").Replace("٥", "5")
                .Replace("٦", "6").Replace("٧", "7").Replace("٨", "8")
                .Replace("٩", "9");
        }
    }
}
