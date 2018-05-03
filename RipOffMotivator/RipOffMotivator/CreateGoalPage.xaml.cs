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
			DateTime date = goalDate.Date;
			var time = goalTime.Time;
			if (!string.IsNullOrWhiteSpace(title) && long.TryParse(goalPrice.Text, out long amount))
			{
				var tag = (Tag)((dynamic)BindingContext).SelectedTag;
			    var rejectId = await contractService.Value.AddGoalAsync(amount, date);
				repo.AddGoal(new Goal {
					Amount = amount,
					Date = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0),
					Title = title,
					TagId = tag.Id,
					RejectTrigger = rejectId
				});
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