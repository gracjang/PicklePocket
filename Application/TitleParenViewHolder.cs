using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.Widget;
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

        public TitleParenViewHolder(View view) : base(view)
        {

            mTextView = view.FindViewById<TextView>(Resource.Id.parent_title);
            mImageButton = view.FindViewById<ImageButton>(Resource.Id.imageButton1);
            context = view.Context;
            
            mImageButton.Click += delegate
            {
                Intent i = new Intent(context, typeof(AddActivity));
                i.PutExtra("name", mTextView.Text);
                ((Activity)context).StartActivityForResult(i, MainActivity.RequestCode);
            };
          
        }

       
    }
  }
