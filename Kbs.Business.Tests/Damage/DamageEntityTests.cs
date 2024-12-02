namespace Kbs.Business.Damage;

public class DamageEntityTests
{
    [Theory]
    [InlineData(1, 1, DamageStatus.Solved, "Test Description", new byte[] { 0x20, 0x21, 0x22 })]
    [InlineData(0, 0, DamageStatus.UnSolved, "", new byte[] { })]
    [InlineData(int.MinValue, int.MinValue, (DamageStatus)0, null, null)]
    public void Properties_ShouldSetValues(int id, int boatId, DamageStatus status, string description, byte[] image)
    {
        // Arrange
        var damage = new DamageEntity();
        
        // Act
        damage.DamageId = id;
        damage.BoatId = boatId;
        damage.Status = status;
        damage.Description = description;
        damage.Image = image;
        
        // Assert
        Assert.Equal(id, damage.DamageId);
        Assert.Equal(boatId, damage.BoatId);
        Assert.Equal(status, damage.Status);
        Assert.Equal(description, damage.Description);
        Assert.Equal(image, damage.Image);
    }
}