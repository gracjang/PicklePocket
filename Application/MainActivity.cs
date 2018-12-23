using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections;
using System.Collections.Generic;
using System;
using Android.Support.Design.Widget;
using Android.Views;
using ButtonCircle.FormsPlugin.Droid;
using XamDroid.ExpandableRecyclerView;

namespace Application
{
    

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private MyAdapter mAdapter;
        private List<IParentObject> parentObject = new List<IParentObject>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            /*
            mAdapter = new MyAdapter(this, parentObject);
            mAdapter.SetParentClickableViewAnimationDefaultDuration();
            mAdapter.ParentAndIconExpandOnClick = true;
            mRecyclerView.SetAdapter(mAdapter);
            */
            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += delegate { ShowDialog(); };
        }

        private void insertSingleItem(string data)
        {

            TitleCreator titleCreator = new TitleCreator();
            titleCreator.Add(data);
            var titles = titleCreator.GetAll;


            foreach (var title in titles)
            {
                var childList = new List<Object>();
                for (int i = 1; i <= 5; i++)
                {
                    childList.Add(new TitleChild($"Child{i}"));
                    title.ChildObjectList = childList;
                }


                
                if (mAdapter == null)
                {
                    parentObject.Add(title);
                    mAdapter = new MyAdapter(this, parentObject);
                    mAdapter.SetParentClickableViewAnimationDefaultDuration();
                    mAdapter.ParentAndIconExpandOnClick = true;
                    mRecyclerView.SetAdapter(mAdapter);
                }
                else
                {
                    parentObject.Add(title);
                    mAdapter = new MyAdapter(this, parentObject);
                    mAdapter.SetParentClickableViewAnimationDefaultDuration();
                    mAdapter.ParentAndIconExpandOnClick = true;
                    mRecyclerView.SetAdapter(mAdapter);
                }
                
            }
            

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

        /*  private List<IParentObject> InitData()
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