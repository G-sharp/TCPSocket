using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
namespace TCPClient
{
    class Client
    {
        
        private int _port;
        private Socket ClientSocket;
        private IPAddress _ip;  //将IP地址字符串转换成IPAddress实例
        static byte[] _buf = new byte[1024 * 10];
        public Client(String IP = "127.0.0.1", int Port = 8001)
        {
            _port = Port;
            _ip = IPAddress.Parse(IP);
            if (Connect())
            {
                Console.WriteLine("Client : Connected to {0}:{1} Succeed!", IP , Port);
            }

        }
        
        private bool Connect()
        {
            try
            {
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.Connect(_ip, _port);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Send( byte [] msg)
        {
            try
            {
                ClientSocket.Send(msg);
                Console.WriteLine(DateTime.Now.ToString() + "---发送消息为：" + Encoding.ASCII.GetString(msg));
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public byte[] Recv()
        {
            try
            {
                int len = ClientSocket.Receive(_buf);
                byte[] msg = new byte[len];
                Array.Copy(_buf, msg, len);
                Console.WriteLine(DateTime.Now.ToString() + "---接收消息为：" + Encoding.ASCII.GetString(msg));
                return msg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public bool isAlive()
        {
            
            return (!((ClientSocket.Poll(1000, SelectMode.SelectRead) && (ClientSocket.Available == 0)) || !ClientSocket.Connected));

        }
    }
}
