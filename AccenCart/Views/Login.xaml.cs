using System;
using AccenCart.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AccenCart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
           // NavigationPage m2 = new NavigationPage(new Views.Menu());
            BindingContext = new LoginViewModel();
            //btnlog.Clicked += Btnlog_Clicked;
        }

       

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {

        }
    }
}