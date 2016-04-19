using System;
using System.Runtime.Serialization;

namespace MoodleObjects
{
    [DataContract]
	public class MoodleEvent:Moodle
	{
        [DataMember]
        public int userID { get; set; }
        [DataMember]
        public int courseID { get; set; }
        [DataMember]
        public string action {get;set;}
        [DataMember]
        public DateTime time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="userID"></param>
        /// <param name="courseID"></param>
        /// <param name="action"></param>
        /// <param name="time"></param>
		public MoodleEvent (int ID,int userID,int courseID,string action,DateTime time):base(ID)
		{
			this.userID = userID;
			this.courseID = courseID;
			this.action = action;
            this.time = time;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            MoodleEvent evt = (MoodleEvent)obj;
            if (this.userID < evt.userID)
                return 1;
            else if (this.userID > evt.userID) return -1;
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return String.Format("User ID: {0}, courseID: {1}, action: {2}, time: {3}", this.userID, this.courseID, this.action, this.time.ToString());
        }
    }
}

