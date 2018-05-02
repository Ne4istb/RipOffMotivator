using System;
using System.Linq;

using RipOffMotivator.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RipOffMotivator
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GoalListPage : ContentPage
	{
		readonly Repository repo;

		public GoalListPage(Repository repository)
		{
			InitializeComponent ();
			repo = repository;

			listView.ItemsSource = repo.Goals;
		}

		async void OnCreateGoal(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CreateGoalPage(repo));
		}

		async void OnAddTrigger(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTriggerPage(repo));
		}
	}
}