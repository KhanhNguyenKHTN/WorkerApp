using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModel.MainPage;
using WorkerApp.Controls;
using WorkerApp.Services;
using WorkerModel.Menu;
using WorkerModel.Order;
using Xamarin.Forms;

namespace WorkerApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainViewModel viewModel;
        Services.RabbitConnect connect = new Services.RabbitConnect();
        List<OrderDetail> ListNotifi = new List<OrderDetail>();
        bool isShowingAlert = false;
        IAudioNoti audio;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            BindingContext = viewModel;
            MessagingCenter.Subscribe<RabbitConnect, OrderDetail>(this, "AddDetailQuere", (s, e) => {
                ListNotifi.Add(e);
                DisplayNotifyCation();
            });
            ConnectData();
        }

        private void DisplayNotifyCation()
        {
            if (!isShowingAlert)
            {
                ShowMessage();
            }
        }

        private async void ShowMessage()
        {
            string original = "Đang chờ: " + viewModel.ListWaiting?.Count + " món";
            var e = ListNotifi.First();
            var check = viewModel.ListWaiting.FirstOrDefault(x => x.OrderDetailId == e.OrderDetailId);
            if (check == null)
            {
                string mess = "Đã thêm: " + e.Quantity + " món (" + e.Dish.LabName + ") vào danh sách chờ";

                Device.BeginInvokeOnMainThread(() =>
                {
                    isShowingAlert = true;
                    audio.playAudio();
                    viewModel.InsertItem(e);
                    Notify.Text = mess;

                });
                await Task.Delay(3000);


            }
            else
            {
                string mess = "Đã cập nhật món " + e.Dish.LabName + " lên " + e.Quantity;

                Device.BeginInvokeOnMainThread(() =>
                {
                    isShowingAlert = true;
                    audio.playAudio();
                    check.Quantity = e.Quantity;
                    Notify.Text = mess;

                });
                await Task.Delay(3000);
            }
            ListNotifi.Remove(e);
            Device.BeginInvokeOnMainThread(() =>
            {
                isShowingAlert = false;
                Notify.Text = original;
            });

            if (ListNotifi.Count != 0)
            {
                ShowMessage();
            }
        }

        private async void ConnectData()
        {
            await Task.Delay(1000);
            Device.BeginInvokeOnMainThread(() =>
            {
                connect.ReceiveNotifyRabbitMQ();
            });
        }



        private void waitingTab(object sender, EventArgs e)
        {
            viewModel.SelectedIndex = 1;
            
        }

        private void doingTab(object sender, EventArgs e)
        {
            viewModel.SelectedIndex = 2;
            
        }

        private async void ItemTap(object sender, EventArgs e)
        {
            var label = sender as Label;
            await DisplayAlert("Ghi chú", label.Text, "Ok");
        }

        private async void btnDoneClicked(object sender, EventArgs e)
        {
            if(viewModel.SelectedIndex == 1)
            {

            }
            else
            {
               var res = await DisplayAlert("Xác nhận", "Bạn có muốn xác nhận hoàn thành món này?" , "Ok", "Hủy");
               
            }
        }
        
    }
}
