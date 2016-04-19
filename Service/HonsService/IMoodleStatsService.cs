using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using MoodleObjects;

namespace HonsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMoodleStatsService
    {
        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>User Details</returns>
        [OperationContract]
        MoodleUser LoginMoodle(string username, string password);

        [OperationContract]
        MoodleUser login(string username, string password);

        [OperationContract]
        void Register(string moodleUsername, string moodlePassword, string password, string fName, string lName, string location);
        /// <summary>
        /// Gets all the users on a course.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [OperationContract]
        List<MoodleUser> getUserByCourse(int ID);

        /// <summary>
        /// Gets a user by there id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>user details</returns>
        [OperationContract]
        MoodleUser getUserByID(int ID);

        /// <summary>
        /// get a course by id
        /// </summary>
        /// <param name="ID">ID of the course</param>
        /// <returns>Course details</returns>
        [OperationContract]
        MoodleCourse getCourse(int ID);

        /// <summary>
        /// add a location of the user every 10 mins
        /// </summary>
        /// <param name="userID">Users ID</param>
        /// <param name="courseID">Course ID</param>
        /// <param name="x">lat </param>
        /// <param name="y">log</param>
        [OperationContract]
        void addLocation(int userID, double x, double y);

        /// <summary>
        /// 
        /// </summary>
        [OperationContract]
        void updateLocationDB();
        /****************
         * AVGS
         * *************/

        [OperationContract]
        Dictionary<MoodleCourse, List<LocationAvg>> getAvgsYear(int ID);

        [OperationContract]
        Dictionary<MoodleCourse, List<LocationAvg>> getAvgsMonth(int ID);

        [OperationContract]
        Dictionary<MoodleCourse, List<LocationAvg>> getAvgsWeek(int ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID">ID of the user</param>
        /// <param name="target">Users target mins.</param>
        [OperationContract]
        void setUserTarget(int userID, int target);


        /// <summary>
        /// Gets the users current target
        /// </summary>
        /// <param name="userID">Users ID</param>
        /// <returns>Users target as int. may be -1</returns>
        [OperationContract]
        int getUserTarget(int userID);
    }
}
