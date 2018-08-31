using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;

namespace LocalLendApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //DrawerLayout navDrawer;
        //List<string> leftNavDrawer = new List<string>();
        //ArrayAdapter leftNavAdapter;
        //ListView lvNavDrawer;


        TextView lblTitle;
        Button btnBorrow;
        Button btnLend;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            lblTitle = FindViewById<TextView>(Resource.Id.lblTitle);
            btnBorrow = FindViewById<Button>(Resource.Id.btnBorrow);
            btnLend = FindViewById<Button>(Resource.Id.btnLend);

            btnBorrow.Click += BtnBorrow_Click;
            btnLend.Click += BtnLend_Click;

        }

        private void BtnLend_Click(object sender, System.EventArgs e)
        {
            //Look into more
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            LendDialog lendDialog = new LendDialog();
            lendDialog.Show(transaction, "lendDialog fragment");
        }

        private void BtnBorrow_Click(object sender, System.EventArgs e)
        {
            Intent showBorrowList = new Intent(this, typeof(BorrowListActivity));
            StartActivity(showBorrowList);
        }
    }
}

