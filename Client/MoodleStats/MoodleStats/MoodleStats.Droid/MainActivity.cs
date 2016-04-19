using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using System.Threading;

namespace MoodleStats.Droid
{
	[Activity (Label = "MoodleStats")]
	public class MainActivity : Activity
	{
		private Button btnLogin;
		private Button btnCreateAccount;
		private ProgressBar pgsBar;
		private int view; //1 for login 2 for create.

		protected override void OnCreate (Bundle bundle)
		{
			try {
				base.OnCreate (bundle);
				AppContext.setContext(this);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			this.btnLogin = FindViewById <Button> (Resource.Id.btnLogin);
			this.btnCreateAccount = FindViewById<Button> (Resource.Id.btnCreateAccount);
			this.pgsBar = FindViewById<ProgressBar> (Resource.Id.pgsBar);
			this.pgsBar.Visibility = ViewStates.Invisible;

			this.btnLogin.Click += delegate {
				
				FragmentTransaction ft = FragmentManager.BeginTransaction();
				dialog_login login = new dialog_login();
				login.con= this;
				login.Show(ft,"dialog_login");
				this.view = 1;
			};

			this.btnCreateAccount.Click += delegate {
				FragmentTransaction ft = FragmentManager.BeginTransaction();
				dialog_createAccount createAccount = new dialog_createAccount();
					createAccount.con = this;
				createAccount.Show(ft,"dialog_createAccount");
				this.view = 2;
			};
			} catch
			{
			}
		}

		public override void OnWindowFocusChanged (bool hasFocus)
		{
			base.OnWindowFocusChanged (hasFocus);
			if (hasFocus)
			{
				if(this.view == 1)
				{
					
					this.pgsBar.Visibility = ViewStates.Visible;
					try
					{
						this.login();
					} catch(Exception e) 
					{
						Toast.MakeText (this, "Error Starting thread 'login' " + e.ToString(), ToastLength.Long);
					}
				}
				else if(this.view ==2)
				{
					try
					{
						this.register();
					} catch 
					{
					}
					this.pgsBar.Visibility = ViewStates.Visible;
				}
			}
		}

		public void login()
		{
			try {
				this.pgsBar.Visibility = ViewStates.Visible;
				Controller.getController().user = Controller.getController ().service.login (Controller.getController().getStore().username, Controller.getController().getStore().password);
				if (Controller.getController().user != null) 
				{
					this.pgsBar.Visibility = ViewStates.Invisible;
					this.StartActivity(typeof(winGraph));
				} else
					Toast.MakeText (this, "Invalid login", ToastLength.Long).Show ();
				
			}catch(Exception e) 
			{
				Toast.MakeText (this, e.ToString (), ToastLength.Long).Show ();
			}
		}

		public void register()
		{
			Store s = Controller.getController ().getStore ();
			MoodleStats.Controller.getController ().service.Register (s.StoreList[0].ToString(), s.StoreList[1].ToString(), s.StoreList[2].ToString(), s.StoreList[3].ToString(), s.StoreList[4].ToString(), s.StoreList[5].ToString());
			this.pgsBar.Visibility = ViewStates.Invisible;
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			AppContext.setContext (this);
		}

	}
}


