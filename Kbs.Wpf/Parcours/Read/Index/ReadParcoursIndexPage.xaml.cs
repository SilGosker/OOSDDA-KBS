using System.Windows.Controls;
using System.Windows.Input;
using Kbs.Data.Parcours;

namespace Kbs.Wpf.Parcours.Read.Index;

public partial class ReadParcoursIndexPage : Page
{
    private readonly INavigationManager _navigationManager;
    private readonly ParcoursRepository _parcoursRepository = new();
    private ReadParcoursIndexViewModel ViewModel => (ReadParcoursIndexViewModel)DataContext;
    public ReadParcoursIndexPage(INavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InitializeComponent();

        foreach (var parcours in _parcoursRepository.GetAll())
        {
            ViewModel.Items.Add(new ReadIndexParcoursParcoursViewModel(parcours));
        }
    }

    private void ParcourClicked(object sender, MouseButtonEventArgs e)
    {
        
    }
}