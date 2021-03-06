﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkerApp.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForgetPassword : ContentPage
	{
		public ForgetPassword ()
		{
			InitializeComponent ();
		}

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            Global.GlobalInfo.URL = @"http://" + txbMailOrPhone.Text + @":8081/";
            await Navigation.PopModalAsync();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}