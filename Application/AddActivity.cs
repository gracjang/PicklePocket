using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Plugin.Geolocator;
using Android.Gms.Location.Places;
using Android.Gms.Location.Places.UI;

namespace Application
{
    [Activity(Label = "AddActivity")]
    public class AddActivity : AppCompatActivity
    {
        private static readonly int PLACE_REQUEST = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addplace_layout);
            var button = FindViewById<Button>(Resource.Id.Button1);
            var text = FindViewById<TextView>(Resource.Id.text1);
            var text2 = FindViewById<TextView>(Resource.Id.text2);
            button.Click += OnPickAPlaceLocation;
            //button.Click += delegate { GetLocationButton_Clicked(text, text2); };
        }

        private void OnPickAPlaceLocation(object sender, EventArgs e)
        {
            var builder = new PlacePicker.IntentBuilder();
            StartActivityForResult(builder.Build(this), PLACE_REQUEST);
        }

        private async void GetLocationButton_Clicked(TextView txt1, TextView txt2)
        {
            await RetreiveLocation(txt1,txt2);
        }

        private async Task RetreiveLocation(TextView txt1, TextView txt2)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            txt1.Text = "Latitude" + position.Latitude.ToString();
            txt2.Text = "Longitude" + position.Longitude.ToString();
        }
    }
}