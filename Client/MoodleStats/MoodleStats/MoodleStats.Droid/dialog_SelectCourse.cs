
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MoodleObjects;

namespace MoodleStats.Droid
{
	public class dialog_SelectCourse : DialogFragment
	{
		Spinner spin;
		Button btnSelectCourse;
		MoodleCourse selected;
		public Context con { get;set;}
		Store s = Controller.getController().getStore();
		public dialog_SelectCourse(Context con)
		{
			this.con = con;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			View view = null;
			view = inflater.Inflate (Resource.Layout.dialog_SelectCourse,container, false);
			try 
			{
			this.spin = view.FindViewById<Spinner>(Resource.Id.spinCourse);
			this.btnSelectCourse = view.FindViewById<Button> (Resource.Id.btnSelectCourse);
			this.btnSelectCourse.Click += delegate 
			{
				this.Dismiss();
			};
			string[] adapterData = new string[s.userCources.Count];
			for(int i=0;i<s.userCources.Count;i++)
			{
				adapterData [i] = s.userCources [i].name;
			}

			ArrayAdapter adapter = new ArrayAdapter (this.con, Android.Resource.Layout.SimpleSpinnerItem, adapterData);
			this.spin.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spin_ItemSelected);
			this.spin.Adapter = adapter;
			}catch(Exception e) {
				Toast.MakeText (this.con, e.ToString (), ToastLength.Long).Show ();
			}
			return view;
		}

		public void spin_ItemSelected(object sender,AdapterView.ItemSelectedEventArgs e)
		{
			try{
				s.selectedCourse = s.userCources [e.Position];

			}catch(Exception ex) {
				Toast.MakeText (this.con, ex.ToString (), ToastLength.Long).Show ();
			}
		}
	}
}

