using System;
using System.Collections.Generic;
using MoodleObjects;


namespace HonsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class MoodleStatsService : IMoodleStatsService
    {
        /// <summary>
        /// add a users current location to the location db
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="x">lat</param>
        /// <param name="y">long</param>
        public void addLocation(int userID, double x, double y)
        {
            LocationDB.getLocationDB().addUserLocation(userID, x, y);
        }
        /// <summary>
        /// Get a course from moodle
        /// Tested: Working
        /// </summary>
        /// <param name="ID">Course ID</param>
        /// <returns>MoodleCourse Object</returns>
        public MoodleCourse getCourse(int ID)
        {
            return MoodleDB.getMoodleDB().getCourse(ID);
        }

        /// <summary>
        /// Login a user
        ///Tested Working.
        /// </summary>
        /// <param name="username">Users username</param>
        /// <param name="password">User password</param>
        /// <returns>A MoodleUser Object</returns>
        public MoodleUser LoginMoodle(string username, string password)
        {
            return System.getSystem().LoginMoodle(username, password);
        }

        /// <summary>
        /// Get all the users of a course.
        /// </summary>
        /// <param name="ID">Course ID</param>
        /// <returns>A list of MoodleUser objects</returns>
        public List<MoodleUser> getUserByCourse(int ID)
        {
            return MoodleDB.getMoodleDB().getUsersByCourse(ID);
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="ID">Users ID</param>
        /// <returns>A moodleuser object</returns>
        public MoodleUser getUserByID(int ID)
        {
            MoodleUser user;
            user = MoodleDB.getMoodleDB().getUserByID(ID);
            return user;
        }

		/// <summary>
		/// Updates the LocationDB
		/// </summary>
        public void updateLocationDB()
        {
            System.getSystem().updatesLocationDB();

        }

        public void setUserTarget(int userID, int target)
        {
            LocationDB.getLocationDB().setUserTarget(userID, target);
        }

        public int getUserTarget(int userID)
        {
            return LocationDB.getLocationDB().getUserTarget(userID);   
        }

        public MoodleUser login(string username, string password)
        {
            return System.getSystem().Login(username, password);
        }

        /// <summary>
        /// Register the user with the db.
        /// Tested:Working.
        /// </summary>
        /// <param name="moodleUsername"></param>
        /// <param name="moodlePassword"></param>
        /// <param name="password"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="location"></param>
        public void Register(string moodleUsername, string moodlePassword, string password, string fName, string lName, string location)
        {
            System.getSystem().Register(moodleUsername, moodlePassword, password, fName, lName, location);
        }

        public List<int> getBestMatchesAvgsMonth(int userID)
        {
            throw new NotImplementedException();
        }

        public Dictionary<MoodleCourse, List<LocationAvg>> getAvgsYear(int ID)
        {
            return System.getSystem().getAvgsYear(ID);
        }

        public Dictionary<MoodleCourse, List<LocationAvg>> getAvgsMonth(int ID)
        {
            return System.getSystem().getAvgsMonth(ID);
        }

        public Dictionary<MoodleCourse, List<LocationAvg>> getAvgsWeek(int ID)
        {
            return System.getSystem().getAvgsWeek(ID);
        }

    }
}
