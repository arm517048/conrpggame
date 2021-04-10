using conrpggame.Adventures.Interfaces;
using System;



namespace conrpggame.Game
{
    public class GameService
    {
        private IAdventrueService adventrueService;

        public GameService(IAdventrueService AdventrueService)
        {
            adventrueService = AdventrueService;
        }
        public void StartGame()
        {
            var initailAdventrue = adventrueService.GetInitalAdventrue();
            Console.WriteLine($"Adventure : { initailAdventrue.Title}");
            Console.WriteLine($"Description : {initailAdventrue.Description}");
        }
    }
}
