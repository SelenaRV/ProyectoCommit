using AccenCart.Droid.Services;
using AccenCart.Interfaces;
using Android.Content;
using Android.Media;
using Android.Nfc;
using Android.OS;
using Poz1.NFCForms.Droid;
using System;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(DroidNFCReader))]
namespace AccenCart.Droid.Services
{
    public class DroidNFCReader : INFCReader
    {
        public event EventHandler<string> TagReaded;
        public event EventHandler<string> ReadingCancelled;

        public NfcAdapter NFCdevice;
        public NfcForms x;
        private object objectLock = new Object();
        private bool reading = false;

        public DroidNFCReader()
        {
        }

        public Task Start()
        {
            x.NewTag += X_NewTag;
            x.TagDisconnected += X_TagDisconnected;

            reading = true;
            return Task.CompletedTask;
        }

        public Task<bool> IsNFCCapable()
        {
            var nfcEnable = false;

            NfcManager NfcManager = (NfcManager)Android.App.Application.Context.GetSystemService(Context.NfcService);
            if(NfcManager != null )
            {
                var adapter = NfcManager.DefaultAdapter;

                if (adapter != null && adapter.IsEnabled)
                    nfcEnable = true;
            }

            return Task.FromResult<bool>(nfcEnable);
        }

        public Task Stop()
        {
            reading = false;
            return Task.CompletedTask;
        }

        private void X_TagDisconnected(object sender, Poz1.NFCForms.Abstract.NfcFormsTag e)
        {
            ReadingCancelled?.Invoke(this, "Tag disconnected");
        }

        private void X_NewTag(object sender, Poz1.NFCForms.Abstract.NfcFormsTag e)
        {
            if (!reading)
                return;

            ToneGenerator generator = new ToneGenerator(Android.Media.Stream.System, 100);
            generator.StartTone(Tone.CdmaAlertCallGuard);
            SystemClock.Sleep(1000);
            generator.Release();

            StringBuilder idStr = new StringBuilder();
            for (int i = 0; i < e.Id.Length; i++)
            {
                idStr.Append(string.Format("{0:x2}", e.Id[i]));
            }

            TagReaded?.Invoke(this, idStr.ToString());
        }

        event EventHandler<string> INFCReader.TagReaded
        {
            add
            {
                lock (objectLock)
                {
                    TagReaded += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    TagReaded -= value;
                }
            }
        }

        event EventHandler<string> INFCReader.ReadingCancelled
        {
            add
            {
                lock (objectLock)
                {
                    ReadingCancelled += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    ReadingCancelled -= value;
                }
            }
        }
    }
}