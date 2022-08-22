using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KazanNeft.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage();
            MainPage.Title = ((MainPage as NavigationPage).BindingContext as Page).Title;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
