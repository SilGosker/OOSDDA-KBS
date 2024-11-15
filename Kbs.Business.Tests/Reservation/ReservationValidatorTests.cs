using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Business.Reservation
{
    public class ReservationValidatorTests
    {
        [Fact]
        public void ValidForCreation_ReturnDictionary()
        {
            //Arrange
            //Act
            //Assert
        }
        public void ValidForUpdates_ReturnDictionary()
        {
            //Arrange
            //Act
            //Assert
        }
        public void IsReservationLimitReached_AboveLimit_ReturnsTrue()
        {
            //Arrange
            //Act
            //Assert
        }
        public void IsReservationLimitReached_UnderLimit_ReturnsFalse()
        {
            //Arrange
            //Act
            //Assert
        }
        public void IsWithinDayLightHours_OutsideOfDaylightHours_ReturnsFalse()
        {
            //Arrange
            //Act
            //Assert
        }
        public void IsWithinDayLightHours_InsideOfDayLightHours_ReturnsTrue()
        {
            //Arrange
            //Act
            //Assert
        }
        public void IsDurationValid_Above120Minutes_ReturnsFalse()
        {
            //Arrange
            //Act
            //Assert
        }
        public void IsDurationValid_Under120Minutes_ReturnsTrue()
        {
            //Arrange
            //Act
            //Assert
        }
        public void CompetitionReservationValidator_NotMadeByOfficial_ReturnsFalse()
        {
            //Arrange
            //Act
            //Assert
        }
        public void CompetitionReservationValidator_MadeByOfficial_ReturnsTrue()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}
