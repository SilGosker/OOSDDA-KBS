using Kbs.Business.Parcours;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Parcours.Read.Index;

public class ReadIndexParcoursParcoursViewModel : ViewModel
{
    private int _parcourId;
    private string _name;
    private string _difficulty;

    public ReadIndexParcoursParcoursViewModel(ParcoursEntity parcours)
    {
        ParcourId = parcours.ParcoursId;
        Name = parcours.Name;
        Difficulty = parcours.Difficulty.ToDutchString();
    }

    public int ParcourId
    {
        get => _parcourId;
        set => SetField(ref _parcourId, value);
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Difficulty
    {
        get => _difficulty;
        set => SetField(ref _difficulty, value);
    }
}