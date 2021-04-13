using conrpggame.Adventures.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace conrpggame.Adventures
{
    class AdventureService :  IAdventrueService
    {
        public Adventrues GetInitalAdventrue()
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
            }
            return initailAdventrue;
        }    
    }
}
