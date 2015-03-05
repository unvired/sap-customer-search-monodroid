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
using System.Threading.Tasks;
using AndroidSample.ApplicationActivities;
using Unvired.Kernel.Database;
using Unvired.Kernel.Core;
using Entity;
using AndroidSample.Utils;
using Unvired.Kernel.Log;
using Unvired.Kernel.Login;
using Unvired.Kernel.Model;
using Unvired.Kernel.Sync;

namespace AndroidSample
{
    [Activity(Label = "Customer Search", Icon = "@drawable/logo", Theme = "@android:style/Theme.DeviceDefault.Light", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class AppScreen : Activity, NotificationListener
    {
        ListView customer_list_view;
        EditText local_search;
        IDataManager datamanager;
        IEnumerable<CUSTOMERS_RESULTS_HEADER> customers_list;
        IEnumerable<CUSTOMERS_RESULTS_HEADER> filtered_list;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
            SetContentView(Resource.Layout.AppScreen);
           
			customer_list_view = FindViewById<ListView>(Resource.Id.CustomersList);
            customer_list_view.FastScrollEnabled = true;
            local_search = FindViewById<EditText>(Resource.Id.LocalSearchField);
            local_search.TextChanged += (sender, e) =>
            {
                RefreshList((e.Text).ToString());
            };
            customer_list_view.ItemClick += OnListItemClick;          

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.AppActivityActions, menu);
            var search = menu.FindItem(Resource.Id.action_search);           
            return true; 
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_search:
                    NavigateToOnlineSearch();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            SyncEngine.Instance.RegisteredNotificationListener = this;
            LoadList();
        }


        private async void LoadList()
        {
            customer_list_view.Adapter = null;
            datamanager = ApplicationManager.Instance.GetDataManager();
            customers_list = await datamanager.Get<CUSTOMERS_RESULTS_HEADER>();
            if (customers_list != null && customers_list.Count() > 0)
            {
                List<CUSTOMERS_RESULTS_HEADER> newlist = customers_list.OrderBy(x => x.NAME).ToList();
                customer_list_view.Adapter = new CustomerScreenAdapter(this, newlist);
                RefreshList("");
            }
        }

        private  void RefreshList(string hint)
        {
            customer_list_view.Adapter = null;
            filtered_list = customers_list.Where(x => x.NAME.ToLower().Contains(hint.ToLower()) || x.STREET.ToLower().Contains(hint.ToLower()));
            customer_list_view.Adapter =  new CustomerScreenAdapter(this, filtered_list.ToList());
        }

        public void NavigateToOnlineSearch()
        {
            try
            {
                Intent i = new Intent(this, typeof(OnlineSearch));
                StartActivity(i);
                OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.left_out);
            }
            catch (Exception)
            {                
               
            }
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = filtered_list.ToList()[e.Position];
            Constants.SelectedCustomer = t;
            Intent i = new Intent(this, typeof(CustomerDetails));
            StartActivity(i);
        }

        public void NotifyApplicationReset()
        {
            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
           
        }

		public void NotifyDataChange(List<DataNotification> dataNotifications){}
		public void NotifyDataReceiveCompletion(){}
		public void NotifyDataSend(OutObject outObject){}
		public void NotifyServerMessages(List<InfoMessage> infoMessages){}
		public void NotifyOutBoxItemDiscarded(InfoMessage infoMessage){}
      	public void NotifyAttachmentDownloadFailure(AttachmentItem attachmentItem, string errorMessage){}
      	public void NotifyAttachmentDownloadSuccess(AttachmentItem attachmentItem){}
      	public void NotifyAttachmentOutBoxItemDiscarded(InfoMessage infoMessage){}
      	public void NotifyDemoMode(string demoMessage){}
      	public void NotifyQueryFunctionAdded(string functionName){}
		public void NotifyQueryFunctionRemoved(string functionName){}
        
	}
}