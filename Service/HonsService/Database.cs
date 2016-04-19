using System;
using MySql.Data.MySqlClient;
using MoodleObjects;

namespace HonsService
{
	public class Database
	{
		//Declare Variable
		public string dbURL {get;set;}
		public string dbPort { get; set; }
		public string dbName { get; set; }
		public string dbUsername { get; set; }
		public string dbPassword{ get; set; }
        protected MySqlConnection connection;

        /// <summary>
        /// Creates the database object and calls the connect method.
        /// </summary>
        /// <param name="url">URL/IP of the database</param>
        /// <param name="port">Port of the database</param>
        /// <param name="name">db name<param>
        /// <param name="username">db access username</param>
        /// <param name="password">db user password</param>
        public Database (string url, string port, string name, string username, string password) 
		{
			this.dbURL = url;
			this.dbPort = port;
			this.dbName = name;
			this.dbUsername = username;
            this.dbPassword = password;
		}
        /// <summary>
        /// Run the query.
        /// </summary>
        /// <param name="query">String with the query</param>
        /// <returns>Results</returns>
        protected MySqlDataReader runQuery(string query)
        {
            Console.WriteLine("Query: " + query);
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        protected void runQueryNonReturn(string query)
        {
            Console.WriteLine("Query: " + query);
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            using(MySqlDataReader reader = cmd.ExecuteReader())
            {

            }
        }

		/// <summary>
		/// Connect this instance.
		/// </summary>
        protected void connect()
        {
            string connect = String.Format("SERVER={0};PORT={1};DATABASE={2};UID={3};PASSWORD={4}", this.dbURL, this.dbPort, this.dbName, this.dbUsername, this.dbPassword);
            this.connection = new MySqlConnection(connect);
            this.connection.Open();
        }


        /*******
         * CREATE OBJECTS
         **********************/
        /// <summary>
        /// Creates the object from the locationDB
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="reader">Reader.</param>
        protected Campus getCampusObject(MySql.Data.MySqlClient.MySqlDataReader reader)
        {
            return new Campus(int.Parse(reader["ID"].ToString()), reader["name"].ToString(), double.Parse(reader["north"].ToString()), double.Parse(reader["east"].ToString()), double.Parse(reader["south"].ToString()), double.Parse(reader["west"].ToString()));
        }

        /// <summary>
        /// Creates this object using from the  database
        /// </summary>
        /// <param name="reader">MySQL Data Reader !You must run throught each index calling this method.</param>
        /// <returns>instance of this class</returns>
        protected  LocationAvg getLocationAvgObject(MySql.Data.MySqlClient.MySqlDataReader reader)
        {
            DateTime dt = Database.fromUnix(Convert.ToInt64(reader["locTime"].ToString()));
            return new LocationAvg(int.Parse(reader["UserID"].ToString()),dt , int.Parse(reader["inuni"].ToString()), int.Parse(reader["outUni"].ToString()));
        }

        /// <summary>
        /// Creates this object using from the database
        /// </summary>
        /// <param name="reader">MySQL data reader !You must run throught each index calling this method.</param>
        /// <returns>instance of this class</returns>
        protected  LocationAvg getLocationAvgObjectWithID(MySql.Data.MySqlClient.MySqlDataReader reader)
        {
            DateTime dt = Database.fromUnix(Convert.ToInt64(reader["locTime"].ToString()));
            return new LocationAvg(int.Parse(reader["UserID"].ToString()), int.Parse(reader["courseID"].ToString()), dt, int.Parse(reader["inuni"].ToString()), int.Parse(reader["outUni"].ToString()));
        }

        protected  LocationEvent getLocationEventObject(MySql.Data.MySqlClient.MySqlDataReader reader)
        {
            DateTime dt = Database.fromUnix(Convert.ToInt64(reader["locTime"].ToString()));
            LocationEvent evt = new LocationEvent(int.Parse(reader["userID"].ToString()),dt , double.Parse(reader["lat"].ToString()), double.Parse(reader["lng"].ToString()));
            return evt;
        }

        /// <summary>
        /// Transfers user from SQLObject to MoodleCourse Object
        /// </summary>
        /// <param name="reader">MySQLReader</param>
        /// <returns>MoodleCourse Object</returns>
        protected  MoodleCourse getCourseObject(MySqlDataReader reader)
        {
            MoodleCourse course;
            course = new MoodleCourse(int.Parse(reader["ID"].ToString()), reader["fullname"].ToString(), reader["shortname"].ToString(), reader["summary"].ToString());
            return course;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected  MoodleEvent getEventObject(MySqlDataReader reader)
        {
            MoodleEvent mooEvent;
            mooEvent = new MoodleEvent(int.Parse(reader["id"].ToString()), int.Parse(reader["userid"].ToString()), int.Parse(reader["courseid"].ToString()), reader["action"].ToString(), Database.fromUnix(Convert.ToInt64(reader["timecreated"])));
            return mooEvent;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected  MoodleUser getUserObject(MySqlDataReader reader)
        {
            MoodleUser user;
            user = new MoodleUser(int.Parse(reader["ID"].ToString()), reader["username"].ToString(), reader["password"].ToString(), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["city"].ToString(), reader["country"].ToString());
            return user;
        }

        public static long toUnix(DateTime dateTime)
        {

            return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }
        public static DateTime fromUnix(long seconds)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long ticks = (long)(seconds * TimeSpan.TicksPerSecond);
            return new DateTime(start.Ticks + ticks);
        }





	}
}

