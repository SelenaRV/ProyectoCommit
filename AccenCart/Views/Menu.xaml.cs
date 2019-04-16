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
    public partial class Menu : MasterDetailPage
    {
        public Menu()
        {
            InitializeComponent();
            MyMenu();
        }
        public void MyMenu()
        {
            
            List<OpMenu> Menuop = new List<OpMenu>
            { new OpMenu{ page = new Contacto(), MenuTitle = "Contacto ", MenuDetail = " ", icon = "icoMkter.jpg" }
            };
            ListMenu.ItemsSource = Menuop;

            void ListMenu_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
            {
                var menu = e.SelectedItem as Menu;
             }
            

            Detail = new NavigationPage(new MainPage());
        }
          
        public class OpMenu
        {
            public string MenuTitle
        {
            get;
            set;
        }
        public String MenuDetail
        {
            get;
            set;
        }

        public ImageSource icon
        {
            get;
            set;
        }

        public Page page
        {
            get;
            set;

        }

        }

        private void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void ListMenu_ItemSelected_1(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}