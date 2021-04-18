using conrpggame.Entities.Interfaces;
using conrpggame.Entities.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace conrpggame.Entities
{
    public class CharacterService: ICharacterService
    {
        public Character LoadInitialCharacter()
        {
            var basePath = $"{AppDomain.CurrentDomain.BaseDirectory}Character";
            var initailCharacter = new Character();
            
            if (File.Exists($"{basePath}\\conan.json"))
            {
                var directory = new DirectoryInfo(basePath);
                var intailJsonFile = directory.GetFiles("conan.json");


                using StreamReader fi = File.OpenText(intailJsonFile[0].FullName);
                initailCharacter = JsonConvert.DeserializeObject<Character>(fi.ReadToEnd());
            }
            else
            {
                throw new Exception("Initail character not found.");
            }

            return initailCharacter;
        }
    }
}
