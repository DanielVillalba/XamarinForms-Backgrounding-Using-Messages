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
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BackgroundingXamarinForms.Droid.Services
{
    [Service]
    public class LongRunningTaskService : Service
    {
        CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    // invoke the shared code with the task that needs to be performed in the background
                    var counter = new TaskCounter();
                    counter.RunCounter(_cts.Token).Wait();

                }
                catch(Exception ex)
                {

                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        var message = new Messages.CancelledMessage();
                        Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(message, "CancelledMessage"));
                    }

                }
            });

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            if(_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}