namespace conrpggame.Utilities.Interfaces
{
    public interface IMessageHandler
    {
        public void Write(string message = "");

        public string Read();

        /// <summary>
        /// 用來清理螢幕
        /// </summary>
        public void Clear();
    }
}
