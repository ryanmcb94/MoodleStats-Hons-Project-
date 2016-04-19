using System;
using UIKit;

namespace MoodleStats.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			UILabel lblUsername = new UILabel {
				Text = "Enter username",
				TextAlignment = UITextAlignment.Center,
				Frame = new CoreGraphics.CGRect (50, 50, 120, 440)
			};
			UITextView txtUsername = new UITextView {
				Text = "",
				Frame = new CoreGraphics.CGRect(170,50,120,440)
			};
			UILabel lblPassword = new UILabel {
				Text = "Enter Password",
				TextAlignment = UITextAlignment.Center,
				Frame = new CoreGraphics.CGRect (290, 50, 120, 440)
			};
			UITextView txtPassword = new UITextView {
				Frame = new CoreGraphics.CGRect (410, 50, 120, 440)
			};
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

