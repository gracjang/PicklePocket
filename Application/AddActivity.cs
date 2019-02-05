using System;
using System.Collections.Generic;
using System.IO;
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
using Android.Util;
using Application.Resources.DataHelper;
using Application.Resources.Model;
using Com.Cloudrail.SI.Services;
using Java.IO;
using Java.Lang;
using Java.Security;
using Newtonsoft.Json;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Base64 = Java.Util.Base64;
using Button = Android.Widget.Button;
using String = System.String;
using Task = Android.Gms.Tasks.Task;
using View = Android.Views.View;


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

        private EditText _nameOfPlace;

        private EditText _description;

        private RatingBar _ratingBar;

        private TextView _placeSecondName;

        private DataBase _db;
        private LatLng picked;
        private List<string> _imagesStorage;
        //var text2 = FindViewById<TextView>(Resource.Id.text2);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _db = new DataBase();
            SetContentView(Resource.Layout.addplace_layout);
            _imagesStorage = new List<string>();
            var button = FindViewById<Button>(Resource.Id.GetLocationButton);
            _ratingBar = FindViewById<RatingBar>(Resource.Id.rating);
            _placeName = FindViewById<TextView>(Resource.Id.titleOfLocation);
            _placeSecondName = FindViewById<TextView>(Resource.Id.title2OfLocation);
            _description = FindViewById<EditText>(Resource.Id.editor);
            mGeoDataClient = PlacesClass.GetGeoDataClient(this, null);
            mPlaceDetectionClient = PlacesClass.GetPlaceDetectionClient(this,null);
            var btnAddNewPlace = FindViewById<FloatingActionButton>(Resource.Id.addButton);
            _nameOfPlace = FindViewById<EditText>(Resource.Id.editor2);
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
            foreach (var item in _imagesStorage)
            {
                Images image = new Images()
                {
                    ImageBytes = item,
                    PlaceName = _nameOfPlace.Text

                };
                _db.InsterIntoTableImage(image);
            }

            var images = _db.selectTableImages();
            String name = Intent.GetStringExtra("name");
            //Intent data = new Intent();
            Intent data = new Intent();
            Bundle extras = new Bundle();
            Bundle extras2 = new Bundle();
            extras.PutString("name", name);
            //extras.PutInt("PlaceId", _nameOfPlace.Id);
            extras.PutString("PlaceName", _nameOfPlace.Text);
            extras.PutFloat("ratingText", _ratingBar.Rating);
            extras.PutString("locationPlaceName", _placeName.Text);
            extras.PutString("secondLocationPlaceName", _placeSecondName.Text);
            extras.PutString("description", _description.Text);
            extras.PutDouble("latitude",picked.Latitude);
            extras.PutDouble("longitude", picked.Longitude);
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

            int count = 0;
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

                        string imgaBitmapToString = ConvertBitmapToString(bitmap);


                       _imagesStorage.Add(imgaBitmapToString);
                        

                        image.SetImageBitmap(bitmap);
                        gallery.AddView(view);
                        count++;

                    }

                    

                }
  
            }

            
        }

        public static string ConvertBitmapToString(Bitmap theBitmap)
        {
            string strImage = string.Empty;
            using (var stream = new System.IO.MemoryStream())
            {
                theBitmap.Compress(Bitmap.CompressFormat.Png, 25, stream);
                var bytes = stream.ToArray();
                strImage = Convert.ToBase64String(bytes);
                
            }

            return strImage;
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
            var pickBounds = placePicked.LatLng;
            picked = new LatLng(pickBounds.Latitude,pickBounds.Longitude);
            
            //LatLngBounds bounds = PlacePicker.GetLatLngBounds(data);
            _placeName.Text = placePicked?.NameFormatted.ToString();
            _placeSecondName.Text = placePicked?.AddressFormatted.ToString();

           
        }
    }
}