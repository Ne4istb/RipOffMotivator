using System;
using System.Linq;

using RipOffMotivator.Data;
using RipOffMotivator.Models;

using SmartContractsProxy;

using Xamarin.Forms;

namespace RipOffMotivator
{
    public partial class CreateGoalPage : ContentPage
    {
		readonly Repository repo;
		readonly Lazy<ISmartContractsProxy> contractService = new Lazy<ISmartContractsProxy>(()=> new SmartContractsProxy.SmartContractsProxy());

		public CreateGoalPage(Repository repo)
        {
			this.repo = repo;

			InitializeComponent();
			BindingContext = new {Tags = repo.Tags, SelectedTag = repo.Tags.FirstOrDefault()};
		}

		async void OnAddGoal(object sender, EventArgs e)
		{
			if (!repo.Tags.Any())
			{
				await DisplayAlert("No tags entered!", "Please add some tags first", "Ok");
				await Navigation.PushAsync(new AddTagPage(repo));
				return;
			}

			var title = goalTitle.Text;
			var date = goalDate.Date;
			if (!string.IsNullOrWhiteSpace(title) && decimal.TryParse(goalPrice.Text, out decimal amount))
			{
				var tag = (Tag)((dynamic)BindingContext).SelectedTag;
				//contractService.Value.AddGoal(amount, date, tag.Id);
				repo.AddGoal(new Goal {Amount = amount, Date = date, Title = title, TagId = tag.Id});
				repo.TagUsed(tag.Id);
				await repo.Commit();
			}

			await Navigation.PushAsync(new GoalListPage(repo));

		}
		
        async void OnViewGoals(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoalListPage(repo));
        }
    }
}