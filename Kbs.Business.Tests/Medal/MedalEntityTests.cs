namespace Kbs.Business.Medal
{
    public class MedalEntityTests
    {
        [Theory]
        [InlineData(1, 1, 1, 1, MedalMaterial.Silver)]
        [InlineData(-1, null, int.MinValue, 0, MedalMaterial.Bronze)]
        [InlineData(int.MaxValue, -1, -1, int.MinValue, (MedalMaterial)0)]
        public void Properties_ShouldSetValues(int id, int? boatId, int userId, int gameId, MedalMaterial medalMaterial)
        {
            // Arrange
            var medal = new MedalEntity();

            // Act
            medal.MedalId = id;
            medal.BoatId = boatId;
            medal.UserId = userId;
            medal.GameId = gameId;
            medal.Material = medalMaterial;

            // Assert
            Assert.Equal(id, medal.MedalId);
            Assert.Equal(boatId, medal.BoatId);
            Assert.Equal(userId, medal.UserId);
            Assert.Equal(gameId, medal.GameId);
            Assert.Equal(medalMaterial, medal.Material);
        }
    }
}
