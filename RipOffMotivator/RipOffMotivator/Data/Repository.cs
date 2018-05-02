using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RipOffMotivator.Models;

using Xamarin.Forms;

namespace RipOffMotivator.Data
{
    public class Repository
	{
		const string GoalsKey = "GOALS";
		const string TagsKey = "TAGS";
		IList<Goal> goals;
		int goalsCount = 0;
		IList<Tag> tags;
		int tagsCount = 0;

		readonly Application appProperties;
		
		public Repository(Application appProperties)
		{
			this.appProperties = appProperties;
		}

		public IList<Goal> Goals => goals ?? (goals = ReadGoals() ?? new List<Goal>());
		public IList<Tag> Tags => tags ?? (tags = ReadTags()?? new List<Tag>());

		public void AddGoal(Goal goal)
		{
			Goals.Add(goal);
		}

		public void AddTag(Tag tag)
		{
			Tags.Add(tag);
		}

		public Task Commit()
		{
			if (goals != null && goalsCount != goals.Count)
				appProperties.Properties[GoalsKey] = Serialization.SerializeToJson(goals);
			if (tags != null && tagsCount != tags.Count)
				appProperties.Properties[TagsKey] = Serialization.SerializeToJson(tags);

			return appProperties.SavePropertiesAsync();
		}

		IList<Goal> ReadGoals()
		{
			if (appProperties.Properties.ContainsKey(GoalsKey))
			{
				var list = Serialization.DeserializeFromJson<IList<Goal>>((string)appProperties.Properties[GoalsKey]);
				goalsCount = list.Count;
				return list;
			}

			return null;
		}

		IList<Tag> ReadTags()
		{
			if (appProperties.Properties.ContainsKey(TagsKey))
			{
				var list = Serialization.DeserializeFromJson<IList<Tag>>((string)appProperties.Properties[TagsKey]);
				tagsCount = list.Count;
				return list;
			}
			return null;
		}
	}
}
