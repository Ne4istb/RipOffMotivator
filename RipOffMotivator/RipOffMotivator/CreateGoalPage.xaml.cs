using System;

using Xamarin.Forms;

namespace RipOffMotivator
{
    public partial class CreateGoalPage : ContentPage
    {
		public CreateGoalPage()
        {
            InitializeComponent();
        }

		async void OnAddGoal(object sender, EventArgs e)
		{
			var title = goalTitle.Text;
			var date = goalDate.Date;
			if (!string.IsNullOrWhiteSpace(title) && decimal.TryParse(goalPrice.Text, out decimal amount))
			{
				var contractService = DependencyService.Get<IContractService>();
				if (contractService != null)
				{
					if (contractService.AddGoal(date, amount, Guid.NewGuid()))
					{
						App.Goals.Add($"{title} date to: {date:g} amount: {amount:C}");
						await Navigation.PushAsync(new GoalListPage());
					}
				}
			}

		}
		
        async void OnViewGoals(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoalListPage());
        }
    }
}