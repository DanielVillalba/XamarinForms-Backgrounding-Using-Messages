using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BackgroundingXamarinForms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Wire up the buttons

            startButtton.Clicked += (s, e) =>
            {
                var message = new Messages.StartLongRunningTaskMessage();
                MessagingCenter.Send(message, "StartLongRunningTaskMessage");
            };

            stopButton.Clicked += (s, e) =>
            {
                var message = new Messages.StopLongRunningTaskMessage();
                MessagingCenter.Send(message, "StopLongRunningTaskMessage");
            };

            HandleReceivedMessages();
        }

        private void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<Messages.TickedMessage>(this, "TickedMessage", message =>
           {
               Device.BeginInvokeOnMainThread(() =>
               {
                   ticker.Text = message.Message;
               });
           });

            MessagingCenter.Subscribe<Messages.CancelledMessage>(this, "CancelledMessage", message =>
             {
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     ticker.Text = "Cancelled";
                 });
             });
        }
    }
}
