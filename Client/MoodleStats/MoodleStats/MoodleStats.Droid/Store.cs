using System;
using System.Collections.Generic;
using MoodleObjects;

namespace MoodleStats
{
	public class Store
	{
		public List<object> StoreList {get;set;}
		public List<MoodleCourse> userCources {get;set;}
		public MoodleCourse selectedCourse { get; set;}
		public Dictionary<MoodleCourse,LocationAvg[]> weekData {get;set;}
		public Dictionary<MoodleCourse,LocationAvg[] >monthData { get; set;}
		public Dictionary<MoodleCourse,LocationAvg[]> YearData {get;set;}
		public string username { get; set; }
		public string password {get;set;}

		public Store ()
		{
			this.StoreList = new List<object> ();

		}
	}
}

