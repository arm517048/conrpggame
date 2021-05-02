using conrpggame.Adventures;
using conrpggame.Adventures.Interfaces;
using conrpggame.Adventures.Models;
using conrpggame.Entities.Interfaces;
using conrpggame.Entities.Models;
using conrpggame.Game.Interfaces;
using conrpggame.Utilities.Interfaces;
using conrpggame.Entities;
using System;
using System.Collections.Generic;

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

        private void CreateTitleBanner(string title)//把Consolec換成messageHandler。因為已經建立messageHandler(訊息控制中心。所以蟲那發放
        {
            messageHandler.Clear();
            //messageHandler.Write();

            //create Title Banner
            for (int i = 0; i <= title.Length + 3; i++)
            {
                Console.Write("*", false);
                if (i == title.Length + 3)
                {
                    messageHandler.Write("\n");
                }
            }
            messageHandler.Write($"| {title} |");
            for (int i = 0; i <= title.Length + 3; i++)
            {
                Console.Write("*", false);
                if (i == title.Length + 3)
                {
                    messageHandler.Write("\n");
                }
            }
        }
        private void RoomProcessor(Room room)//建立房間
        {
            RoomDescription(room);
            RoomOptions(room);
        }
        private void RoomDescription(Room room)
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
            if (room.chest != null)
            {
                messageHandler.Write($"看裡面有個箱子!!");
            }
        }
        private void RoomOptions(Room room)
        {
            messageHandler.Write("貪婪的冒險者阿~你想做什?");
            messageHandler.Write("------------------------");
            messageHandler.Write("L)ook for traps");
            messageHandler.Write("U)se an exit");
            foreach (var exit in room.Exits)
            {
                messageHandler.Write($"({exit.WallLocation.ToString().Substring(0, 1)}){exit.WallLocation.ToString().Substring(1)}");
            }
            if (room.chest != null)
            {
                messageHandler.Write("O)pen the chest");
                messageHandler.Write("C)heck the traps");
            }
            var playerDescription = messageHandler.Read().ToLower();
            var exitroom = false;

            while (exitroom == false)
            {
                switch (playerDescription)
                {
                    case "1":
                    case "c":
                        CheckForTraps(room);
                        break;
                    case "o":
                        if (room.chest != null)
                        {
                            Openchect(room.chest);
                        }
                        else
                        {
                            messageHandler.Write("裡面沒有箱子喔");
                        }
                        break;
                    case "n":
                    case "s":
                    case "e":
                    case "w":
                        Exitroom(room);
                        break;
                }
            }
        }
        private void CheckForTraps(Room room)
        {
            if (room.tarp != null)
            {
                if (room.tarp.TrippedOrDisarmed)
                {
                    messageHandler.Write("你看到陷阱但是閃躲跌倒了");
                    return;
                }
                if (room.tarp.SearchedFor)
                {
                    messageHandler.Write("已經勘查過了，沒必要再勘察");
                    return;
                }
                var tarpBouns = 0 + Character.Abilities.Intelligence;
                if (Character.Class == CharacterClass.Thief)
                {
                    tarpBouns += 2;
                }
                var dice = new Dice();
                var findTrapRoll = dice.RollDice(new List<Die> { Die.D32 }) + tarpBouns;

                if (findTrapRoll < 30)
                {
                    messageHandler.Write("你沒有發現陷阱");
                    room.tarp.SearchedFor = true;
                    return;
                }
                var disarmTarpRoll =dice.RollDice(new List<Die> { Die.D32})+tarpBouns;
                if (disarmTarpRoll < 25)
                {
                    messageHandler.Write("未發現陷阱，受到4點傷害");
                }
                else
                {
                    messageHandler.Write("發現陷阱啦~~");
                }
                room.tarp.TrippedOrDisarmed = true;
                return;
                
            }
            messageHandler.Write("裡面沒有陷阱");
            return;
        }
        private void Openchect(Chest chest)
        {
            throw new NotImplementedException();
        }
        private void Exitroom(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
