using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System.Windows;

namespace Reservoom.Commands
{
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly HotelStore _hotelStore;
        private readonly NavigationService _reservationViewNavigationService;
        private MakeReservationViewModel _makeReservationViewModel;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, HotelStore hotelStore, NavigationService reservationViewNavigationService)
        {
            this._makeReservationViewModel = makeReservationViewModel;
            this._hotelStore = hotelStore;
            this._reservationViewNavigationService = reservationViewNavigationService;

            _makeReservationViewModel.PropertyChanged += _makeReservationViewModel_PropertyChanged;
        }

        private void _makeReservationViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeReservationViewModel.Username) || e.PropertyName == nameof(MakeReservationViewModel.FloorNumber))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
                _makeReservationViewModel.FloorNumber > 0 
                && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.Username,
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate);

            try
            {
                await _hotelStore.MakeReservation(reservation);

                MessageBox.Show("Successfully reserved room.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                _reservationViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to make reservation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
