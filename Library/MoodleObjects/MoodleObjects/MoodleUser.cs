using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace MoodleObjects
{
    [DataContract]
	public class MoodleUser:Moodle
	{
        //Declare Variables
        [DataMember]
        public string username {get;set;}
        [DataMember]
        public string password {get;set;}
        [DataMember]
        public string fName { get; set; }
        [DataMember]
        public string lName {get;set;}
        [DataMember]
        public List<MoodleCourse> courses {get;set;}
        [DataMember]
        public string city { get; set;}
        [DataMember]
        public string country {get;set;}


		/// <summary>
		/// Creates a new Moodle user.
		/// </summary>
		/// <param name="ID">The ID of the user 'id'</param>
		/// <param name="username">Username of the user 'username'</param>
		/// <param name="password">Password of the user 'password'</param>
		/// <param name="fName">The first name of the user 'firstname'</param>
		/// <param name="lName">The last name of the user 'lastname'</param>
		/// <param name="city">the city of the user 'location'</param>
		/// <param name="country">Users country 'country'</param>
		public MoodleUser(int ID,string username,string password,string fName,string lName,string city,string country):base(ID)
		{
            this.ID = ID;
			this.username = username;
			this.password = password;
			this.fName = fName;
			this.lName = lName;
			this.city = city;
			this.country = country;
			this.courses = new List<MoodleCourse> ();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return String.Format("ID: {0}, username {1},password {2}, Name: {3},{4}, City: {5}, Country {6}",this.ID,this.username,this.password,this.lName,this.fName,this.city,this.country);
        }





	
	}
}

