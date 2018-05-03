﻿using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;


namespace RipOffMotivator.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MainTheme")]
	[IntentFilter(new[] { Intent.ActionMain })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		internal static MainActivity Instance { get; private set; }

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			Instance = this;
			Xamarin.Forms.Forms.Init(this, bundle);

			Xamarin.Forms.DependencyService.Register<INFCIntegration, NFCIntegration>();
			((NFCIntegration)Xamarin.Forms.DependencyService.Get<INFCIntegration>()).SetContext(this);

            //var nfc = new NFCIntegration();
            //nfc.SetContext(this);

			LoadApplication(new App());
		}
	}
}

