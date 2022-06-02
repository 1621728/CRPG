using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NAudio;
using System.Windows.Forms;

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
            }else if (input.Contains("attack") || input.Contains("a"))
            {
                if(_player.CurrentLocation.MonsterLivingHere == null)
                {
                    Console.WriteLine("\nThere is nothing here to attack.\n");
                }
                else
                {
                    if (_player.CurrentWeapon == null)
                    {
                        Console.WriteLine("\nYou are unarmed.\n");
                    }
                    else
                    {
                        _player.UseWeapon(_player.CurrentWeapon);


                        //Battle music
                        //NAudio.Wave.WaveFileReader wave;
                        //NAudio.Wave.DirectSoundOut output;

                        //OpenFileDialog open = new OpenFileDialog();
                        //open.Filter = "MP3 File (*.mp3)|*.mp3;";
                        //if (open.ShowDialog() != DialogResult.OK) return;

                        //wave = new NAudio.Wave.WaveFileReader(open.FileName);

                        //output = new NAudio.Wave.DirectSoundOut();
                        //output.Init(new NAudio.Wave.WaveChannel32(wave));
                        //output.Play();
                    }
                }
            }else if (input.StartsWith("equip "))
            {
                _player.UpdateWeapons();
                string inputWeaponName = input.Substring(6).Trim();
                if (string.IsNullOrEmpty(inputWeaponName))
                {
                    Console.WriteLine("\nYou must enter the name of the weapon to equip it.\n");
                }
                else
                {
                    Weapon weaponToEquip = _player.Weapons.SingleOrDefault( x => x.Name.ToLower() == inputWeaponName || x.NamePlural.ToLower() == inputWeaponName);

                    if(weaponToEquip == null)
                    {
                        Console.WriteLine("\nYou do not have the weapon {0}.\n", inputWeaponName);
                    }
                    else
                    {
                        _player.CurrentWeapon = weaponToEquip;
                        Console.WriteLine("\nEquipped {0}.\n", inputWeaponName);
                    }
                }
            }else if(input == "weapons")
            {
                _player.UpdateWeapons();
                Console.WriteLine("\nList of Weapons: \n");
                foreach(Weapon w in _player.Weapons)
                {
                    Console.WriteLine("\t{0}", w.Name);
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
