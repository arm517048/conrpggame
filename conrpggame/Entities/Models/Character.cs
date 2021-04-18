using conrpggame.Items.Interfaces;
using System.Collections.Generic;

namespace conrpggame.Entities.Models
{
    public class Character :Entity //建立基本資料
    {
        public string Name;
        public int Level;
        public Abilities Abilities;     //使用屬性的method
        public int Gold;
        public string Background;
        public int InventoryWeghit;
        public List<string> AdventruesPlated;
        public bool IsAlive;
        public int ArmoClass;
        public List<IItem> Inventory;
        public CharacterClass Class;

    }
    public class Abilities  //建立屬性
    {
        public int strength;    //力量
        public int Dexterity;   //靈巧
        public int Constitution;//體質
        public int Intelligence;//智力
        public int Wisdom;      //智慧
        public int Charisma;    //魅力
        public int Lucky;       //幸運
        
    }
    public enum CharacterClass//職業選單
    {
        Fighter,
        Thief,
        MagicUser,
        Healer,
    }
}
