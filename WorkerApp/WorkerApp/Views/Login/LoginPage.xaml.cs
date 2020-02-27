using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerModel.Order;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkerApp.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            btnLogin.Clicked += BtnLogin_Clicked;
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            gridWaiting.IsVisible = true;
            if(txbUserName.Text == "cook01")
            {
                Global.GlobalInfo.UserLogged = new WorkerModel.Order.Pic() {
                    EmployeeId = 10,
                    UserInfo = new UserInfo()
                    {
                        DisplayName = "Đầu bếp 01",
                        UserId = 15,
                        
                    },
                    Description = "cook01"
                };
            }else if(txbUserName.Text == "cook02")
            {
                Global.GlobalInfo.UserLogged = new WorkerModel.Order.Pic()
                {
                    EmployeeId = 11,
                    UserInfo = new UserInfo()
                    {
                        DisplayName = "Đầu bếp 02",
                        UserId = 16,

                    },
                    Description = "cook02"
                };
            } else
            {
                Global.GlobalInfo.UserLogged = new WorkerModel.Order.Pic()
                {
                    EmployeeId = 12,
                    UserInfo = new UserInfo()
                    {
                        DisplayName = "Đầu bếp 03",
                        UserId = 17,

                    },
                    Description = "cook03"
                };
            }
            await Navigation.PushModalAsync(new MainPage());
            gridWaiting.IsVisible = false;
        }

        private async void BtnForgotPass_Clicked(object sender, EventArgs e)
        {
            gridWaiting.IsVisible = true;
            await Navigation.PushModalAsync(new ForgetPassword());
            gridWaiting.IsVisible = false;
        }
    }
}