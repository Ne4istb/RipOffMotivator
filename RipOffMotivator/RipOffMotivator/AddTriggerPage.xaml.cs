using System;

using RipOffMotivator.Data;
using RipOffMotivator.Models;

using Xamarin.Forms;

namespace RipOffMotivator
{
    public partial class AddTriggerPage : ContentPage
	{
		readonly Repository repo;
		INFCIntegration nfc;
		public AddTriggerPage(Repository repo)
		{
			this.repo = repo;
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
			repo.AddTag(new Tag{Id= tagId, Title = title});
			repo.Commit().Wait();
		}

		async void OnViewGoals(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoalListPage(repo));
        }
	}
}