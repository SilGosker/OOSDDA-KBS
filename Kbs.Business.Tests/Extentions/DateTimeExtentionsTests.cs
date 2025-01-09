namespace Kbs.Business.Extentions
{
    public class DateTimeExtentionsTests
    {
        [Fact]

        public void ToDutchString_TranslatesValueDateOnly()
        {

            // Arrange
            DateTime dateTime1 = new DateTime(2024, 6, 9);
            DateTime dateTime2 = new DateTime(2024, 12, 31);

            // Act
            var result1 = dateTime1.ToDutchString();
            var result2 = dateTime2.ToDutchString();

            // Assert
            Assert.Equal("2024-06-09", result1);
            Assert.Equal("2024-12-31", result2);
        }

        [Fact]

        public void ToDutchString_TranslatesValue()
        {
            // Arange
            DateTime dateTime1 = new DateTime(2024, 6, 9, 23, 59, 59);
            DateTime dateTime2 = new DateTime(2024, 12, 31, 12, 30, 50);

            // Act
            var result1 = dateTime1.ToDutchString(true);
            var result2 = dateTime2.ToDutchString(true);

            // Assert
            Assert.Equal("2024-06-09 23:59", result1);
            Assert.Equal("2024-12-31 12:30", result2);
        }

        [Theory]
        [InlineData(0, "Woensdag")]
        [InlineData(1, "Donderdag")]
        [InlineData(2, "Vrijdag")]
        [InlineData(3, "Zaterdag")]
        [InlineData(4, "Zondag")]
        [InlineData(5, "Maandag")]
        [InlineData(6, "Dinsdag")]

        public void ToDutchString_TranslatesValueDayOfWeek(int daysFromWednessday, string result)
        {
            DateTime day = new DateTime(2024, 11, 27);
            Assert.Equal(result, (string)(day.AddDays(daysFromWednessday)).DayOfWeek.ToDutchString());
        }
    }
}
