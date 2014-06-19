namespace Odis.Core.Games.Communication
{
    public class SocketEntity : Entity
    {
        public int Port { get; private set; }
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