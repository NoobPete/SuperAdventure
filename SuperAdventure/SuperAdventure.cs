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
		private Monster currentMonster;

		public SuperAdventure() {
			InitializeComponent();

			player = new Player(10, 10, 20, 0, 1);
			moveTo(World.LocationByID(World.LOCATION_ID_HOME));
			player.inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));
			

			lblHitPoints.Text = player.currentHitPoints.ToString();
			lblGold.Text = player.gold.ToString();
			lblExperience.Text = player.experiencePoints.ToString();
			lblLevel.Text = player.level.ToString();
			lblRace.Text = player.race.ToString();
		}

		private void btnNorth_Click(object sender, EventArgs e) {
			moveTo(player.CurrentLocation.locationToNorth);
		}

		private void btnWest_Click(object sender, EventArgs e) {
			moveTo(player.CurrentLocation.locationToWest);
		}

		private void btnEast_Click(object sender, EventArgs e) {
			moveTo(player.CurrentLocation.locationToEast);
		}

		private void btnSouth_Click(object sender, EventArgs e) {
			moveTo(player.CurrentLocation.locationToSouth);
		}

		private void moveTo(Location newLocation) {
			//Does the location have any required items
			if (newLocation.itemRequiredToEnter != null) {
				// See if the player has the required item in their inventory
				bool playerHasRequiredItem = false;

				foreach(InventoryItem ii in player.inventory) {
					if(ii.details.ID == newLocation.itemRequiredToEnter.ID) {
						// We found the required item
						playerHasRequiredItem = true;
						break; // Exit out of the foreach loop
					}
				}

				if(!playerHasRequiredItem) {
					// we did not find the required item in their inventory, so display a message and stop trying to move
					rtbMessages.Text += "You must have a " + newLocation.itemRequiredToEnter.name + " to enter this location" + Environment.NewLine;
					return;
				}
			}

			// Update the player's current location
			player.CurrentLocation = newLocation;

			// Show/hide available movemt buttons
			btnNorth.Visible = (newLocation.locationToNorth != null);
			btnEast.Visible = (newLocation.locationToEast != null);
			btnSouth.Visible = (newLocation.locationToSouth != null);
			btnWest.Visible = (newLocation.locationToWest != null);

			// Display current location name and description
			rtbLocation.Text = newLocation.name + Environment.NewLine;
			rtbLocation.Text += newLocation.description + Environment.NewLine;

			// Completely heal the player
			player.currentHitPoints = player.maximumHitPoints;

			// Updates Hit Points in UI
			lblHitPoints.Text = player.currentHitPoints.ToString();

			// Does the location have a quest?
			if(newLocation.questAvailableHere != null) {
				// See if the player already has the quest, and if they have completed it
				bool playerAlreadyHasQuest = false;
				bool playerAlreadyCompletedQuest = false;

				foreach(PlayerQuest playerQuest in player.quests) {
					if(playerQuest.details.ID == newLocation.questAvailableHere.ID) {
						playerAlreadyHasQuest = true;

						if(playerQuest.isCompleted) {
							playerAlreadyCompletedQuest = true;
						}
					}
				}

				// See if the player already has the quest
				if(playerAlreadyHasQuest) {
					// If the player has not completed the quest yet
					if(!playerAlreadyCompletedQuest) {
						// See if the player has all the items needed to complete the quest
						bool playerHasAllItemsToCompleteQuest = true;

						foreach(QuestCompletionItem qci in newLocation.questAvailableHere.questCompletionItems) {
							bool foundItemInPlayersInventory = false;

							// Check each item in the player's inventory, to see if they have it, and enough of it
							foreach(InventoryItem ii in player.inventory) {
								// If the player has this item in their inventory
								if(ii.details.ID == qci.details.ID) {
									foundItemInPlayersInventory = true;
									if(ii.quantity < qci.quantity) {
										// The player does not have enough of this item to complete the quest
										playerHasAllItemsToCompleteQuest = false;

										// There is no reason to continue checking for the other quest completion items
										break;
									}

									// We found the item, so do not check the rest of the player's inventory
									break;
								}
							}

							// If we did not find the required item, set our variable and stop looking for other items
							if(!foundItemInPlayersInventory) {
								// The player does not have this item in their inventory
								playerHasAllItemsToCompleteQuest = false;

								// There is no reason to continue checking for the other quest completion items
								break;
							}
						}

						// The player has all items required to complete the quest
						if(playerHasAllItemsToCompleteQuest) {
							// Display message
							rtbMessages.Text += Environment.NewLine;
							rtbMessages.Text += "You completed the " + newLocation.questAvailableHere.name + " quest." + Environment.NewLine;

							// Remove quest items from inventory
							foreach(QuestCompletionItem qci in newLocation.questAvailableHere.questCompletionItems) {
								foreach(InventoryItem ii in player.inventory) {
									if(ii.quantity == qci.quantity) {
										// Subtract the quantity from the player's inventory that was needed to complete the quest
										ii.quantity -= qci.quantity;
										break;
									}
								}
							}

							// Give quest rewards
							rtbMessages.Text += "You receive: " + Environment.NewLine;
							rtbMessages.Text += newLocation.questAvailableHere.rewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
							rtbMessages.Text += newLocation.questAvailableHere.rewardGold.ToString() + " gold" + Environment.NewLine;
							rtbMessages.Text += newLocation.questAvailableHere.rewardItem + Environment.NewLine;

							player.experiencePoints += newLocation.questAvailableHere.rewardExperiencePoints;
							player.gold += newLocation.questAvailableHere.rewardGold;

							// Add the reward item to the player's inventory
							bool addedItemToPlayerInventory = false;

							foreach(InventoryItem ii in player.inventory) {
								if(ii.details.ID == newLocation.questAvailableHere.rewardItem.ID) {
									// They have the item in their inventory, so increase the quantity by one
									ii.quantity++;

									addedItemToPlayerInventory = true;

									break;
								}
							}

							// They did not have the item, so add it to their inventory, with the quantity of 1
							if (!addedItemToPlayerInventory) {
								player.inventory.Add(new InventoryItem(newLocation.questAvailableHere.rewardItem, 1));
							}

							// Mark the quest as completed
							// Find the quest in the player's quest list
							foreach(PlayerQuest pq in player.quests) {
								if (pq.details.ID == newLocation.questAvailableHere.ID) {
									// Mark it as completed
									pq.isCompleted = true;

									break;
								}
							}
						}
					}
				}
				else {
					// The player does not already have the quest

					// Display the messages
					rtbMessages.Text += "You receive the " + newLocation.questAvailableHere.name + " quest." + Environment.NewLine;
					rtbMessages.Text += newLocation.questAvailableHere.description + Environment.NewLine;
					rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
					foreach (QuestCompletionItem qci in newLocation.questAvailableHere.questCompletionItems) {
						if(qci.quantity == 1) {
							rtbMessages.Text += qci.quantity.ToString() + " " + qci.details.name + Environment.NewLine;
						}
					}
					rtbMessages.Text += Environment.NewLine;

					// Add the quest to the player's quest list
					player.quests.Add(new PlayerQuest(newLocation.questAvailableHere));
				}
			}

			// Does the location have a monster=
			if(newLocation.monsterLivingHere != null) {
				rtbMessages.Text += "You see a " + newLocation.monsterLivingHere.name + Environment.NewLine;
				
				// Make a new monster, using the values from the standard monster in the World.Monster list
				Monster standardMonster = World.MonsterByID(newLocation.monsterLivingHere.ID);
				currentMonster = new Monster(standardMonster.ID, standardMonster.name, standardMonster.maximumDamage, standardMonster.rewardExperiencePoints, standardMonster.rewardGold, standardMonster.currentHitPoints, standardMonster.maximumHitPoints);
				foreach (LootItem lootItem in standardMonster.lootTable) {
					currentMonster.lootTable.Add(lootItem);
				}

				cboWeapons.Visible = true;
				cboPotions.Visible = true;
				btnUseWeapon.Visible = true;
				btnUsePotion.Visible = true;
			}
			else {
				currentMonster = null;

				cboWeapons.Visible = false;
				cboPotions.Visible = false;
				btnUseWeapon.Visible = false;
				btnUsePotion.Visible = false;
			}

			// Refresh the player's inventory list
			dgvInventory.RowHeadersVisible = false;

			dgvInventory.ColumnCount = 2;
			dgvInventory.Columns[0].Name = "Name";
			dgvInventory.Columns[0].Width = 197;
			dgvInventory.Columns[1].Name = "Quantity";

			dgvInventory.Rows.Clear();

			foreach(InventoryItem inventoryItem in player.inventory) {
				if(inventoryItem.quantity > 0) {
					dgvInventory.Rows.Add(new[] { inventoryItem.details.name, inventoryItem.quantity.ToString() });
				}
			}

			// Refresh the player's quest list
			dgvQuests.ColumnCount = 2;
			dgvQuests.Columns[0].Name = "Name";
			dgvQuests.Columns[0].Width = 197;
			dgvQuests.Columns[1].Name = "Done?";

			dgvQuests.Rows.Clear();

			foreach(PlayerQuest playerQuest in player.quests) {
				dgvQuests.Rows.Add(new[] { playerQuest.details.name, playerQuest.isCompleted.ToString() });
			}

			// Refresh the player's weapons combobox
			List<Weapon> weapons = new List<Weapon>();

			foreach(InventoryItem inventoryItem in player.inventory) {
				if(inventoryItem.details is Weapon) {
					if(inventoryItem.quantity > 0) {
						weapons.Add((Weapon)inventoryItem.details);
					}
				}
			}

			if(weapons.Count == 0) {
				// The player does not have any weapon, so hide the weapon combobox and the "Use" button
				cboWeapons.Visible = false;
				btnUseWeapon.Visible = false;
			}
			else {
				cboWeapons.DataSource = weapons;
				cboWeapons.DisplayMember = "Name";
				cboWeapons.ValueMember = "ID";

				cboWeapons.SelectedIndex = 0;
			}

			// Refresh the player's potions combobox
			List<HealingPotion> healingPotions = new List<HealingPotion>();

			foreach (InventoryItem inventoryItem in player.inventory) {
				if (inventoryItem.details is HealingPotion) {
					if (inventoryItem.quantity > 0) {
						healingPotions.Add((HealingPotion)inventoryItem.details);
					}
				}
			}

			if (healingPotions.Count == 0) {
				// The player does not have any potions, so hide the potion combobox and the "Use" button
				cboPotions.Visible = false;
				btnUsePotion.Visible = false;
			}
			else {
				cboPotions.DataSource = healingPotions;
				cboPotions.DisplayMember = "Name";
				cboPotions.ValueMember = "ID";

				cboWeapons.SelectedIndex = 0;
			}
		}

		private void btnUseWeapon_Click(object sender, EventArgs e) {

		}

		private void btnUsePotion_Click(object sender, EventArgs e) {

		}
	}
}
