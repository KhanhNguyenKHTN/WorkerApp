using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.ViewModel.MainPage
{
    public class MainViewModel: BaseViewModel
    {
        private int _SelectedIndex = 1;
        public int SelectedIndex { get => _SelectedIndex; set { _SelectedIndex = value; OnPropertyChanged(); } }



    }
}
