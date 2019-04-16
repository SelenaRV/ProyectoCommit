using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AccenCart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartAccenCart : ContentPage
    {
        public StartAccenCart()
        {
            InitializeComponent();
         //  MainPage = new NavigationPage(new Login());
        }
    }
}