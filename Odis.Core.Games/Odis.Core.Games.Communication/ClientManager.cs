using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Odis.Core.Games.Communication
{
    /// <summary>
    /// Exposes 
    /// </summary>
    public static class ClientManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static ConcurrentQueue<IClient> ClientCollection { get; private set; }

        static ClientManager()
        {
            ClientCollection = new ConcurrentQueue<IClient>();
        }
    }

    /// <summary>
    /// Interface that describes a client of the server
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Use by the server to send a message to the client
        /// </summary>
        /// <param name="message"></param>
        void SendMessage(IMessage message);
        /// <summary>
        /// Send a message to a client from another client
        /// </summary>
        /// <param name="message"></param>
        /// <param name="client"></param>
        void SendMessage(IMessage message, IClient client);
    }

    /// <summary>
    /// Interface that describes a client of the server
    /// </summary>
    /// <typeparam name="T">client protocol like UDPClient or TCP Client</typeparam>
    public interface IClient<T> : IClient
    {
        T Client { get; }
    }

    /// <summary>
    /// Udp client 
    /// </summary>
    public class OdisUdpClient : IClient<UdpClient>
    {

        public OdisUdpClient(UdpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client parameter is null");
            }
            this.Client = client;
        }

        #region IClient Members

        public void SendMessage(IMessage message)
        {
            byte[] bytedata;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();

                try { bf.Serialize(ms, message); }
                catch { return; }

                bytedata = ms.ToArray();
            }

            try
            {
                lock (this.Client)
                {
                    this.Client.Client.BeginSend(BitConverter.GetBytes(bytedata.Length), 0, bytedata.Length, SocketFlags.None, EndSend, null);
                    this.Client.Client.BeginSend(bytedata, 0, bytedata.Length, SocketFlags.None, EndSend, null);
                }
            }
        }
        private void EndSend(IAsyncResult ar)
        {
            try { this.Client.Client.EndSend(ar); }
            catch { }
        }

        public void SendMessage(IMessage message, IClient client)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IClient<UdpClient> Members

        public UdpClient Client { get; private set; }

        #endregion
    }

    public class OdisTcpClient : IClient<TcpClient>
    {
        public OdisTcpClient(TcpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client parameter is null");
            }
            this.Client = client;
        }

        #region IClient<TcpClient> Members

        public TcpClient Client { get; private set; }

        #endregion

        #region IClient Members

        public void SendMessage(IMessage message)
        {
        }
        private void EndSend(IAsyncResult ar)
        {
            try { this.Client.Client.EndSend(ar); }
            catch { }
        }

        public void SendMessage(IMessage message, IClient client)
        {
            byte[] bytedata;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();

                try { bf.Serialize(ms, message); }
                catch { return; }

                bytedata = ms.ToArray();
            }

            try
            {
                lock (this.Client)
                {
                    this.Client.Client.BeginSend(BitConverter.GetBytes(bytedata.Length), 0, bytedata.Length, SocketFlags.None, EndSend, null);
                    this.Client.Client.BeginSend(bytedata, 0, bytedata.Length, SocketFlags.None, EndSend, null);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Generic Message sended from a client to another
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageFromClient<out T> : IMessage<T>
    {
        /// <summary>
        /// Strong typed message dataItem
        /// </summary>
        T DataItem { get; }

        /// <summary>
        /// Current client
        /// </summary>
        IClient Client { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        IMessageFromClient<T> GetInstance<T>(IClient client);
    }

    /// <summary>
    /// Message sended by a client to another one
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageFromClient<T> : IMessageFromClient<T>
    {
        /// <summary>
        /// Raw data of the message
        /// </summary>
        public string RawData { get; private set; }
        /// <summary>
        /// client who send the message
        /// </summary>
        public IClient Client { get; private set; }
        /// <summary>
        /// Get an instance of the class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <returns></returns>
        IMessageFromClient<T> IMessageFromClient<T>.GetInstance<T>(IClient client)
        {
            return GetInstance<T>(client);
        }

        /// <summary>
        /// Get an instance of the class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <returns></returns>
        public static IMessageFromClient<T> GetInstance<T>(IClient client)
        {
            return new MessageFromClient<T>(client);
        }

        /// <summary>
        /// Item of the given type
        /// </summary>
        public T DataItem { get; private set; }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="client"></param>
        private MessageFromClient(IClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client", "Parameter can't be null");
            }
            this.Client = client;
        }
    }
}
