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
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Application.Resources.Model
{
    [Table("City")]
    public class City
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int IdPlace { get; set; }

        public string Name { get; set; }
    }
}