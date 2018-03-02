using System;  
using System.Net;  
using System.Net.Sockets;  
using System.Text;

namespace ServerSocket {

    public class SynchronousSocketListener {

    public static void StartListening() {
        
        string data = null; 

        byte[] bytes = new Byte[1024]; 

        #region -- Pegando o server automaticamente --

        // var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());        
        // var ipAddress = ipHostInfo.AddressList[0]; 
        // var localEndPoint = new IPEndPoint(ipAddress, 11000);
        // var listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); 

        #endregion -- Pegando o server automaticamente --

        #region -- Setando o server manualmente --

        var strEnderecoIP = "127.0.0.1";
        var localEndPoint = new IPEndPoint(IPAddress.Parse(strEnderecoIP), 11000);        
        var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        #endregion -- Setando o server manualmente --

        try {
            listener.Bind(localEndPoint); 
            listener.Listen(10); 

            // Start 
            while (true) {
                Console.WriteLine("Eperando conexão..."); 
                
                Socket handler = listener.Accept(); 
                data = null; 

                while (true) {
                    bytes = new byte[1024]; 
                    int bytesRec = handler.Receive(bytes); 
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec); 
                    if (data.IndexOf("<EOF>") > -1) {
                        break; 
                    }
                }
 
                Console.WriteLine("Texto recebido : {0}", data); 
 
                byte[] msg = Encoding.ASCII.GetBytes(data); 

                handler.Send(msg); 
                handler.Shutdown(SocketShutdown.Both); 
                handler.Close(); 
            }

        }catch (Exception e) {
            Console.WriteLine(e.ToString()); 
        }

        Console.WriteLine("\nPrecione enter para continuar..."); 
        Console.Read(); 
    }
    }
}