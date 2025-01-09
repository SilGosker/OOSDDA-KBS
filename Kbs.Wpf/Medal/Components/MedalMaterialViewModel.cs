using Kbs.Business.Medal;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Medal.Components
{
    public class MedalMaterialViewModel : ViewModel
    {
        public MedalMaterialViewModel(MedalMaterial medalMaterial)
        {
            this.MedalMaterial = medalMaterial;
        }

        public MedalMaterial MedalMaterial { get; }

        // Override ToString to display the MedalMaterials in Dutch
        public override string ToString()
        {
            return MedalMaterial.ToDutchString();
        }
    }
}

