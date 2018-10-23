using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget ;
using XamDroid.ExpandableRecyclerView;

namespace App1.Models
{
    class TitleParent : IParentObject
    {
        Guid id;
        List<Object> _childrenList;
        public string Title { get; set; }
        public List<object> ChildObjectList
        {
            get { return _childrenList; }
            set { _childrenList = value; }
        }
        public Guid Id
        {
            get { return id; }
            private set { }
        }
        public TitleParent ()
        {
            id = Guid.NewGuid();
        }
    }
}