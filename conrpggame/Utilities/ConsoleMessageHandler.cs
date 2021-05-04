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

        public void Write(string message = "", bool withLine = true)
        {
            if (withLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
        }
        public void WriteRead(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
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
