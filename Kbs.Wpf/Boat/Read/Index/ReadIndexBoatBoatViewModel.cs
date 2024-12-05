using System.Windows.Media;
using Kbs.Business.Boat;
using Kbs.Business.Helpers;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Read.Index;

public class ReadIndexBoatBoatViewModel : ViewModel
{
    public ReadIndexBoatBoatViewModel(BoatEntity boat, ReadIndexBoatBoatTypeViewModel boatType, bool hasDamage)
    {
        ThrowHelper.ThrowIfNull(boat);
        ThrowHelper.ThrowIfNull(boatType);

        Name = boat.Name;
        BoatId = boat.BoatId;
        Status = boat.Status.ToDutchString();
        BoatType = boatType?.Name ?? "Onbekend";
        if (hasDamage)
        {
            DamageMessage = "Boot heeft schade";
        }
    }

    private string _name;
    private string _status;
    private string _boatType;
    private int _boatId;
    private string _damageMessage;
    public int BoatId
    {
        get => _boatId;
        set => SetField(ref _boatId, value);
    }
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    public string BoatIdString => "Boot nr. " + BoatId;
    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }
    public Brush StatusColor
    {
        get
        {
            if (Status == BoatStatus.Operational.ToDutchString())
            {
                return Brushes.Green;
            }
            return Brushes.Red;
        }
    }
    public string BoatType
    {
        get => _boatType;
        set => SetField(ref _boatType, value);
    }
    public string DamageMessage
    {
        get => _damageMessage;
        set => SetField(ref _damageMessage, value);
    }
}