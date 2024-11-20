using System.Windows.Controls;

namespace Kbs.Wpf.BoatType.CreateBoatType;

public partial class CreateBoatTypePage : Page
{
    private CreateBoatTypeViewModel ViewModel => (CreateBoatTypeViewModel)DataContext;
    public CreateBoatTypePage()
    {
        InitializeComponent();
    }

    private void ExperienceChanged(object sender, SelectionChangedEventArgs e)
    {
        var experience = (CreateBoatExperienceViewModel)((ComboBox)sender).SelectedItem;
        ViewModel.RequiredExperience = experience.RequiredExperience;
    }
}