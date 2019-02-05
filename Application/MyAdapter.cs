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
using Application.Resources.DataHelper;
using Application.Resources.Model;
using Java.Lang;
using XamDroid.ExpandableRecyclerView;

namespace Application
{
    public class MyAdapter : ExpandableRecyclerAdapter<TitleParenViewHolder, ChildrenViewHolder>
    {
        LayoutInflater mInflater;
        private Context context;
        private View viewParent;

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
                context = viewParent.Context;
                Intent i = new Intent((Activity)context, typeof(EditActivity));
                i.PutExtra("NamePlace", childViewHolder.option1.Text);
                ((Activity)context).StartActivityForResult(i, MainActivity.RequestEditCode);

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
             viewParent = mInflater.Inflate(Resource.Layout.article_layout, parentViewGroup, false);
         
            return new TitleParenViewHolder(viewParent);
        }
        
    }
}