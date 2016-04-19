using System;
using System.Threading;
using System.Threading.Tasks;
using CoreLocation;
using UIKit;

namespace MoodleStats.iOS
{
	public class LocationThread
	{
		public LocationThread ()
		{
			LocationManager manager = new LocationManager ();
			while (true) 
			{
				this.AddLocation (manager.locMgr.Location);
				Thread.Sleep (600000);
			}
		}
		public void AddLocation(CLLocation loc)
		{
			Controller.getController ().service.addLocation (Controller.getController ().user.ID, loc.Coordinate.Latitude, loc.Coordinate.Longitude);
		}

	}

	public class LocationManager
	{
		private static LocationManager manager;
		public CLLocationManager locMgr{get;set;}

		public LocationManager() 
		{
			this.locMgr = new CLLocationManager ();
			this.locMgr.PausesLocationUpdatesAutomatically = false;

			//iOS8 Fix
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) 
			{
				locMgr.RequestAlwaysAuthorization ();
			}
			//iOS9
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) 
			{
				this.locMgr.AllowsBackgroundLocationUpdates = true;
			}
			this.start ();
		}

		public void start()
		{
			if (CLLocationManager.LocationServicesEnabled)
			{
				this.locMgr.DesiredAccuracy = 1;
				this.locMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) => {
					//LocationUpdataed(this,new CLLocationUpdatedEventArgs(e.Locations [e.Locations.Length -1]));
				};
			}
		}
		public CLLocationManager getManager 
		{
			get {return this.locMgr;}
		}
	}
}