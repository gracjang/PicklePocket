﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Application
{
    class TitleCreator
    {
        //static TitleCreator mTitleCreator;
        List<TitleParent> mTitleParent;

        //private string title;
        //List<TitleChild> mTitleChild;
        //private Context context;

        public TitleCreator()
        {
            mTitleParent = new List<TitleParent>();
        }

        public void Add(string name_of_city)
        {
            
            // mTitleChild = new List<TitleChild>();

            var title = new TitleParent()
            {
                Title = name_of_city
            };
            mTitleParent.Add(title);
           

        }
        public void Remove(string name_of_city)
        {

            // mTitleChild = new List<TitleChild>();

            var title = new TitleParent()
            {
                Title = name_of_city
            };
            mTitleParent.Remove(title);


        }
        public List<TitleParent> GetAll
        {
            get
            {
                return mTitleParent;
            }
            private set
            {

            }
        }
        
    }
}