using System; 
using System.Net; 
using System.Net.Sockets; 
using System.Text; 

namespace ClientSocket {
    public class SynchronousSocketClient {

    public static void StartClient() {

        byte[] bytes = new byte[1024]; 

        try {

            #region -- Pegando o server automaticamente --

            // var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());        
            // var ipAddress = ipHostInfo.AddressList[0]; 
            // var remoteEP = new IPEndPoint(ipAddress, 11000);
            // var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); 

            #endregion -- Pegando o server automaticamente --

            #region -- Server manual --

            System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            var remoteEP = new IPEndPoint(ipAddress, 11000);            
            var sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 

            #endregion -- Server manual --

            try {
                sender.Connect(remoteEP); 

                Console.WriteLine("Socket conectado a {0}", sender.RemoteEndPoint.ToString()); 

                // Encode the data string into a byte array.  
                byte[] msg = Encoding.ASCII.GetBytes("Mensagem de teste<EOF>"); 

                // Enviando
                int bytesSent = sender.Send(msg); 
                
                int bytesRec = sender.Receive(bytes); 
                Console.WriteLine("Retorno teste = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec)); 

                // Terminando o socket  
                sender.Shutdown(SocketShutdown.Both); 
                sender.Close(); 

            }catch (ArgumentNullException ane) {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString()); 
            }catch (SocketException se) {
                Console.WriteLine("SocketException : {0}", se.ToString()); 
            }catch (Exception e) {
                Console.WriteLine("Unexpected exception : {0}", e.ToString()); 
            }

        }catch (Exception e) {
            Console.WriteLine(e.ToString()); 
        }
    }
    }
}