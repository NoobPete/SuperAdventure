using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
	public class Location {
		public int ID { get; set; }
		public string name { get; set; }
		public string description { get; set; }

		public Item itemRequiredToEnter { get; set; }
		public Quest questAvailableHere { get; set; }
		public Monster monsterLivingHere { get; set; }
		public Location locationToNorth { get; set; }
		public Location locationToEast { get; set; }
		public Location locationToSouth { get; set; }
		public Location locationToWest { get; set; }

		public Location(int cid, string cname, string cdescription, Item cItemRequiredToEnter = null, Quest cQuestAvailableHere = null, Monster cMonsterLivingHere = null) {
			ID = cid;
			name = cname;
			description = cdescription;
			itemRequiredToEnter = cItemRequiredToEnter;
			questAvailableHere = cQuestAvailableHere;
			monsterLivingHere = cMonsterLivingHere;
		}
	}
}
