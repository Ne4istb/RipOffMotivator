using System;

using SmartContractsProxy;

using Xamarin.Forms;

namespace RipOffMotivator
{
    public partial class CreateGoalPage : ContentPage
    {
		readonly Lazy<ISmartContractsProxy> contractService = new Lazy<ISmartContractsProxy>(()=> new SmartContractsProxy.SmartContractsProxy());

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
				try
				{
					contractService.Value.AddGoal(amount, date, Guid.NewGuid());
					App.Goals.Add($"{title} date to: {date:g} amount: {amount:C}");
				}
				catch (Exception)
				{
					throw;
				}

				await Navigation.PushAsync(new GoalListPage());
			}

		}
		
        async void OnViewGoals(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoalListPage());
        }
    }
}