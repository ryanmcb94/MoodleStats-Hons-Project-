using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EggsToGo;
using BarChart;
using MoodleObjects;


namespace MoodleStats.Droid
{
	[Activity (Label = "Graph", MainLauncher=true, Icon = "@drawable/icon",NoHistory=true)]	
	public class winGraph : Activity
	{
		//Swipe Controls
		Easter swipeLeft,swipeRight;
		int windowID = 0,courseWindow=0;
		bool selectCourse;
		BarChartView chart = null;
		RelativeLayout revLayout;
		Button btnCourseSelect;
		MoodleCourse course;
		Store s = Controller.getController().getStore();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			//Window Setup
			base.OnCreate (savedInstanceState);
			this.SetContentView (Resource.Layout.winGraph);

			//Get Data From Service.
			int userID = Controller.getController ().user.ID;
			s.weekData = Controller.getController ().service.getAvgsWeek (userID);
			s.monthData = Controller.getController ().service.getAvgsMonth (userID);
			s.YearData = Controller.getController ().service.getAvgsYear (userID);
			s.userCources = new List<MoodleCourse> ();
			foreach (MoodleCourse c in s.weekData.Keys) {
				s.userCources.Add (c);
			}
			s.selectedCourse = s.userCources[0];

			//Get UI Items.
			this.revLayout = FindViewById<RelativeLayout> (Resource.Id.winChartLayout);
			this.btnCourseSelect = FindViewById<Button> (Resource.Id.btnCourseSelect);
			this.btnCourseSelect.Click += delegate {
				this.selectCourse = true;
				FragmentTransaction ft = FragmentManager.BeginTransaction ();
				dialog_SelectCourse sc = new dialog_SelectCourse (this);
				sc.Show (ft, "dialog_SelectCourse");
				
			};
			//Setup Swipe.
			this.swipeLeft = new Easter (new CustomEgg ("Left").WatchForSequence (Command.SwipeLeft ()));
			this.swipeRight = new Easter (new CustomEgg ("right").WatchForSequence (Command.SwipeRight ()));
			this.swipeLeft.EggDetected += (Egg Egg) => {
				if (this.windowID < 2) {
					this.windowID++;
				}
				this.redoWindow ();
			};
			this.swipeRight.EggDetected += (Egg egg) => {
				if (this.windowID > 0) {
					this.windowID--;
				}
				this.redoWindow ();
			};
			this.redoWindow ();
		}

		/// <summary>
		/// Updates the window with each swipe.
		/// </summary>
		private void redoWindow()
		{
			try 
			{
			List<BarModel> bars = new List<BarModel> ();
			this.revLayout.RemoveView (chart);
			this.chart = null;
			this.chart = new BarChartView (this);
			LocationAvg[] avgs = new LocationAvg[3];
			//Get Data
			if (this.windowID == 0) 
			{ //Week
				this.Title = "Week, " + s.selectedCourse.name;
				avgs = s.weekData [s.selectedCourse];
			} 
			else if (this.windowID == 1) 
			{  //Month
					foreach(MoodleCourse c in s.monthData.Keys)
					{
						if(c.ID == s.selectedCourse.ID)
						{
							avgs = s.monthData[c];
						}
					}
					this.Title = "Month, " + s.selectedCourse.name;
			}
			else 
			{  // Year
					foreach(MoodleCourse c in s.YearData.Keys)
					{
						if(c.ID == s.selectedCourse.ID)
						{
							avgs = s.YearData [c];
						}
					}
					this.Title = "Year, " + s.selectedCourse.name;
			}
			
			//Null Check
			for (int i = 0; i < avgs.Length; i++) 
			{
				LocationAvg avg = avgs [i];
				if (avg == null) 
				{
					avg = new LocationAvg (-1, DateTime.Now, 0, 0);
				}
				avgs [i] = avg;
			}
			
			//Create Bar Models
			BarModel userOn = new BarModel 
			{ //UserOn
				Value = avgs [0].on,
				Color = Android.Graphics.Color.Aqua,
				ValueCaptionHidden = false,
				ValueCaption = "User On"
			};
			BarModel userOff = new BarModel 
			{  //UserOff
				Value = avgs [0].off,
				Color = Android.Graphics.Color.Red,
				ValueCaptionHidden = false,
				ValueCaption = "User Off"
			};
			BarModel avgOn = new BarModel 
			{ //AvgOn
				Value = avgs [1].on,
				Color = Android.Graphics.Color.Green,
				ValueCaptionHidden = false,
				ValueCaption = "Avg On"
			};
			BarModel avgOff = new BarModel 
			{   //AvgOff
				Value = avgs [1].off,
					Color = Android.Graphics.Color.LightBlue,
				ValueCaptionHidden = false,
				ValueCaption = "Avg Off"
			};

			if (this.windowID == 0) { //Week
			} else if (this.windowID == 1) { //Month
			} else { //Year
			}

			//Create Chart 
			bars.Add (userOn);
			bars.Add (userOff);
			bars.Add (avgOn);
			bars.Add (avgOff);
			this.chart.ItemsSource = bars;
			this.revLayout.AddView (chart, new ViewGroup.LayoutParams (ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
			}catch(Exception e)
			{
				Toast.MakeText(this,"winGraph " +e.ToString(),ToastLength.Long).Show();
			}
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			AppContext.setContext (this);
		}

		public override bool DispatchTouchEvent (MotionEvent ev)
		{
			this.swipeLeft.OnTouchEvent (ev);
			this.swipeRight.OnTouchEvent (ev);
			return base.DispatchTouchEvent (ev);
		}

		public override void OnWindowFocusChanged (bool hasFocus)
		{
			base.OnWindowFocusChanged (hasFocus);

			if (hasFocus) {
				this.s = Controller.getController ().getStore ();
				this.redoWindow ();
			}
		}

	}
}

