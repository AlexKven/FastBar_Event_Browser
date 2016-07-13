using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastBar_Event_Browser
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DatabaseManager.Initialize();
            var startPage = new MainPage();
            MainPage = new NavigationPage(startPage);
            startPage.NavigateToEventsIfAppropriate();
        }

        public async Task Navigate(Page navigateTo)
        {
            await (MainPage as NavigationPage)?.PushAsync(navigateTo);
        }

        public async Task NavigateBack()
        {
            await (MainPage as NavigationPage)?.PopAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
