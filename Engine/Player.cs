using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
	public class Player :LivingCreature {
		public int gold { get; set; }
		public int experiencePoints { get; set; }
		public int level { get; set; }
		public List<InventoryItem> inventory { get; set; }
		public List<PlayerQuest> quests { get; set; }
		public Location CurrentLocation { get; set; }

		public Player(int cCurrentHitPoints, int cMaximumHitPoints, int g, int xp, int lvl) : base(cCurrentHitPoints, cMaximumHitPoints) {
			gold = g;
			experiencePoints = xp;
			level = lvl;

			inventory = new List<InventoryItem>();
			quests = new List<PlayerQuest>();
		}
	}
}
