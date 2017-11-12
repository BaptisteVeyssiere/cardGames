using DotNetty.Transport.Channels.Embedded;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetty.Transport.Channels;
using server;
// <copyright file="PlayerTest.HasColorInHand.g.cs">Copyright ©  2017</copyright>
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
public void HasColorInHand94201()
{
    EmbeddedChannel embeddedChannel;
    Player player;
    bool b;
    IChannelHandler[] iChannelHandlers = new IChannelHandler[0];
    embeddedChannel =
      new EmbeddedChannel((IChannelId)null, false, false, iChannelHandlers);
    embeddedChannel.RunPendingTasks();
    player = new Player((IChannel)embeddedChannel);
    player.SetConnection(false);
    b = this.HasColorInHand(player, Card.Color.Pique);
    Assert.AreEqual<bool>(false, b);
    Assert.IsNotNull((object)player);
}

[TestMethod]
[PexGeneratedBy(typeof(PlayerTest))]
public void HasColorInHand94202()
{
    EmbeddedChannel embeddedChannel;
    Player player;
    bool b;
    IChannelHandler[] iChannelHandlers = new IChannelHandler[0];
    embeddedChannel =
      new EmbeddedChannel((IChannelId)null, false, true, iChannelHandlers);
    embeddedChannel.RunPendingTasks();
    player = new Player((IChannel)embeddedChannel);
    player.SetConnection(false);
    b = this.HasColorInHand(player, Card.Color.Pique);
    Assert.AreEqual<bool>(false, b);
    Assert.IsNotNull((object)player);
}
    }
}
