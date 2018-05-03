using System;
using System.Linq;

using RipOffMotivator.Data;
using RipOffMotivator.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RipOffMotivator
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TagsListPage : ContentPage
	{
		readonly Repository repo;
		TagList tags;

		public TagsListPage(Repository repo)
		{
			InitializeComponent ();
			this.repo = repo;
			tags = new TagList(this.repo.Tags);
			listView.ItemsSource = tags.Items;
		}

		void OnRemoveTag(object sender, EventArgs e)
		{
			var item = (Button)sender;
			var tag = tags.Items.SingleOrDefault(t => t.SerialNumber == (string) item.CommandParameter);
			if(tag==null)
				return;

			repo.RemoveTag(tag);
			tags.Items.Remove(tag);
			repo.Commit();
		}

		async void OnAddTag(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTagPage(repo));
		}
	}
}