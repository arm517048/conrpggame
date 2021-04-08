using conrpggame.Adventures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace conrpggame.Game
{
    public class GameService
    {
        public void StartGame()
        {
            var basePath = $"{AppDomain.CurrentDomain.BaseDirectory}adventures";
            var initailAdventrue = new Adventrues();
            Console.WriteLine("遊戲啟程~祝好運");
            if (File.Exists($"{basePath}\\intial.json"))
            {
                var directory = new DirectoryInfo(basePath);
                var intailJsonFile = directory.GetFiles("intial.json");


                using (StreamReader fi = File.OpenText(intailJsonFile[0].FullName))
                {
                    initailAdventrue = JsonConvert.DeserializeObject<Adventrues>(fi.ReadToEnd());
                }

                Console.WriteLine($"Adventure : { initailAdventrue.Title}");
                Console.WriteLine($"Description : {initailAdventrue.Description}");
            }
        }
    }
}
