using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
	public class Monster :LivingCreature {
		public int ID { get; set; }
		public string name { get; set; }
		public int maximumDamage { get; set; }
		public int rewardExperiencePoints { get; set; }
		public int rewardGold { get; set; }

		public Monster(int id, string cName, int cMaximumDamage, int rewardXP, int rewardG, int cCurrentHitPoints, int cMaximumHitPoints) : base(cCurrentHitPoints, cMaximumHitPoints) {
			ID = id;
			name = cName;
			maximumDamage = cMaximumDamage;
			rewardExperiencePoints = rewardXP;
			rewardGold = rewardG;
		}
	}
}
