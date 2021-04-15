using conrpggame.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace conrpggame.Items.Models
{
    public class Item:IItem
    {
        public Itemtype Name;
        public string Description;
        public int ObjectiveNumber;
        public int Weight;
        public int GoldValue;
    }

    public enum Itemtype    //利用enum來列舉Itemtype包含的物品如:食物、水等相關
    {
        Food,
        Water,
        Weapon,
        Rope,   //繩索
        Torch,  //火炬

    }
}
