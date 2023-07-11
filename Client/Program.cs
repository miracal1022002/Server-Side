using System;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main(string[] args) 
    {
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect("127.0.0.1", 3306);
        s.Close();
        s.Dispose();
    }
}
