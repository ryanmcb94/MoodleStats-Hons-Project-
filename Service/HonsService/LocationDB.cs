using System;
using MoodleObjects;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HonsService
{
	public class LocationDB:Database
	{
		private static LocationDB locDB;
   
        /**********************
        *Constrcutors and instances
        *****************************/
        /// <summary>
        /// Call Base and creates link to DB
        /// </summary>
        /// <param name="dbURL"></param>
        /// <param name="dbPort"></param>
        /// <param name="dbName"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
		private LocationDB (string dbURL,string dbPort,string dbName, string user, string password):base(dbURL,dbPort,dbName,user,password)
		{
            this.connect();
		}

        /// <summary>
        /// Get the LocationDb Object
        /// </summary>
        /// <returns></returns>
		public static LocationDB getLocationDB() 
		{
			if(locDB == null)
			{
                locDB = new LocationDB("52.29.133.101", "3306", "MoodleLocation", "Ryan", "Pa$$w0rd");
			}
			return locDB;
		}

        /// <summary>
        /// Gets all the campus
        /// </summary>
        /// <returns>List of cmapus</returns>
        public List<Campus> getCampus()
        {
            string query = "SELECT * FROM Campus";
            List<Campus> campus = new List<Campus>();
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    campus.Add(Campus.createObject(reader));
                }
            }
            return campus;
        }

        /// <summary>
        /// Gets a list of the users location's
        /// </summary>
        /// <param name="userID">Users OD</param>
        /// <returns>list of all locations</returns>
        public List<LocationEvent> getUserLocation(int userID)
        {
            string query = String.Format("SELECT * FROM locationDay;");
            List<LocationEvent> locations = new List<LocationEvent>();
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    locations.Add(this.getLocationEventObject(reader));
                }
            }
            return locations;

        }

        /// <summary>
        /// list of all location avg's in location week.
        /// </summary>
        /// <returns>list of all locationavg's in location week</returns>
        public List<LocationAvg> getWeekAvg()
        {
            string query = "SELECT * FROM LocationWeek";
            List<LocationAvg> week = new List<LocationAvg>();
            using (MySqlDataReader reader = runQuery(query))
            {
                while (reader.Read())
                {
                    week.Add(getLocationAvgObject(reader));
                }
            }
            return week;
        }

        /// <summary>
        /// Gets all rows in location month
        /// </summary>
        /// <returns>list of all locationavg's in location month</returns>
        public List<LocationAvg> getMonthAvg()
        {
            string query = "SELECT * FROM Locationmonth";
            List<LocationAvg> month = new List<LocationAvg>();
            using (MySqlDataReader reader = runQuery(query))
            {
                while (reader.Read())
                {
                    month.Add(getLocationAvgObject(reader));
                }
            }
            return month;
        }

        /// <summary>
        /// Get All Rows in LocationYear
        /// </summary>
        /// <returns>list of all locationAvg's in location year</returns>
        public List<LocationAvg> getYearAvg()
        {
            string query = "SELECT * FROM LocationYear";
            List<LocationAvg> year = new List<LocationAvg>();
            using (MySqlDataReader reader = runQuery(query))
            {
                while (reader.Read())
                {
                    year.Add(getLocationAvgObject(reader));
                }
            }
            return year;
        }



        /// <summary>
        /// list of all location avg's in location course week.
        /// </summary>
        /// <returns>list of all locationavg's in location course week</returns>
        public List<LocationAvg> getCourseWeekAvg()
		{
			string query = "SELECT * FROM LocationCourseWeek";
			List<LocationAvg> week = new List<LocationAvg> ();
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    week.Add(getLocationAvgObject(reader));
                }
            }
                return week;
		}

        /// <summary>
        /// list of all location avg's in location course month.
        /// </summary>
        /// <returns>list of all locationavg's in location course month</returns>
        public List<LocationAvg> getCourseMonthAvg()
        {
            string query = "SELECT * FROM LocationCourseMonth";
            List<LocationAvg> month = new List<LocationAvg>();
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    month.Add(getLocationAvgObject(reader));
                }
            }
            return month;
        }

        /// <summary>
        /// list of all location avg's in location course year.
        /// </summary>
        /// <returns>list of all locationavg's in location course year</returns>
        public List<LocationAvg> getCourseYearAvg()
        {
            string query = "SELECT * FROM LocationCourseyear";
            List<LocationAvg> year = new List<LocationAvg>();
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    year.Add(getLocationAvgObject(reader));
                }
            }
            return year;
        }
       
        /// <summary>
        /// Finds user with the same town as user.
        /// </summary>
        /// <param name="user">User to finds matches for</param>
        /// <returns>List of users</returns>
        public List<MoodleUser> getUsersWithSameTown(MoodleUser user)
        {
            List<MoodleUser> users = new List<MoodleUser>();
            string query = String.Format("SELECT * FROM Students WHERE City={0} AND ID NOT {1}",user.city,user.ID);
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    users.Add(getUserObject(reader));
                }
            }
            return users;
        }


        /***************
        *Add OR Remove from locationdb
        ****************/
        /// <summary>
        /// Add a users week avg to the database
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="on">Amount of time on campus </param>
        /// <param name="off">Amount of time off campus</param>
        public void addWeekAverage(int userID,int on,int off)
        {
            string query = String.Format("INSERT INTO LocationWeek VALUES({0},{1}.{2},{3})", userID, DateTime.Now.ToShortDateString(), on, off);
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// Adds the users month avg to the database
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="on">Amount of time off campus</param>
        /// <param name="off">Amount of time on campus</param>
        public void addMonthAverage(int userID, int on, int off)
        {
            string query = String.Format("INSERT INTO LocationMonth VALUES({0},{1},{2},{3})", userID, DateTime.Now.ToShortDateString(), on, off);
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// Add or Update Year to DB
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="on"></param>
        /// <param name="off"></param>
        public void addYearAverage(int userID,int on, int off)
        {
            LocationAvg avg = this.yearExists(userID);
            if(avg!=null)
            {
                avg.off = avg.off + off;
                avg.on = avg.on + on;
                string.Format("Update LocationYear SET inUni = {0}, offUni = {1} WHERE UserID = {2} AND LocTime = {3}", avg.on, avg.off, userID, avg.locTime);
            }else
            {
                string query = String.Format("INSERT INTO LocationYear VALUES({0},{1},{2},{3})", userID, DateTime.Now.ToShortDateString(), on, off);
            }
        }
        /// <summary>
        /// Add or Update Year to DB
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="courseID"></param>
        /// <param name="on"></param>
        /// <param name="off"></param>
        public void addYearAverage(int userID,int courseID, int on, int off)
        {
            LocationAvg avg = this.yearExists(userID,courseID);
            if (avg != null)
            {
                avg.off = avg.off + off;
                avg.on = avg.on + on;
                string.Format("Update LocationCourseYear SET inUni = {0}, offUni = {1} WHERE UserID = {2} AND LocTime = {3} AND CourseID = {4}", avg.on, avg.off, userID, avg.locTime,avg.courseID);

            }
            else
            {
                string query = String.Format("INSERT INTO LocationCourseYear VALUES({0},{1},{2},{3},{4})", userID,courseID, DateTime.Now.ToShortDateString(), on, off);
            }
        }

        /// <summary>
        /// Check if year exists
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public LocationAvg yearExists(int UserID)
        {
            string query = String.Format("SELECT * FROM LocationYear WHERE UserID = {0} ORDER BY LocTime DESC", UserID);
            using (MySqlDataReader reader = this.runQuery(query))
            {
                if(reader.HasRows)
                {
                    return this.getLocationAvgObjectWithID(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Check if year exists
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public LocationAvg yearExists(int UserID,int courseID)
        {
            string query = String.Format("SELECT * FROM LocationCourseYear WHERE UserID = {0} AND CourseID = {1} ORDER BY LocTime DESC", UserID,courseID);
            using (MySqlDataReader reader = this.runQuery(query))
            {
                if (reader.HasRows)
                {
                    return this.getLocationAvgObject(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Add a users week avg to the database from the course
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="courseID"></param>
        /// <param name="on">Amount of time on campus </param>
        /// <param name="off">Amount of time off campus</param>m>
        public void addWeekCourseAverage(int userID,int courseID,int on,int off)
        {
            string query = String.Format("INSERT INTO LocationCourseWeek VALUES({0},{1},{2},{3},{4}", userID, courseID, Database.toUnix(DateTime.UtcNow), on, off);
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// Add a users week avg to the database from the course
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="courseID"></param>
        /// <param name="on">Amount of time on campus </param>
        /// <param name="off">Amount of time off campus</param>
        public void addMonthCourseAverage(int userID,int courseID,int on, int off)
        {
            string query = String.Format("INSERT INTO LocationCourseMonth VALUES({0},{1},{2},{3},{4}", userID, courseID, Database.toUnix(DateTime.UtcNow), on, off);
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// clears all month tables both course and non.
        /// </summary>
		public void removeMonth()
		{
			string query = "DELETE FROM LocationMonth";
			this.runQueryNonReturn(query);
			query = "DELETE FROM LocationCourseMonth";
			this.runQueryNonReturn(query);
		}

        /// <summary>
        /// Removes all data from locationweek and locationcourseweek
        /// </summary>
        public void removeWeek()
        {
            string query = "DELETE FROM LocationWeek";
            this.runQueryNonReturn(query);
            query = "DELETE FROM LocationCourseWeek";
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// Clears the location day and location course day tables.
        /// </summary>
        public void removeDay()
        {
            string query = "DELETE FROM LocationDay";
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// Add users location to the day
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="x">X point</param>
        /// <param name="y">Y Point</param>
        public void addUserLocation(int userID, double x, double y)
        {
            string query = String.Format(" INSERT INTO LocationDay VALUES({0},{1},{2},{3})", userID,Database.toUnix(DateTime.UtcNow), x, y);
            this.runQueryNonReturn(query);
        }

       /// <summary>
       /// Adds the user location.
       /// </summary>
       /// <param name="userID">User ID.</param>
       /// <param name="x">The x coordinate.</param>
       /// <param name="y">The y coordinate.</param>
       /// <param name="date">Date of the location.</param>
        public void addUserLocation(int userID, double x, double y,DateTime date)
        {
            string query = String.Format(" INSERT INTO LocationDay VALUES({0},{1},{2},{3})", userID, Database.toUnix(date), x, y);
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// Add a users location event
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="courseID"></param>
        /// <param name="onCampus"></param>
        /// <param name="offCampus"></param>
        public void addLocationDayEventsCourse(int userID,int courseID,int onCampus,int offCampus)
        {
            string query = String.Format("INSERT INTO LocationCourseDay VALUES(0,{0},{1},default,{2},{3})", userID, courseID, onCampus, offCampus);
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// Add a users week events for a course.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="courseID"></param>
        /// <param name="onCampus"></param>
        /// <param name="offCampus"></param>
        public void addLocationWeekEventsCourse(int userID, int courseID, int onCampus, int offCampus)
        {
            string query = String.Format("INSERT INTO LocationCourseWeek VALUES({0},{1},{2},{3},{4})", userID, courseID,Database.toUnix(DateTime.UtcNow), onCampus, offCampus);
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// Add a users month events for a course.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="courseID"></param>
        /// <param name="onCampus"></param>
        /// <param name="offCampus"></param>
        public void addLocationMonthEventsCourse(int userID, int courseID, int onCampus, int offCampus)
        {
            string query = String.Format("INSERT INTO LocationCourseMonth VALUES(0,{0},{1},{2},{3})", userID, courseID, onCampus, offCampus);
            this.runQueryNonReturn(query);
        }



        /// <summary>
        /// Add user avgs to the database
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="onCampus"></param>
        /// <param name="offCampus"></param>
        public void addLocationWeekEvents(int userID, int onCampus, int offCampus)
        {
            string query = String.Format("INSERT INTO LocationWeek VALUES({0},{1},{2},{3})", userID,Database.toUnix(DateTime.UtcNow), onCampus, offCampus);
            this.runQueryNonReturn(query);
        }

        /// <summary>
        /// Add user avgs to the database
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="onCampus"></param>
        /// <param name="offCampus"></param>
        public void addLocationMonthEvents(int userID, int onCampus, int offCampus)
        {
            string query = String.Format("INSERT INTO LocationMonth VALUES({0},{1},{2},{3})", userID,Database.toUnix(DateTime.UtcNow), onCampus, offCampus);
            this.runQueryNonReturn
                (query);
        }

        /*******************
        *Get Stats
        ********************/

        /// <summary>
        /// Returns List of Course and all of the LocationAvgCourse Objects 
        /// </summary>
        /// <param name="courses">List of each couse and the moodle users to be found</param>
        /// <returns></returns>
        public Dictionary<MoodleCourse,List<LocationAvg>> getCourseAveragesMonth(Dictionary<MoodleCourse,List<MoodleUser>>courses)
		{
            Dictionary<MoodleCourse, List<LocationAvg>> avgs = new Dictionary<MoodleCourse, List<LocationAvg>>();

            foreach (MoodleCourse course in courses.Keys)
			{
				int avg;
                List<LocationAvg> courseAvg = new List<LocationAvg>();
                List<MoodleUser> users = courses[course];
                foreach(MoodleUser user in users)
                {
                   courseAvg.Add(this.getUsersAvgMonth(course.ID, user.ID));
                }
                avgs.Add(course, courseAvg);
			}
            return avgs;
		}

        /// <summary>
        /// Returns List of Course and all of the LocationAvgCourse Objects 
        /// </summary>
        /// <param name="courses">List of each couse and the moodle users to be found</param>
        /// <returns></returns>
        public  List<LocationAvg> getCourseAveragesWeek(List<MoodleUser> users,int courseID)
        {
            List<LocationAvg> courseAvg = new List<LocationAvg>(); 
            foreach (MoodleUser user in users)
            {
                courseAvg.Add(this.getUsersAvgWeek(courseID, user.ID));
            }
            return courseAvg;
        }

        /// <summary>
        /// A the week avg for each user in users.
        /// </summary>
        /// <param name="users">A list of locationAvg</param>
        /// <returns></returns>
		public List<LocationAvg> getAveragesWeek(List<MoodleUser> users)
		{
			List<LocationAvg> avgs = new List<LocationAvg> ();
			foreach (MoodleUser user in users) 
			{
                LocationAvg avg = this.getUsersAvgWeek(user.ID);
                if(avg !=null)
                avgs.Add (avg);
			}
			return avgs;
		}

        /// <summary>
        /// A the month avg for each user in users.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public List<LocationAvg> getAveragesMonth(List<MoodleUser> users)
		{
			List<LocationAvg> avgs = new List<LocationAvg> ();
			foreach (MoodleUser user in users) 
			{
				avgs.Add(this.getUsersAvgMonth(user.ID));
			}
			return avgs;
		}

        /// <summary>
        /// A the year avg for each user in users.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public List<LocationAvg>getAveragesYear(List<MoodleUser> users)
        {
            List<LocationAvg> avgs = new List<LocationAvg>();
            foreach (MoodleUser user in users)
            {
                avgs.Add(this.getUsersAvgYear(user.ID));
            }
            return avgs;
        }

        /// <summary>
        /// A the week avg for user
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public LocationAvg getUsersAvgWeek(int courseID,int userID)
		{
            LocationAvg avg = null;
            string query = String.Format("SELECT * FROM LocationCourseWeek WHERE UserID = {0} AND courseID = {1}", userID, courseID);
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    avg = this.getLocationAvgObjectWithID(reader);
                }
            }
            return avg;
		}

        /// <summary>
        /// A the week avg for user.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
		public LocationAvg getUsersAvgWeek(int userID)
		{
			LocationAvg avg = null;
			string query = String.Format ("SELECT * FROM LocationWeek WHERE UserID={0}", userID);
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    avg = getLocationAvgObject(reader);
                }
            }
			return avg;
		}

        /// <summary>
        /// A the month avg for user.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
		public LocationAvg getUsersAvgMonth(int userID)
		{
			LocationAvg avg = null;
			string query = String.Format ("SELECT * FROM LocationMonth WHERE UserID={0}", userID);
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    avg = getLocationAvgObject(reader);
                }
            }
			return avg;
		}


        /// <summary>
        /// A the month avg for user.
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public LocationAvg getUsersAvgMonth(int courseID,int userID)
        {
            LocationAvg avg = null;
            string query = String.Format("SELECT * FROM LocationCourseMonth WHERE UserID = {0} AND courseID = {1}", userID, courseID);
            using(MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    avg = this.getLocationAvgObjectWithID(reader);
                }
            }
            return avg;
        }

        /// <summary>
        /// A the year avg for user.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public LocationAvg getUsersAvgYear(int UserID)
        {
            LocationAvg avg = null;
            string query = String.Format("SELECT * FROM LocationYear WHERE UserID={0}", UserID);
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    avg = getLocationAvgObject(reader);
                }
            }
            return avg;
        }

        /// <summary>
        /// A the year avg for each user.
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public LocationAvg getUsersAvgYear(int courseID,int userID)
        {
            LocationAvg avg = null;
            string query = String.Format("SELECT * FROM LocationCourseYear WHERE UserID = {0} AND courseID = {1}", userID, courseID);
            using (MySqlDataReader reader = this.runQuery(query))
            {
                while (reader.Read())
                {
                    avg = this.getLocationAvgObjectWithID(reader);
                }
            }
            return avg;
        }
        
        /// <summary>
        /// Sets the users target.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="target"></param>
        public void setUserTarget(int userID, int target)
        {
            string query = String.Format("UPDATE User SET target = {0} WHERE UserID = {1}",target, userID);
            this.runQuery(query);
        }

        /// <summary>
        /// Gets the users target
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int getUserTarget(int ID)
        {
            string query = String.Format("SELECT target FROM User where UserID = {0}",ID);
            using(var reader = this.runQuery(query))
            {
                int i = int.Parse(reader["target"].ToString());
                return i;
            }
            return -0;
        }

        /// <summary>
        /// Allows the user to login to the system
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int login(string email,string password)
        {
            int id = -1;
            string query = String.Format("SELECT MoodleID FROM User where moodleEmail = '{0}' AND pass = '{1}'", email, password);
            using(MySqlDataReader reader = this.runQuery(query))
            {
                while(reader.Read())
                {
                    id = int.Parse(reader["MoodleID"].ToString());
                }
            }
            return id;
        }

        /// <summary>
        ///  Register the user.
        /// </summary>
        /// <param name="moodleUsername"></param>
        /// <param name="moodlePassword"></param>
        /// <param name="moodleID"></param>
        /// <param name="password"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="location"></param>
        public void Register(string moodleUsername, string moodlePassword,int moodleID, string password, string fName, string lName, string location)
        {
            string query = String.Format("INSERT INTO User VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')", moodleID, moodleUsername, moodlePassword, password, fName, lName, location, 0);
            this.runQueryNonReturn(query);
        }
    }
}

