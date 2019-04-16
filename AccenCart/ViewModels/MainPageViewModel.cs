using AccenCart.Interfaces;
using AccenCart.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace AccenCart.ViewModels
{

    public class MainPageViewModel : BaseViewModel
    {
        private int connectionTries;
        private string centerImage;
        private bool tapToBeginVisible;
        public Command ProductTappedCommand { get; }
        public Command OfferTappedCommand { get; }
        public Command MapTappedCommand { get; }

       
        public MainPageViewModel()
        {
            Title = "Menu Principal";
            ProductTappedCommand = new Command(() => ExecuteProductTappedCommand());
            OfferTappedCommand = new Command(() => ExecuteOfferTappedCommand());
            MapTappedCommand = new Command(() => ExecuteMapTappedCommand());

        }

        private void ExecuteMapTappedCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
              //var offerPg = new OfferPage();
               //Navigation.PushAsync(offerPg);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            IsBusy = false;


        }

        private void ExecuteOfferTappedCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

                      
                try
                {
                    var offerPg = new OffersPage();
                    Navigation.PushAsync(offerPg);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                IsBusy = false;

            
        }

        private void ExecuteProductTappedCommand()
        {
            
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                //var offerPg = new OffersPage();
                //Navigation.PushAsync(offerPg);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            IsBusy = false;
          

        }
       
    }

 }

    
