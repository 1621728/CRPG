using System;
using System.Collections.Generic;
using System.Text;

namespace CRPG
{
    public class Player:LivingCreature
    {
        public string Name;
        public int Gold;
        public int ExperiencePoints;
        public int Level;
        public Location CurrentLocation;
        public List<InventoryItem> Inventory;
        public List<PlayerQuest> Quests;

        public Player(string name, int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints, int level): base(currentHitPoints, maximumHitPoints)
        {
            Name = name;
            Gold = gold;
            ExperiencePoints = experiencePoints;
            Level = level;
            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public Player() { }
        public void MoveTo(Location loc)
        {
            //Check To make sur if player has any required items
            if(loc.ItemRequiredToEnter != null)
            {
                //Check for items
                bool playerHasRequiredItem = false;
                foreach(InventoryItem ii in this.Inventory)
                {
                    if (ii.Details.ID == loc.ItemRequiredToEnter.ID)
                    {
                        playerHasRequiredItem = true;
                        break;
                    }
                }

                if (!playerHasRequiredItem)
                {
                    Console.WriteLine("\nYou must have a {0} to enter this location.", loc.ItemRequiredToEnter.Name);
                    return;
                }
            }


            CurrentHitPoints = MaximumHitPoints;
            CurrentLocation = loc;
            GameEngine.QuestProcessor(this, loc);
        }

        public void MoveNorth()
        {
            if(CurrentLocation.LocationToNorth != null)
            {
                MoveTo(CurrentLocation.LocationToNorth);
                Program.DisplayCurrentLocation();
            }
            else
            {
                Console.WriteLine("You cannot move north...");
            }
        }

        public void MoveEast()
        {
            if (CurrentLocation.LocationToEast != null)
            {
                MoveTo(CurrentLocation.LocationToEast);
                Program.DisplayCurrentLocation();
            }
            else
            {
                Console.WriteLine("You cannot move east...");
            }
        }

        public void MoveSouth()
        {
            if (CurrentLocation.LocationToSouth != null)
            {
                MoveTo(CurrentLocation.LocationToSouth);
                Program.DisplayCurrentLocation();
            }
            else
            {
                Console.WriteLine("You cannot move south...");
            }
        }

        public void MoveWest()
        {
            if (CurrentLocation.LocationToWest != null)
            {
                MoveTo(CurrentLocation.LocationToWest);
                Program.DisplayCurrentLocation();
            }
            else
            {
                Console.WriteLine("You cannot move west...");
            }
        }

    }//Class Player
}
