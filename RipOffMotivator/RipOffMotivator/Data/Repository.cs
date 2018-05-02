using System;
using System.Collections.Generic;
using System.Linq;
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
		bool goalsDirty = false;
		IList<Tag> tags;
		bool tagsDirty = false;

		readonly Application appProperties;
		
		public Repository(Application appProperties)
		{
			this.appProperties = appProperties;
		}

		public IList<Goal> Goals => goals ?? (goals = ReadGoals() ?? new List<Goal>());
		public IList<Tag> Tags => tags ?? (tags = ReadTags()?? new List<Tag>());

		public void AddGoal(Goal goal)
		{
			goalsDirty = true;
			Goals.Add(goal);
		}

		public void AddTag(Tag tag)
		{
			tagsDirty = true;
			Tags.Add(tag);
		}

		public Task Commit()
		{
			var commit = false;
			if (goals != null && goalsDirty)
			{
				appProperties.Properties[GoalsKey] = Serialization.SerializeToJson(goals);
				goalsDirty = false;
				commit = true;
			}
			
			if (tags != null && tagsDirty)
			{
				appProperties.Properties[TagsKey] = Serialization.SerializeToJson(tags);
				tagsDirty = false;
				commit = true;
			}

			return commit ? appProperties.SavePropertiesAsync() : Task.Delay(0);
		}

		IList<Goal> ReadGoals()
		{
			if (appProperties.Properties.ContainsKey(GoalsKey))
			{
				var list = Serialization.DeserializeFromJson<IList<Goal>>((string)appProperties.Properties[GoalsKey]);
				goalsDirty = list.Any(g=> g.IsExpired(DateTime.Now));
				return list.Where(g=> !g.IsExpired(DateTime.Now)).ToList();
			}

			return null;
		}

		IList<Tag> ReadTags()
		{
			if (appProperties.Properties.ContainsKey(TagsKey))
				return Serialization.DeserializeFromJson<IList<Tag>>((string)appProperties.Properties[TagsKey]);
			return null;
		}

		public void RemoveTag(Tag tag)
		{
			if (Tags.Remove(tag))
			{
				tagsDirty = true;
			}
		}

		public void TagUsed(Guid tagId)
		{
			Tags.First(t => t.Id == tagId).Used = true;
			tagsDirty = true;
		}
	}
}
