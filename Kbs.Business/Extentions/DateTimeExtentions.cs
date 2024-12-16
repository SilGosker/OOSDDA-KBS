using Kbs.Business.Boat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
