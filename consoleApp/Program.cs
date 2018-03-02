using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace consoleApp
{
    class Program
    {
        public static void Main(String[] args) {
            Console.WriteLine("Start Client Socket");
            ClientSocket.SynchronousSocketClient.StartClient();
        }  
    }
}
