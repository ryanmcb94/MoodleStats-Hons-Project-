
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
using MoodleStats;
using Android.Net;
using Android.Locations;
using System.Threading;
 

namespace MoodleStats.Droid
{
	[Activity (Label = "MoodleStats", MainLauncher = true, Icon = "@drawable/icon",NoHistory=true)]		
	public class winSplash : Activity
	{
		ConnectivityManager conMgr;
		bool serviceFound = false;
		DateTime startTime = DateTime.Now;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			this.SetContentView (Resource.Layout.winSplash);
			AppContext.setContext (this);
			this.TryConnection ();
				
		}

		protected void TryConnection()
		{
			try {
			//Get Network Info.
			this.conMgr = GetSystemService (Context.ConnectivityService) as ConnectivityManager;
			NetworkInfo activeConnection = conMgr.ActiveNetworkInfo;
			if (this.serviceFound == false && activeConnection.IsConnected) 
			{
				Controller.getController();
				this.serviceFound = true;
				//Create Location Thread.
				Toast.MakeText(this,"Loc Starting",ToastLength.Long).Show();
				LocationThread loc = new LocationThread((GetSystemService(Context.LocationService)as LocationManager),conMgr);
				Thread locThread = new Thread (loc.MainLoop);
				locThread.Start ();
				//Nav.
				StartActivity(typeof(MainActivity));
			}
			}catch(Exception e) 
			{
				Toast.MakeText (this, e.ToString (), ToastLength.Long).Show ();
			}
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			this.TryConnection ();
			Toast.MakeText (this, "Finding Connection", ToastLength.Long).Show ();
		}


			
	}
}

