using DotNetty.Transport.Channels;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using server;
// <copyright file="SalonTest.PlayGame.g.cs">Copyright ©  2017</copyright>
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
    public partial class SalonTest
    {

[TestMethod]
[PexGeneratedBy(typeof(SalonTest))]
[PexRaisedException(typeof(TypeInitializationException))]
public void PlayGameThrowsTypeInitializationException965()
{
    Player player;
    Salon salon;
    bool b;
    player = new Player((IChannel)null);
    player.SetConnection(false);
    salon = new Salon(player, player, player, player);
    b = this.PlayGame(salon);
}
    }
}
