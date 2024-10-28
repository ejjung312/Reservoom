using Reservoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Stores
{
    public class NavigationStore
    {
        public ViewModelBase _currnetViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currnetViewModel;
            set
            {
                _currnetViewModel = value;
                OnCurrnetViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrnetViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
