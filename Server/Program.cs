using Server;
using System;
using System.Net;
using System.Net.Sockets;

class Program
{
    static Listener listener;
    static List<Socket> sockets;
    static void Main(string[] args)
    {
        listener = new Listener(10);
        listener.SocketAccepted += new Listener.SocketAcceptedHandler(listener_SocketAccepted);
        listener.Start();
        sockets = new List<Socket>();
        Console.Read();
    }

    static void listener_SocketAccepted(System.Net.Sockets.Socket e)
    {
        Console.WriteLine("New Connection: {0}\n{1}\n========", e.RemoteEndPoint, DateTime.Now);
        sockets.Add(e);
    }
}