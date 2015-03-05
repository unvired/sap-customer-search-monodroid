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
    class Constants
    {
            public const string Login_Type_UnviredID = "Unvired Id Login";

            public const string Login_Type_MicrosoftLogin = "Microsoft(R) ADS Login";

            public static CUSTOMERS_RESULTS_HEADER SelectedCustomer = null;

        	public const string APPLICATION_NAME = "UNVIRED_SAMPLE_SAP_ERP";

	        public const string GET_CUSTOMER_PROCESS_AGENT_FUNCTION_NAME = "UNVIRED_SAMPLE_SAP_ERP_PA_GET_CUSTOMERS";
	
	        public const string GET_MATERIAL_PROCESS_AGENT_FUNCTION_NAME = "UNVIRED_SAMPLE_SAP_ERP_PA_GET_MATERIALS";

	        public const string CURRENT_ACTIVITY = "CURRENT_ACTIVITY";
	
	        public const string SELECTED_CUSTOMER = "SELECTED_CUSTOMER";

	}    
}