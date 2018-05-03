using System;

using RipOffMotivator.Data;
using RipOffMotivator.Models;

using Xamarin.Forms;

namespace RipOffMotivator
{
    public partial class AddTagPage : ContentPage
	{
		readonly Repository repo;
		INFCIntegration nfc;

		public AddTagPage(Repository repo)
		{
			this.repo = repo;
			InitializeComponent();
		}

		void OnScan(object sender, EventArgs e)
		{
			var title = triggerName.Text;
			if(string.IsNullOrWhiteSpace(title))
				return;

			nfc = DependencyService.Get<INFCIntegration>();

			nfc.CreateNFCTag(title, StoreTag);
		}

		async void StoreTag(string tagId, string title)
		{
			repo.AddTag(new Tag{Id= tagId, Title = title});
            await repo.Commit();
            await Navigation.PushAsync(new TagsListPage(repo));

		}

		async void OnViewGoals(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoalListPage(repo));
        }

		async void OnViewTags(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new TagsListPage(repo));
		}
	}
}