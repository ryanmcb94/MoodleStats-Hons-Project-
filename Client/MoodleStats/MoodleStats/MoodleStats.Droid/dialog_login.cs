
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;

namespace MoodleStats.Droid
{
	public class dialog_login:DialogFragment
	{
		public Context con { get;set;}
		Button btnLogin;
		EditText txtUsername;
		EditText txtPassword;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = null;
			base.OnCreateView (inflater, container, savedInstanceState);
			view = inflater.Inflate (Resource.Layout.dialog_login,container, false);
			this.btnLogin = view.FindViewById<Button> (Resource.Id.btnLoginDialog);
			this.txtUsername = view.FindViewById<EditText> (Resource.Id.txtUsername);
			this.txtPassword = view.FindViewById<EditText> (Resource.Id.txtPassword);

			this.btnLogin.Click += delegate {
				string msg = "";
				if(this.txtUsername.Text == "") msg = "Enter Username"; 
				else if(this.txtPassword.Text == "") msg= "Enter Password";
				else 
				{
					Controller.getController().getStore().username=txtUsername.Text;
					Controller.getController().getStore().password=txtPassword.Text;
					this.Dismiss();
				}
				if(msg !="")
				{
					Toast.MakeText(this.con,msg,ToastLength.Long).Show();
				}
			};
			return view;
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			Dialog.Window.RequestFeature (WindowFeatures.NoTitle);
			base.OnActivityCreated (savedInstanceState);
		}
	}

}

