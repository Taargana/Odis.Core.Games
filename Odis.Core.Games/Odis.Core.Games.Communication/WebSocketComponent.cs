using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Odis.Core.Games.Communication
{
    public class WebSocketComponent : UpdateComponent
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
                myListener = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), ((SocketEntity)this.Entity).Port);
                myListener.Start();
                SocketState = SocketState.Listenning;
                while (true)
                {
                    //Accept a new connection
                    using (TcpClient client = myListener.AcceptTcpClient())
                    {
                        using (NetworkStream stream = client.GetStream())
                        {
                            while (true)
                            {
                                while (!stream.DataAvailable) ;
                                Byte[] bytes = new Byte[client.Available];
                                stream.Read(bytes, 0, bytes.Length);

                                //translate bytes of request to string
                                String data = Encoding.UTF8.GetString(bytes);

                                this.Entity.GameManager.MessageCollection.Enqueue(new Message(data));

                                if (!new Regex("^GET").IsMatch(data)) continue;
                                /*String acceptWS = Convert.ToBase64String(
                                    SHA1.Create().ComputeHash(
                                        Encoding.UTF8.GetBytes(
                                            new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim()+ "258EAFA5-E914-47DA-95CA-C5AB0DC85B11")));
                                    
                                StringBuilder sb = new StringBuilder("HTTP/1.1 101 Switching Protocols");
                                    
                                sb.AppendLine("Connection: Upgrade")
                                    .AppendLine("Upgrade: websocket")
                                    .AppendLine(String.Format("Sec-WebSocket-Accept: {0}", acceptWS))
                                    .AppendLine(Environment.NewLine);

                                Byte[] response = Encoding.UTF8.GetBytes(sb.ToString());*/

                                Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
        + "Connection: Upgrade" + Environment.NewLine
        + "Upgrade: websocket" + Environment.NewLine
        + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
            SHA1.Create().ComputeHash(
                Encoding.UTF8.GetBytes(
                    new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                )
            )
        ) + Environment.NewLine
        + Environment.NewLine);


                                stream.Write(response, 0, response.Length);
                            }
                        }
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
