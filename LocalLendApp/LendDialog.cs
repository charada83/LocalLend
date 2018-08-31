using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LocalLendApp.DataAccess;

namespace LocalLendApp
{
    class LendDialog : DialogFragment
    {

        DBStore database = new DBStore();

        private EditText txtEditItemName;
        private EditText txtEditItemDescription;
        private ImageView imgUpload;
        private Button btnAddImage;
        private Button btnAddItem;
        private Button btnCancel;
        //private ImageView selectedImage;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var lendView = inflater.Inflate(Resource.Layout.LendDialog, container, false);

            txtEditItemName = lendView.FindViewById<EditText>(Resource.Id.txtEditItemName);
            txtEditItemDescription = lendView.FindViewById<EditText>(Resource.Id.txtEditItemDescription);
            imgUpload = lendView.FindViewById<ImageView>(Resource.Id.imgUpload);
            btnAddImage = lendView.FindViewById<Button>(Resource.Id.btnAddImage);
            btnAddItem = lendView.FindViewById<Button>(Resource.Id.btnAddItem);
            btnCancel = lendView.FindViewById<Button>(Resource.Id.btnCancel);

            //Action<ImageView> action = ImageSelected;

            btnAddItem.Click += BtnAddItem_Click;
            btnCancel.Click += BtnCancel_Click;
            btnAddImage.Click += BtnAddImage_Click;
            return lendView;
        }

        //private void ImageSelected(ImageView imageSelected)
        //{
        //    selectedImage = imageSelected;
        //    Intent uploadImageIntent = new Intent();
        //    uploadImageIntent.SetType("image/*");
        //    uploadImageIntent.SetAction(Intent.ActionGetContent);
        //    this.StartActivityForResult(Intent.CreateChooser(uploadImageIntent, "Select an Image"), 200);
        //}

        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            Intent uploadImageIntent = new Intent(Intent.ActionPick, Android.Provider.MediaStore.Images.Media.ExternalContentUri);
            StartActivityForResult(uploadImageIntent, 200);
        }


        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if ((requestCode == 200) && (resultCode == Result.Ok))
            {
                //Stream stream = ContentResolver.OpenInputStream(data.Data);
                //selectedImage.SetImageBitmap(BitmapFactory.DecodeStream(stream));
                Android.Net.Uri selectedImage = data.Data;
                imgUpload.SetImageURI(selectedImage);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {

            Item item = new Item()
            {
                ItemName = txtEditItemName.Text,
                ItemDescription = txtEditItemDescription.Text,
                // ImageDrawableID = imgUpload
            };

            database.InsertIntoTableItem(item);
            Toast.MakeText(Activity, "Your item has been added", ToastLength.Long).Show();
            // Dismiss();


            //Byte[] newImage = imgUpload.ToArray<Byte>();
            //Item item = new Item()
            //{ItemName = txtEditItemName.Text, ItemDescription = txtEditItemDescription.Text, ImageDrawableID = newImage };

            //Intent addToBorrowList = new Intent(this, typeof(BorrowListActivity));
            //addToBorrowList.PutExtra("txtName", txtEditItemName.Text);
            //StartActivityForResult(addToBorrowList, 100);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);

        }

       
    }
}