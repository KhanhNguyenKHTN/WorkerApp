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
                    IsWaiting = true;
                }else
                {
                    IsWaiting = false;
                }
                    
            }
        }
        private bool _IsWaiting = true;
        public bool IsWaiting { get => _IsWaiting; set { _IsWaiting = value; OnPropertyChanged(); OnPropertyChanged("IsDoing"); } }

        public bool IsDoing { get { return !_IsWaiting; } }

        private ObservableCollection<OrderDetail> _ListWaiting;
        public ObservableCollection<OrderDetail> ListWaiting { get => _ListWaiting; set { _ListWaiting = value; OnPropertyChanged(); } }

        private ObservableCollection<OrderDetail> _ListDoing;
        public ObservableCollection<OrderDetail> ListDoing { get => _ListDoing; set { _ListDoing = value; OnPropertyChanged(); } }

        private bool _IsLoading1;
        public bool IsLoading1 { get => _IsLoading1; set { _IsLoading1 = value; OnPropertyChanged(); } }

        private bool _IsLoading2;
        public bool IsLoading2 { get => _IsLoading2; set { _IsLoading2 = value; OnPropertyChanged(); } }

        public async Task<bool> LoadData()
        {
            await GetWaitingList();
            await GetDoingList();
            return true;
        }

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

        public async void ChangeStatusToDoing(OrderDetail e)
        {
            await service.ChangeStatus(e, "ĐANG THỰC HIỆN");


        }

        public async Task<bool> GetDoingList()
        {
            IsLoading2 = true;
            ListDoing = await service.GetListPicAndStatus(Global.GlobalInfo.UserLogged, "ĐANG THỰC HIỆN");
            IsLoading2 = false;
            return ListDoing != null;
        }

        public async void StartDish(OrderDetail e)
        {
            // throw new NotImplementedException();
            await service.ChangeStatus(e, "ĐANG THỰC HIỆN");
        }

        public async void CompleteDish(OrderDetail e)
        {
            await service.ChangeStatus(e, "HOÀN TẤT");
            //throw new NotImplementedException();
        }
    }
}
