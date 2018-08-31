using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace LocalLendApp.DataAccess
{
    class DBStore
    {
        public static string DBLocation
        {
            get;
        }

        static DBStore()
        {
            if (string.IsNullOrEmpty(DBLocation))
            {
                DBLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                                                    "LocalLendDB.db3");

                InitialiseDB();
            }
        }

        private static void InitialiseDB()
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBLocation))
                {
                    cxn.DropTable<Item>();

                    cxn.CreateTable<Item>();
                    //TableQuery<Item> query = cxn.Table<Item>();
                    //if (query.Count() == 0)
                    //{
                    //    Item item = new Item()
                    //    {
                    //        ItemName =
                    //        ItemDescription =
                    //        ItemImage =
                    //    };

                    //    cxn.Insert(item);
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void InsertIntoTableItem(Item item)
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBStore.DBLocation))
                {
                    cxn.Insert(item);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<Item> SelectItemTable()
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBStore.DBLocation))
                {
                    return cxn.Table<Item>().ToList();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void UpdateItemTable(Item item)
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBStore.DBLocation))
                {
                    cxn.Query<Item>("UPDATE Item SET ItemName=?, ItemDescription=?, ItemImage=? " +
                        "WHERE ItemID=?", item.ItemName, item.ItemDescription, item.ItemImage, item.ItemID);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void DeleteTableItem(Item item)
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBStore.DBLocation))
                {
                    cxn.Delete(item);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public IEnumerable<Item> GetItems()
        {
            try
            {
                using (SQLiteConnection cxn = new SQLiteConnection(DBStore.DBLocation))
                {
                    IEnumerable<Item> items = cxn.Query<Item>("SELECT * FROM Item");

                    return items;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }
    }
}