using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RipOffMotivator
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GoalListPage : ContentPage
	{
		public GoalListPage ()
		{
			InitializeComponent ();
		}

		async void OnCreateGoal(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CreateGoalPage());
		}

		void OnAddTrigger(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}