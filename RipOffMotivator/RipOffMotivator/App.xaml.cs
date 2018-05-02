using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RipOffMotivator
{
    public partial class App : Application
    {
        public static IList<string> Goals { get; set; }

        public App()
        {
            InitializeComponent();
            Goals = new List<string>();
            MainPage = new NavigationPage(new GoalListPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
