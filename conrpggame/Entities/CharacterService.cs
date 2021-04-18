﻿using conrpggame.Entities.Interfaces;
using conrpggame.Entities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;



namespace conrpggame.Entities
{
    public class CharacterService: ICharacterService
    {


        public Character LoadCharacter(string name)
        {
            var basePath = $"{AppDomain.CurrentDomain.BaseDirectory}Character";
            var Character = new Character();
            
            if (File.Exists($"{basePath}\\{name}.json"))
            {
                var directory = new DirectoryInfo(basePath);
                var characterJsonFile = directory.GetFiles($"{name}.json");


                using StreamReader fi = File.OpenText(characterJsonFile[0].FullName);
                Character = JsonConvert.DeserializeObject<Character>(fi.ReadToEnd());
            }
            else
            {
                throw new Exception("Character not found.");
            }

            return Character;
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
