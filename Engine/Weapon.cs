using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
	public class Weapon :Item{
		public int minimumDamage { get; set; }
		public int maximumDamage { get; set; }

		public Weapon(int id, string cName, string cNamePlural, int cMinimumDamage, int cMaximumDamage) : base(id, cName, cNamePlural) {
			minimumDamage = cMinimumDamage;
			maximumDamage = cMaximumDamage;
		}
	}
}
