using System;
using Android.Content;

namespace MoodleStats.Droid
{
	public class AppContext
	{
		public Context con { get; set; }
		private static AppContext ac;


		private AppContext()
		{
		}

		public static void setContext(Context cont)
		{
			if (ac == null) {
				ac = new AppContext ();
			}
			ac.con = cont;
		}
		public static Context getContext()
		{
			if (ac == null) {
				ac = new AppContext ();
			}
			return ac.con;
		}
	}
}

