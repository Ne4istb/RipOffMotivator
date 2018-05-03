using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;


namespace RipOffMotivator.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MainTheme", LaunchMode = LaunchMode.SingleTop)]
	[IntentFilter(new[] { Intent.ActionMain, "android.nfc.action.TAG_DISCOVERED" })]
    [IntentFilter(new[] { "android.nfc.action.NDEF_DISCOVERED" },
        DataMimeType = ViewApeMimeType,
        Categories = new[] { "android.intent.category.DEFAULT" })]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
        const string ViewApeMimeType = "application/vnd.xamarin.nfcxample";

        internal static MainActivity Instance { get; private set; }

        static NFCIntegration nfc;
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			Instance = this;
			Xamarin.Forms.Forms.Init(this, bundle);

			Xamarin.Forms.DependencyService.Register<INFCIntegration, NFCIntegration>();

            nfc = ((NFCIntegration)Xamarin.Forms.DependencyService.Get<INFCIntegration>());

            LoadApplication(new App());
		}

        protected override void OnStart()
        {
            base.OnStart();
            if (nfc.NfcAdapter == null)
            { 
                var alert = new AlertDialog.Builder(this).Create();
                alert.SetMessage("NFC is not supported on this device.");
                alert.SetTitle("NFC Unavailable");
                alert.Show();
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            var intentAction = intent.Action ?? String.Empty;
            var intentType = intent.Type ?? String.Empty;
            if (nfc.InWriteMode)
            {
                nfc.AdjustNFCTag(intent);
                return;
            }

          if (!intentAction.Equals("android.nfc.action.TAG_DISCOVERED") && !intentType.Equals(ViewApeMimeType))
				return;

			var tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
			var tagId = BitConverter.ToString(tag.GetId()).Replace("-", "");

			GoalResolved(tagId);
		}

        protected override void OnResume()
        {
            base.OnResume();
            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };

            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            if (nfc.NfcAdapter == null)
                return;

            nfc.NfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
        }

        public static void EnableWriteMode() {
            nfc.InWriteMode = true;
        }

		void GoalResolved(string tagId)
		{
			((App)Xamarin.Forms.Application.Current).ResolveGoal(tagId);
		}
	}
}

