package md54317d8a7b50ec6ca3ce527a922464225;


public class dialog_SelectCourse
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MoodleStats.Droid.dialog_SelectCourse, Moodle Stats, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", dialog_SelectCourse.class, __md_methods);
	}


	public dialog_SelectCourse () throws java.lang.Throwable
	{
		super ();
		if (getClass () == dialog_SelectCourse.class)
			mono.android.TypeManager.Activate ("MoodleStats.Droid.dialog_SelectCourse, Moodle Stats, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public dialog_SelectCourse (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == dialog_SelectCourse.class)
			mono.android.TypeManager.Activate ("MoodleStats.Droid.dialog_SelectCourse, Moodle Stats, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
