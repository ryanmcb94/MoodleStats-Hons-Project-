using System;
using System.Runtime.Serialization;


namespace MoodleObjects
{
	[DataContract]
	public class LocationEvent 
	{
		public int userID { get; set;}
		public DateTime time {get;set;}
		public double lat  { get; set; }
		public double lng { get; set; }


        public LocationEvent(int userID, DateTime time, double lat, double lng)
        {
            this.userID = userID;
            this.time = time;
			this.lat = lat;
			this.lng = lng;
        }



    }
}

