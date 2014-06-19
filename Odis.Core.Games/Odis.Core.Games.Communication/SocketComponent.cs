using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Odis.Core.Games.Communication
{
    public class SocketComponent : UpdateComponent
    {
        private TcpListener myListener;
        /// <summary>
        /// Get the socket state
        /// </summary>
        public SocketState SocketState { get; private set; }

        private void StartListen()
        {
            try
            {
                myListener = new TcpListener(new IPAddress(new byte[]{127,0,0,1}), ((SocketEntity)this.Entity).Port);
                myListener.Start();
                SocketState = SocketState.Listenning;
                while (true)
                {
                    //Accept a new connection
                    using (Socket mySocket = myListener.AcceptSocket())
                    {
                        if (!mySocket.Connected) continue;
                        //make a byte array and receive data from the client 
                        Byte[] bReceive = new Byte[1024];
                        int i = mySocket.Receive(bReceive, bReceive.Length, 0);

                        //Convert Byte to String
                        string sBuffer = Encoding.ASCII.GetString(bReceive);
                        this.Entity.GameManager.MessageCollection.Enqueue(new Message(sBuffer));
                    }
                }
            }
            catch (ThreadAbortException abortException)
            {
                SocketState = SocketState.Stopped;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (SocketState == SocketState.Stopped)
            {
                new Thread(StartListen).Start();
            }
        }
    }
}