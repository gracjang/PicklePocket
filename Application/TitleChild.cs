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
using XamDroid.ExpandableRecyclerView;

namespace Application
{
    class TitleChild
    {
        public string mOption1 { get; set; }

        public TitleChild(string option1)
        {
            mOption1 = option1;
        }
    }
}