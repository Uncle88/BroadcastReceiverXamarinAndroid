using Android.App;
using Android.Content;
using System;

namespace BroadcastReceiverDemo
{
    [BroadcastReceiver(Name = "com.App1.SampleReceiver", Enabled = true, Exported = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class SampleReceiver : BroadcastReceiver
    {
        public event EventHandler ConnectionStatusChanged;

        public override void OnReceive(Context context, Intent intent)
        {
            ConnectionStatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}