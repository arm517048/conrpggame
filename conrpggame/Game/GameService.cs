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
using System.Linq;

namespace conrpggame.Game
{
    public class GameService : IGameService
    {
        private readonly IAdventrueService adventureService;
        private readonly ICharacterService characterService;
        private readonly IMessageHandler messageHandler;

        private Character character;
        private Adventrues gameAdventure;
        private bool gameWon = false;
        private string gameWinningDescription;

        public GameService(IAdventrueService AdventureService, ICharacterService CharacterService, IMessageHandler MessageHandler)
        {
            adventureService = AdventureService;
            characterService = CharacterService;
            messageHandler = MessageHandler;
        }
        public bool StartGame(Adventrues adventures = null)
        {
            gameAdventure = adventures;
            if (gameAdventure == null)
            {
                gameAdventure = adventureService.GetInitalAdventrue();
            }
            CreateTitleBanner(gameAdventure.Title);

            CreateDescriptionBanner(gameAdventure);

            var charactersInRange = characterService.GetCharactersInRange(gameAdventure.GUID, gameAdventure.MinimumLevel, gameAdventure.MaxLevel);
            if (charactersInRange.Count == 0)
            {
                messageHandler.Write("Sorry, you do not have any characters in the range level of the adventure you are trying to play.");
                messageHandler.Write("Would you like to:");
                messageHandler.Write("C)reate a new character");
                messageHandler.Write("R)eturn to the Main Menu?");
                var playerDecision = messageHandler.Read().ToLower();
                if (playerDecision == "r")
                {
                    messageHandler.Clear();
                    Program.MakeMainMenu();
                }
                else if (playerDecision == "c")
                {
                    messageHandler.Clear();
                    characterService.CreateCharacter();
                }
            }
            else
            {
                messageHandler.Write("WHO DOTH WISH TO CHANCE DEATH!?");
                var characterCount = 0;
                foreach (var character in charactersInRange)
                {
                    messageHandler.Write($"#{characterCount} {character.Name} Level - {character.Level} {character.Class}");
                    characterCount++;
                }
            }
            character = characterService.LoadCharacter(charactersInRange[Convert.ToInt32(messageHandler.Read())].Name);
            //Monster Mymonster = new Monster();  //Dont need - kill for next level
            var rooms = gameAdventure.Rooms;
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

            messageHandler.Write($"\nLevels : {adventrues.MinimumLevel} - {adventrues.MaxLevel}");
            messageHandler.Write($"\nCompletion Rewards = {adventrues.CompletionGolereward} gold & {adventrues.CompletionXPReward} xp");
            messageHandler.Write();
        }

