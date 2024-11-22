﻿using Kbs.Business.BoatType;
using Kbs.Wpf.Components;

namespace Kbs.Wpf.BoatType.ViewBoatTypes
{
    public class ViewBoatTypeBoatTypeViewModel : ViewModel
    {
        private string _name;
        private int _id;
        public ViewBoatTypeBoatTypeViewModel(BoatTypeEntity boatType)
        {
            this.Name = boatType.Name;
            this.Id = boatType.BoattypeID;
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }
        private int _boatTypeID;
        public int BoatTypeID
        {
            get => _boatTypeID;
            set => SetField(ref _boatTypeID, value);
        }

    }
}
