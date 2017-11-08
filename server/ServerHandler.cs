namespace server
{
    using DotNetty.Transport.Channels;
    using System.Collections.Generic;

    public class ServerHandler : SimpleChannelInboundHandler<ProtobufCommand.Command>
    {
        static List<Player> clientqueue = new List<Player>();
        static List<Player> ingamequeue = new List<Player>();
        static bool availability = true;

        public ServerHandler() { }

        public int GetClientNbr()
        {
            return (clientqueue.Count);
        }

        public bool IsAvailable()
        {
            return (availability);
        }

        public Salon MakeSalon()
        {
            List<Player> salon = new List<Player>();

            if (clientqueue.Count < 4 || availability == false)
            {
                throw new System.Exception("Can't make a new salon, not enough client");
            }
            for (int i = 0; i < 4; ++i)
            {
                Player tmp = clientqueue[0];
                clientqueue.RemoveAt(0);
                salon.Add(tmp);
                System.Console.WriteLine("[" + salon[i] + "] has join a salon");
                ingamequeue.Add(salon[i]);
            }
            return (new Salon(salon[0], salon[1], salon[2], salon[3]));
        }

        public override void HandlerAdded(IChannelHandlerContext ctx)
        {
            IChannel incoming = ctx.Channel;

            while (!availability)
            {
                System.Threading.Thread.Sleep(100);
            }
            availability = false;
            clientqueue.Add(new Player(incoming));
            System.Console.WriteLine("[" + incoming.RemoteAddress + "] has join the server");
            availability = true;
        }

        public override void HandlerRemoved(IChannelHandlerContext ctx)
        {
            IChannel incoming = ctx.Channel;

            while (!availability)
            {
                System.Threading.Thread.Sleep(100);
            }
            availability = false;
            foreach (Player player in clientqueue)
            {
                if (player.GetChannel() == incoming)
                {
                    clientqueue.Remove(player);
                    System.Console.WriteLine("[" + incoming.RemoteAddress + "] has left the server");
                    break;
                }
            }
            foreach (Player player in ingamequeue)
            {
                if (player.GetChannel() == incoming)
                {
                    player.SetConnection(false);
                    ingamequeue.Remove(player);
                    System.Console.WriteLine("[" + incoming.RemoteAddress + "] has left the server");
                    break;
                }
            }
            availability = true;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, ProtobufCommand.Command command)
        {
            ProtobufCommand.Command builder = new ProtobufCommand.Command();
            IChannel incoming = ctx.Channel;

            foreach (Player player in clientqueue)
            {
                if (player.GetChannel() == incoming)
                {
                    System.Console.WriteLine("[" + incoming.RemoteAddress + "] : " + command.Request);
                    builder.Request = "KO";
                    ctx.WriteAsync(builder);
                    return;
                }
            }
            foreach (Player player in ingamequeue)
            {
                if (player.GetChannel() == incoming)
                {
                    player.AddCommand(command.Request);
                    return;
                }
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext ctx)
        {
            ctx.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, System.Exception cause)
        {
            System.Console.WriteLine(cause.Message);
            ctx.CloseAsync();
        }
    }
}
