namespace Kbs.Business.Medal
{
    public static class MedalMaterialExtensions
    {
        public static string ToDutchString(this MedalMaterial medalMaterial)
        {
            return medalMaterial switch
            {
                MedalMaterial.Bronze => "Brons",
                MedalMaterial.Silver => "Zilver",
                MedalMaterial.Gold => "Goud",
                _ => "Onbekend"
            };
        }
    }
}
