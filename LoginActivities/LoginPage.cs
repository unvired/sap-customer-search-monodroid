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
using Unvired.Kernel.Login;
using Unvired.Kernel.Database;
using AndroidProject;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using Unvired.Kernel.UI;
using System.Threading.Tasks;
using AndroidSample.Utils;

namespace AndroidSample
{
    [Activity(Label = "Login", Icon = "@drawable/logo", Theme = "@android:style/Theme.Holo.Light", WindowSoftInputMode = SoftInput.AdjustPan, NoHistory = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class LoginPage : Activity
    {

        public IDataManager appDataManager = null;

        ImageView login_type_image;
        TextView heading_label;
        TextView urlprefix_label;
        TextView url_label;
        TextView final_url;
        TextView domainLabel;
        TextView company_label;
        Spinner url_prefix;

        EditText url;
        EditText domainTextField;
        EditText company_field;
        EditText username_field;
        EditText password_field;

        Button login_button;
        Button cancel_button;
        Button options_button;

        AlertDialog dlgAlert = null;
        List<string> login_type_collection;
        static ProgressDialog mDialog;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginPage);


            // Create your application here
            login_type_collection = new List<string>();
            login_type_collection.Add(Constants.Login_Type_MicrosoftLogin);


            login_type_image = FindViewById<ImageView>(Resource.Id.LoginTypeImage);
            heading_label = FindViewById<TextView>(Resource.Id.HeadingLabel);
            urlprefix_label = FindViewById<TextView>(Resource.Id.URLPrefixLabel);
            url_prefix = FindViewById<Spinner>(Resource.Id.URLPrefixSpinner);
            url_label = FindViewById<TextView>(Resource.Id.URLLabel);
            final_url = FindViewById<TextView>(Resource.Id.FinalURLLabel);
            domainLabel = FindViewById<TextView>(Resource.Id.DomainLabel);
            company_label = FindViewById<TextView>(Resource.Id.CompanyLabel);

            url = FindViewById<EditText>(Resource.Id.URLTextfield);
            domainTextField = FindViewById<EditText>(Resource.Id.DomainTextField);
            username_field = FindViewById<EditText>(Resource.Id.UserNameTextField);
            password_field = FindViewById<EditText>(Resource.Id.PasswordTextfield);
            company_field = FindViewById<EditText>(Resource.Id.CompanyTextField);

            login_button = FindViewById<Button>(Resource.Id.LoginButton);
            cancel_button = FindViewById<Button>(Resource.Id.CancelButton);
            options_button = FindViewById<Button>(Resource.Id.OptionsButton);

            if (LoginParameters.LoginMode == LoginParameters.LOGIN_MODE.UNVIRED_ID_LOCAL_AUTH)
            {
                url_prefix.Visibility = ViewStates.Gone;
                url.Visibility = ViewStates.Gone;
                final_url.Visibility = ViewStates.Gone;
                domainLabel.Visibility = ViewStates.Gone;
                domainTextField.Visibility = ViewStates.Gone;
                company_label.Visibility = ViewStates.Gone;
                company_field.Visibility = ViewStates.Gone;
                cancel_button.Visibility = ViewStates.Gone;
                options_button.Visibility = ViewStates.Gone;
                urlprefix_label.Visibility = ViewStates.Gone;
                url_label.Visibility = ViewStates.Gone;
            }

            login_button.Click += delegate { DoLogin(); };

            cancel_button.Click += delegate { ClearAll(); };

            url_prefix.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            url.TextChanged += delegate { URLChanged(); };

            options_button.Click += delegate { methodInvokeAlertDialogWithListView(); };

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.domains, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            url_prefix.Adapter = adapter;
            domainLabel.Visibility = ViewStates.Gone;
            domainTextField.Visibility = ViewStates.Gone;
        }

        public void DoLogin()
        {
            if (Validate())
            {
                LoginParameters.Url = final_url.Text;
                LoginParameters.Company = company_field.Text;
                mDialog = new ProgressDialog(this);
                mDialog.SetMessage("Please wait...");
                mDialog.SetCancelable(false);
                mDialog.Show();

                if (LoginParameters.CurrentLoginType == LoginParameters.LOGIN_TYPE.UNVIRED_ID)
                {
                    if (LoginParameters.LoginMode == LoginParameters.LOGIN_MODE.UNVIRED_ID_LOCAL_AUTH)
                        LoginCustomControl.LoginWithCredentialParameters(" ", username_field.Text, password_field.Text, null, " ", null);
                    else
                        LoginCustomControl.LoginWithCredentialParameters(LoginParameters.Url, username_field.Text, password_field.Text, null, LoginParameters.Company, null);
                }
                else
                    if (LoginParameters.CurrentLoginType == LoginParameters.LOGIN_TYPE.ADS)
                    {
                        LoginCustomControl.LoginWithCredentialParameters(LoginParameters.Url, username_field.Text, password_field.Text, domainTextField.Text, LoginParameters.Company, null);
                    }
                return;
            }
            return;
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(username_field.Text))
            {
                Android.Widget.Toast.MakeText(this, "Username cannot be empty", Android.Widget.ToastLength.Short).Show();
                return false;
            }
            if (string.IsNullOrEmpty(password_field.Text))
            {
                Android.Widget.Toast.MakeText(this, "Password cannot be empty", Android.Widget.ToastLength.Short).Show();
                return false;
            }

            if (LoginParameters.LoginMode == LoginParameters.LOGIN_MODE.UNVIRED_ID_LOCAL_AUTH)
                return true;

            if (string.IsNullOrEmpty(url.Text))
            {
                Android.Widget.Toast.MakeText(this, "URL cannot be empty", Android.Widget.ToastLength.Short).Show();
                return false;
            }

            if (string.IsNullOrEmpty(company_field.Text))
            {
                Android.Widget.Toast.MakeText(this, "Company cannot be empty", Android.Widget.ToastLength.Short).Show();
                return false;
            }
            if (LoginParameters.CurrentLoginType == LoginParameters.LOGIN_TYPE.ADS)
            {
                if (string.IsNullOrEmpty(domainTextField.Text))
                {
                    Android.Widget.Toast.MakeText(this, "Domain cannot be empty", Android.Widget.ToastLength.Short).Show();
                    return false;
                }
            }
            return true;
        }

