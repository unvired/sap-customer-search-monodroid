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
using AndroidSample.Utils;

namespace AndroidSample.ApplicationActivities
{
    [Activity(Label = "Customer Details", Icon = "@drawable/logo", Theme = "@android:style/Theme.DeviceDefault.Light", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class CustomerDetails : Activity
    {
        TextView name;
        TextView street;
        TextView city1;
        TextView kunnr;
        TextView postcode1;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CustomerDetails);
            name = FindViewById<TextView>(Resource.Id.Name);
            street = FindViewById<TextView>(Resource.Id.street);
            city1 = FindViewById<TextView>(Resource.Id.City);
            kunnr = FindViewById<TextView>(Resource.Id.Kunnr);
            postcode1 = FindViewById<TextView>(Resource.Id.Postcode1);            
        }
        protected override void OnResume()
        {
            base.OnResume();

            if(Constants.SelectedCustomer != null)
            {
                name.Text = Constants.SelectedCustomer.NAME;
                street.Text = Constants.SelectedCustomer.STREET;
                city1.Text = Constants.SelectedCustomer.CITY1;
                kunnr.Text = Constants.SelectedCustomer.KUNNR;
                postcode1.Text = Constants.SelectedCustomer.POST_CODE1;
            }
        }
    }
}