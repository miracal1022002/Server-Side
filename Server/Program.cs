using Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{ 
    static void Main(string[] args)
    {
        TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1302);
        listener.Start();
        while (true)
        {
            Console.WriteLine("Wating for a connection");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client accepted");
            NetworkStream stream = client.GetStream();
            StreamReader sr = new StreamReader(client.GetStream());
            StreamWriter sw = new StreamWriter(client.GetStream());
            try
            {
                byte[] buffer = new byte[1024];
                stream.Read(buffer, 0, buffer.Length);
                int receive = 0;
                foreach(byte b in buffer)
                {
                    if(b != 0)
                    {
                        receive++;
                    }
                }
                string request = Encoding.UTF8.GetString(buffer, 0, receive);
                Console.WriteLine("Request received: " + request);
                sw.WriteLine("Response from server");
                sw.Flush();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}