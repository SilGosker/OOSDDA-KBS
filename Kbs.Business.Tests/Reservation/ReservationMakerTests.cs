﻿
using Kbs.Business.Mock;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;

namespace Kbs.Business.Reservation
{
    public class ReservationMakerTests
    {

        [Fact]
        
        public void Properties_ShouldSetValues()
        {
            var mock = new MockReservationRepository();
            ReservationMaker resmaker = new ReservationMaker(mock);

            mock.Create(new ReservationEntity() { StartTime = new DateTime(2024, 11, 26, 9, 30, 0), Length = TimeSpan.FromMinutes(30) });

            var res = resmaker.MakeReservableTimes(new DateTime(2024, 11, 26), new Boat.BoatEntity());

            Assert.True(res.Count == 2);

            ReservationTime res1 = new ReservationTime(new DateTime(2024, 11, 26, 9, 0, 0), new DateTime(2024, 11, 26, 9, 30, 0));
            ReservationTime res2 = new ReservationTime(new DateTime(2024, 11, 26, 10, 0, 0), new DateTime(2024, 11, 26, 17, 0, 0));

            Assert.Equal(res[0].Length, res1.Length);
            Assert.Equal(res[0].StartTime, res1.StartTime);
            Assert.Equal(res[0].EndTime, res1.EndTime);

            Assert.Equal(res[1].Length, res2.Length);
            Assert.Equal(res[1].StartTime, res2.StartTime);
            Assert.Equal(res[1].EndTime, res2.EndTime);
        }

        [Theory]
        [InlineData(0, "Woensdag")]
        [InlineData(1, "Donderdag")]
        [InlineData(2, "Vrijdag")]
        [InlineData(3, "Zaterdag")]
        [InlineData(4, "Zondag")]
        [InlineData(5, "Maandag")]
        [InlineData(6, "Dinsdag")]

        public void ConvertDayOfWeekToDutchTests(int daysFromWednessday, string result)
        {
            DateTime day = new DateTime(2024, 11, 27);
            Assert.Equal(result, ReservationMaker.ConvertDayOfWeekToDutch(day.AddDays(daysFromWednessday)));
        }

    }
}
