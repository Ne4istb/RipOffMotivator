using System;

using Xamarin.Forms;

namespace RipOffMotivator
{
    public partial class AddTriggerPage : ContentPage
	{
		INFCIntegration nfc;
		public AddTriggerPage()
        {
			InitializeComponent();
        }

		async void OnScan(object sender, EventArgs e)
		{
			var title = triggerName.Text;
			nfc = DependencyService.Get<INFCIntegration>();

			await nfc.CreateNFCTag(title).ContinueWith(task =>
			{
				if (task.IsCompleted)
					StoreTag(task.Result, title);
			});

	}

		void StoreTag(Guid tagId, string title)
		{
			throw new NotImplementedException();
		}

		async void OnViewGoals(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoalListPage());
        }
	}
}