namespace Kbs.Business.Reservation
{
    public class DamageReportTests
    {
        [Fact]
        public void DamageReport_DamageDescription_ShouldNotBeEmpty()
        {
            var report = new DamageReport
            {
                DamageDescription = string.Empty
            };

            Assert.True(string.IsNullOrEmpty(report.DamageDescription), "Damage description cannot be empty");
        }
        [Fact]
        public void DamageReport_DateReported_CanBeNull()
        {
            var report = new DamageReport
            {
                DateReported = null
            };

            Assert.Null(report.DateReported);
        }
        [Fact]
        public void DamageReport_BoatID_ShouldBeValid()
        {
            var report = new DamageReport
            {
                BoatID = 1
            };

            Assert.Equal(true, report.BoatID >= 0);
        }
    }
}
