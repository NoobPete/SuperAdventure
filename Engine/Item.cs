using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
	public class Item {
		public int ID { get; set; }
		public string name { get; set; }
		public string namePlural { get; set; }

		public Item (int id, string cName, string cNamePlural) {
			ID = id;
			name = cName;
			namePlural = cNamePlural;
		}
	}
}
