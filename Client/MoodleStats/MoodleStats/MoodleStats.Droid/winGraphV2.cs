
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
	[Activity (Label = "winGraphV2", MainLauncher = false, Icon = "@drawable/icon",NoHistory=true)]			
	public class winGraphV2 : Activity
	{
		//Swipe Controls
		Easter swipeLeft,swipeRight;
		int windowID = 0,courseWindow=0;
		BarChartView chart = null;
		RelativeLayout revLayout;
		Dictionary<MoodleCourse,LocationAvg[]> weekData,monthData,YearData;
		List<MoodleCourse> courses;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			//Window Setup
			base.OnCreate (savedInstanceState);
			this.SetContentView (Resource.Layout.winGraph);
			//Get UI Items.
			this.revLayout = FindViewById<RelativeLayout> (Resource.Id.winChartLayout);

			//Setup Swipe.
			this.swipeLeft = new Easter (new CustomEgg ("Left").WatchForSequence (Command.SwipeLeft ()));
			this.swipeRight = new Easter (new CustomEgg ("right").WatchForSequence (Command.SwipeRight ()));
			this.swipeLeft.EggDetected += (Egg Egg) => {
				if (this.windowID < 2) {
					this.windowID++;
				}
				Toast.MakeText(this,"WindowID: " + windowID,ToastLength.Long).Show();
				this.redoWindow ();
			};
			this.swipeRight.EggDetected += (Egg egg) => {
				if (this.windowID > 0) {
					this.windowID--;
				}
				Toast.MakeText(this,"WindowID: " +windowID,ToastLength.Long).Show();
				this.redoWindow ();
			};

			//Get Data From Service. 
			Controller.getController ().user = Controller.getController ().service.getUserByID (6);
			int userID = Controller.getController ().user.ID;

			this.weekData = Controller.getController ().service.getAvgsWeek (userID);
			this.monthData = Controller.getController ().service.getAvgsMonth (userID);
			this.YearData = Controller.getController ().service.getAvgsYear (userID);
			this.courses = new List<MoodleCourse> ();
			foreach (MoodleCourse c in weekData.Keys) 
			{
				this.courses.Add (c);
			}
			this.redoWindow ();
		}

		/// <summary>
		/// Updates the window with each swipe.
		/// </summary>
		private void redoWindow()
		{
			if (this.revLayout == null) {
				Toast.MakeText (this, "null", ToastLength.Long).Show ();
			}
			List<BarModel> bars = new List<BarModel> ();
			this.revLayout.RemoveView (chart);
			this.chart = null;
			this.chart = new BarChartView (this);
			LocationAvg[] avgs;

			//Get Data
			if (this.windowID == 0) { //Week
				avgs = this.weekData [this.courses [courseWindow]];
				this.Title = "Week, " + this.courses [courseWindow].name;
			} else if (this.windowID == 1) {  //Month
				Toast.MakeText (this, "Month Avg Window:" + courseWindow, ToastLength.Long).Show ();
				avgs = this.monthData [this.courses [courseWindow]];
				this.Title = "Month, " + this.courses [courseWindow].name;
			} else {  // Year
				avgs = this.YearData [this.courses [courseWindow]];
				this.Title = "Year, " + this.courses [courseWindow].name;
			}
			//Null Check
			for (int i = 0; i < avgs.Length; i++) {
				LocationAvg avg = avgs [i];
				if (avg == null) {
					avg = new LocationAvg (-1, DateTime.Now, 0, 0);
				}
				avgs [i] = avg;
			}

			//Create Bar Models
			Toast.MakeText (this, "Create User on", ToastLength.Long).Show ();
			BarModel userOn = new BarModel { //UserOn
				Value = avgs [0].on,
				Color = Android.Graphics.Color.Aqua,
				ValueCaptionHidden = false,
				ValueCaption = "User On"
			};
			Toast.MakeText (this, "Create User off", ToastLength.Long).Show ();
			BarModel userOff = new BarModel {  //UserOff
				Value = avgs [0].off,
				Color = Android.Graphics.Color.Red,
				ValueCaptionHidden = false,
				ValueCaption = "User Off"
			};
			Toast.MakeText (this, "Create avg on", ToastLength.Long).Show ();
			BarModel avgOn = new BarModel { //AvgOn
				Value = avgs [1].on,
				Color = Android.Graphics.Color.Green,
				ValueCaptionHidden = false,
				ValueCaption = "Avg On"
			};
			Toast.MakeText (this, "Create avg off", ToastLength.Long).Show ();
			BarModel avgOff = new BarModel {   //AvgOff
				Value = avgs [1].off,
				Color = Android.Graphics.Color.Blue,
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
	}
}

