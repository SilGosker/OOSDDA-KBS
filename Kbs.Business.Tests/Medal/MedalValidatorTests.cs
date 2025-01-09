namespace Kbs.Business.Medal
{
    public class MedalValidatorTests
    {

        [Theory]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        public void ValidateForCreate_WithNullUserId_ReturnsError(int userId)
        {
            // Arrange
            var medal = new MedalEntity
            {
                UserId = userId,
                GameId = 1,
                Material = MedalMaterial.Gold

            };

            var validator = new MedalValidator();

            // Act
            var result = validator.ValidateForCreate(medal);

            // Assert
            Assert.Single(result);
            Assert.True(result.TryGetValue(nameof(medal.UserId), out string userIdError));
            Assert.Equal("UserId is verplicht", userIdError);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        public void ValidateForCreate_WithNullGameId_ReturnsError(int gameId)
        {
            // Arrange
            var medal = new MedalEntity
            {
                UserId = 1,
                GameId = gameId,
                Material = MedalMaterial.Gold

            };

            var validator = new MedalValidator();

            // Act
            var result = validator.ValidateForCreate(medal);

            // Assert
            Assert.Single(result);
            Assert.True(result.TryGetValue(nameof(medal.GameId), out string gameIdError));
            Assert.Equal("GameId is verplicht", gameIdError);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void ValidateForCreate_WithNegativeBoatId_ReturnsError(int boatId)
        {
            // Arrange
            var medal = new MedalEntity
            {
                UserId = 1,
                GameId = 1,
                Material = MedalMaterial.Gold,
                BoatId = boatId
            };

            var validator = new MedalValidator();

            // Act
            var result = validator.ValidateForCreate(medal);

            // Assert
            Assert.Single(result);
            Assert.True(result.TryGetValue(nameof(medal.BoatId), out string boatIdError));
            Assert.Equal("BoatId moet positief zijn", boatIdError);
        }

        [Fact]
        public void ValidateForCreate_WithBadMaterial_ReturnsError()
        {
            // Arrange
            var medal = new MedalEntity
            {
                UserId = 1,
                GameId = 1,
                Material = (MedalMaterial)12
                
            };

            var validator = new MedalValidator();

            // Act
            var result = validator.ValidateForCreate(medal);

            // Assert
            Assert.Single(result);
            Assert.True(result.TryGetValue(nameof(medal.Material), out string materialError));
            Assert.Equal("Materiaal is verplicht", materialError);
        }

        [Fact]
        public void ValidateForCreate_WithValidMedal_ReturnsEmptyDictionary()
        {
            // Arrange
            var medal = new MedalEntity
            {
                UserId = 1,
                GameId = 1,
                Material = MedalMaterial.Gold

            };

            var validator = new MedalValidator();

            // Act
            var result = validator.ValidateForCreate(medal);

            // Assert
            Assert.Empty(result);
        }

    }
}
