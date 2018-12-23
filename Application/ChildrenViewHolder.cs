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
using XamDroid.ExpandableRecyclerView;

namespace Application
{
    public class ChildrenViewHolder: ChildViewHolder
    {
        public TextView option1;

        public ChildrenViewHolder(View view) : base(view)
        {
            option1 = view.FindViewById<TextView>(Resource.Id.children);
            
        }
    }
}