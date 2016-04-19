using System;
using System.Runtime.Serialization;

namespace HonsService
{
    [DataContract]
    public class Campus
    {
        public int ID { get; set; }
        public string name { get; set; }
        public double north { get; set; }
        public double south { get; set; }
        public double east { get; set; }
        public double west { get; set; }

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
            if(lat < north && lat > south && lng > west && lng < east)
            {
                return true;
            }
            return false;

        }

		/// <summary>
		/// Creates the object from the locationDB
		/// </summary>
		/// <returns>The object.</returns>
		/// <param name="reader">Reader.</param>
        public static Campus createObject(MySql.Data.MySqlClient.MySqlDataReader reader)
        {
            return new Campus(int.Parse(reader["ID"].ToString()), reader["name"].ToString(), double.Parse(reader["north"].ToString()), double.Parse(reader["east"].ToString()), double.Parse(reader["south"].ToString()), double.Parse(reader["west"].ToString()));
            
        }


    }
}
