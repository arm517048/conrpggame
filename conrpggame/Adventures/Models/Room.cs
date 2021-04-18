using conrpggame.Adventures.Models;
using conrpggame.Entities.Models;
using System.Collections.Generic;

namespace conrpggame.Adventures.Models
{
    public class Room
    {
        public int RoomNumber;
        public string Description;
        public Tarp tarp;
        public List<Monster>Monsters;
        public Chest chest;
        public Objective FinalObjective;
        public List<Exit> Exits;
    }
}
