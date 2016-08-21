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

		public Location(int cid, string cname, string cdescription) {
			ID = cid;
			name = cname;
			description = cdescription;
		}
	}
}
