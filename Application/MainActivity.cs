using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Application.Resources.DataHelper;
using Application.Resources.Model;
using XamDroid.ExpandableRecyclerView;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace Application
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static int RequestCode = 1;
        private List<City> cityList = new List<City>();

        public DataBase db;
        private MyAdapter mAdapter;
        private RecyclerView mRecyclerView;
        public List<IParentObject> parentObject = new List<IParentObject>();
        private TitleCreator titleCreator = new TitleCreator();
        internal static int RequestEditCode =2 ;

        public static bool IsDelete = false;
        //List<object> childList = new List<object>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            // Create DB

            db = new DataBase();

            db.DropAllTable();
            db.CreateDataBase();
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            var mImageButton = FindViewById<ImageButton>(Resource.Id.imageButton1);
            mAdapter = new MyAdapter(this, parentObject);
            mAdapter.SetParentClickableViewAnimationDefaultDuration();
            mAdapter.ParentAndIconExpandOnClick = true;
            mRecyclerView.SetAdapter(mAdapter);

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

            LoadData(false);
            fab.Click += delegate { ShowDialog(); };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == RequestCode)
                if (resultCode == Result.Ok)
                {
                    var titles = titleCreator.GetAll;
                    cityList = db.selectTableCities();
                    foreach (var title in titles)
                        if (title.Title == data.GetStringExtra("name"))
                        {
                            if (title.ChildObjectList == null)
                            {
                                var place = new Place
                                {
                                    Id = data.GetIntExtra("PlaceId", 1),
                                    Name = data.GetStringExtra("PlaceName"),
                                    Description = data.GetStringExtra("description"),
                                    Rating = data.GetFloatExtra("ratingText", 0),
                                    LocPlaceName = data.GetStringExtra("locationPlaceName"),
                                    LocSecondPlaceName = data.GetStringExtra("secondLocationPlaceName"),
                                    Latitude = data.GetDoubleExtra("latitude", 0),
                                    Longitude = data.GetDoubleExtra("longitude", 0),
                                    CityName = title.Title
                                };
                                db.InsterIntoTablePlace(place);
                                var childList = new List<object>();
                                childList.Add(new TitleChild($"{data.GetStringExtra("PlaceName")}"));
                                title.ChildObjectList = childList;
                            }
                            else
                            {
                                var place = new Place
                                {
                                    Id = data.GetIntExtra("PlaceId", 1),
                                    Name = data.GetStringExtra("PlaceName"),
                                    Description = data.GetStringExtra("description"),
                                    Rating = data.GetFloatExtra("ratingText", 0),
                                    LocPlaceName = data.GetStringExtra("locationPlaceName"),
                                    LocSecondPlaceName = data.GetStringExtra("secondLocationPlaceName"),
                                    Latitude = data.GetDoubleExtra("latitude",0),
                                    Longitude = data.GetDoubleExtra("longitude",0),
                                    CityName = title.Title
                                };

                                db.InsterIntoTablePlace(place);
                              
                                var newChildList = title.ChildObjectList;
                                newChildList.Add(new TitleChild($"{data.GetStringExtra("PlaceName")}"));
                                title.ChildObjectList = newChildList;
                            }
                        }

                    mAdapter = new MyAdapter(this, parentObject);
                    mRecyclerView.SetAdapter(mAdapter);

                    Toast.MakeText(this, data.GetStringExtra("PlaceName"), ToastLength.Short).Show();
                }
            if (requestCode == RequestEditCode)
                if (resultCode == Result.Ok)
                {
                    LoadData(true);
                }
        }

        private void AddCityButton_Click()
        {
            var inflater = (LayoutInflater) BaseContext.GetSystemService(LayoutInflaterService);
            var addView = inflater.Inflate(Resource.Layout.addplace_layout, null);
        }

        private void insertSingleItem(EditText data)
        {
            var city = new City
            {
                IdPlace = data.Id,
                Name = data.Text
            };
            db.InsterIntoTableCity(city);
            var zmienna = db.selectOneCity(city.Name);
            if (IsDelete == false)
            {
                SecondTimeLoadData(zmienna.Name);
            }
            else
            {
                LoadData(false);
            }
            
        }

        private void ShowDialog()
        {
            var layoutInflater = LayoutInflater.From(this);
            var mView = layoutInflater.Inflate(Resource.Layout.AddDialog, null);
            var addDialog = new AlertDialog.Builder(this);
            addDialog.SetView(mView);
            var content = mView.FindViewById<EditText>(Resource.Id.editText1);
            addDialog.SetCancelable(false)
                .SetPositiveButton("Add", delegate { insertSingleItem(content); })
                .SetNegativeButton("Cancel", delegate { addDialog.Dispose(); });
            var adialog = addDialog.Create();
            addDialog.Show();
        }

        private void LoadData(bool After)
        {
            if (After == true || IsDelete==true)
            {
                titleCreator=new TitleCreator();
                parentObject=new List<IParentObject>();
                IsDelete = false;

            }
            cityList = db.selectTableCities();
            var child = db.selectTablePlace();

            for (var x = 0; x < cityList.Count; x++)
            {
                titleCreator.Add(cityList[x].Name);
            }

            var titles = titleCreator.GetAll;


            foreach (var title in titles)
            {
                var childList = new List<object>();
                var titleName = db.selectTablePlaceWhere(title);
                foreach (var childrenPlace in titleName)
                {
                    childList.Add(new TitleChild(childrenPlace.Name));
                    title.ChildObjectList = childList;
                }

                parentObject.Add(title);
            }


            mAdapter = new MyAdapter(this, parentObject);
            mRecyclerView.SetAdapter(mAdapter);
        }

        public void SecondTimeLoadData(string name)
        {
          
           
            cityList = db.selectTableCities();
          
            titleCreator.Add(name);
            

            var titles = titleCreator.GetAll;
            parentObject.Add(titles.LastOrDefault());

            mAdapter = new MyAdapter(this, parentObject);
            mRecyclerView.SetAdapter(mAdapter);
        }

        public void CheckUpdatesData()
        {
            cityList = db.selectTableCities();
            titleCreator = new TitleCreator();
            var parentObject = new List<IParentObject>();

            AddDataAfterUpdate(parentObject);

            mAdapter = new MyAdapter(this, parentObject);
            mRecyclerView.SetAdapter(mAdapter);
        }

        private void AddDataAfterUpdate(List<IParentObject> parentObject)
        {
            for (var x = 0; x < cityList.Count; x++) titleCreator.Add(cityList[x].Name);

            var titles = titleCreator.GetAll;
            foreach (var title in titles)
            {
                var childList = new List<object>();
                var titleName = db.selectTablePlaceWhere(title);
                foreach (var childrenPlace in titleName)
                {
                    childList.Add(new TitleChild(childrenPlace.Name));
                    title.ChildObjectList = childList;
                }

                parentObject.Add(title);
            }
        }

        /*private List<IParentObject> InitData()
          {
              var titleCreator = new TitleCreator();
              var titles = titleCreator.GetAll;
              var parentObject = new List<IParentObject>();
              if (titles == null)
              {
  
              }
              else
              {
                  foreach (var title in titles)
                  {
                      var childList = new List<Object>();
                      for (int i = 1; i <= 5; i++)
                      {
                          childList.Add(new TitleChild($"Child{i}"));
                          title.ChildObjectList = childList;
                      }
  
  
                      parentObject.Add(title);
                  }
              }
              
              return parentObject;
          } 
  
  
          private List<IParentObject> AddData(string data)
          {
              TitleCreator titleCreator = new TitleCreator();
              titleCreator.Add(data);
              var titles = titleCreator.GetAll;
              var parentObject = new List<IParentObject>();
  
                  foreach (var title in titles)
                  {
                      var childList = new List<Object>();
                      for (int i = 1; i <= 5; i++)
                      {
                          childList.Add(new TitleChild($"Child{i}"));
                          title.ChildObjectList = childList;
                      }
  
  
                      parentObject.Add(title);
                  }
              
              
              return parentObject;
          } */
    }
}