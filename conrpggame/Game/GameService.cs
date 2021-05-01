using conrpggame.Adventures;
using conrpggame.Adventures.Interfaces;
using conrpggame.Entities.Interfaces;
using conrpggame.Entities.Models;
using conrpggame.Game.Interfaces;
using System;

namespace conrpggame.Game
{
    public class GameService : IGameService
    {
        private IAdventrueService adventrueService;
        private ICharacterService characterService;
        private Character Character;

        public GameService(IAdventrueService AdventrueService, ICharacterService CharacterService)
        {
            adventrueService = AdventrueService;
            characterService = CharacterService;
        }
        public bool StartGame(Adventrues adventures = null)
        {
             if (adventures == null)
                {
                    adventures = adventrueService.GetInitalAdventrue();
                }
                CreateTitleBanner(adventures.Title);
                CreateDescriptionBackcolor(adventures.Description);

                var charactersInRange = characterService.GetCharactersInRange(adventures.MinimumLevel, adventures.MaxLevel);
                if (charactersInRange.Count == 0)
                {
                    Console.WriteLine("很抱歉，您的等級不符合目前的區間，請重新選擇等級區間。");
                    return false;
                }
                else
                {
                    Console.WriteLine("誰想選擇死亡!?");
                    var characterCount = 0;
                    foreach (var character in charactersInRange)
                    {
                        Console.WriteLine($"#{characterCount} {character.Name} Level -{character.Level} {character.Class}");
                        characterCount++;
                    }
                    Character = characterService.LoadCharacter(charactersInRange[Convert.ToInt32(Console.ReadLine())].Name);

                    Monster Mymonster = new Monster();  //Dont need - kill for next level



                }
          
            return true;
        }

        private static void CreateDescriptionBackcolor(string description)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n{description.ToUpper()}");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void CreateTitleBanner(string title)
        {
            Console.Clear();
            Console.WriteLine();
            //create Title Banner
            for (int i = 0; i <= title.Length + 3; i++)
            {
                Console.Write("*");
                if (i == title.Length + 3)
                {
                    Console.WriteLine("\n");
                }

            }
            Console.WriteLine($"|{title}|");
            for (int i = 0; i <= title.Length + 3; i++)
            {
                Console.Write("*");
                if (i == title.Length + 3)
                {
                    Console.WriteLine("\n");
                }

            }
        }
    }
}
