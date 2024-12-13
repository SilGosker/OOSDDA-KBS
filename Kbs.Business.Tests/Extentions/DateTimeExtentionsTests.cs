using Kbs.Business.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Extentions
{
    public class DateTimeExtentionsTests
    {
        [Fact]
        
        public void ToDutchString_TranslatesValueDateOnly()
        {
            DateTime dateTime = new DateTime(2024, 6, 9);
            var result = dateTime.ToDutchString();
            Assert.Equal("2024-06-09", result);

            dateTime = new DateTime(2024, 12, 31);
            result = dateTime.ToDutchString();
            Assert.Equal("2024-12-31", result);
        }

        [Fact]

        public void ToDutchString_TranslatesValue()
        {
            DateTime dateTime = new DateTime(2024, 6, 9, 23, 59, 59);
            var result = dateTime.ToDutchString(true);
            Assert.Equal("2024-06-09 23:59", result);

            dateTime = new DateTime(2024, 12, 31, 12, 30, 50);
            result = dateTime.ToDutchString(true);
            Assert.Equal("2024-12-31 12:30", result);
        }
    }
}
