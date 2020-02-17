using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModel.MainPage;
using WorkerModel.Menu;
using Xamarin.Forms;

namespace WorkerApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainViewModel viewmodel;
        Services.RabbitConnect connect = new Services.RabbitConnect();
        public MainPage()
        {
            InitializeComponent();
            viewmodel = new MainViewModel();
            BindingContext = viewmodel;
            loadDate();
            ConnectData();
            
            //testloadContent();
            //gridTest.BindingContext = new Food() { LabName = "Tôm csssssssssshiên", Likes = 123, ImagesSource = @"https://www.w3schools.com/w3css/img_lights.jpg", RealCost = 35000, Unit = "đ/cái" };
        }

        private async void ConnectData()
        {
            await Task.Delay(1000);
            Device.BeginInvokeOnMainThread(() =>
            {
                connect.ReceiveNotifyRabbitMQ();
            });
        }

        public async void testloadContent()
        {
            await Task.Delay(2000);

            
            BackgroundWorker wk = new BackgroundWorker();
            wk.DoWork += async (s, z) =>
            {
                int i = 1;
                while (true)
                {
                    //if (i % 3 == 0)
                    //{
                    //    Device.BeginInvokeOnMainThread(() => {
                    //        stkTest.Children.RemoveAt(2);//Insert(0, new Label() { Text = "add " + i.ToString() });
                    //    });

                    //}
                    //else
                    //{
                        
                    //}
                    Device.BeginInvokeOnMainThread(() => {
                        //stkTest.Children.Insert(0, new Label() { Text = "add " + i.ToString(), WidthRequest=50 });
                    });

                    await Task.Delay(500);
                    i++;
                    Console.WriteLine("add");
                    //stkTest.
                }
            };
            wk.RunWorkerAsync();
        }
        ObservableCollection<Food> ListFoods;
        ObservableCollection<Food> lsTemp;

        void loadDate()
        {
            ListFoods = new ObservableCollection<Food>()
            {
                new Food() { LabName="Gỏi cuốn", Likes = 21,ImagesSource=@"https://www.w3schools.com/w3css/img_lights.jpg", RealCost=5000, Unit = "đ/dĩa",
                            Description ="Không hành tỏi" },

                new Food() {  LabName= "Cơm chiên" , ImagesSource="bannerFood2.jpg", RealCost=10000, Unit = "đ/dĩa"
                             , Likes = 123, Description ="Không ớt" },

                new Food() {  LabName= "Rau xào", Likes = 123,ImagesSource="food1.jpg",  RealCost=15000, Unit = "đ/dĩa",
                             Description ="aa" },


                new Food() {  LabName= "Tôm chiên" , Likes = 123,ImagesSource="food1.jpg", RealCost=35000, Unit = "đ/cái" ,
                    Description="Từ ngày 20/10 - 20/11 khuyến mãi đặc biệt dành cho khách hàng thuê xe ô tô 4 chỗ trở lên" },

                new Food() { LabName="Lẩu thái", Likes = 21,ImagesSource="bannerFood1.jpg",  RealCost=150000, Unit = "đ/cái",
                            Description ="sđa" },

             
                new Food() {  LabName="Gỏi đu đủ", Likes = 21,ImagesSource="bannerFood1.jpg", RealCost=45000, Unit = "đ/cái",
                            Description ="sư" },

                new Food() {  LabName= "Lẩu cá lóc" , ImagesSource="bannerFood2.jpg",  RealCost=215000, Unit = "đ/cái", Quantity = 2
                             , Likes = 123, Description ="Cay" },

                new Food() {  LabName= "Heo quay", Likes = 123,ImagesSource="food1.jpg",  RealCost=500000, Unit = "đ/con", Description="Nóng" },

                new Food() { LabName= "Vịt quay" , Likes = 123,ImagesSource="food1.jpg", RealCost=350000, Unit = "đ/con" ,Description="Từ ngày 20/10 - 20/11 khuyến mãi đặc biệt dành cho khách hàng thuê xe ô tô 4 chỗ trở lên" },

            };

            lsTemp = new ObservableCollection<Food>()
            {
                   new Food() { LabName= "Lẩu hải sản" , ImagesSource=@"https://dining.wsu.edu/media/1207/southside-100x100.jpg", RealCost=175000, Unit = "đ/cái"
                             , Likes = 123, Description ="Không cay" },

                new Food() {  LabName= "Gỏi tôm yump", Likes = 123,ImagesSource="food1.jpg",  RealCost=55000, Unit = "đ/dĩa", 
                    Description="None" },

                new Food() {  LabName= "Bún thêm" , Likes = 123,ImagesSource="food1.jpg",  RealCost=5000, Unit = "đ/dĩa" ,Description="Ko" },

            };
            //lsDoing.ItemsSource = ListFoods;
            lsFoods.ItemsSource = ListFoods;
            //lsComplete.ItemsSource = ListFoods;
            //lsChef.ItemsSource = ListFoods;
        }

        private void waitingTab(object sender, EventArgs e)
        {
            viewmodel.SelectedIndex = 1;
            lsFoods.ItemsSource = ListFoods;
        }

        private void doingTab(object sender, EventArgs e)
        {
            viewmodel.SelectedIndex = 2;
            lsFoods.ItemsSource = lsTemp;
        }

        private async void ItemTap(object sender, EventArgs e)
        {
            var label = sender as Label;
            await DisplayAlert("Ghi chú", label.Text, "Ok");
        }

        private async void btnDoneClicked(object sender, EventArgs e)
        {
            if(viewmodel.SelectedIndex == 1)
            {

            }
            else
            {
               var res = await DisplayAlert("Xác nhận", "Bạn có muốn xác nhận hoàn thành món này?" , "Ok", "Hủy");
               
            }
        }

        //private void btnNextClick(object sender, EventArgs e)
        //{
        //    regionPicker.IsVisible = true;
        //}

        //private void tapClosePicker(object sender, EventArgs e)
        //{
        //    regionPicker.IsVisible = false;
        //}
    }
}
