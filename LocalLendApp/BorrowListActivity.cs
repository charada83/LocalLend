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
using LocalLendApp.Adapters;
using LocalLendApp.DataAccess;

namespace LocalLendApp
{
    [Activity(Label = "BorrowListActivity")]
    public class BorrowListActivity : Activity
    {
        EditText txtSearch;
        ListView lvItems;
        List<Item> itemList = new List<Item>();
        //Button btnDeleteItem;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BorrowList);
            // Create your application here
            
            lvItems = FindViewById<ListView>(Resource.Id.lvViewItems);
            txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);
            //btnDeleteItem = FindViewById<Button>(Resource.Id.btnDeleteItem);

            LoadItemsFromDataStore();

            lvItems.Adapter = new ItemAdapter(this, itemList);     
            
            lvItems.ItemClick += LvItems_ItemClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // btnDeleteItem.Click += BtnDeleteItem_Click;
        }

        //private void BtnDeleteItem_Click(object sender, EventArgs e)
        //{
        //    Item item = new Item()
        //    {


        //    };
        //}     

        private void LoadItemsFromDataStore()
        {
            DBStore dbStore = new DBStore();

            IEnumerable<Item> items = dbStore.GetItems();
            itemList = items.ToList();

            //itemList.Add(new Item("Power Drill", "Powerful Tool", Resource.Drawable.powerdrill));
            //itemList.Add(new Item("Wheelbarrow", "Good condition, can lend for up to 3 days", Resource.Drawable.wheelbarrow));
        }
     

        private void LvItems_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var itemClickPosition = e.Position;
            var item = itemList[itemClickPosition] as Item;
            Intent getItem = new Intent(this, typeof(BorrowItemDetailActivity));
            getItem.PutExtra("itemName", item.ItemName.ToString());
            getItem.PutExtra("itemDescription", item.ItemDescription.ToString());
            getItem.PutExtra("itemImage", item.ItemImage);

            StartActivity(getItem);
        }

        private void TxtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var itemToLower = txtSearch.Text.ToLower();
            List<Item> searchedItems = (from item in itemList
                                        where item.ItemName.ToLower().Contains(itemToLower)
                                        select item).ToList<Item>();

            //itemAdapter = new ItemAdapter(this, searchedItems);
            //lvItems.Adapter = itemAdapter;

        }
    }
}