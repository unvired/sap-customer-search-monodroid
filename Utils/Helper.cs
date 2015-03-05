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

namespace AndroidSample.Utils
{
    class Helper
    {
        static AlertDialog dlgAlert;
        internal static void ShowExitPopup(Context c)
        {            
            dlgAlert = (new AlertDialog.Builder(c)).Create();
            dlgAlert.SetMessage("Do you want to Exit?");
            dlgAlert.SetTitle("Alert");
            dlgAlert.SetButton("OK", handllerNotingButton);
            dlgAlert.SetButton2("Cancel", CancelDialog);
            dlgAlert.Show();
        }

        static void handllerNotingButton(object sender, DialogClickEventArgs e)
        {
            AlertDialog objAlertDialog = sender as AlertDialog;
            Button btnClicked = objAlertDialog.GetButton(e.Which);
            if (btnClicked.Text.Equals("OK"))
            {
                objAlertDialog.Dismiss();
                System.Environment.Exit(0);
            }           
        }

        internal static void ShowSimpleMessageAlert(Context c ,string title,string message)
        {            
            dlgAlert = (new AlertDialog.Builder(c)).Create();
            dlgAlert.SetMessage(message);
            dlgAlert.SetTitle(title);
            dlgAlert.SetButton("OK", CancelDialog);
            dlgAlert.Show();
        }

        private static void CancelDialog(object sender, DialogClickEventArgs e)
        {
            AlertDialog objAlertDialog = sender as AlertDialog;
            objAlertDialog.Dismiss();
        }
    }
}