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
			InitializeComponent();
			repo = repository;

			listView.ItemsSource = repo.Goals;

			var hasTags = repo.Tags.Any();
			BindingContext = new {HasTags = hasTags, NoTags = !hasTags};
		}

		async void OnCreateGoal(object sender, EventArgs e)
		{
			if (!repo.Tags.Any())
			{
				await DisplayAlert("No tags entered!", "Please add some tags first", "Ok");
				return;
			}

			await Navigation.PushAsync(new CreateGoalPage(repo));
		}

		async void OnAddTag(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTagPage(repo));
		}

		async void OnViewTags(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new TagsListPage(repo));
		}
	}
}