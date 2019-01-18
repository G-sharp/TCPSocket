using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPServer
{
    class Program
    {
        static public Server instance_Server = new Server(8001);
        static void Main(string[] args)
        {
            
            //byte[] message = Encoding.ASCII.GetBytes("Server: Hello!");
            //instance_Server.Send(message);
            Thread[] thread = new Thread[100];
            for(int i = 0; i < 100; i++)
            {
                thread[i] = new Thread(Test);
                thread[i].Start();
            }
            Console.ReadKey();
        }
        static public void  Test()
        {
            byte[] _buf = new byte[1024 * 8];
            Socket ServerSocket = instance_Server.ServerSocket.Accept();
            while (!((ServerSocket.Poll(1000, SelectMode.SelectRead) && (ServerSocket.Available == 0)) || !ServerSocket.Connected))
            {
                try
                {
                    int len = ServerSocket.Receive(_buf);
                    byte[] msg = new byte[len];
                    Array.Copy(_buf, msg, len);
                    Console.WriteLine(DateTime.Now.ToString() + " ---接收消息为：["+ ((IPEndPoint)ServerSocket.RemoteEndPoint) .Address+ ":"+ ((IPEndPoint)ServerSocket.RemoteEndPoint).Port + "] "+ Encoding.ASCII.GetString(msg));
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    
                }
            }
        }

    }
}
