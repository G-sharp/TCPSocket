using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace TCPServer
{
    class Server
    {
        private int _port;
        public Socket ServerSocket;
        static byte[] _buf = new byte[1024 * 8];
        public Server(int Port = 8001)
        {
            _port = Port;
            start();
        }
        private void start()
        {
            try
            {
                ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _port);
                ServerSocket.Bind(endPoint); //绑定IP地址和端口号
                ServerSocket.Listen(10);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public bool Send(byte[] msg)
        {
            try
            {
                ServerSocket.Send(msg);
                Console.WriteLine(DateTime.Now.ToString() + "---发送消息为：" + Encoding.ASCII.GetString(msg));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public byte[] Recv()
        {
            try
            {
                int len = ServerSocket.Receive(_buf);
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

            return (!((ServerSocket.Poll(1000, SelectMode.SelectRead) && (ServerSocket.Available == 0)) || !ServerSocket.Connected));

        }
    }
}
