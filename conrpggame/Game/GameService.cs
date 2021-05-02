using System;
using System.Collections.Generic;
using System.Linq;
using conrpggame.Adventures;
using conrpggame.Adventures.Interfaces;
using conrpggame.Adventures.Models;
using conrpggame.Entities.Interfaces;
using conrpggame.Entities.Models;
using conrpggame.Game.Interfaces;
using conrpggame.Utilities;
using conrpggame.Utilities.Interfaces;
using conrpggame.Adventures.Models;

namespace conrpggame.Game
{
    public class GameService : IGameService
    {
        private IAdventrueService adventrueService;
        private ICharacterService characterService;
        private IMessageHandler messageHandler;

        private Character Character;

        public GameService(IAdventrueService AdventureService, ICharacterService CharacterService, IMessageHandler MessageHandler)
        {
            adventrueService = AdventureService;
            characterService = CharacterService;
            messageHandler = MessageHandler;

        }
        public bool StartGame(Adventrues adventures = null)
        {
            if (adventures == null)
            {
                adventures = adventrueService.GetInitalAdventrue();
            }
            CreateTitleBanner(adventures.Title);

            CreateDescriptionBackcolor(adventures);

            var charactersInRange = characterService.GetCharactersInRange(adventures.MinimumLevel, adventures.MaxLevel);
            if (charactersInRange.Count == 0)
            {
                messageHandler.Write("很抱歉，您的等級不符合目前的區間，請重新選擇等級區間。");
                return false;
            }
            else
            {
                messageHandler.Write("誰想選擇死亡!?");
                var characterCount = 0;
                foreach (var character in charactersInRange)
                {
                    messageHandler.Write($"#{characterCount} {character.Name} Level -{character.Level} {character.Class}");
                    characterCount++;
                }
            }
            Character = characterService.LoadCharacter(charactersInRange[Convert.ToInt32(messageHandler.Read())].Name);
            //Monster Mymonster = new Monster();  //Dont need - kill for next level
            var rooms = adventures.rooms;
            RoomProcessor(rooms[0]);
            return true;
        }

        private void RoomProcessor(Room room)
        {
            messageHandler.Clear();
            messageHandler.Write("~!@#$%^&~~~~~~~~~~~~~~");

            messageHandler.Write($"{room.RoomNumber} {room.Description} ");

            if (room.Exits.Count == 1)
            {
                messageHandler.Write($"出口在那邊~{room.Exits[0].WallLocation} 的牆.");
            }
            else
            {
                var exitDescription = "";
                foreach (var exit in room.Exits)
                {
                    exitDescription += $"{exit.WallLocation},";
                }
                messageHandler.Write($"出口在那邊~{exitDescription.Remove(exitDescription.Length - 1)} 的牆.");
            }



        }

        private void CreateDescriptionBackcolor(Adventrues adventrues)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            messageHandler.Write($"\n{adventrues.Description.ToUpper()}");

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            messageHandler.Write($"\nLevels : {adventrues.MinimumLevel}-{adventrues.MaxLevel}");
            messageHandler.Write($"\nCompletion Rewards={adventrues.CompletionGolereward}gold & {adventrues.CompletionXPReward}xp");
            messageHandler.Write();
        }

        private void CreateTitleBanner(string title)
        {
            messageHandler.Clear();
            messageHandler.Write();

            //create Title Banner
            for (int i = 0; i <= title.Length + 3; i++)
            {
                messageHandler.Write("*");
                if (i == title.Length + 3)
                {
                    messageHandler.Write("\n");
                }

            }
            messageHandler.Write($"|{title}|");
            for (int i = 0; i <= title.Length + 3; i++)
            {
                messageHandler.Write("*");
                if (i == title.Length + 3)
                {
                    messageHandler.Write("\n");
                }

            }
        }
    }
}
