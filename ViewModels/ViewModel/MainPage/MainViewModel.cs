using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WorkerModel.Order;

namespace ViewModels.ViewModel.MainPage
{
    public class MainViewModel: BaseViewModel
    {
        MainService service;
        public MainViewModel()
        {
            service = new MainService();
            ListWaiting = new ObservableCollection<OrderDetail>();
            ListDoing = new ObservableCollection<OrderDetail>();
        }
        private int _SelectedIndex = 1;
        public int SelectedIndex { get => _SelectedIndex;
            set
            {
                _SelectedIndex = value;
                OnPropertyChanged();
                if (value == 1)
                {
                    ListDetail = ListWaiting;
                    IsLoading = IsLoading1;
                }else
                {
                    ListDetail = ListDoing;
                    IsLoading = IsLoading2;
                }
                    
            }
        }

        private ObservableCollection<OrderDetail> _ListWaiting;
        public ObservableCollection<OrderDetail> ListWaiting { get => _ListWaiting; set { _ListWaiting = value; OnPropertyChanged(); } }

        private ObservableCollection<OrderDetail> _ListDoing;
        public ObservableCollection<OrderDetail> ListDoing { get => _ListDoing; set { _ListDoing = value; OnPropertyChanged(); } }

        private ObservableCollection<OrderDetail> _ListDetail;
        public ObservableCollection<OrderDetail> ListDetail { get => _ListDetail; set { _ListDetail = value; OnPropertyChanged(); } }

        private bool _IsLoading1;
        public bool IsLoading1 { get => _IsLoading1; set { _IsLoading1 = value; OnPropertyChanged(); } }

        private bool _IsLoading2;
        public bool IsLoading2 { get => _IsLoading2; set { _IsLoading2 = value; OnPropertyChanged(); } }

        private bool _IsLoading;
        public bool IsLoading { get => _IsLoading; set { _IsLoading = value; OnPropertyChanged(); } }


        public void InsertItem(OrderDetail detail)
        {
            if (detail.Status == "ĐANG CHỜ")
                ListWaiting.Add(detail);
            else
            {
                ListDoing.Add(detail);
            }
        }

        public async Task<bool> GetWaitingList()
        {
            IsLoading1 = true;
            ListWaiting = await service.GetListPicAndStatus(Global.GlobalInfo.UserLogged, "ĐANG CHỜ");
            IsLoading1 = false;
            return ListWaiting != null;
        }

        public async Task<bool> GetDoingList()
        {
            IsLoading2 = true;
            ListDoing = await service.GetListPicAndStatus(Global.GlobalInfo.UserLogged, "ĐANG THỰC HIỆN");
            IsLoading2 = false;
            return ListDoing != null;
        }
    }
}
