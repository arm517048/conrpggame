using conrpggame.Adventures;
using System;

namespace conrpggame
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeTitle();        //標題
            MakeMainMenu();     //主選單
            Console.ReadLine();
            
        }
        private static void MakeTitle()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("**************************************************************************************");
            Console.WriteLine("*                                                                                    *");
            Console.WriteLine("*      ┌─┐┌─┐┌┐┌┌─┐┌─┐┬    ┌─┐  ┌─┐┬─┐ ┌─┐┬  ┬┬       *");
            Console.WriteLine("*      │    │  ││││└─┐│  ││    ├┤    │    ├┬┘ ├─┤││││       *");
            Console.WriteLine("*      └─┘└─┘┘└┘└─┘└─┘┴─┘└─┘  └─┘┴└─ ┴  ┴└┴┘┴─┘   *");
            Console.WriteLine("*                                                                                    *");
            Console.WriteLine("**************************************************************************************\n\n");
        }
        private static void MakeMainMenu()
        {
            MakeMeunOptions();
            var inputInvalid = false;    //利用布林代數來做判斷 //預設是錯的
            
            while (!inputInvalid)       //如果inputInvalid不是錯的往下執行
            {
                switch (Console.ReadLine().ToUpper())  //輸入選項並自動轉換成大寫 使用switch
                {
                    case "S":
                        StartGame();
                        inputInvalid = true;
                        break;
                    case "C":
                        Creatcher();
                        inputInvalid = true;
                        break;
                    case "L":
                        LoadGame();
                        inputInvalid = true;
                        break;
                    default:
                        
                        Console.WriteLine("\n請重新選擇");
                        MakeMeunOptions();
                        inputInvalid = false;
                        break;
                }
               
            }


        }

        private static void MakeMeunOptions()
        {
            Console.WriteLine("(S)tart a new game[開始新遊戲]");
            Console.WriteLine("(L)oad game[繼續遊戲]");
            Console.WriteLine("(C)reate new character[建立新角色]");
        }

        private static void LoadGame()
        {
            Console.WriteLine("讀取角色中");
        }

        private static void StartGame()
        {
            var basePath = $"{AppDomain.CurrentDomain.BaseDirectory}adventures";
            var initailAdventrue = new Adventrues();
            Console.WriteLine("遊戲啟程~祝好運");
        }
        private static void Creatcher()
        {
            Console.WriteLine("請創立角色");
        }
    }

    
}
