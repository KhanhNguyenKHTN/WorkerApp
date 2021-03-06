﻿using System;
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
                else if(e.Status == "ĐANG CHỜ")
                {
                    string mess = "Bạn không cần làm món " + e.Dish.LabName + " nữa!";
                   
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isShowingAlert = true;
                        audio.playAudio();
                        viewModel.ListWaiting.Remove(check);
                        Notify.Text = mess;
                        Notify.TextColor = Color.OrangeRed;
                    });
                    await Task.Delay(3000);
                }
                
            }
            ListNotifi.Remove(e);
           
            Device.BeginInvokeOnMainThread(() =>
            {
                isShowingAlert = false;
                string original = "Đang chờ: " + viewModel.ListWaiting?.Count + " món";
                Notify.Text = original;
                Notify.TextColor = Color.White;
            });

            if (ListNotifi.Count != 0)
            {
                ShowMessage();
            }
        }

        private async void ConnectData()
        {
            
            await Task.Delay(500);
            Device.BeginInvokeOnMainThread(async () =>
            {
                await viewModel.LoadData();
                string original = "Đang chờ: " + viewModel.ListWaiting?.Count + " món";
                Notify.Text = original;
                connect.ReceiveNotifyRabbitMQ();
                gridWaiting.IsVisible = false;
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
                    MenuItem_Clicked(e.Item, e);
                   // viewModel.StartDish(lsWaiting.SelectedItem as OrderDetail);
                }

            }
            else
            {
                var res = await DisplayAlert("Xác nhận", "Bạn có muốn xác nhận hoàn thành món này?", "Ok", "Hủy");
                if (res == true)
                {
                    MenuItem_Clicked_1(e.Item, e);
                }
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var a = sender as OrderDetail;
            viewModel.ChangeStatusToDoing(a);
            viewModel.ListWaiting.Remove(a);
            viewModel.ListDoing.Add(a);
            Device.BeginInvokeOnMainThread(async () =>
            {
                string original = "Đang chờ: " + viewModel.ListWaiting?.Count + " món";
                Notify.Text = original;
            });
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var a = sender as OrderDetail;
            viewModel.CompleteDish(a);
            viewModel.ListDoing.Remove(a);
        }
    }
}
