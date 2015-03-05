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
using Entity;
using Java.Util.Logging;
using Unvired.Kernel.Core;
using Unvired.Kernel.Database;
using Unvired.Kernel.Sync;
using AndroidSample.Utils;
using Unvired.Kernel.Model;
using System.Threading;
using System.Threading.Tasks;

namespace AndroidSample.ApplicationActivities
{
    [Activity(Label = "Search in SAP", Icon = "@drawable/logo", Theme = "@android:style/Theme.DeviceDefault.Light", ParentActivity = typeof(AppScreen), ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class OnlineSearch : Activity
    {

        string inputxml = null;
        ListView listView = null;
        List<CUSTOMERS_RESULTS_HEADER> customers = new List<CUSTOMERS_RESULTS_HEADER>();
        EditText customer_name_field;
        EditText customer_number_field;
        Button search_button;
        ProgressDialog pd;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.OnlineSearch);
            ActionBar.SetDisplayShowHomeEnabled(true);
            customer_name_field = FindViewById<EditText>(Resource.Id.NameTextField);
            customer_number_field = FindViewById<EditText>(Resource.Id.NumberTextField);
            search_button = FindViewById<Button>(Resource.Id.OnlineSearchButton);
            listView = FindViewById<ListView>(Resource.Id.CustomersList);
            search_button.Click += delegate { GetCustomer(); };
            listView.ItemClick += OnListItemClick;  
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    NavigateUpTo(ParentActivityIntent);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
        
        public void GetCustomer()
        {
            string name = "";
            string number = "";

            bool flag = false;
            if (!string.IsNullOrEmpty(customer_name_field.Text.Trim()) && !customer_name_field.Text.Trim().Equals("\n"))
            {
                name = customer_name_field.Text.Trim();
                flag = true;
            }
            if (!string.IsNullOrEmpty(customer_number_field.Text.Trim()) && !customer_number_field.Text.Trim().Equals("\n"))
            {
                number = customer_number_field.Text.Trim();
                flag = true;
            }
            if (flag)
            {
                pd = ProgressDialog.Show(this, "Processing..", "Please wait", true);
                pd.SetProgressStyle(ProgressDialogStyle.Horizontal);
                 LoadList(name, number);                
            }
            else
                methodInvokeBaseAlertDialog("Error", "Please enter either name or number");
        }

        void methodInvokeBaseAlertDialog(string title,string msg)
        {
            var dlgAlert = (new AlertDialog.Builder(this)).Create();
            dlgAlert.SetMessage(msg);
            dlgAlert.SetTitle(title);
            dlgAlert.SetButton("OK", handllerNotingButton);
            dlgAlert.Show();
        }

        void handllerNotingButton(object sender, DialogClickEventArgs e)
        {
             (sender as AlertDialog).Cancel();
        }


        private void LoadList(string customer_name,string customer_number)
        {
            INPUT_CUSTOMER inputcust = new INPUT_CUSTOMER();
            inputcust.CUSTOMER_NUMBER = customer_number;
            inputcust.MC_NAME = customer_name;
            inputxml = inputcust.GetXML();            
            MakeServerCall();
        }

        public async void MakeServerCall()
        {
            try
            {
                ISyncResponse iSyncResponse = null;
                if (!string.IsNullOrEmpty(inputxml))
                {
                    iSyncResponse = await SyncEngine.Instance.SubmitInForeground(SyncConstants.MESSAGE_REQUEST_TYPE.PULL, null, inputxml, Constants.GET_CUSTOMER_PROCESS_AGENT_FUNCTION_NAME, true);
                }
                SyncBEResponse response = (SyncBEResponse)iSyncResponse;
                if (response.DataBEs != null && response.DataBEs.Count > 0)
                {                    
                   Finish();
                }
                else
                {                   
                    string error_msg = string.Empty;
                    foreach (var item in response.InfoMessages)
                    {
                        if (item.Category == "FAILURE")
                        {
                            error_msg += item.Message;
                        }
                    }
                    methodInvokeBaseAlertDialog("Search result", error_msg);
                }
            }
            catch(Exception ex)
            {
                methodInvokeBaseAlertDialog("Search result", ex.Message);
            }
            finally
            {
                pd.Dismiss();
            }
        }
      

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = customers.ToList()[e.Position];
            Constants.SelectedCustomer = t;
            Intent intent = new Intent(this, typeof(CustomerDetails));
            StartActivity(intent);
        }       
    }
}