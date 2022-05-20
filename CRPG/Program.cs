using System;
// Benjamin Morell YYYY
namespace CRPG
{
    class Program
    {

        private static Player _player = new Player("Fred the Fearless", 10, 10, 20, 0, 1);
        static void Main(string[] args)
        {
            GameEngine.Initialize();
            _player.MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            //Console.WriteLine(RandomNumberGenerator.NumberBetween(4, 10));
            InventoryItem sword = new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1);
            InventoryItem club = new InventoryItem(World.ItemByID(World.ITEM_ID_CLUB), 1);
            InventoryItem aPass = new InventoryItem(World.ItemByID(World.ITEM_ID_ADVENTURER_PASS), 1);
            _player.Inventory.Add(sword);
            //_player.Inventory.Add(aPass);
            //_player.Inventory.Add(club);

            while (true)
            {
                Console.Write("> ");
                string userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    continue;
                }
                string cleanedInput = userInput.ToLower();

                if(cleanedInput == "exit")
                {
                    break;
                }
                ParseInput(cleanedInput);
            }//while

            
         
        }//Main

        public static void ParseInput(string input)
        {
            if (input.Contains("help"))
            {
                Console.WriteLine("Help is on the way...later...");

            } else if (input.Contains("look"))
            {
                //DisplayCurrentlocation
                DisplayCurrentLocation();
            }else if (input.Contains("north") || input == "n")
            {
                _player.MoveNorth();

            }
            else if (input.Contains("east") || input == "e")
            {
                _player.MoveEast();

            }
            else if (input.Contains("south") || input == "s")
            {
                _player.MoveSouth();

            }
            else if (input.Contains("west") || input == "w")
            {
                _player.MoveWest();

            }else if (input.Contains("debug"))
            {
                GameEngine.DebugInfo();
            }else if (input == "inventory" || input == "i")
            {
                Console.WriteLine("\nCurrent Inventory:");
                foreach (InventoryItem invItem in _player.Inventory)
                {
                    Console.WriteLine("\t{0} : {1}", invItem.Details.Name, invItem.Quantity);
                }
            }else if(input == "stats")
            {
                Console.WriteLine("\nStats for {0}", _player.Name);
                Console.WriteLine("\tCurrent HP: \t{0}", _player.CurrentHitPoints);
                Console.WriteLine("\tMaximum HP: \t{0}", _player.MaximumHitPoints);
                Console.WriteLine("\tXP: \t\t{0}", _player.ExperiencePoints);
                Console.WriteLine("\tLevel: \t\t{0}", _player.Level);
                Console.WriteLine("\tGold: \t\t{0}", _player.Gold);
            }else if (input == "quests")
            {
                if (_player.Quests.Count == 0)
                {
                    Console.WriteLine("\n You do not have any quests.\n");
                }
                else
                {
                    foreach (PlayerQuest playerQuest in _player.Quests)
                    {
                        Console.WriteLine("{0}: {1}", playerQuest.Details.Name, playerQuest.IsCompleted ? "Completed" : "Incomplete");
                    }
                }
            }
            else
            {
                Console.WriteLine("*****Cannot compute!*****");
            }
        }//ParseInput

        public static void DisplayCurrentLocation()
        {
            Console.WriteLine("\nYou are at: {0}", _player.CurrentLocation.Name);
            if(_player.CurrentLocation.Description != "")
            {
                Console.Write("\t{0}\n", _player.CurrentLocation.Description);

            }
        }
    }
}
