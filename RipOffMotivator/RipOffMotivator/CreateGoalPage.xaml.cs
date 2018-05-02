using System;

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
		}

		async void OnAddGoal(object sender, EventArgs e)
		{
			var title = goalTitle.Text;
			var date = goalDate.Date;
			if (!string.IsNullOrWhiteSpace(title) && decimal.TryParse(goalPrice.Text, out decimal amount))
			{
				var tagId = Guid.NewGuid();
				contractService.Value.AddGoal(amount, date, tagId);
				repo.AddGoal(new Goal {Amount = amount, Date = date, Title = title, TagId = tagId});
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