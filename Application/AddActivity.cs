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
using Android.Graphics;
using Android.Locations;
using Android.Support.Design.Widget;
using Android.Test.Suitebuilder.Annotation;
using Com.Cloudrail.SI.Services;
using Java.Lang;
using Java.Security;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Xaml;
using String = System.String;
using Task = Android.Gms.Tasks.Task;


namespace Application
{
    [Activity(Label = "AddActivity")]
    public class AddActivity : AppCompatActivity
    {
        private static readonly int PLACE_REQUEST = 1;
     
        private TextView _placeName;
        private GeoDataClient mGeoDataClient;
        private PlaceDetectionClient mPlaceDetectionClient;
        private ImageView image;
        private EditText nameOfPlace;
        private TextView _placeSecondName;
        //var text2 = FindViewById<TextView>(Resource.Id.text2);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addplace_layout);
           var button = FindViewById<Button>(Resource.Id.GetLocationButton);
            
            _placeName = FindViewById<TextView>(Resource.Id.titleOfLocation);
            _placeSecondName = FindViewById<TextView>(Resource.Id.title2OfLocation);
            mGeoDataClient = PlacesClass.GetGeoDataClient(this, null);
            mPlaceDetectionClient = PlacesClass.GetPlaceDetectionClient(this,null);
            var btnAddNewPlace = FindViewById<FloatingActionButton>(Resource.Id.addButton);
            nameOfPlace = FindViewById<EditText>(Resource.Id.editor2);
            button.Click += OnPickAPlaceLocation;
            btnAddNewPlace.Click += OnPickAddNewPlaceButton;
            //button.Click += delegate { GetLocationButton_Clicked(text, text2); };
        }

        private void Image_Click(object sender, EventArgs e)
        {
            LayoutInflater inflanter = LayoutInflater.From(this);

        }

        private void OnPickAPlaceLocation(object sender, EventArgs e)
        {

            var builder = new PlacePicker.IntentBuilder();
            StartActivityForResult(builder.Build(this), PLACE_REQUEST);
            
      
        }

        private void OnPickAddNewPlaceButton(object sender, EventArgs e)
        {

            String name = Intent.GetStringExtra("name");
            //Intent data = new Intent();
            Intent data = new Intent();
            Bundle extras = new Bundle();
            extras.PutString("name", name);
            extras.PutString("PlaceName", nameOfPlace.Text);
            data.PutExtras(extras);
            SetResult(Result.Ok,data);
            Finish();
        }
        private async void GetPhotos(Intent data)
        {
            var placePicked = PlacePicker.GetPlace(this, data);
            var gallery = FindViewById<LinearLayout>(Resource.Id.linearGallery);
            LayoutInflater inflanter = LayoutInflater.From(this);
            //_Image = FindViewById<ImageView>(Resource.Id.ImageGoogle);
            string placeId = placePicked.Id;
            if (gallery.ChildCount >0)
            {
                gallery.RemoveAllViews();
            }

            using (var photoMetadataResponse = await mGeoDataClient.GetPlacePhotosAsync(placeId))
            using (var photoMetadataBuffer = photoMetadataResponse.PhotoMetadata)
            {
                foreach (var item in photoMetadataBuffer)
                {

                    using (var photoResponse = await mGeoDataClient.GetPhotoAsync(item))
                    {
                        
                        View view = inflanter.Inflate(Resource.Layout.ImageItem, gallery, false);
                        image = view.FindViewById<ImageView>(Resource.Id.ImageGoogle);
                        var bitmap = photoResponse.Bitmap;
                        image.SetImageBitmap(bitmap);
                        gallery.AddView(view);

                    }

                    

                }

                
            }
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
                GetPhotos(data);

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