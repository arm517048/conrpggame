using conrpggame.Entities.Interfaces;
using conrpggame.Entities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public List<Character> GetCharactersInRange(int minLevel = 1, int maxLevel = 20)
        {
            var basePath = $"{AppDomain.CurrentDomain.BaseDirectory}Character";
            var charactersInRange = new List<Character>();
            try
            {
                var directory = new DirectoryInfo(basePath);
                foreach (var file in directory.GetFiles($"*.json"))
                {
                    using (StreamReader fi = File.OpenText(file.FullName))
                    {
                        var potentialCharacterInRange = JsonConvert.DeserializeObject<Character>(fi.ReadToEnd());
                        if (potentialCharacterInRange.IsAlive && (potentialCharacterInRange.Level >= minLevel && potentialCharacterInRange.Level <= maxLevel))
                        {
                            charactersInRange.Add(potentialCharacterInRange);
                        }
                    }                        
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"發生了一些錯誤，地經跑出來了   {ex.Message}");
            }
            return charactersInRange;
            
        }
    }
}
