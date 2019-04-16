using Xamarin.Forms;

namespace AccenCart
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

             MainPage = new NavigationPage(new Views.Login());
              
           // MainPage = new NavigationPage(new Views.Menu());
        }

        protected override void OnStart()
        {
            //MainPage = new NavigationPage(new Views.StartAccenCart());
            //MainPage = new NavigationPage(new Views.Login());
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
