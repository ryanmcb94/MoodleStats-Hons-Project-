using System;
using System.Runtime.Serialization;

namespace MoodleObjects
{
    [DataContract]
    public class LocationAvg
    {
        [DataMember]
        public int userID { get; set; }
        /// <summary>
        /// Day of the Event
        /// </summary>
        [DataMember]
        public DateTime locTime { get; set; }

        [DataMember]
        public int courseID { get; set; }
        /// <summary>
        /// on the Cmapus
        /// </summary>
        [DataMember]
        public int on { get; set; }
        /// <summary>
        /// off the Campus
        /// </summary>
        [DataMember]
        public int off { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="time"></param>
        /// <param name="on"></param>
        /// <param name="off"></param>
        public LocationAvg(int UserID,int courseID,DateTime time, int on, int off)
        {
            this.userID = userID;
            this.courseID = courseID;
            this.locTime = time;
            this.on = on;
            this.off = off;
        }

                /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="time"></param>
        /// <param name="on"></param>
        /// <param name="off"></param>
        public LocationAvg(int UserID,DateTime time, int on, int off)
        {
            this.userID = UserID;
            this.courseID=-1;
            this.locTime = time;
            this.on = on;
            this.off = off;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            LocationAvg evt = (LocationAvg)obj;
            if (this.userID < evt.userID)
                return 1;
            else if (this.userID > evt.userID) return -1;
            return 0;
        }

    }
}
