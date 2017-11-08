namespace server
{
    using DotNetty.Transport.Channels;
    using System.Collections.Concurrent;

    public class Player
    {
        private IChannel channel;
        private BlockingCollection<string> commands = new BlockingCollection<string>();
        private bool connected = true;

        public Player(IChannel ch)
        {
            channel = ch;
        }

        public int GetCommandNbr()
        {
            return (commands.Count);
        }

        public string GetCommand()
        {
            if (commands.Count < 1)
            {
                return (null);
            }
            return (commands.Take());
        }

        public IChannel GetChannel()
        {
            return (channel);
        }

        public void AddCommand(string command)
        {
            commands.Add(command);
            System.Console.WriteLine("New command added: " + command);
        }

        public void SendCommand(string command)
        {
            ProtobufCommand.Command builder = new ProtobufCommand.Command
            {
                Request = command
            };
            channel.WriteAndFlushAsync(builder);
        }

        public void SetConnection(bool status)
        {
            connected = status;
        }

        public bool GetConnection()
        {
            return (connected);
        }
    }
}
