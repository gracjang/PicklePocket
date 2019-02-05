using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Application.Resources.Model;
using Java.Util.Concurrent;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms.Internals;
using Environment = System.Environment;
using Log = Android.Util.Log;

namespace Application.Resources.DataHelper
{
    public class DataBase
    {
        private string Folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder,"City.db")))
                {
                    connection.CreateTable<City>();
                    connection.CreateTable<Place>();
                    connection.CreateTable <Images>();
                    return true;
                }
              

            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return false;
            }
        }

       
        public bool InsterIntoTableCity(City city)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    connection.Insert(city);
                    //using (var conn = new SQLiteConnection(System.IO.Path.Combine(Folder, "Place.db")))
                   // {
                   //     conn.Insert(place);
                   // }
                        
                   // city.Places = new List<Place>{place};
                   // connection.UpdateWithChildren(city);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return false;
            }
        }

        public bool InsterIntoTablePlace(Place place)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    connection.Insert(place);


                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return false;
            }
        }

        public bool InsterIntoTableImage(Images images)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {

                    connection.Insert(images);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return false;
            }
        }

        public bool UpdateTableImages(Images images)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    connection.Query<Images>(
                        "UPDATE Images SET PlaceName=? WHERE ID =?", images.PlaceName, images.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return false;
            }
        }

        public List<City> selectTableCities()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                   
                    return connection.Table<City>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return null;
            }
        }

        public List<Images> selectTableImages()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {

                  
                    return connection.Table<Images>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return null;
            }
        }

        public List<Place> selectTablePlace()
        {
            try
            {
                
                using (var connection = new SQLiteConnection( System.IO.Path.Combine(Folder, "City.db")))
                {
                    
                    return connection.Table<Place>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return null;
            }
        }

        public List<Place> selectTablePlaceWhere(TitleParent title)
        {
            try
            {

                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    return connection.Table<Place>().Where(x => x.CityName == title.Title).ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return null;
            }
        }

        public List<Images> SelectTableImagesList(string name)
        {
            try
            {

                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    return connection.Table<Images>().Where(x => x.PlaceName == name).ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return null;
            }
        }

        public Place selectOnePlace(string namePlace)
        {
            try
            {

                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    return connection.Table<Place>().FirstOrDefault(x => x.Name == namePlace);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return null;
            }
        }

        public Place selectOnePlaceFromCityString(string namePlace)
        {
            try
            {

                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    return connection.Table<Place>().FirstOrDefault(x => x.CityName == namePlace);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return null;
            }
        }

        public City selectOneCity(string namePlace)
        {
            try
            {

                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    return connection.Table<City>().FirstOrDefault(x => x.Name == namePlace);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return null;
            }
        }

        public bool DeleteTablePerson(City city)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    //connection.Query<Place>("DELETE FROM Images Where PlaceName=?", place.CityName);
                    connection.Execute("DELETE FROM Place Where CityName=?", city.Name);
                    connection.Execute("DELETE FROM City Where Name=?", city.Name);
                    
                    
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return false;
            }
        }

        public bool DeleteTableImages(Place place)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    if (place != null)
                    {
                        connection.Query<Images>("DELETE * FROM Images Where PlaceName=?", place.CityName);
                    }
                    

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return false;
            }
        }

        public bool DropAllTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Folder, "City.db")))
                {
                    connection.Query<City>("DROP TABLE CITY");
                    connection.Query<Place>("DROP TABLE PLACE");
                    connection.Query<Place>("DROP TABLE IMAGES");
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return false;
            }
        }

        public bool UpdateTablePlace(Place place)
        {
            try
            {
                using (var connection = new SQLiteConnection(Path.Combine(Folder, "City.db")))
                {
                    connection.Query<Place>(
                        "UPDATE Place SET Name=?,Description=?,Rating=?,LocPlaceName=?,LocSecondPlaceName=? WHERE ID=?",
                        place.Name, place.Description, place.Rating, place.LocPlaceName, place.LocSecondPlaceName,
                        place.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLIte exception", ex.Message);
                return false;
            }
        }

    }
}