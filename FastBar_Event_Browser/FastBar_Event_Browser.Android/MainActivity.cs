using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace FastBar_Event_Browser.Android
{
    [Activity(Label = "FastBar_Event_Browser", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //Xamarin.Forms.DependencyService.Register<SQLiteConnectionFactory>();
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

