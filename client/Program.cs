namespace client
{
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels.Sockets;
    using DotNetty.Codecs.Protobuf;

    public class Program
    {
        private static ClientHandler handler = null;
        public static void ThreadLoop()
        {
            try
            {
                if (handler == null)
                {
                    throw new System.Exception("Object handler is not set");
                }
                string line = null;
                while (handler.GetStatus())
                {
                    line = System.Console.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line == "quit" || !handler.GetStatus())
                        {
                            break;
                        }
                        handler.SendCommand(line);
                    }
                    System.Threading.Thread.Sleep(100);
                }
            } catch (System.Exception e)
            {
                System.Console.WriteLine("Error encountered: " + e.Message);
            }
        }

        public static async System.Threading.Tasks.Task RunClientAsync(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                System.Console.WriteLine("Usage: host port");
                return;
            }
            string host = args[0];
            if (!int.TryParse(args[1], out int port))
            {
                System.Console.WriteLine("Bad int conversion");
                System.Environment.Exit(84);
            }
            MultithreadEventLoopGroup group = new MultithreadEventLoopGroup();
            try
            {
                Bootstrap b = new Bootstrap();
                b.Group(group).Channel<TcpSocketChannel>().Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline p = channel.Pipeline;

                    p.AddLast(new ProtobufVarint32FrameDecoder());
                    p.AddLast(new ProtobufDecoder(ProtobufCommand.Command.Parser));
                    p.AddLast(new ProtobufVarint32LengthFieldPrepender());
                    p.AddLast(new ProtobufEncoder());
                    p.AddLast(new ClientHandler());
                }));
                System.Net.IPAddress adress = System.Net.IPAddress.Parse(host);
                System.Net.IPEndPoint endpoint = new System.Net.IPEndPoint(adress, port);
                IChannel ch = await b.ConnectAsync(endpoint);
                handler = (ClientHandler)ch.Pipeline.Last();
                System.Threading.Thread mythread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadLoop));
                mythread.Start();
                try
                {
                    if (handler == null)
                    {
                        throw new System.Exception("Object handler is not set");
                    }
                    while (handler.GetStatus())
                    {
                        if (!handler.GetStatus())
                            return;
                        if (handler.GetCommandSize() > 0)
                            System.Console.WriteLine(handler.GetCommand());
                        System.Threading.Thread.Sleep(100);
                    }
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("Error encountered: " + e.Message);
                }
                await ch.CloseAsync();
                mythread.Abort();
            } catch (System.Exception e)
            {
                System.Console.WriteLine("Error encountered: " + e.Message);
            } finally
            {
                await group.ShutdownGracefullyAsync();
                System.Console.WriteLine("Client closed gracefully");
                System.Environment.Exit(0);
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                RunClientAsync(args).Wait();
            } catch (System.Exception e)
            {
                System.Console.WriteLine("Error encountered: " + e.Message);
            }
        }
    }
}
