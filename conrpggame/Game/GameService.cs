using conrpggame.Adventures;
using conrpggame.Adventures.Interfaces;
using conrpggame.Entities.Interfaces;
using System;
using Newtonsoft.Json;



namespace conrpggame.Game
{
    public class GameService
    {
        private IAdventrueService adventrueService;
        private ICharacterService characterService;

        public GameService(IAdventrueService AdventrueService , ICharacterService CharacterService)
        {
            adventrueService = AdventrueService;
            characterService = CharacterService;
        }
        public void StartGame(Adventrues adventures = null)
        {
            try
            {
                if(adventures == null)
                {
                    adventures = adventrueService.GetInitalAdventrue();
                }
                Console.Clear();
                Console.WriteLine();
                //create Title Banner
                for (int i = 0; i <= adventures.Title.Length + 3; i++)
                {
                    Console.Write("*");
                    if(i == adventures.Title.Length + 3)
                    {
                        Console.WriteLine("\n");
                    }

                }
                Console.WriteLine($"|{adventures.Title}|");
                for (int i = 0; i <= adventures.Title.Length + 3; i++)
                {
                    Console.Write("*");
                    if (i == adventures.Title.Length + 3)
                    {
                        Console.WriteLine("\n");
                    }

                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n{adventures.Description.ToUpper()}");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;

                var initalCharcter = characterService.LoadInitialCharacter();

            //Console.WriteLine($"Adventure : { initailAdventrue.Title}");
            //Console.WriteLine($"Description : {initailAdventrue.Description}");
            Console.WriteLine($"Charcter Name :{initalCharcter.Name}");
            Console.WriteLine($"Level : {initalCharcter.Level}");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"發生了一些錯誤，目前正要從地牢逃脫中 {ex.Message}");
            }

        }
    }
}
