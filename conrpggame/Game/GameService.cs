﻿using conrpggame.Adventures.Interfaces;
using conrpggame.Entities.Interfaces;
using System;



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
        public void StartGame()
        {
            var initailAdventrue = adventrueService.GetInitalAdventrue();
            var initalCharcter = characterService.LoadInitialCharacter();

            Console.WriteLine($"Adventure : { initailAdventrue.Title}");
            Console.WriteLine($"Description : {initailAdventrue.Description}");
            Console.WriteLine($"Charcter Name :{initalCharcter.Name}");
            Console.WriteLine($"Level : {initalCharcter.Level}");
        }
    }
}
