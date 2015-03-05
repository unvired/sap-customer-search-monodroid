using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Unvired.Kernel.Login;
using Unvired.Kernel.Database;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using Unvired.Kernel.Util;
using Unvired.Kernel.Log;
using AndroidSample.Utils;

namespace AndroidSample
{
    [Activity(Label = "Customer Search", Theme = "@style/Theme.Splash", MainLauncher = true, Icon = "@drawable/AppIcon", NoHistory = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : Activity, LoginListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Login parameters required to authenticate with Unvired Mobile Platform
            LoginParameters.AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().ToString();
            LoginParameters.AssemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            LoginParameters.Url = "live.unvired.io/";
            LoginParameters.AppTitle = "Unvired SAP Sample";
            LoginParameters.AppName = "UNVIRED_SAMPLE_SAP_ERP";
            LoginParameters.showCompany = true;

            LoginParameters.Protocol = LoginParameters.PROTOCOL.https;
            LoginParameters.LoginTypes = new LoginParameters.LOGIN_TYPE[] { LoginParameters.LOGIN_TYPE.UNVIRED_ID, LoginParameters.LOGIN_TYPE.ADS, LoginParameters.LOGIN_TYPE.SAP };

            LoginParameters.DeviceType = LoginParameters.DEVICE_TYPE.ANDROID_PHONE;
            LoginParameters.LoginListener = this;

            using (StreamReader sr = new StreamReader(Assets.Open("MetaData.xml")))
            {
                string content = sr.ReadToEnd();
                LoginParameters.MetaDataXml = XDocument.Parse(content);
            }

            //To support SQLCipher add SQLCipher component into project and set EncryptDataBase ti true
            // LoginParameters.EncryptDataBase = true;

            Util.InitializeNative(this);
            AuthenticationService.Login();
        }



        private void NavigateToAppScreen()
        {
            var intent = new Intent(this, typeof(AppScreen));
            StartActivity(intent);
        }

        public void LoginSuccessful()
        {
            NavigateToAppScreen();
        }

        public void AuthenticateAndActivationSuccessful()
        {
            AuthenticationService.UpdateClientCredential();
            NavigateToAppScreen();
        }

        public void LoginCancelled() { }  
        public void LoginFailure(string errorMessage) { }
        public void AuthenticateAndActivationFailure(string errorMessage){} 
        public void DemologinSuccessful(){}     
        public void InvokeLoginScreen(bool isSuccessiveLogin) { }
    }
}

