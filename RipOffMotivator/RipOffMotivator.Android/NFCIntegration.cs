using System;
using System.IO;
using System.Text;
using Android.App;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;
using Android.OS;

namespace RipOffMotivator.Droid
{
    public class NFCIntegration : Activity, INFCIntegration
    {
        const string ViewApeMimeType = "application/vnd.xamarin.nfcxample";

        public NfcAdapter NfcAdapter;
        Context Context;
        public Boolean InWriteMode = false;
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
            MainActivity.EnableWriteMode();
        }

        public void AdjustNFCTag(Intent intent)
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

            var mimeBytes = Encoding.ASCII.GetBytes(ViewApeMimeType);
            var id = tag.GetId();
            var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, id, Encoding.ASCII.GetBytes(Message));
            var ndefMessage = new NdefMessage(new[] { apeRecord });

            if (ndef.MaxSize < ndefMessage.ToByteArray().Length)
                return false;

            try
            {
                ndef.Connect();
                ndef.WriteNdefMessage(ndefMessage);

                Action(new Guid(id), Message);
                ndef.Close();
                return true;
            }
            catch (Java.IO.IOException)
            {
                var format = NdefFormatable.Get(tag);
                if (format == null)
                {
                    return false;
                    //DisplayMessage("Tag does not appear to support NDEF format.");
                }
                else
                {
                    try
                    {
                        format.Connect();
                        format.Format(ndefMessage);
                        Action(new Guid(id), Message);
                        format.Close();
                        return true;
                    }
                    catch (IOException)
                    {
                        return false;
                    }
                }
            }
        }

        public void CreateNFCTag(string message, Action<Guid, string> action)
        {
            Message = message;
            Action = action;

            EnableWriteMode();
        }
    }
}