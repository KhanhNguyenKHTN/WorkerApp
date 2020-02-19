using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WorkerApp.Controls;

namespace WorkerApp.Droid
{
    public class PlayAudio : IAudioNoti
    {
        public void playAudio()
        {
            GlobalClassAndroid.Player.Start();
        }
    }
}