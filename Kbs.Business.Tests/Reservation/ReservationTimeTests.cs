
using Kbs.Business.Boat;

namespace Kbs.Business.Reservation
{
    public class ReservationTimeTests
    {


        [Theory]
        [InlineData(1.5, 90)]
        [InlineData(2.5, 150)]
        [InlineData(0, 0)]
        public void Properties_ShouldSetValues(double expectedLength, int addedMinutes)
        {
            DateTime starttime = DateTime.Now;
            DateTime endtime = starttime.AddMinutes(addedMinutes);

            var restime = new ReservationTime(starttime, endtime);

            Assert.Equal(starttime, restime.StartTime);
            Assert.Equal(endtime, restime.EndTime);
            Assert.Equal(expectedLength, restime.Length);
        }
    }
}
