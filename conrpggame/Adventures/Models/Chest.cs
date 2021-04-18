using conrpggame.Adventures.Models;
using conrpggame.Items.Models;
using System.Collections.Generic;

namespace conrpggame.Entities.Models
{
    public class Chest
    {
        public bool Locked = false;
        public Tarp tarp;
        public List<Item> Treasure;
        public int Gold;
    }
}