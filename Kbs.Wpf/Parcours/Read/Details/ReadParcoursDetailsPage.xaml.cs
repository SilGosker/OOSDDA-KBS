using System.Windows.Controls;
using Kbs.Business.Parcours;
using Kbs.Data.Parcours;

namespace Kbs.Wpf.Parcours.Read.Details;

public partial class ReadParcoursDetailsPage : Page
{
    private readonly ParcoursRepository _parcoursRepository = new();
    private ReadParcoursDetailsViewModel ViewModel => (ReadParcoursDetailsViewModel)DataContext;
    public ReadParcoursDetailsPage(int id)
    {
        InitializeComponent();
        var parcours = _parcoursRepository.GetById(id);

        ViewModel.Name = parcours.Name;
        ViewModel.Description = parcours.Description;
        ViewModel.Image = parcours.Image;
        ViewModel.Difficulty = parcours.Difficulty.ToDutchString();
        ViewModel.Id = parcours.ParcoursId;
    }
}