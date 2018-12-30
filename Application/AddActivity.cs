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
using Android.Gms.Maps.Model;
using Android.Locations;
using Plugin.Geolocator.Abstractions;

namespace Application
{
    [Activity(Label = "AddActivity")]
    public class AddActivity : AppCompatActivity
    {
        private static readonly int PLACE_REQUEST = 1;

        private TextView _placeName;

        private TextView _placeSecondName;
        //var text2 = FindViewById<TextView>(Resource.Id.text2);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addplace_layout);
           var button = FindViewById<Button>(Resource.Id.GetLocationButton);
           _placeName = FindViewById<TextView>(Resource.Id.titleOfLocation);
            _placeSecondName = FindViewById<TextView>(Resource.Id.title2OfLocation);

            button.Click += OnPickAPlaceLocation;
            //button.Click += delegate { GetLocationButton_Clicked(text, text2); };
        }

        private void OnPickAPlaceLocation(object sender, EventArgs e)
        {

            var builder = new PlacePicker.IntentBuilder();
            StartActivityForResult(builder.Build(this), PLACE_REQUEST);
      
        }

        private async void GetLocationButton_Clicked()
        {
            var position = await RetreiveLocation();
            LatLng southwest = new LatLng(position.Latitude,position.Longitude);
            
          
        }

        private async Task<Position> RetreiveLocation()
        {

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            return position;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == PLACE_REQUEST && resultCode == Result.Ok)
            {
                GetPlaceFromPicker(data);
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }

        private void GetPlaceFromPicker(Intent data)
        {
            var placePicked = PlacePicker.GetPlace(this, data);
            var picked = placePicked.LatLng;
            //LatLngBounds bounds = PlacePicker.GetLatLngBounds(data);
            _placeName.Text = placePicked?.NameFormatted.ToString();
            _placeSecondName.Text = placePicked?.AddressFormatted.ToString();
        }
    }
}