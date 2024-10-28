﻿using Reservoom.Commands;
using Reservoom.Models;
using Reservoom.Stores;
using System.Windows.Input;

namespace Reservoom.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
		private string _username;
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
				OnPropertyChanged(nameof(Username));
			}
		}

		private int _roomNumber;
		public int RoomNumber
		{
			get
			{
				return _roomNumber;
			}
			set
			{
				_roomNumber = value;
				OnPropertyChanged(nameof(RoomNumber));
			}
		}

		private int _floorNumber;
		public int FloorNumber
		{
			get
			{
				return _floorNumber;
			}
			set
			{
				_floorNumber = value;
				OnPropertyChanged(nameof(FloorNumber));
			}
		}

		private DateTime _startDate = new DateTime(2024, 10, 28);
		public DateTime StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
				OnPropertyChanged(nameof(StartDate));
			}
		}

		private DateTime _endDate = new DateTime(2024, 10, 28);
        public DateTime EndDate
		{
			get
			{
				return _endDate;
			}
			set
			{
				_endDate = value;
				OnPropertyChanged(nameof(EndDate));
			}
		}

		public ICommand SubmitCommand { get; }
		public ICommand CancelCommand { get; }

        public MakeReservationViewModel(HotelStore hotelStore, Services.NavigationService reservationViewNavigationService)
        {
			SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
			CancelCommand = new NavigateCommand(reservationViewNavigationService);
        }
    }
}
