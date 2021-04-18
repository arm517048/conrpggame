

using conrpggame.Adventures.Models;
using System.Collections.Generic;

namespace conrpggame.Adventures
{
    public class Adventrues
    {
        public string GUID;//圖形介面
        public string Title;//標題
        public string Description;//描述
        public int CompletionXPReward;//完成獎勵(XP)
        public int CompletionGolereward;//完成獎勵(金錢)
        public int MaxLevel; //(最大等級
        public int MinimumLevel; //最小等級)
        public List<Room> rooms;




        public Adventrues()
        {

        }
        
    }

}
