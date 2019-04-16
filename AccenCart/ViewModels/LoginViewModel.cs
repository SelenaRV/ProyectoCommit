using AccenCart.Interfaces;
using AccenCart.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AccenCart.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string backgroundColor;
        private bool nfcEnabled;
        private bool tapToBeginVisible;
        private string centerImage;
        private int connectionTries;
        public Command AppearingCommand { get; }
        public Command LogoTappedCommand { get; }
        
        INFCReader nfcReader;

        public LoginViewModel()
        {
            Title = "Accenture Smart Cart";
            Header = "Accenture Smart Cart";
            Slogan = "High performance. Delivered";
            Footer = "Copyright 2019";
            BackgroundColor = "#FFFFFF";
            //BackgroundColor = "#4286f4";
            CenterImage = "accenture_logo.png";
            TapToBeginVisible = true;

            AppearingCommand = new Command(async () => await ExecuteAppearingCommand());
            LogoTappedCommand = new Command( () => ExecuteLogoTappedCommand());
            nfcReader = DependencyService.Get<INFCReader>();
            nfcReader.TagReaded += NfcReader_TagReaded;
            nfcReader.ReadingCancelled += NfcReader_ReadingCancelled;
        }

        private void NfcReader_ReadingCancelled(object sender, string e)
        {
            throw new System.NotImplementedException();
        }

        private void NfcReader_TagReaded(object sender, string e)
        {
            string msg = string.Empty;
            msg = e;
        }

        #region Properties
        public string Header { get; set; }
        public string Slogan { get; set; }
        public string Footer { get; set; }
        
        public string BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public bool TapToBeginVisible
        {
            get
            {
                return tapToBeginVisible;
            }
            set
            {
                tapToBeginVisible = value;
                OnPropertyChanged(nameof(TapToBeginVisible));
            }
        }

        public string CenterImage
        {
            get
            {
                return centerImage;
            }
            set
            {
                centerImage = value;
                OnPropertyChanged(nameof(CenterImage));
            }
        }

        #endregion

        #region Commands

        private async Task ExecuteAppearingCommand()
        {
            nfcEnabled = await nfcReader.IsNFCCapable();

            if(nfcEnabled)
                await nfcReader.Start();
        }

        private void ExecuteLogoTappedCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            connectionTries = 0;

            if(CenterImage == "accenture_logo.png")
            {
                TapToBeginVisible = false;
                CenterImage = "qr_code.png";

                Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                {
                    connectionTries++;

                    if (connectionTries >= 2)
                    {
                        var mainPage = new MainPage();
                        Navigation.PushAsync(mainPage);
                        return false;
                    }

                    return true;
                });
            }
            else
            {
                TapToBeginVisible = true;
                CenterImage = "accenture_logo.png";
            }
            
            IsBusy = false;
        }

        #endregion
    }
}
