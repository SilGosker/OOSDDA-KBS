using Kbs.Business.Parcours;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Parcours.Read.Index;

public class ReadIndexParcoursParcoursViewModel : ViewModel
{
    private int _id;
    private string _name;
    private string _difficulty;

    public ReadIndexParcoursParcoursViewModel(ParcoursEntity parcours)
    {
        Id = parcours.ParcoursId;
        Name = parcours.Name;
        Difficulty = parcours.Difficulty.ToDutchString();
    }

    public int Id
    {
        get => _id;
        set => SetField(ref _id, value);
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