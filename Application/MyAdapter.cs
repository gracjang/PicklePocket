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
using Java.Lang;
using XamDroid.ExpandableRecyclerView;

namespace Application
{
    public class MyAdapter : ExpandableRecyclerAdapter<TitleParenViewHolder, ChildrenViewHolder>
    {
        LayoutInflater mInflater;


        public MyAdapter(Context context, List<IParentObject> itemList) :base(context,itemList)
        {
            mInflater = LayoutInflater.From(context);

        }

        public override void OnBindChildViewHolder(ChildrenViewHolder childViewHolder, int position, object childObject)
        {
            var title = (TitleChild)childObject;
            childViewHolder.option1.Text = title.mOption1;

            childViewHolder.option1.Click += delegate
            {
                    childViewHolder.option1.Text = "Click";
                
            };
           
        }

       

        public override void OnBindParentViewHolder(TitleParenViewHolder parentViewHolder, int position, object parentObject)
        {
            var title = (TitleParent)parentObject;
            
            parentViewHolder.mTextView.Text = title.Title;
            
        }

      


        public override ChildrenViewHolder OnCreateChildViewHolder(ViewGroup childViewGroup)
        {
            var view = mInflater.Inflate(Resource.Layout.children_layout, childViewGroup, false);
            return new ChildrenViewHolder(view);
        }

        public override TitleParenViewHolder OnCreateParentViewHolder(ViewGroup parentViewGroup)
        {
            var view = mInflater.Inflate(Resource.Layout.article_layout, parentViewGroup, false);
         
            return new TitleParenViewHolder(view);
        }
        
    }
}