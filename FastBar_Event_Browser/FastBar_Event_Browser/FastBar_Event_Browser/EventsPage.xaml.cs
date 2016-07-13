using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FastBar_Event_Browser
{
    public partial class EventsPage : ContentPage
    {
        public EventsPage()
        {
            InitializeComponent();
        }

        private void EventsPage_Appearing(object sender, EventArgs e)
        {
            this.Title = "Events for " + DatabaseManager.LoggedInUser.Email ?? "user";
            MainStackLayout.Children.Clear();
            foreach (var ev in DatabaseManager.RetrieveEvents())
            {
                MainStackLayout.Children.Add(new Label()
                {
                    FontSize = 32,
                    Margin = new Thickness(2),
                    Text = ev.Name,
                    TextColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Start,
                    LineBreakMode = LineBreakMode.WordWrap,
                    HorizontalOptions = LayoutOptions.Fill
                });
                MainStackLayout.Children.Add(new Label()
                {
                    FontSize = 20,
                    Margin = new Thickness(2),
                    Text = "Start: " + ev.DateTimeStartUtc.ToLocalTime().ToString(),
                    TextColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Start,
                    LineBreakMode = LineBreakMode.WordWrap,
                    HorizontalOptions = LayoutOptions.Fill
                });
                MainStackLayout.Children.Add(new Label()
                {
                    FontSize = 20,
                    Margin = new Thickness(2),
                    Text = "End: " + ev.DateTimeEndUtc.ToLocalTime().ToString(),
                    TextColor = Color.White,
                    HorizontalTextAlignment = TextAlignment.Start,
                    LineBreakMode = LineBreakMode.WordWrap,
                    HorizontalOptions = LayoutOptions.Fill
                });
            }
        }
    }
}
