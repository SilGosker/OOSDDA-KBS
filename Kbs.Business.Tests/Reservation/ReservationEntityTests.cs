namespace Kbs.Business.Reservation;

public class ReservationEntityTests
{

    [Theory]
    [InlineData(123)]
    [InlineData(-3)]
    [InlineData(9999)]

    public void GetAndSet_ReservationId_ReturnsAndSetsReservationId(int reservationId)
    {
        //Arrange
        var reservation = new ReservationEntity();
        //Act
        reservation.ReservationId = reservationId;
        //Assert
        Assert.Equal(reservationId, reservation.ReservationId);
    }
        
    [Theory]
    [InlineData(ReservationStatus.Expired)]
    [InlineData(ReservationStatus.Active)]
    [InlineData(ReservationStatus.Deleted)]
    public void GetAndSet_Status_ReturnsAndSetsStatus(ReservationStatus status)
    {
        //Arrange
        var reservation = new ReservationEntity();
        //Act
        reservation.Status = status;
        //Assert
        Assert.Equal(status, reservation.Status);
    }
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GetAndSet_IsForCompetition_ReturnsAndSets(bool o)
    {
        //Arrange
        var reservation = new ReservationEntity();
        //Act
        reservation.IsForCompetition = o;
        //Assert
        Assert.Equal(reservation.IsForCompetition, o);
    }

    [Fact]
    public void ReservationEntity_EndTime_ShouldBeCalculatedCorrectly()
    {
        var reservation = new ReservationEntity
        {
            StartTime = new DateTime(2024, 1, 1, 10, 0, 0),
            Length = TimeSpan.FromHours(2)
        };

        Assert.Equal(new DateTime(2024, 1, 1, 12, 0, 0), reservation.EndTime);
    }
}