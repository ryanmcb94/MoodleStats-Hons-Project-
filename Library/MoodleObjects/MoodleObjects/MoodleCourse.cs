using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MoodleObjects
{
    [DataContract]
	public class MoodleCourse:Moodle
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string shortName {get;set;}
        [DataMember]
        public string desc { get; set; }
        [DataMember]
        public DateTime startDate {get;set;}
        [DataMember]
        public List<MoodleUser> studetns {get;set;}


		/// <summary>
		/// Creates a moodle course object
		/// </summary>
		/// <param name="id">ID of the course 'id'</param>
		/// <param name="name">name of the course 'name'</param>
		/// <param name="shortName">shortend name of the course 'shortName'</param>
		/// <param name="desc">"Description of the course 'Moodle' "</param>
		public MoodleCourse (int id, string name,string shortName,string desc):base(id)
		{
			this.name = name;
			this.shortName = shortName;
			this.desc = desc;
			this.studetns = new List<MoodleUser> ();
		}



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return String.Format("ID: {0}, Name: {1}, shortName: {2}, Description: {3}, startDate: {4}, No of Student: {5}",this.ID,this.name,this.shortName,this.desc,this.startDate.ToString(),this.studetns.Count.ToString());
        }

    }
}

