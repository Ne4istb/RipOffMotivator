using System;
using System.Linq;

using Android.App;
using Android.OS;


namespace RipOffMotivator.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MainTheme")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		internal static MainActivity Instance { get; private set; }

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			Instance = this;
			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new App());
		}
	}
}

