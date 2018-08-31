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
                                                                    "LocLend.db3");

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
                    //        ItemName = lend.txt
                    //        ItemDescription =
                    //        ItemImage =
                    //        };

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
                    TableQuery<Item> query = cxn.Table<Item>();
                    if (query.Count() == 0)
                    {
                        //cxn.Insert(new Item("Power Drill", "Powerful Tool", Resource.Drawable.powerdrill));
                        //cxn.Insert(new Item("Wheelbarrow", "Good condition, can lend for up to 3 days", Resource.Drawable.wheelbarrow));
                        cxn.Insert(item);
                    }
                    //cxn.Insert(item);
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