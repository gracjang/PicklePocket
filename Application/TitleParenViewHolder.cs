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
using Application.Resources.DataHelper;
using Application.Resources.Model;
using XamDroid.ExpandableRecyclerView;

namespace Application
{
    public class TitleParenViewHolder : ParentViewHolder
    {
        public TextView mTextView;
        public ImageButton mImageButton;
        public ImageButton ImageButtonDelete;
        private Context context;
        private DataBase db = new DataBase();
        public TitleParenViewHolder(View view) : base(view)
        {
            context = view.Context;
            mTextView = view.FindViewById<TextView>(Resource.Id.parent_title);
            ImageButtonDelete = view.FindViewById<ImageButton>(Resource.Id.imgButtonDelete);
            mImageButton = view.FindViewById<ImageButton>(Resource.Id.imageButton1);
            mTextView.Tag = mTextView.Id;
            

            mImageButton.Click += delegate
            {
                Intent i = new Intent(context, typeof(AddActivity));
                i.PutExtra("name", mTextView.Text);
                ((Activity)context).StartActivityForResult(i, MainActivity.RequestCode);
            };

           ImageButtonDelete.Click += delegate
            {

                City city = new City()
                {
                    Id = mTextView.Id,
                    Name = mTextView.Text

                };
                ///ogarnąć delete
                db.DeleteTablePerson(city);
                var name = db.selectOnePlaceFromCityString(city.Name);
                db.DeleteTableImages(name);
                MainActivity.IsDelete = true;
                ((MainActivity) this.context).CheckUpdatesData();
            };

        } 

       
    }
  }
