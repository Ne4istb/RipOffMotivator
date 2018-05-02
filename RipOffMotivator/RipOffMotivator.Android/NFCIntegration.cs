using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;
using Android.OS;

namespace RipOffMotivator.Droid.NFCModule
{
    class NFCIntegration : Activity, INFCIntegration
    {
        const string ViewApeMimeType = "application/vnd.xamarin.nfcxample";

        NfcAdapter NfcAdapter;
        Boolean InWriteMode = false;
        string Message = string.Empty;
        Guid Id = Guid.Empty;

        public NFCIntegration(NfcAdapter nfcAdapter)
        {
            NfcAdapter = nfcAdapter;
        }

        public void CreateNFCTag(string message, Guid id)
        {
            Message = message;
            Id = id;

            EnableWriteMode();
        }

        private void EnableWriteMode()
        {
            InWriteMode = true;

            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };

            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            if (NfcAdapter != null)
                NfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
            else
            {
                var alert = new AlertDialog.Builder(this).Create();
                alert.SetMessage("NFC is not supported on this device.");
                alert.SetTitle("NFC Unavailable");
                alert.Show();
            }
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
                var alert = new AlertDialog.Builder(this).Create();
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

            var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, Id.ToByteArray(), Convert.FromBase64String(Message));
            var ndefMessage = new NdefMessage(new[] { apeRecord });

            ndef.Connect();

            if (ndef.MaxSize < ndefMessage.ToByteArray().Length)
                return false;

            ndef.WriteNdefMessage(ndefMessage);

            return true;
        }

		public Task<Guid> CreateNFCTag(string message)
		{
			throw new NotImplementedException();
		}
	}
}