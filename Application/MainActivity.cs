using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Support.Design.Widget;
using Android.Views;
using ButtonCircle.FormsPlugin.Droid;
using Java.Lang;
using XamDroid.ExpandableRecyclerView;
using Object = Java.Lang.Object;

namespace Application
{
    

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView mRecyclerView;
       // private RecyclerView.LayoutManager mLayoutManager;
        private MyAdapter mAdapter;
        public List<IParentObject> parentObject = new List<IParentObject>();
        public static int RequestCode = 1;

        private TitleCreator titleCreator= new TitleCreator();
        //List<object> childList = new List<object>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            var mImageButton = FindViewById<ImageButton>(Resource.Id.imageButton1);
            mAdapter = new MyAdapter(this, parentObject);
            mAdapter.SetParentClickableViewAnimationDefaultDuration();
            mAdapter.ParentAndIconExpandOnClick = true;
            mRecyclerView.SetAdapter(mAdapter);
            
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

            
            fab.Click += delegate { ShowDialog(); };


        }
        
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == RequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    
                    
                    var titles = titleCreator.GetAll;
                   
                    foreach (var title in titles)
                    {
                      
                        if (title.Title== data.GetStringExtra("name"))
                        {
                            if (title.ChildObjectList == null)
                            {
                                var childList = new List<object>();
                                childList.Add(new TitleChild($"{data.GetStringExtra("PlaceName")}"));
                                title.ChildObjectList = childList;
                            }
                            else
                            {
                                List<object> newChildList = title.ChildObjectList;
                                newChildList.Add(new TitleChild($"{data.GetStringExtra("PlaceName")}"));
                                title.ChildObjectList = newChildList;
                            }
                            
                        }
                    }
                    
                    mAdapter = new MyAdapter(this, parentObject);
                    mRecyclerView.SetAdapter(mAdapter);

                    Toast.MakeText(this,data.GetStringExtra("PlaceName"),ToastLength.Short).Show();
                }
            }
        }

      
        
        private void AddCityButton_Click()
        {
            LayoutInflater inflater = (LayoutInflater) BaseContext.GetSystemService(Context.LayoutInflaterService);
            View addView = inflater.Inflate(Resource.Layout.addplace_layout, null);

        }
        
        

        private void insertSingleItem(string data)
        {

            
            titleCreator.Add(data);
            var titles = titleCreator.GetAll;
            
                parentObject.Add(titles.LastOrDefault());
               mAdapter = new MyAdapter(this, parentObject);
                mRecyclerView.SetAdapter(mAdapter);

            
            

        }

        private void ShowDialog()
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View mView = layoutInflater.Inflate(Resource.Layout.AddDialog, null);
            Android.Support.V7.App.AlertDialog.Builder addDialog = new Android.Support.V7.App.AlertDialog.Builder(this);
            addDialog.SetView(mView);
            var content = mView.FindViewById<EditText>(Resource.Id.editText1);
            addDialog.SetCancelable(false)
                .SetPositiveButton("Add", delegate
                {
                    insertSingleItem(content.Text);
                    
                })
                .SetNegativeButton("Cancel", delegate
                {
                    addDialog.Dispose();
                });
            Android.Support.V7.App.AlertDialog adialog = addDialog.Create();
            addDialog.Show();
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