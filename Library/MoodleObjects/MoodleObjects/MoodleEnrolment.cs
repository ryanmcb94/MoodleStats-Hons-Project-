using System;
using System.Runtime.Serialization;

namespace MoodleObjects
{
    [DataContract]
	public class MoodleEnrolment:Moodle
	{
        //Declare Variables
        [DataMember]
        public int userID {get;set;}
        [DataMember]
        public int courseID{ get; set;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="userID"></param>
        /// <param name="courseID"></param>
		public MoodleEnrolment (int ID,int userID,int courseID):base(ID)
		{
			this.userID = userID;
			this.courseID = courseID;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return String.Format("User ID: {0}, Course ID: {1}", this.userID, this.courseID);
        }
	}
}

