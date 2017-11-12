namespace client
{
    using DotNetty.Codecs;
    using DotNetty.Handlers.Tls;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using System.Collections.Concurrent;

    public class ClientHandler : SimpleChannelInboundHandler<ProtobufCommand.Command>
    {
        private volatile IChannel channel;
        private BlockingCollection<ProtobufCommand.Command> answer = new BlockingCollection<ProtobufCommand.Command>();
        private bool connected = false;

        public  ClientHandler() {}

        public string   GetCommand()
        {
            bool interrupted = false;

            try
            {
                if (this.answer.Count > 0)
                {
                    return (this.answer.Take().Request);
                }
            } catch (System.Exception)
            {
                interrupted = true;
            }
            if (interrupted)
            {
                return ("");
            }
            return (null);
        }

        public int  GetCommandSize()
        {
            return (answer.Count);
        }

        public void SendCommand(string command)
        {
            ProtobufCommand.Command builder = new ProtobufCommand.Command
            {
                Request = command
            };
            channel.WriteAndFlushAsync(builder);
        }

        public bool  GetStatus()
        {
            return (connected);
        }

        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
            connected = false;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, ProtobufCommand.Command command)
        {
            answer.Add(command);
        }

        public override void ChannelRegistered(IChannelHandlerContext ctx)
        {
            channel = ctx.Channel;
            connected = true;
        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            ctx.CloseAsync();
        }
    }
}
