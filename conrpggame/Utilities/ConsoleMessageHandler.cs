using conrpggame.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace conrpggame.Utilities
{
    public class ConsoleMessageHandler : IMessageHandler    
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string message = "")
        {
            Console.WriteLine(message);
        }
        /// <summary>
        /// 用來清理螢幕
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }
    }
}
