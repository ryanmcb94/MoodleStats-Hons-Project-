using System;
using System.Runtime.Serialization;

namespace MoodleObjects
{
    [DataContract]
	public class Moodle
	{
        [DataMember]
        public int ID { get; set; }

		public Moodle (int ID)
		{
			this.ID = ID;
		}
	}
}

