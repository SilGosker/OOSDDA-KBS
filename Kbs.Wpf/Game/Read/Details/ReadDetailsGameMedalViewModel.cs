using Kbs.Business.Medal;
using Kbs.Business.User;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Game.Read.Details;

public class ReadDetailsGameMedalViewModel : ViewModel
{
    private int _userId;
    private int _medalId;
    private string _userName;
    private string _material;

    public ReadDetailsGameMedalViewModel(UserEntity user, MedalEntity medal)
    {
        UserId = user.UserId;
        UserName = user.Name;
        MedalId = medal.MedalId;
        Material = medal.Material.ToDutchString();
    }
    public int UserId
    {
        get => _userId;
        set => SetField(ref _userId, value);
    }
    public int MedalId
    {
        get => _medalId;
        set => SetField(ref _medalId, value);
    }
    public string UserName
    {
        get => _userName;
        set => SetField(ref _userName, value);
    }
    public string Material
    {
        get => _material;
        set => SetField(ref _material, value);
    }
}