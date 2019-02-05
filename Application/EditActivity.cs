using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Application.Resources.DataHelper;
using Application.Resources.Model;
using Com.Iarcuschin.Simpleratingbar;

namespace Application
{
    [Activity(Label = "EditActivity")]
    public class EditActivity : Activity, IOnMapReadyCallback
    {
        private FloatingActionButton _btnBack;
        private EditText _description;
        private ImageView _image;
        private List<Bitmap> _imagesStorage;
        private EditText _nameOfPlace;
        private EditText _nameOfPlace2;
        private TextView _placeName;
        private TextView _placeSecondName;
        private SimpleRatingBar _ratingBar;
        private FloatingActionButton _updateButton;
        private DataBase db;

        public void OnMapReady(GoogleMap googleMap)
        {
            var name = Intent.GetStringExtra("NamePlace");
            var bounds = db.selectOnePlace(name);
            var position = new LatLng(bounds.Latitude, bounds.Longitude);
            var markerOpt1 = new MarkerOptions();
            markerOpt1.SetPosition(position);
            markerOpt1.SetTitle($"{bounds.LocPlaceName}");
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(position, 15));
            googleMap.AddMarker(markerOpt1);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.edit_layout);
            _imagesStorage = new List<Bitmap>();
            _ratingBar = FindViewById<SimpleRatingBar>(Resource.Id.ratingUpd);
            _placeName = FindViewById<TextView>(Resource.Id.titleOfLocationUpdate);
            _placeSecondName = FindViewById<TextView>(Resource.Id.title2OfLocationUpdate);
            _description = FindViewById<EditText>(Resource.Id.editorUpdate);
            _updateButton = FindViewById<FloatingActionButton>(Resource.Id.updateButton);
            _updateButton.Visibility = ViewStates.Invisible;
            _btnBack = FindViewById<FloatingActionButton>(Resource.Id.backButton);
            _nameOfPlace2 = FindViewById<EditText>(Resource.Id.editor2Update);
            _nameOfPlace = FindViewById<EditText>(Resource.Id.editorUpdate);
            var mapFragment = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
            db = new DataBase();
            UpdateData();

            _description.AfterTextChanged += EnableUpdateButton;
            _nameOfPlace2.AfterTextChanged += EnableUpdateButton;
            _nameOfPlace.AfterTextChanged += EnableUpdateButton;
            _ratingBar.RatingBarChange += _ratingBar_RatingBarChange;

            _updateButton.Click += ClickUpdateButton;
            _btnBack.Click += _btnBack_Click;
        }

        private void _ratingBar_RatingBarChange(object sender, SimpleRatingBar.RatingBarChangeEventArgs e)
        {
            _updateButton.Visibility = ViewStates.Visible;
        }

        private void _btnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void ClickUpdateButton(object sender, EventArgs e)
        {

            var name = Intent.GetStringExtra("NamePlace");
            var emp222 = db.selectTablePlace();
            var imagesList = db.SelectTableImagesList(name);
            var _picked = db.selectOnePlace(name);
            var emp2= db.selectTablePlace();
            var place = new Place()
            {
                Id = _picked.Id,
                Name = _nameOfPlace2.Text,
                Description = _nameOfPlace.Text,
                Rating = _ratingBar.Rating,
                LocPlaceName = _placeName.Text,
                LocSecondPlaceName = _placeSecondName.Text
            };
            db.UpdateTablePlace(place);
            foreach (var image in imagesList)
            {
                image.PlaceName = place.Name;
            }
            var emp = db.selectTablePlace();
            SetResult(Result.Ok);
            Finish();
           

        }

    

        private void EnableUpdateButton(object sender, AfterTextChangedEventArgs e)
        {
            _updateButton.Visibility = ViewStates.Visible;
        }

        private void UpdateData()
        {
            var emp = db.selectTablePlace();
            var name = Intent.GetStringExtra("NamePlace");
            var placePick = db.selectOnePlace(name);
            _nameOfPlace2.Text = placePick.Name;
            _nameOfPlace.Text = placePick.Description;
            _ratingBar.Rating = placePick.Rating;
            _placeName.Text = placePick.LocPlaceName;
            _placeSecondName.Text = placePick.LocSecondPlaceName;
            SetStringFromImagesTable(name);
        }

        private static Bitmap ConvertStringToBitmap(string theBitmap)
        {
            Bitmap img = null;

            if (theBitmap != null)
            {
                var decodedByte = Base64.Decode(theBitmap, 0);
                img = BitmapFactory.DecodeByteArray(decodedByte, 0, decodedByte.Length);
            }

            return img;
        }

        private void SetStringFromImagesTable(string name)
        {
            
            var gallery = FindViewById<LinearLayout>(Resource.Id.linearEditGallery);
            var inflanter = LayoutInflater.From(this);
           
            var imagesList = db.SelectTableImagesList(name);
            foreach (var image in imagesList)
            {
                var view = inflanter.Inflate(Resource.Layout.ImageItem, gallery, false);
                _image = view.FindViewById<ImageView>(Resource.Id.ImageGoogle);
                var imgBitMap = ConvertStringToBitmap(image.ImageBytes);
                _image.SetImageBitmap(imgBitMap);
                gallery.AddView(view);
            }
            
        }
    }
}