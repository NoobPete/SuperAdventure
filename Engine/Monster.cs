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

	}
}
