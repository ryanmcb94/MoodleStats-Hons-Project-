using System;
using MySql.Data.MySqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using CryptSharp;
using MoodleObjects;
using System.Net;
using System.IO;
using System.Web;
using System.Runtime.Serialization.Json;

namespace HonsService
{
    public class MoodleDB:Database
	{
        //Declare Variable.
        private static MoodleDB moodleDB;
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="port"></param>
        /// <param name="name"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
		private MoodleDB (string url,string port,string name, string username,string password):base(url,port,name,username,password)
		{
            connect();
		}

        /// <summary>
        /// Get the database
        /// </summary>
        /// <returns>MoodleDB Object</returns>
		public static MoodleDB getMoodleDB() 
		{
			if (moodleDB == null) 
			{
                moodleDB = new MoodleDB("52.29.133.101", "3306", "moodle", "Ryan", "Pa$$w0rd");
			}
			return moodleDB;
		}

        /********************
        *GET From Database
        **********************/
        /// <summary>
        /// Get a User from the moodle DB
        /// </summary>
        /// <param name="ID">ID of the User</param>
        /// <returns>Moodle User</returns>
		public MoodleUser getUser(int ID) 
		{
			MoodleUser user=null;
			string query = String.Format("SELECT * FROM mdl_User WHERE id = {0}",ID);
			using (var reader = this.runQuery(query))
			{
				while (reader.Read()) 
				{
                    user = this.getUserObject(reader);
				}
			}
            return user;

		}

