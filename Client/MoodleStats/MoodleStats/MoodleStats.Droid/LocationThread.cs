using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Locations;
using Android.Nfc;
using Android.Net;
using Android.Widget;
using Android.Views;


namespace MoodleStats.Droid
{
	public class LocationThread : ILocationListener
	{
		//Define Variables
		string provider;
		public LocationManager locMgr { get; set; }
		public Location loc { get; set; }
		public ConnectivityManager conMgr;
		public List<Location> storedLocations { get; set; }

		public LocationThread(LocationManager mgr,ConnectivityManager conMgr)
		{
			Toast.MakeText (AppContext.getContext (), "Constructor LM", ToastLength.Short).Show ();
			this.storedLocations = new List<Location> ();
			this.locMgr = mgr;
			this.conMgr = conMgr;
			this.provider = LocationManager.GpsProvider;

			if (this.locMgr.IsProviderEnabled (provider)) {
				this.locMgr.RequestLocationUpdates (provider, 2000, 0, this);
			}
			else {
				
			}
		}

		public void MainLoop()
		{
			Toast.MakeText (AppContext.getContext (), "MainLoop LM", ToastLength.Short).Show ();
			while (true) 
			{
				sendLocation ();
				Thread.Sleep (600000);
			}
		}

		public void sendLocation()
		{
			NetworkInfo activeConnection = this.conMgr.ActiveNetworkInfo;
			if ((activeConnection != null) && activeConnection.IsConnected)
			{
				Location loc = this.locMgr.GetLastKnownLocation(this.provider);
				Controller.getController().service.addLocation(Controller.getController().user.ID,loc.Latitude,loc.Longitude);
			} 
		}

		#region ILocationListener implementation
		public void OnLocationChanged (Location location)
		{
			throw new NotImplementedException ();
		}
		public void OnProviderDisabled (string provider)
		{
			throw new NotImplementedException ();
		}
		public void OnProviderEnabled (string provider)
		{
			throw new NotImplementedException ();
		}
		public void OnStatusChanged (string provider, Availability status, Android.OS.Bundle extras)
		{
			throw new NotImplementedException ();
		}
		#endregion
		#region IDisposable implementation
		public void Dispose ()
		{
			throw new NotImplementedException ();
		}
		#endregion
		#region IJavaObject implementation
		public IntPtr Handle {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}