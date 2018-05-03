using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace RipOffMotivator.Models
{
   public class TagList : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Tag> _items;

		public ObservableCollection<Tag> Items
		{
			get { return _items; }
			set { _items = value; OnPropertyChanged("Items"); }
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public TagList(IList<Tag> itemList)
		{
			Items = new ObservableCollection<Tag>();
			foreach (var itm in itemList)
			{
				Items.Add(itm);
			}
		}
	}
}