        private void CreateTitleBanner(string title)//把Consolec換成messageHandler。因為已經建立messageHandler(訊息控制中心。所以蟲那發放
        {
            messageHandler.Clear();
            messageHandler.Write();

            //create Title Banner
            for (int i = 0; i <= title.Length + 3; i++)
            {
                messageHandler.Write("*", false);
                if (i == title.Length + 3)
                {
                    messageHandler.Write("\n");
                }
            }
            messageHandler.Write($"| {title} |");
            for (int i = 0; i <= title.Length + 3; i++)
            {
                messageHandler.Write("*", false);
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
            messageHandler.Write("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            messageHandler.Write($"{room.RoomNumber} {room.Description}");

            if (room.Exits.Count == 1)
            {
                messageHandler.Write($"There is an exit in this room on the {room.Exits[0].WallLocation} wall.");
            }
            else
            {
                var exitDescription = "";
                foreach (var exit in room.Exits)
                {
                    exitDescription += $"{exit.WallLocation},";
                }

                messageHandler.Write($"This room has exits on the {exitDescription.Remove(exitDescription.Length - 1)} walls.");
            }

            if (room.chest != null)
            {
                messageHandler.Write($"There is a chest in the room!");
            }
        }
        private void RoomOptions(Room room)
        {
            WriteRoomOptions(room);
            var playerDecision = messageHandler.Read().ToLower();
            var exitRoom = false;

            while (exitRoom == false)
            {
                switch (playerDecision)
                {
                    case "l":
                    case "c":
                        CheckForTraps(room);
                        WriteRoomOptions(room);
                        playerDecision = messageHandler.Read().ToLower();
                        break;
                    case "o":
                        if (room.chest != null)
                        {
                            OpenChest(room.chest);
                            if (gameWon)
                            {
                                GameOver();
                            }
                            WriteRoomOptions(room);
                            playerDecision = messageHandler.Read().ToLower();
                        }
                        else
                        {
                            messageHandler.Write("Alas, there is NO CHEST in this room!");
                        }
                        break;
                    case "n":
                    case "s":
                    case "e":
                    case "w":
                        var wallLocation = CompassDirection.North;
                        if (playerDecision == "s") wallLocation = CompassDirection.South;
                        else if (playerDecision == "w") wallLocation = CompassDirection.West;
                        else if (playerDecision == "e") wallLocation = CompassDirection.East;

                        if (room.Exits.FirstOrDefault(x => x.WallLocation == wallLocation) != null)
                        {
                            ExitRoom(room, wallLocation);
                        }
                        else
                        {
                            Console.WriteLine("\n Um... that's a wall friend....\n");
                        }

                        break;
                }
            }
        }
        private void WriteRoomOptions(Room room)
        {
            messageHandler.Write("WHAT WOULD YOU LIKE TO DO!?");
            messageHandler.Write("----------------------------");
            messageHandler.Write("L)ook for traps");
            if (room.chest != null)
            {
                messageHandler.Write("O)pen the chest");
                messageHandler.Write("C)heck chest for traps");
            }
            messageHandler.Write("Use an Exit:");
            foreach (var exit in room.Exits)
            {
                messageHandler.Write($"({exit.WallLocation.ToString().Substring(0, 1)}){exit.WallLocation.ToString().Substring(1)}");
            }
        }
        private void CheckForTraps(Room room)
        {
            if (room.tarp != null)
            {
                if (room.tarp.TrippedOrDisarmed)
                {
                    messageHandler.Write("You've already found and disarmed this trap... or tripped it (ouch)");
                    return;
                }

                if (room.tarp.SearchedFor)
                {
                    messageHandler.Write("You've already search for a trap, friend!");
                    return;
                }

                var trapBonus = 0 + character.Abilities.Intelligence;
                if (character.Class == CharacterClass.Thief)
                {
                    trapBonus += 2;
                }

                var dice = new Dice();
                var findTrapRoll = dice.RollDice(new List<Die> { Die.D32 }) + trapBonus;

                if (findTrapRoll < 12)
                {
                    messageHandler.Write("You find NO traps.");
                    room.tarp.SearchedFor = true;
                    return;
                }

                messageHandler.Write("You've found a trap! And are forced to try and disarm...");
                var disarmTrapRoll = dice.RollDice(new List<Die> { Die.D32 }) + trapBonus;

                if (disarmTrapRoll < 11)
                {
                    ProcessTrapMessagesAndDamage(room.tarp);
                }
                else
                {
                    messageHandler.Write("SHEW!!!  You disarmed the trap!");
                }
                room.tarp.TrippedOrDisarmed = true;
                return;
            }
            messageHandler.Write("You find no traps");
            return;
        }
        private void ProcessTrapMessagesAndDamage(Tarp tarp)
        {
            var dice = new Dice();

            messageHandler.Write($"CLANK! A sound of metal falls into place... you TRIPPED a {tarp.TrapType} trap!");
            var trapDamage = dice.RollDice(new List<Die>() { tarp.DamageDie });
            character.Hitpoints -= trapDamage;
            var hitPoints = character.Hitpoints;
            messageHandler.Write($"YOU WERE DAMAGED FOR {trapDamage} HIT POINTS!  You now have {hitPoints} hit pointss!");
            if (hitPoints < 1)
            {
                messageHandler.Write("AND......you're dead.");
                character.CauseOfDeath = $"Killed by a {tarp.TrapType}... it was ugly.";
                character.DiedInAdventure = gameAdventure.Title;
                character.IsAlive = false;
                GameOver();
            }
            messageHandler.Read();
        }

        private void OpenChest(Chest chest)
        {
            if (chest.Lock == null || !chest.Lock.Locked)
            {
                if (chest.tarp != null && !chest.tarp.TrippedOrDisarmed)
                {
                    ProcessTrapMessagesAndDamage(chest.tarp);
                    chest.tarp.TrippedOrDisarmed = true;
                }
                else
                {
                    messageHandler.Write("You open the chest..");
                    if (chest.Gold > 0)
                    {
                        character.Gold += chest.Gold;
                        messageHandler.Write($"Woot! You find {chest.Gold} gold! Your total gold is now {character.Gold}\n");
                        chest.Gold = 0;
                    }

                    if (chest.Treasure != null && chest.Treasure.Count > 0)
                    {
                        messageHandler.Write($"You find {chest.Treasure.Count} items in this chest!  And they are:");

                        foreach (var item in chest.Treasure)
                        {
                            messageHandler.Write(item.Name.ToString());

                            if (item.ObjectiveNumber == gameAdventure.FinalObjective)
                            {
                                gameWon = true;
                                gameWinningDescription = item.Description;
                                character.Gold += gameAdventure.CompletionGolereward;
                                character.XP += gameAdventure.CompletionXPReward;
                                character.AdventuresPlayed.Add(gameAdventure.GUID);
                            }
                        }
                        messageHandler.Write("\n");

                        character.Inventory.AddRange(chest.Treasure);
                        chest.Treasure = new List<Item>();

                        if (gameWon)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                            messageHandler.Write("***************************************************");
                            messageHandler.Write("*  ~~~YOU FOUND THE FINAL OBJECTIVE!  YOU WIN!~~~ *");
                            messageHandler.Write("***************************************************");

                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            messageHandler.Write("YOU FOUND : " + gameWinningDescription);
                            messageHandler.Write("XP Reward = " + gameAdventure.CompletionXPReward);
                            messageHandler.Write("Gold Reward = " + gameAdventure.CompletionGoldReward);
                            messageHandler.Write(character.Name + " now has " + character.XP + " XP and " + character.Gold + " gold.");
                        }
                        return;
                    }

                    if (chest.Gold == 0 && (chest.Treasure == null || chest.Treasure.Count == 0))
                    {
                        messageHandler.Write("The chest is empty... \n");
                    }
                }
            }
            else
            {
                if (TryUnlock(chest.Lock))
                {
                    OpenChest(chest);
                    if (gameWon)
                    {
                        GameOver();
                    }
                }
            }
        }
        private void Exitroom(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
