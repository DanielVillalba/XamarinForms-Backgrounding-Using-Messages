using BackgroundingXamarinForms.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BackgroundingXamarinForms
{
    public class TaskCounter
    {
        // This should be the task the we need to run when the app is backgrounded
        public async Task RunCounter (CancellationToken token)
        {
            await Task.Run(async () =>
            {
                for (long i = 0; i < long.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();

                    await Task.Delay(250);
                    var message = new TickedMessage
                    {
                        Message = i.ToString()
                    };

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
                    });

                }
            }, token);
        }
    }
}
