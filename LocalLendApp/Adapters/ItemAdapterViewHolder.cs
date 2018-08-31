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

namespace LocalLendApp.Adapters
{
    class ItemAdapterViewHolder : Java.Lang.Object
    {
        public ImageView ItemImage { get; }
        public TextView ItemName { get; }
        public TextView ItemDescription { get; }

        public ItemAdapterViewHolder(ImageView itemImage, TextView itemName,
                                       TextView itemDescription)
        {
            ItemImage = itemImage;
            ItemName = itemName;
            ItemDescription = itemDescription;
        }

    }
}