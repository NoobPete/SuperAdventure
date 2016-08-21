using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace SuperAdventure {
	public partial class SuperAdventure : Form {
		private Player player;
		public SuperAdventure() {
			InitializeComponent();

			Location location = new Location(1, "Home", "This is your house");

			player = new Player(10, 10, 20, 0, 1);
			
			lblHitPoints.Text = player.currentHitPoints.ToString();
			lblGold.Text = player.gold.ToString();
			lblExperience.Text = player.experiencePoints.ToString();
			lblLevel.Text = player.level.ToString();
		}

		
	}
}
