using DotNetty.Transport.Channels.Embedded;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetty.Transport.Channels;
using server;
// <copyright file="PlayerTest.GetCommandNbr.g.cs">Copyright ©  2017</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace server.Tests
{
    public partial class PlayerTest
    {

[TestMethod]
[PexGeneratedBy(typeof(PlayerTest))]
public void GetCommandNbr15501()
{
    EmbeddedChannel embeddedChannel;
    Player player;
    int i;
    IChannelHandler[] iChannelHandlers = new IChannelHandler[0];
    embeddedChannel =
      new EmbeddedChannel((IChannelId)null, false, false, iChannelHandlers);
    embeddedChannel.RunPendingTasks();
    player = new Player((IChannel)embeddedChannel);
    player.SetConnection(false);
    i = this.GetCommandNbr(player);
    Assert.AreEqual<int>(0, i);
    Assert.IsNotNull((object)player);
}

[TestMethod]
[PexGeneratedBy(typeof(PlayerTest))]
public void GetCommandNbr15502()
{
    EmbeddedChannel embeddedChannel;
    Player player;
    int i;
    IChannelHandler[] iChannelHandlers = new IChannelHandler[0];
    embeddedChannel =
      new EmbeddedChannel((IChannelId)null, false, true, iChannelHandlers);
    embeddedChannel.RunPendingTasks();
    player = new Player((IChannel)embeddedChannel);
    player.SetConnection(false);
    i = this.GetCommandNbr(player);
    Assert.AreEqual<int>(0, i);
    Assert.IsNotNull((object)player);
}
    }
}
