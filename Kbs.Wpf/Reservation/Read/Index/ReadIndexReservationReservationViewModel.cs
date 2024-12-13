using System;
﻿using System.Windows.Media;
using Kbs.Business.Extentions;
using Kbs.Business.Helpers;
using Kbs.Business.Reservation;
using Kbs.Data.Reservation;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.Reservation.Read.Index;

public class ReadIndexReservationReservationViewModel : ViewModel
{
    private DateTime _startTime;
    private TimeSpan _length;

    
    public ReadIndexReservationReservationViewModel(ReservationEntity reservation)
    {
        ThrowHelper.ThrowIfNull(reservation);
        
        StartTime = reservation.StartTime;
        Length = reservation.Length;
        ReservationId = reservation.ReservationId;
        Status = reservation.Status;
    }

    private int _reservationId;
    private ReservationStatus _status;
    
    public ReservationStatus Status
    {
        get => _status;
        set
        {
            SetField(ref _status, value);
            OnPropertyChanged(nameof(StatusColor));
        }
    }
    public Brush StatusColor
    {
        get
        {
            if (Status == ReservationStatus.Active)
            {
                return Brushes.Green;
            }
            return Brushes.Red;
        }
    }

    public DateTime StartTime
    {
        get => _startTime;
        set => SetField(ref _startTime, value);
    }
    
    public string ReservationIdString => $"Reservering #{ReservationId}";
    public string StartTimeString => StartTime.ToDutchString(true);
    public string DurationString => $"{Length.TotalMinutes:F0} min";

    public TimeSpan Length
    {
        get => _length;
        set => SetField(ref _length, value);
    }
    public int ReservationId
    {
        get => _reservationId;
        set => SetField(ref _reservationId, value);
    }
    
}