using System.Windows.Controls;
using Kbs.Business.Boat;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.User;
using Kbs.Wpf.Attributes;

namespace Kbs.Wpf.Boat.Details;

[HasRole(Role.MaterialCommissioner)]
public partial class BoatDetailPage : Page
{
    private readonly IBoatRepository _boatRepository = new BoatRepository();
    private readonly IUserRepository _userRepository = new UserRepository();
    public BoatDetailViewModel ViewModel => (BoatDetailViewModel)DataContext;
    public BoatDetailPage(int boatId)
    {
        InitializeComponent();
        var boat = _boatRepository.GetById(boatId);
        ViewModel.Name = boat.Name;
        ViewModel.Status = boat.Status.ToDutchString();


        ViewModel.BoatTypeName = "Onbekend";
    }
}