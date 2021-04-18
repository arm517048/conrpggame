namespace conrpggame.Adventures
{
        public class Adventrues
    {
        public string GUID { get; set; }//圖形介面
        public string Title { get; set; }//標題
        public string Description { get; set; }//描述
        public int CompletionXPReward { get; set; } //完成獎勵(XP)
        public int CompletionGolereward { get; set; }//完成獎勵(金錢)
        public int MaxLevel { get; set; } //(最大等級
        public int MinimumLevel { get; set; }//最小等級)


        public Adventrues()
        {

        }
        /// <summary>
        /// 3個斜線可以直接註解
        /// </summary>
        public void mymethod()
        {

        }
    }

}
