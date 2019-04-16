using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AccenCart.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using AccenCart.Views;
using System.Threading.Tasks;
using FFImageLoading.Forms;


namespace AccenCart.ViewModels
{
    public class OffersPageViewModel : BaseViewModel
    {

        
        public OffersPageViewModel()
        {
            Title = "OFERTAS";
           

            MyItemsSource = new ObservableCollection<View>()
            {
                new CachedImage() { Source = "Offer1.jpg", DownsampleToViewSize = true, Aspect = Aspect.Fill },
                new CachedImage() { Source = "Offer2.jpg", DownsampleToViewSize = true, Aspect = Aspect.Fill },
                new CachedImage() { Source = "Offer3.jpg", DownsampleToViewSize = true, Aspect = Aspect.Fill }
            };
            MyCommand = new Command(() =>
            {
                Debug.WriteLine("Position selected.");
            });

        }

        ObservableCollection<View> myItemsSource;

        

        public ObservableCollection<View> MyItemsSource
        {
            set
            {
                myItemsSource = value;
                OnPropertyChanged("MyItemsSource");
            }
            get
            {
                return myItemsSource;
            }
        }

        public Command MyCommand { protected set; get; }


    }
    
}

    
