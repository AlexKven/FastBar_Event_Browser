using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastBar_Event_Browser
{
    public partial class MainPage : ContentPage
    {
        private bool? IsNarrowLayout = null;

        //Set to true for the first load of the page (I.E., app startup), then false.
        public bool AutomaticallyLoadEvents { get; set; } = false;

        public MainPage()
        {
            InitializeComponent();
        }

        #region Event Handlers
        private void CreateAccountButton_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://getfastbar.com/Account/SignUp"));
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            User currentUser;
            try
            {
                string token = await APIManager.GetToken(UsernameBox.Text, PasswordBox.Text);
                if (token == null)
                {
                    //Failure of some sort
                    MessageText.Text = "Login unsuccessful. Check your email/password.";
                }
                else
                {
                    MessageText.Text = "Login success!";
                }
                currentUser = new User();
                currentUser.Token = token;
                currentUser.Email = UsernameBox.Text;
                DatabaseManager.LoggedInUser = currentUser;
                NavigateToEventsIfAppropriate();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageText.Text = "Error connecting to server. Check your internet connection.";
            }
        }

        private void This_Appearing(object sender, EventArgs e)
        {
            SetLayout();
            UsernameBox.Text = "";
            MessageText.Text = "Please log in to view your upcoming events.";
            if (AutomaticallyLoadEvents)
                NavigateToEventsIfAppropriate();
            AutomaticallyLoadEvents = false;
        }

        private void This_SizeChanged(object sender, EventArgs e)
        {
            SetLayout();
        }
        #endregion

        private void IntializePlatformSpecificUI()
        {
            Device.OnPlatform(Android: () =>
            {
                //For some strange reason, Android seems to be ignoring placeholder text color.
                //TODO: Investigate why placeholder text color isn't working on Android.
                PasswordBox.PlaceholderColor = UsernameBox.PlaceholderColor = Color.FromHex("DDDDDD");
            });

            LogoImage.Source = Device.OnPlatform(
            iOS: ImageSource.FromFile("FastBar_Logo.jpg"),
            Android: ImageSource.FromFile("fastbar_logo.jpg"),
            WinPhone: ImageSource.FromFile("Assets/FastBar_Logo.png"));
        }

        private void SetLayout()
        {
            if ((!IsNarrowLayout.HasValue || !IsNarrowLayout.Value) && Width < 500)
            {
                //The page has a narrow width, and the controls are not set to a narrow layout
                //So set the controls to a narrow layout
                NarrowRow.Height = GridLength.Auto;
                WideColumn.Width = new GridLength(0);
                LoginButton.Margin = new Thickness(10, 4, 10, 4);
                CreateAccountButton.Margin = new Thickness(10, 4, 10, 4);
                LogoImage.WidthRequest = 200;
                LogoImage.HeightRequest = 55;
                DescriptionText.FontSize = 24;
                Grid.SetRow(MovingLayout, 1);
                Grid.SetColumn(MovingLayout, 0);

                IsNarrowLayout = true;
            }
            else if ((!IsNarrowLayout.HasValue || IsNarrowLayout.Value) && Width >= 500)
            {
                //The page has a wide width, and the controls are not set to a wide layout
                //So set the controls to a wide layout
                NarrowRow.Height = new GridLength(0);
                WideColumn.Width = new GridLength(210);
                LoginButton.Margin = new Thickness(0, 4, 10, 4);
                CreateAccountButton.Margin = new Thickness(0, 4, 10, 4);
                LogoImage.WidthRequest = 400;
                LogoImage.HeightRequest = 110;
                DescriptionText.FontSize = 32;
                Grid.SetRow(MovingLayout, 0);
                Grid.SetColumn(MovingLayout, 1);

                
                IsNarrowLayout = false;
            }
        }

        internal async void NavigateToEventsIfAppropriate()
        {
            if (DatabaseManager.LoggedInUser == null)
                return;
            MessageText.Text = "Getting your events...";
            UsernameBox.Text = DatabaseManager.LoggedInUser.Email;
            UsernameBox.IsEnabled = false;
            PasswordBox.IsEnabled = false;
            LoginButton.IsEnabled = false;
            CreateAccountButton.IsEnabled = false;
            var success = await DatabaseManager.TryStoreEventsFromLoggedInUser();
            UsernameBox.IsEnabled = true;
            PasswordBox.IsEnabled = true;
            LoginButton.IsEnabled = true;
            CreateAccountButton.IsEnabled = true;
            if (success)
            {
                await ((App)App.Current).Navigate(new EventsPage());
            }
            else
            {
                MessageText.Text = "Error getting your events. Check your connection and credentials, and try again.";
                UsernameBox.Text = DatabaseManager.LoggedInUser.Email;
            }
        }
    }
}
