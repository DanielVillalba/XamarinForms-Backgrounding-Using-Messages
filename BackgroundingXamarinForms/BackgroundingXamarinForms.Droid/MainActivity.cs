using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Android.Content;
using BackgroundingXamarinForms.Droid.Services;

namespace BackgroundingXamarinForms.Droid
{
    [Activity(Label = "BackgroundingXamarinForms", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            WireUpLongRunningTask();
        }

        void WireUpLongRunningTask()
        {
            MessagingCenter.Subscribe<Messages.StartLongRunningTaskMessage>(this, "StartLongRunningTaskMessage", message =>
             {
                 var intent = new Intent(this, typeof(LongRunningTaskService));
                 StartService(intent);
             });

            MessagingCenter.Subscribe<Messages.StopLongRunningTaskMessage>(this, "StopLongRunningTaskMessage", message =>
             {
                 var intent = new Intent(this, typeof(LongRunningTaskService));
                 StopService(intent);
             });
        }
    }
}

