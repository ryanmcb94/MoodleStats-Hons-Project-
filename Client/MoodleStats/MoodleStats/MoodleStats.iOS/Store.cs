using System;
using System.Collections.Generic;

namespace MoodleStats
{
	public class Store
	{
		public List<object> StoreList {get;set;}
		public Store ()
		{
			this.StoreList = new List<object> ();
		}
	}
}

