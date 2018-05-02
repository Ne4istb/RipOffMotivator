using System;
using System.Collections.Generic;
using System.Linq;

using RipOffMotivator.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RipOffMotivator
{
    public partial class App : Application
    {
		readonly Repository repo;

		public App()
        {
            InitializeComponent();
			repo = new Repository(Current);
			MainPage = new NavigationPage(new GoalListPage(repo));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
		{
			repo.Commit();
		}

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
