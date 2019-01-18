using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
namespace TCPClient
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Thread[] thread  = new Thread[4];
            for(int i = 0; i < 4; i++)
            {
                thread[i] = new Thread(Test);
                Thread.Sleep(1000);
                thread[i].Start();
            }
            //Test();
            Console.ReadKey();
        }
        static public void Test()
        {
            Client instance_Client = new Client("127.0.0.1", 8001);
            byte[] message = Encoding.ASCII.GetBytes("Client: Hello from "+Thread.CurrentThread.ManagedThreadId);
            while(instance_Client.isAlive())
            {
                instance_Client.Send(message);
                Thread.Sleep(3000);
            }

        }
    }
}
