using System;
using System.Runtime.Serialization;

namespace MoodleObjects
{
    [DataContract]
    public class Campus
    {
        [DataMember]
        int ID { get; set; }
        [DataMember]
        string name { get; set; }
        [DataMember]
        double north { get; set; }
        [DataMember]
        double south { get; set; }
        [DataMember]
        double east { get; set; }
        [DataMember]
        double west { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID">ID of the campus</param>
        /// <param name="name">name of the campus</param>
        /// <param name="north">lat of the most north point</param>
        /// <param name="east">lng of the most east point</param>
        /// <param name="south">lat of the most south point</param>
        /// <param name="west">lng of the most west point</param>
        public Campus(int ID,string name, double north, double east, double south, double west)
        {
            this.ID = ID;
            this.name = name;
            this.north = north;
            this.east = east;
            this.south = south;
            this.west = west;
        }

        /// <summary>
        /// Checks if the location is within the campus
        /// </summary>
        /// <param name="lat">latitude</param>
        /// <param name="lng">longatude</param>
        /// <returns>True if is within the area</returns>
        public bool isIn(double lat,double lng)
        {
            if(lat > west && lat < east && lng > south && lng < north)
            {
                return true;
            }
            return false;

        }




    }
}
