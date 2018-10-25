using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Application
{
    public class TitleAdapter : RecyclerView.Adapter
    {
        private List<Application.Title> titles = new List<Title>();

        public TitleAdapter(List<Application.Title> mTitles)
        {
            titles = mTitles;
        }

       

        class MyView : RecyclerView.ViewHolder
        {
            public View mMainView;
            public TextView classTitle;

            public MyView(View view) : base(view)
            {
                mMainView = view;
            }

           
        }

        public override int ItemCount
        {
            get
            {
                return titles.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyView myHolder = holder as MyView;
            myHolder.classTitle.Text = titles[position].Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View article = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.article_layout, parent,false);
            TextView mName = article.FindViewById<TextView>(Resource.Id.article_title);
            MyView view = new MyView(article) { classTitle = mName };
            return view;
        }
    }
}