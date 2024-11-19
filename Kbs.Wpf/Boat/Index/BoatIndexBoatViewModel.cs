using Kbs.Business.Boat;
using Kbs.Business.Helpers;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Boat.Index;

public class BoatIndexBoatViewModel : ViewModel
{
    public BoatIndexBoatViewModel(BoatEntity boat, BoatIndexBoatTypeViewModel boatType)
    {
        ThrowHelper.ThrowIfNull(boat);
        ThrowHelper.ThrowIfNull(boatType);

        Name = boat.Name;
        BoatId = boat.BoatID;
        Status = boat.Status switch
        {
            BoatStatus.Operational => "Operationeel",
            BoatStatus.Maintaining => "In onderhoud",
            BoatStatus.Broken => "Kapot",
            _ => "Onbekend"
        };

        BoatType = boatType?.Name ?? "Onbekend";
    }

    private string _name;
    private string _status;
    private string _boatType;
    private int _boatId;
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
    public string BoatNumber => "Boot nr. " + BoatId;
    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }
    public string BoatType
    {
        get => _boatType;
        set => SetField(ref _boatType, value);
    }
}