using conrpggame.Adventures;
using conrpggame.Entities;
using conrpggame.Game;
using conrpggame.Utilities;
using System;
using System.Media;

namespace conrpggame
{
    class Program
    {
        private static GameService gameService = new GameService(AdventureService, CharacterService, consoleMessageHandler);
        private static readonly AdventureService AdventureService = new AdventureService();
        private static readonly CharacterService CharacterService = new CharacterService(consoleMessageHandler);
        private static readonly ConsoleMessageHandler consoleMessageHandler = new ConsoleMessageHandler();
        
        static void Main(string[] args)
        {
           
            MakeTitle();    //標題
            using (SoundPlayer player = new SoundPlayer($"{AppDomain.CurrentDomain.BaseDirectory}/sounds/shortIntro.wav"))
            {
                player.Play();
                MakeMainMenu(player);//主選單
            }

        }
        private static void MakeTitle()
        {

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("***************************************************");
            Console.WriteLine("*                                                 *");
            Console.WriteLine("*      ┌─┐┌─┐┌┐┌┌─┐┌─┐┬  ┌─┐  ┌─┐┬─┐┌─┐┬ ┬┬       *");
            Console.WriteLine("*      │  │ ││││└─┐│ ││  ├┤   │  ├┬┘├─┤││││       *");
            Console.WriteLine("*      └─┘└─┘┘└┘└─┘└─┘┴─┘└─┘  └─┘┴└─┴ ┴└┴┘┴─┘     *");
            Console.WriteLine("*                                                 *");
            Console.WriteLine("***************************************************\n\n");
        }
        public static void MakeMainMenu(SoundPlayer player = null)
        {
            MakeMeunOptions();
            var inputInvalid = false;    //利用布林代數來做判斷 //預設是錯的
            try
            {

                while (!inputInvalid)       //如果inputInvalid不是錯的往下執行
                {
                    switch (Console.ReadLine().ToUpper())  //輸入選項並自動轉換成大寫 使用switch
                    {
                        case "S":
                            if (player != null) player.Stop();
                            gameService.StartGame();
                            inputInvalid = true;
                            break;
                        case "C":
                            CharacterService.CreateCharacter();
                            inputInvalid = true;
                            break;
                        case "L":
                            LoadGame();
                            inputInvalid = true;
                            break;
                        default:
                            Console.WriteLine("\n請重新選擇");
                            MakeMenuOptions();
                            inputInvalid = false;
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生了一些錯誤，地精逃獄了，現正抓回中。請稍待{ex.Message}");
            }
        }
    }

        private static void MakeMenuOptions()
        {
            Console.WriteLine("(S)tart a new game[開始新遊戲]");
            Console.WriteLine("(L)oad game[繼續遊戲]");
            Console.WriteLine("(C)reate new character[建立新角色]");
        }
        private static void LoadGame()
        {
            Console.WriteLine("Load a game, great job!");
        
        }       

    
}
