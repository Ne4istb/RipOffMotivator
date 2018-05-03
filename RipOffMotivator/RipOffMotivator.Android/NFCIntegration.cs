using System;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;

namespace RipOffMotivator.Droid
{
    public class NFCIntegration : Activity, INFCIntegration
    {
        const string ViewApeMimeType = "application/vnd.xamarin.nfcxample";

        NfcAdapter NfcAdapter;
        Context Context;
        Boolean InWriteMode = false;
        string Message = string.Empty;
        Action<Guid, string> Action;

        public NFCIntegration()
        {
            Context = Application.Context;
            NfcAdapter = NfcAdapter.GetDefaultAdapter(Context);
        }

		public void SetContext(Context ctx)
		{
			Context = ctx;
		}

		void EnableWriteMode()
        {
            InWriteMode = true;

            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };

            var intent = new Intent(Context, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(Context, 0, intent, 0);
           
            if (NfcAdapter != null)
            {
                NfcAdapter.EnableForegroundDispatch(getActivity(Context), pendingIntent, filters, null);
            }
            else
            {
                var alert = new AlertDialog.Builder(Context).Create();
                alert.SetMessage("NFC is not supported on this device.");
                alert.SetTitle("NFC Unavailable");
                alert.Show();
            }
        }

        public Activity getActivity(Context context)
        {
            if (context == null)
                return null;
            if (context is ContextWrapper)
            {
                if (context is Activity)
                    return (Activity)context;

                return getActivity(((ContextWrapper)context).BaseContext);
            }

            return null;
        }

        protected override void OnNewIntent(Intent intent)
        {
            var intentType = intent.Type ?? String.Empty;

            if (InWriteMode)
            {
                AdjustNFCTag(intent);
                return;
            }

            if (!ViewApeMimeType.Equals(intentType, StringComparison.OrdinalIgnoreCase))
                return;

            var rawMessages = intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);
            var record = (((NdefMessage)rawMessages[0]).GetRecords())[0];

            intent.PutExtra("Id", record.GetId());
            StartService(intent);
        }

        void AdjustNFCTag(Intent intent)
        {
            InWriteMode = false;
            var tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;

            if (tag == null)
                return;

            if (!TryUpdateTag(tag))
            {
                var alert = new AlertDialog.Builder(Context).Create();
                alert.SetMessage("Something went wrong when updating NFC tag");
                alert.SetTitle("NFC Tag update fail");
                alert.Show();
            }
        }

        bool TryUpdateTag(Tag tag)
        {
            var ndef = Ndef.Get(tag);

            if (ndef == null || !ndef.IsWritable)
                return false;

            var mimeBytes = Convert.FromBase64String(ViewApeMimeType);
            var id = tag.GetId();
            var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, id, Convert.FromBase64String(Message));
            var ndefMessage = new NdefMessage(new[] { apeRecord });

            ndef.Connect();

            if (ndef.MaxSize < ndefMessage.ToByteArray().Length)
                return false;

            ndef.WriteNdefMessage(ndefMessage);

            Action(new Guid(id), Message);
            return true;
        }

        public void CreateNFCTag(string message, Action<Guid, string> action)
        {
            Message = message;
            Action = action;

            EnableWriteMode();
        }
    }
}