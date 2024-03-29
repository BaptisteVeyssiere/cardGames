﻿namespace server
{
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels.Sockets;
    using DotNetty.Handlers.Logging;
    using DotNetty.Codecs.Protobuf;
    using System.Diagnostics;

    public class Program
    {
        public static async System.Threading.Tasks.Task RunClientAsync(string[] args)
        {
            if (args == null || args.Length != 1 || !int.TryParse(args[0], out int port))
            {
                System.Console.WriteLine("Usage: port");
                return;
            }
            MultithreadEventLoopGroup bossGroup = new MultithreadEventLoopGroup(1);
            MultithreadEventLoopGroup workerGroup = new MultithreadEventLoopGroup();
            try
            {
                ServerBootstrap b = new ServerBootstrap();
                b.Group(bossGroup, workerGroup).Channel<TcpServerSocketChannel>().Handler(new LoggingHandler(LogLevel.INFO)).ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline p = channel.Pipeline;

                    p.AddLast(new ProtobufVarint32FrameDecoder());
                    p.AddLast(new ProtobufDecoder(ProtobufCommand.Command.Parser));
                    p.AddLast(new ProtobufVarint32LengthFieldPrepender());
                    p.AddLast(new ProtobufEncoder());
                    p.AddLast(new ServerHandler());
                }));
                IChannel ch = await b.BindAsync(port);
                Server server = new Server(new ServerHandler());
                server.Loop();
                await ch.CloseAsync();
            } catch (System.Exception e)
            {
                int line = (new StackTrace(e, true)).GetFrame(0).GetFileLineNumber();
                System.Console.WriteLine("Error :" + e.Message + "on line " + line);
            } finally
            {
                await bossGroup.ShutdownGracefullyAsync();
                await workerGroup.ShutdownGracefullyAsync();
                System.Console.WriteLine("Server closed gracefully");
            }
        }
        public static void Main(string[] args)
        {
            try
            {
                RunClientAsync(args).Wait();
            } catch (System.Exception e)
            {
                int line = (new StackTrace(e, true)).GetFrame(0).GetFileLineNumber();
                System.Console.WriteLine("Error :" + e.Message + "on line " + line);
            }
        }
    }
}
