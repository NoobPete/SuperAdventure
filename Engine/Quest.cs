using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
	public class Quest {
		public int ID { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public int rewardExperiencePoints { get; set; }
		public int rewardGold { get; set; }
		public Item rewardItem { get; set; }
		public List<QuestCompletionItem> questCompletionItems { get; set; }

		public Quest (int id, string cName, string cDescription, int rewardXP, int rewardG) {
			ID = id;
			name = cName;
			description = cDescription;
			rewardExperiencePoints = rewardXP;
			rewardGold = rewardG;

			questCompletionItems = new List<QuestCompletionItem>();
		}
	}
}
