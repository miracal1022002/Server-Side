using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Listener
    {
        Socket socket;

        public bool Listening
        {
            get;
            private set;
        }

        public int Port
        {
            get;
            private set;
        }

        public Listener(int port)
        {
            Port = port;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            Console.WriteLine("Server started");
            if (Listening)
            {
                return;
            }
            socket.Bind(new IPEndPoint(0, Port));
            Listening = true;
        }

        public void Stop()
        {
            if (!Listening) {
                return;
            }

            socket.Close();
            socket.Dispose();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        void callback(IAsyncResult ar)
        {
            try
            {
                Socket socket = this.socket.EndAccept(ar);
                if(SocketAccepted != null)
                {
                    SocketAccepted(socket);
                }

                this.socket.BeginAccept(callback, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler SocketAccepted;
    }
}