        private void ClearAll()
        {
            username_field.Text = "";
            password_field.Text = "";
            url.Text = "";
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            string finalstring = "";
            Spinner spinner = (Spinner)sender;
            finalstring = spinner.GetItemAtPosition(e.Position).ToString() + "://" + url.Text;
            final_url.Text = finalstring;
        }

        private void URLChanged()
        {
            if (final_url.Text.Substring(0, 5).Equals("https"))
            {
                final_url.Text = "https://";
            }
            else
            {
                final_url.Text = "http://";
            }
            final_url.Text += url.Text;
        }

        void methodInvokeAlertDialogWithListView()
        {
            dlgAlert = (new AlertDialog.Builder(this)).Create();
            dlgAlert.SetTitle("Login Type(s)");
            var listView = new ListView(this);
            listView.Adapter = new AlertListViewAdapter(this, login_type_collection);
            listView.ItemClick += listViewItemClick;
            dlgAlert.SetView(listView);
            dlgAlert.Show();
        }

        void listViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ChangeLoginMode();
            dlgAlert.Cancel();
        }

        private void ChangeLoginMode()
        {
            if (login_type_collection != null)
            {
                if (LoginParameters.CurrentLoginType == LoginParameters.LOGIN_TYPE.UNVIRED_ID)
                {
                    login_type_collection.Remove(Constants.Login_Type_MicrosoftLogin);
                    login_type_collection.Add(Constants.Login_Type_UnviredID);
                    heading_label.Text = Constants.Login_Type_MicrosoftLogin;
                    LoginParameters.CurrentLoginType = LoginParameters.LOGIN_TYPE.ADS;
                    login_type_image.SetImageResource(Resource.Drawable.Microsoft);
                    domainLabel.Visibility = ViewStates.Visible;
                    domainTextField.Visibility = ViewStates.Visible;
                }
                else if (LoginParameters.CurrentLoginType == LoginParameters.LOGIN_TYPE.ADS)
                {
                    login_type_collection.Remove(Constants.Login_Type_UnviredID);
                    login_type_collection.Add(Constants.Login_Type_MicrosoftLogin);
                    heading_label.Text = Constants.Login_Type_UnviredID;
                    LoginParameters.CurrentLoginType = LoginParameters.LOGIN_TYPE.UNVIRED_ID;
                    login_type_image.SetImageResource(Resource.Drawable.logo);
                    domainLabel.Visibility = ViewStates.Gone;
                    domainTextField.Visibility = ViewStates.Gone;
                }

            }
        }
        public static void LoginNotSuccessfull(string message)
        {
            if (mDialog != null)
            {
                Context c = mDialog.Context;
                mDialog.Cancel();
                Helper.ShowSimpleMessageAlert(c, "Alert", message);
            }

        }

    }
}