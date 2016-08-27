using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
	public class LivingCreature {
		public int maximumHitPoints { get; set; }
		public int currentHitPoints { get; set; }
        public Race race { get; set; }

		public LivingCreature(int cCurrentHitPoints, int cMaximumHitPoints, Race startRace = Race.Monster) {
            race = startRace;
			maximumHitPoints = cMaximumHitPoints;
			currentHitPoints = cCurrentHitPoints;
		}
	}
}
