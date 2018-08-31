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
using LocalLendApp.DataAccess;

namespace LocalLendApp.Adapters
{
    class ItemAdapter : BaseAdapter<Item>
    {

        Context context;
        public List<Item> ItemList { get; }
        //private Action<ImageView> ActionImageSelected;

        public ItemAdapter(Context context, List<Item> items/*, Action<ImageView> imageSelected*/)
        {
            this.context = context;
            ItemList = items;
            //ActionImageSelected = imageSelected;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var itemInfoRowView = convertView;
            ItemAdapterViewHolder itemViewHolder = null;
            if (itemInfoRowView == null)
            {
                var inflator = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                itemInfoRowView = inflator.Inflate(Resource.Layout.BorrowListRow, parent, false);

                var itemImageView = itemInfoRowView.FindViewById<ImageView>(Resource.Id.imageViewBorrowItem);
                var lblItemNameView = itemInfoRowView.FindViewById<TextView>(Resource.Id.lblBorrowItemName);
                var lblItemDescription = itemInfoRowView.FindViewById<TextView>(Resource.Id.lblBorrowItemDescription);               

                itemViewHolder = new ItemAdapterViewHolder(itemImageView, lblItemNameView, lblItemDescription);

                itemInfoRowView.Tag = itemViewHolder;

                //itemImageView.Click += ItemImageView_Click;
            }

            itemViewHolder = itemInfoRowView.Tag as ItemAdapterViewHolder;
            itemViewHolder.ItemImage.SetImageResource(ItemList[position].ItemImage);
            itemViewHolder.ItemName.Text = ItemList[position].ItemName;
            itemViewHolder.ItemDescription.Text = ItemList[position].ItemDescription;

            //if (ItemList[position].ItemImage != null)
            //{
            //    cachedItemAdapterViewHolder.ItemImage.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeByteArray(ItemList[position].ItemImage, 0, ItemList[position].ItemImage.Length));
            //}         

            return itemInfoRowView;
        }

        //private void ItemImageView_Click(object sender, EventArgs e)
        //{
        //    ActionImageSelected.Invoke((ImageView)sender);
        //}

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return ItemList.Count;
            }
        }

        public override Item this[int position] => ItemList[position];
    }

}