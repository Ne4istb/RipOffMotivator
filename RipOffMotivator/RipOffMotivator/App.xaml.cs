using System;
using System.Collections.Generic;
using System.Linq;

using RipOffMotivator.Data;
using RipOffMotivator.Models;

using SmartContractsProxy;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RipOffMotivator
{
    public partial class App : Application
    {
		readonly Repository repo;
		readonly Lazy<ISmartContractsProxy> contractService = new Lazy<ISmartContractsProxy>(() => new SmartContractsProxy.SmartContractsProxy());


		public App()
        {
            InitializeComponent();
            
			repo = new Repository(Current);
			MainPage = new NavigationPage(new GoalListPage(repo));
        }

		public async void ResolveGoal(string tagId)
		{
			if (repo.GoalResolved(tagId, out Goal resolved))
			{
                await repo.Commit();
                await contractService.Value.RejectAsync(resolved.RejectTrigger);
			}

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