        public int getUser(string username,string password)
        {
            int id =-1;
            JsonReponce responce;
            string URLRequest = String.Format("http://ryanmcbroom.eu/Moodle/login/token.php?username={0}&password={1}&service=moodle_mobile_app", username, password);
            HttpWebRequest request = WebRequest.Create(URLRequest) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(JsonReponce));
                object jsonResponce = json.ReadObject(response.GetResponseStream());
                responce = jsonResponce as JsonReponce;
                Console.WriteLine(responce.token);
            }
            if (responce.token == "bfe96f977536fb363fd63d870bed5344")
            {
                string query = String.Format("SELECT ID from mdl_user where username = '{0}'", username);
                using(MySqlDataReader reader = this.runQuery(query))
                {
                    if(reader.Read())
                    {
                        id = int.Parse(reader["id"].ToString());
                    }
                }
                
            }
            return id;
        }

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="ID">ID of the user</param>
        /// <returns>A Moodle user Object</returns>
        public MoodleUser getUserByID(int ID)
       {
            MoodleUser user = null;
            string query = String.Format("SELECT * FROM mdl_user WHERE ID ={0} ",ID);

            using (var reader = this.runQuery(query))
            {
                while(reader.Read())
                {
                    user = getUserObject(reader);
                }
                
            }
            user.courses = MoodleDB.getMoodleDB().getUsersCourses(user);
            return user;

        }

		/// <summary>
		/// Validates the login.
		/// </summary>
		/// <returns><c>true</c>,if login was validated</returns>
		/// <param name="user">User.</param>
		public Boolean validateLogin(MoodleUser user)
		{
            /*****
            string hashPassword = this.hashPassword (user.password);
			string query = String.Format ("SELECT * FROM mdl_user WHERE username={0} AND Password={1}", user.username, hashPassword);
		    MySqlCommand cmd = new MySqlCommand (query, this.connection);
			MoodleUser returnedUser=null;

			using (var reader = cmd.ExecuteReader ()) 
			{
				while (reader.Read()) 
				{
					returnedUser = MoodleUser.getUserObject(reader);
				}
			}

			if (returnedUser != null && returnedUser.ID == user.ID) 
			{
				return true;
			} 
			return false;
            ***/
            return false;
		}

        /// <summary>
        /// Gets a Course by ID
        /// </summary>
        /// <param name="ID">ID of the Course</param>
        /// <returns></returns>
        public MoodleCourse getCourse(int ID)
		{
			string query = String.Format ("SELECT * FROM mdl_course WHERE Id={0}", ID);
			MoodleCourse course=null;
			using (var reader = this.runQuery (query))
			{
				while (reader.Read ()) 
				{
					course = getCourseObject(reader);
				}
			}
            return course;
		}

        /// <summary>
        /// Gets all the users from a course.
        /// </summary>
        /// <param name="courseID">This ID of the Course</param>
        /// <returns>A List of MoodleUser objects</returns>
        public List<MoodleUser> getUsersByCourse(int courseID)
        {
            List<MoodleUser> users = new List<MoodleUser>();
            string query = String.Format("SELECT * FROM mdl_user INNER JOIN mdl_user_enrolments  ON mdl_user.id=mdl_user_enrolments.userid WHERE  enrolid={0}",courseID);
            using (var reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    users.Add(this.getUserObject(reader));
                }
            }
            return users;
        }

        /// <summary>
        /// Gets all of todays events. This event should be run just before midnight each day.
        /// </summary>
        /// <returns>A List of MoodleEvent objects.</returns>
        public List<MoodleEvent> getDaysEvents()
        {
            List<MoodleEvent> todaysEvents = new List<MoodleEvent>();
            //string query = String.Format("SELECT * FROM mdl_logstore_standard_log WHERE timecreated > {0} ORDER BY userID",this.toUnix(new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,1,0)).TotalSeconds);
            string query = String.Format("SELECT * FROM mdl_logstore_standard_log WHERE userID >1 ORDER BY userID");
            
            using (var reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    todaysEvents.Add(this.getEventObject(reader));
                }
            }
            return todaysEvents;
        }

        /// <summary>
        /// gets uses if they have on or more matches and add them once for each match;
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<MoodleUser> getUserWithSameModules(MoodleUser user) 
        {
            List<MoodleUser> users = new List<MoodleUser>();
            user.courses = this.getUsersCourses(user);
            string query = String.Format("SELECT * FROM mdl_user_enrolments JOIN mdl_enrol on mdl_user_enrolments.enrolid=mdl_enrol.id WHERE mdl_enrol.courseID IN (");

            for (int i = 0; i < user.courses.Count; i++)
            {
                if (i == 0)
                {
                    query = query  + user.courses[i].ID;
                }
                else
                {
                    query = query + ", " + user.courses[i].ID;
                }
            }
            query = query + ") AND mdl_user_enrolments.userID !=" + user.ID;
            List<int> userIDs = new List<int>();
            using (var reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    int ID = int.Parse(reader["userID"].ToString());
                    if(!userIDs.Contains(ID))
                    userIDs.Add(ID);
                }
            }
            foreach(int i in userIDs)
            {
                bool found = false;
                foreach(MoodleUser moUser in users)
                {
                    if(i == moUser.ID)
                    { found = true; }
                }
                if(!found)
                users.Add(this.getUserByID(i));
            }
            return users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moUsers"></param>
        /// <returns></returns>
        public List<MoodleUser> getUserDetails(List<MoodleUser> moUsers)
        { 
            List<MoodleUser> users = new List<MoodleUser>();
            foreach(MoodleUser user in moUsers)
            {
                string query = String.Format("SELECT * FROM mdl_user where id={0}", user.ID);
                using (var reader = this.runQuery(query))
                {
                    while (reader.Read())
                    {
                        users.Add(getUserObject(reader));
                    }
                }
            }
            return users;
        }

        public List<MoodleCourse> getUsersCourses(MoodleUser user)
        {
            List<MoodleCourse> courses = new List<MoodleCourse>();
            string query = String.Format("SELECT * FROM mdl_user_enrolments JOIN mdl_enrol on mdl_user_enrolments.enrolid=mdl_enrol.id WHERE mdl_user_enrolments.userID != {0}", user.ID);
            List<int> courseIDs = new List<int>();
            using (var reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    int ID = int.Parse(reader["courseID"].ToString());
                    if(!courseIDs.Contains(ID))
                    courseIDs.Add(ID);
                }
            }
            foreach(int i in courseIDs)
            {
                courses.Add(this.getCourse(i));
            }
            return courses;
        }




        public bool test()
        {
            string query = "SELECT * FROM mdl_logstore_standard_log WHERE timecreated >= 1453500000";
            //"SELECT * FROM mdl_logstore_standard_log WHERE timecreated <  && timecreated >  ORDER BY userID";
            MySqlDataReader reader  =this.runQuery(query);
            return reader.HasRows;
        }
    }




} //Close Class.

