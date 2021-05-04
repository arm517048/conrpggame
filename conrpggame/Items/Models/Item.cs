using conrpggame.Items.Interfaces;

namespace conrpggame.Items.Models
{
    public class Item : IItem
    {
        public ItemType Name;
        public string Description;
        public int ObjectiveNumber;
        public int Weight;
        public int GoldValue;
    }

    public enum ItemType    //利用enum來列舉Itemtype包含的物品如:食物、水等相關
    {
        Rope, //繩索
        Torch, //火炬
        HolySymbol,
        Water,
        Food,
        Tinderbox,
        Key,
        Lockpicks

    }
}
