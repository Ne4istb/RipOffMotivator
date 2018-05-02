using System;

using Xamarin.Forms;

namespace RipOffMotivator
{
    public partial class AddTriggerPage : ContentPage
    {
		public AddTriggerPage()
        {
			
			InitializeComponent();
        }

		async void OnScan(object sender, EventArgs e)
		{
			var title = triggerName.Text;

		}
		
        async void OnViewGoals(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoalListPage());
        }
	}
}