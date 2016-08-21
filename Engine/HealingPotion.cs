using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
	public class HealingPotion : Item{
		public int amountToHeal { get; set; }

		public HealingPotion(int id, string cName, string cNamePlural, int cAmountToHeal) : base(id, cName, cNamePlural) {
			amountToHeal = cAmountToHeal;
		}
	}
}
