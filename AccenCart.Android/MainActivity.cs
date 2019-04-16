using AccenCart.Droid.Services;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;
using Poz1.NFCForms.Abstract;
using Poz1.NFCForms.Droid;
using System;
using Android.Views;




namespace AccenCart.Droid
{
    [Activity(Label = "AccenCart", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        DroidNFCReader nfcReader = new DroidNFCReader();
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            

            base.OnCreate(savedInstanceState);
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init();
            NfcManager NfcManager = (NfcManager)Android.App.Application.Context.GetSystemService(Context.NfcService);
            nfcReader.NFCdevice = NfcManager.DefaultAdapter;
            
            
            CarouselViewRenderer.Init();
            Xamarin.Forms.DependencyService.Register<INfcForms, NfcForms>();
            nfcReader.x = Xamarin.Forms.DependencyService.Get<INfcForms>() as NfcForms;
           

            LoadApplication(new App());
            
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (nfcReader != null && nfcReader.NFCdevice != null)
            {
                var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop); ;
                nfcReader.NFCdevice.EnableForegroundDispatch
                (
                    this,
                    PendingIntent.GetActivity(this, 0, intent, 0),
                    new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
                    new String[][] {new string[] {
                            NFCTechs.NfcA,
                            NFCTechs.NdefFormatable,
                            NFCTechs.MifareUltralight
                        },
                        new string[] {
                            NFCTechs.NfcA,
                            NFCTechs.NdefFormatable,
                            NFCTechs.MifareUltralight
                        },
                    }
                );
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            if(nfcReader != null && nfcReader.NFCdevice != null)
                nfcReader.NFCdevice.DisableForegroundDispatch(this);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            nfcReader.x.OnNewIntent(this, intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}