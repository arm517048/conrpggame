using conrpggame.Entities.Models;
using System.Collections.Generic;

namespace conrpggame.Entities.Interfaces
{
    public interface ICharacterService
    {
        public Character LoadCharacter(string name);

        public List<Character> GetCharactersInRange(int minLevel = 1, int maxLevel = 20);


    }
}
