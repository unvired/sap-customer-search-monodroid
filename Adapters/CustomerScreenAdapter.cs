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

namespace AndroidSample.Utils
{
    class CustomerScreenAdapter : BaseAdapter<CUSTOMERS_RESULTS_HEADER> 
    {
        List<CUSTOMERS_RESULTS_HEADER> items;
        Activity context;
        public CustomerScreenAdapter(Activity context, List<CUSTOMERS_RESULTS_HEADER> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override CUSTOMERS_RESULTS_HEADER this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.NAME;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.STREET;
            
            return view;
        }
    }
}