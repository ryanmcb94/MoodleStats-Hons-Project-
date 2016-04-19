
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

namespace MoodleStats.Droid
{
	public class dialog_createAccount : DialogFragment
	{
		Button btnCreate;
		EditText txtfName;
		EditText txtlName;
		EditText txtEmail;
		EditText txtPassword;
		EditText txtNewPassword;
		EditText txtLocation;
		public Context con{ get; set;}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.dialog_createAccount,container,false);
			this.btnCreate = view.FindViewById<Button> (Resource.Id.btnCreateAccountDialog);
			this.txtfName = view.FindViewById <EditText> (Resource.Id.txtfName);
			this.txtlName = view.FindViewById<EditText> (Resource.Id.txtlName);
			this.txtEmail = view.FindViewById<EditText> (Resource.Id.txtEmail);
			this.txtPassword = view.FindViewById<EditText> (Resource.Id.txtMoodlePassword);
			this.txtNewPassword = view.FindViewById<EditText> (Resource.Id.txtNewPassword);
			this.txtLocation = view.FindViewById<EditText> (Resource.Id.txtLocation);

			this.btnCreate.Click += delegate {
				string msg = "";
				if(this.txtfName.Text =="") msg="Enter First Name"; else if(this.txtlName.Text=="") msg="Enter Last name";else if(this.txtEmail.Text=="") msg="enter email";else if(this.txtPassword.Text=="")msg="enter moodle password";
				else if(this.txtNewPassword.Text=="")msg="enter apps password";else if(this.txtLocation.Text=="")msg="Enter town or city";
				else //Create
				{
					Controller.getController().service.Register(txtEmail.Text,txtPassword.Text,txtNewPassword.Text,txtfName.Text,txtlName.Text,txtLocation.Text);
				}

				if(msg!="")
				{
					Toast.MakeText(Activity,msg,ToastLength.Long).Show();
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

