using System;
using System.Collections.Generic;
using System.Text;

namespace CRPG
{
    public static class GameEngine
    {
        public static string Version = "0.0.3";

        public static void Initialize()
        {
            Console.WriteLine("Initializing Game Engine Version {0}", Version);
            Console.WriteLine("\n\nWelcome to the World of {0}", World.WorldName);
            Console.WriteLine();
            World.ListLocations();

        }

        public static void DebugInfo()
        {
            World.ListLocations();
            World.ListItems();
            World.ListMonsters();
            World.ListQuests();

        }

        public static void QuestProcessor(Player player, Location location)
        {
            if(location.QuestAvailableHere != null)
            {
                bool playerAlreadyHasQuest = false;
                bool playerAlreadyCompletedQuest = false;
                Console.WriteLine("Debug: Q is here!::::::");
                Console.WriteLine("\t{0}", location.QuestAvailableHere.Description);

                foreach ( PlayerQuest playerQuest in player.Quests)
                {
                    if(playerQuest.Details.ID == location.QuestAvailableHere.ID)
                    {
                        playerAlreadyHasQuest = true;

                        if (playerQuest.IsCompleted)
                        {
                            playerAlreadyCompletedQuest = true;
                        }
                    }
                }//Foreach


            }//location.QuestAvailable here.
        }//QuestProcessor
    }
}
