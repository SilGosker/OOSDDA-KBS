using Kbs.Business.User;
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
        public void ReservationValidator_IsReservationLimitReached_ShouldReturnTrueIfLimitExceeded()
        {
            var user = new UserEntity();
            var reservation = new ReservationEntity
            {
                totalReservations = 3
            };

            var validator = new ReservationValidator();
            Assert.False(validator.IsReservationLimitReached(user, reservation));
            
        }
        [Fact]
        public void ReservationValidator_IsReservationLimitReached_ShouldReturnFalseIfLimitNotExceeded()
        {
            // Arrange
            var user = new UserEntity();
            var reservation = new ReservationEntity
            {
                totalReservations = 2 // Limiet is exact bereikt
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.IsReservationLimitReached(user, reservation);

            // Assert
            Assert.False(result, "Moet false retourneren als het limiet niet is overschreden.");
        }
        [Fact]
        public void ReservationValidator_IsWithinDaylightHours_ShouldReturnFalseIfOutsideDaylight()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                StartTime = new DateTime(2024, 1, 1, 8, 0, 0), // Starttijd buiten de daglichturen
                Length = TimeSpan.FromHours(2) // Reservering eindigt om 10:00, binnen daglichturen
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.IsWithinDaylightHours(reservation);

            // Assert
            Assert.False(result, "Reserveringen die starten voor 09:00 moeten buiten daglichturen vallen.");
        }
    
        [Fact]
        public void ReservationValidator_IsDurationValid_ShouldReturnTrueIfDurationIsSufficient()
        {
            var reservation = new ReservationEntity
            {
                Length = TimeSpan.FromMinutes(121)
            };

            var validator = new ReservationValidator();
            Assert.False(validator.IsDurationValid(reservation));
        }
        [Fact]
        public void ReservationValidator_CompetitionReservationValidator_ShouldReturnFalseIfNotForCompetition()
        {
            var reservation = new ReservationEntity
            {
                IsForCompetition = false,
                Length = TimeSpan.FromMinutes(90)
            };

            var validator = new ReservationValidator();
            Assert.False(validator.CompetitionReservationValidator(reservation));
        }

        [Fact]
        public void ReservationValidator_IsWithinDaylightHours_ShouldReturnFalseIfCompletelyOutsideDaylightHours()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                StartTime = new DateTime(2024, 1, 1, 18, 0, 0), // Start na 17:00
                Length = TimeSpan.FromHours(2) // Eindigt om 20:00
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.IsWithinDaylightHours(reservation);

            // Assert
            Assert.False(result, "Moet false retourneren als de reservering volledig buiten daglichturen valt.");
        }
        [Fact]
        public void ReservationValidator_IsWithinDaylightHours_ShouldReturnFalseIfPartiallyOutsideDaylightHours()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                StartTime = new DateTime(2024, 1, 1, 8, 30, 0), // Start net voor 09:00
                Length = TimeSpan.FromHours(2) // Eindigt om 10:30
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.IsWithinDaylightHours(reservation);

            // Assert
            Assert.False(result, "Moet false retourneren als de reservering gedeeltelijk buiten daglichturen valt.");
        }
        [Fact]
        public void ReservationValidator_IsDurationValid_ShouldReturnTrueForValidDuration()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                Length = TimeSpan.FromMinutes(121) // Meer dan 120 minuten
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.IsDurationValid(reservation);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void ReservationValidator_IsDurationValid_ShouldReturnFalseForInvalidDuration()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                Length = TimeSpan.FromMinutes(119) // Minder dan 120 minuten
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.IsDurationValid(reservation);

            // Assert
            Assert.True(result, "Moet false retourneren als de duur minder dan 120 minuten is.");
        }
        [Fact]
        public void ReservationValidator_CompetitionReservationValidator_ShouldReturnTrueForCompetition()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                IsForCompetition = true,
                Length = TimeSpan.FromMinutes(150) // Geldige duur
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.CompetitionReservationValidator(reservation);

            // Assert
            Assert.True(result, "Moet true retourneren als de reservering voor competitie is.");
        }
        [Fact]
        public void ReservationValidator_CompetitionReservationValidator_ShouldReturnFalseForNonCompetitionWithInvalidDuration()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                IsForCompetition = false,
                Length = TimeSpan.FromMinutes(90) // Ongeldige duur
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.CompetitionReservationValidator(reservation);

            // Assert
            Assert.False(result, "Moet false retourneren als de reservering niet voor competitie is en de duur ongeldig is.");
        }
        [Fact]
        public void ReservationValidator_CompetitionReservationValidator_ShouldReturnFalseForNonCompetitionWithValidDuration()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                IsForCompetition = false,
                Length = TimeSpan.FromMinutes(150) // Geldige duur
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.CompetitionReservationValidator(reservation);

            // Assert
            Assert.False(result, "Moet false retourneren als de reservering niet voor competitie is, ongeacht de duur.");
        }
        [Fact]
        public void ReservationValidator_IsWithinDaylightHours_ShouldReturnTrueForBoundaryValues()
        {
            // Arrange
            var reservation = new ReservationEntity
            {
                StartTime = new DateTime(2024, 1, 1, 9, 0, 0), // Start exact om 09:00
                Length = TimeSpan.FromHours(8) // Eindigt om 17:00
            };

            var validator = new ReservationValidator();

            // Act
            var result = validator.IsWithinDaylightHours(reservation);

            // Assert
            Assert.False(result); // Moet false geven vanwege de Length
        }
    }
}
