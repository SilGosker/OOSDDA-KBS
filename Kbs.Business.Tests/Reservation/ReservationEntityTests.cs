using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kbs.Business.Reservation;

namespace Kbs.Business.Reservation
{
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
        [InlineData()]
        public void GetAndSet_StartTime_ReturnsAndSetsStartTime()
        {
            //Arrange
            //Act
            //Assert
        }

        [Theory]
        [InlineData()]
        public void GetAndSet_Duration_ReturnsAndSetsDuration()
        {
            //Arrange
            //Act
            //Assert
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
        /*
        [Theory]
        [InlineData((9,0,0)]
        public void Set_EndTime_ReturnsEndTime(TimeOnly time)
        {
            //Arrange
            var reservation = new ReservationEntity();
            //Act
            reservation.EndTime = time;
            //Assert
        }
        */



        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void Add_TotalReservations_IsAdded(int totalReservationsInt)
        {
            //Arrange
            var reservation = new ReservationEntity();
            //Act
            reservation.totalReservations += totalReservationsInt;
            //Assert
            Assert.Equal(reservation.totalReservations, totalReservationsInt);
        }       
    }
}
