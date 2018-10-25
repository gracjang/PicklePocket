using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections;
using System.Collections.Generic;

namespace Application
{
    

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            List<Title> mTitles = new List<Title>();
            mTitles.Add(new Application.Title() { Name = "Barcelona" });
            mTitles.Add(new Application.Title() { Name = "Madryt" });
            mTitles.Add(new Application.Title() { Name = "Warszawa" });
            mTitles.Add(new Application.Title() { Name = "Poznań" });
            mTitles.Add(new Application.Title() { Name = "Kraków" });
            mTitles.Add(new Application.Title() { Name = "Wałbrzych" });
            mTitles.Add(new Application.Title() { Name = "Wrocław" });
            mTitles.Add(new Application.Title() { Name = "Ciechocinek" });
            mTitles.Add(new Application.Title() { Name = "New York" });
            mTitles.Add(new Application.Title() { Name = "Oslo" });
            mTitles.Add(new Application.Title() { Name = "Berlin" });
            mTitles.Add(new Application.Title() { Name = "Gdańsk" });

            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new TitleAdapter(mTitles);
            mRecyclerView.SetAdapter(mAdapter);
        }
    }
}