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
using XamDroid.ExpandableRecyclerView;

namespace Application
{
    public class TitleParenViewHolder : ParentViewHolder
    {
        public TextView mTextView;
        public ImageButton mImageButton;
        private Context context;
        
        public TitleParenViewHolder(View view ) : base(view)
        {
            
            mTextView = view.FindViewById<TextView>(Resource.Id.parent_title);
            mImageButton = view.FindViewById<ImageButton>(Resource.Id.imageView1);
            context = view.Context;
            mImageButton.Click += delegate
            {
                context.StartActivity(typeof(AddActivity));
                Toast.MakeText(this.context, "Click", ToastLength.Long).Show();
            };

        }
        private void AddCityButton_Click()
        {
           

        }
    }
}