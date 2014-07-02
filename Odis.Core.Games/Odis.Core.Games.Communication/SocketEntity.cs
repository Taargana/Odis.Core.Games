namespace Odis.Core.Games.Communication
{
    /// <summary>
    /// Standard socket entity
    /// </summary>
    public class SocketEntity : Entity
    {
        /// <summary>
        /// Port that a socket entity uses
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Constructor of the entity
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="port"></param>
        public SocketEntity(string entityName, int port = 8586) : base(entityName)
        {
            Port = port;
        }

        public override void Initialize()
        {
            base.Initialize();

        }
    }
}