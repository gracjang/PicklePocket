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
        
        public TitleParenViewHolder(View view ) : base(view)
        {
            mTextView = view.FindViewById<TextView>(Resource.Id.parent_title);
        }
    }
}