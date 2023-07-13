using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
class Program
{
    static TcpListener listener;
    static void Main(string[] args)
    {
        listener = new TcpListener(System.Net.IPAddress.Any, 1302);
        Console.WriteLine("Server started on: " + listener.LocalEndpoint);
        Console.WriteLine("Wating for a connection");
        listener.Start();
        
        for(int i = 0; i < 10; i++)
        {
            new Thread(ClientHandling).Start();
        }
    }

    static void ClientHandling()
    {
        while (true)
        {
            Socket client = listener.AcceptSocket();
            Console.WriteLine("Client connected from: " + client.RemoteEndPoint);
            try
            {
                var stream = new NetworkStream(client);
                var sr = new StreamReader(stream);
                var sw = new StreamWriter(stream);
                sw.AutoFlush = true;
                byte[] buffer = new byte[1024];
                stream.Read(buffer, 0, buffer.Length);
                int receive = 0;
                foreach (byte b in buffer)
                {
                    if (b != 0)
                    {
                       receive++;
                    }
                }
                string request = Encoding.UTF8.GetString(buffer, 0, receive);
                Console.WriteLine("Request received from {0}: {1}", client.RemoteEndPoint, request);
                sw.WriteLine("Hello " + request + " this is a response from the server!!");   
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}