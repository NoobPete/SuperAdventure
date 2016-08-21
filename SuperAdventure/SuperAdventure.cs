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

			player = new Player();
			player.currentHitPoints = 10;
			player.maximumHitPoints = 10;
			player.gold = 20;
			player.experiencePoints = 0;
			player.levell = 1;

			lblHitPoints.Text = player.currentHitPoints.ToString();
			lblGold.Text = player.gold.ToString();
			lblExperience.Text = player.experiencePoints.ToString();
			lblLevel.Text = player.levell.ToString();
		}

		
	}
}
