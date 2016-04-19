
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

namespace MoodleStats.Droid
{
	[Activity (Label = "winSpinner")]			
	public class winSelector : Activity, AdapterView.IOnItemSelectedListener
	{
		private string selectedTime = "";
		Spinner spnTime;
		Spinner spnCourse;
		ArrayAdapter timeAdapter;
		ArrayAdapter<string> courseAdapter;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			this.SetContentView (Resource.Layout.winSelection);
			base.OnCreate (savedInstanceState);
			this.spnTime = FindViewById<Spinner> (Resource.Id.spnTime);
			this.spnCourse = FindViewById<Spinner> (Resource.Id.spnCourse);

			//Service Calls

			//Time Spinner
			this.spnTime.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spnTimeSelected);
			this.timeAdapter = ArrayAdapter.CreateFromResource (this, Resource.Array.timeSpinner, Android.Resource.Layout.SimpleSpinnerItem);
			this.timeAdapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerItem);
			this.spnTime.Adapter = timeAdapter;


			//CourseSpinner
			this.spnCourse.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spnCourseSelected);
			this.courseAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleSpinnerDropDownItem);
			this.spnCourse.Adapter = courseAdapter;

			//Add modules to array adapter.
			courseAdapter.Add("course 1");
			courseAdapter.Add ("course 2");


			// Create your application here
		}

		public void OnNothingSelected(AdapterView parent)
		{
			
		}


		public void OnItemSelected (AdapterView parent, View view, int position, long id)
		{
			if (Resource.Id.spnCourse == id) {
				Toast.MakeText (this, "Course", ToastLength.Long).Show ();
			}
		}

		void spnTimeSelected(object sender,AdapterView.ItemSelectedEventArgs e)
		{
			this.spnTime.SetSelection(e.Position);
		}

		void spnCourseSelected(object sender,AdapterView.ItemSelectedEventArgs e)
		{
			this.spnCourse.SetSelection (e.Position);
		}

	}
}

