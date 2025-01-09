namespace Kbs.Business.Extentions
{
    public static class DateTimeExtentions
    {
        public static string ToDutchString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string ToDutchString(this DateTime dateTime, bool withTime)
        {
            if (!withTime)
            {
                return dateTime.ToDutchString();
            }
            return dateTime.ToString("yyyy-MM-dd HH:mm");
        }

        public static string ToDutchString(this DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek.ToString())
            {
                case "Monday": return "Maandag";
                case "Tuesday": return "Dinsdag";
                case "Wednesday": return "Woensdag";
                case "Thursday": return "Donderdag";
                case "Friday": return "Vrijdag";
                case "Saturday": return "Zaterdag";
                case "Sunday": return "Zondag";
                default: return "geen dag gevonden";
            }
        }
    }
}
