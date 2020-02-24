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
           
            MessagingCenter.Subscribe<RabbitConnect, OrderDetail>(this, "AddDetailQuere", (s, e) => {
                ListNotifi.Add(e);
                DisplayNotifyCation();
            });
            ConnectData();
            audio = DependencyService.Get<IAudioNoti>();
            BindingContext = viewModel;
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
                if(e.Status == "ĐANG THỰC HIỆN")
                {
                    string mess = "Đầu bếp " + e.Pic.UserInfo.DisplayName + " bắt đầu làm món " + e.Dish.LabName;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isShowingAlert = true;
                        audio.playAudio();
                        viewModel.ListWaiting.Remove(viewModel.ListWaiting.FirstOrDefault(x => x.OrderDetailId == e.OrderDetailId));
                        viewModel.ListDoing.Add(e);
                        //viewModel.ChangeStatusToDoing();

                        Notify.Text = mess;

                    });
                    await Task.Delay(3000);
                }
                
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
            Device.BeginInvokeOnMainThread(async () =>
            {
                await viewModel.LoadData();
                
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

        private async void lsFoods_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (viewModel.SelectedIndex == 1)
            {
                var res = await DisplayAlert("Xác nhận", "Bạn có muốn bắt đầu món này?", "Ok", "Hủy");
                if(res == true)
                {
                    viewModel.StartDish(lsWaiting.SelectedItem as OrderDetail);
                }

            }
            else
            {
                var res = await DisplayAlert("Xác nhận", "Bạn có muốn xác nhận hoàn thành món này?", "Ok", "Hủy");
                if (res == true)
                {
                    viewModel.CompleteDish(lsDoning.SelectedItem as OrderDetail);
                }
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var sen = sender as MenuItem;
            var a = sen.BindingContext as OrderDetail;
            viewModel.ChangeStatusToDoing(a);
            viewModel.ListWaiting.Remove(a);
            viewModel.ListDoing.Add(a);
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var sen = sender as MenuItem;
            var a = sen.BindingContext as OrderDetail;
            viewModel.CompleteDish(a);
            viewModel.ListDoing.Remove(a);
        }
    }
}
