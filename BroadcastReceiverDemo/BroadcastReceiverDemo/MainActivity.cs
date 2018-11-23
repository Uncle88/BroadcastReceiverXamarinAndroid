using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace BroadcastReceiverDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        SampleReceiver receiver;
        private Button button;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var _broadcastReceiver = new SampleReceiver();
            _broadcastReceiver.ConnectionStatusChanged += OnNetworkStatusChanged;
            Application.Context.RegisterReceiver(_broadcastReceiver,
            new IntentFilter(ConnectivityManager.ConnectivityAction));
        }

        private void OnNetworkStatusChanged(object sender, EventArgs e)
        {
            if (IsOnline())
            {
                Toast.MakeText(this, "Network Activated", ToastLength.Long).Show();
                CreateDialog();
            }
            else
            {
                Toast.MakeText(this, "Network NonActivated", ToastLength.Long).Show();
                CreateDialog();
            }
        }

        private bool IsOnline()
        {
            var cm = (ConnectivityManager)GetSystemService(ConnectivityService);
            return cm.ActiveNetworkInfo == null ? false : cm.ActiveNetworkInfo.IsConnected;
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(receiver, new IntentFilter("com.App1.SampleReceiver"));
        }

        protected override void OnPause()
        {
            if (receiver != null)
            {
                UnregisterReceiver(receiver);
            }
            base.OnPause();
        }

        private void CreateDialog()
        {
            var dialog = new Android.Support.V7.App.AlertDialog.Builder(this);
            dialog.SetView(Resource.Layout.CheckDialog);

            dialog.SetNeutralButton("Check", (sender, args) =>
            {
                StartActivity(new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings,
                Android.Net.Uri.Parse("package:" + Application.Context.PackageName)));
            });

            dialog.SetNegativeButton("Cancel", (sender, args) =>
            {
                dialog.SetCancelable(true);
            });
            dialog.Create();
            dialog.Show();
        }
    }
}