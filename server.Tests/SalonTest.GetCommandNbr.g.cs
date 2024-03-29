using DotNetty.Transport.Channels;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using server;
// <copyright file="SalonTest.GetCommandNbr.g.cs">Copyright ©  2017</copyright>
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
public void GetCommandNbr94701()
{
    Player player;
    Salon salon;
    int i;
    player = new Player((IChannel)null);
    player.SetConnection(false);
    salon = new Salon(player, player, player, player);
    i = this.GetCommandNbr(salon, 0);
    Assert.AreEqual<int>(0, i);
    Assert.IsNotNull((object)salon);
}

[TestMethod]
[PexGeneratedBy(typeof(SalonTest))]
[PexRaisedException(typeof(ArgumentOutOfRangeException))]
public void GetCommandNbrThrowsArgumentOutOfRangeException922()
{
    Player player;
    Salon salon;
    int i;
    player = new Player((IChannel)null);
    player.SetConnection(false);
    salon = new Salon(player, player, player, player);
    i = this.GetCommandNbr(salon, int.MinValue);
}

[TestMethod]
[PexGeneratedBy(typeof(SalonTest))]
[PexRaisedException(typeof(Exception))]
public void GetCommandNbrThrowsException732()
{
    Player player;
    Salon salon;
    int i;
    player = new Player((IChannel)null);
    player.SetConnection(false);
    salon = new Salon(player, player, player, player);
    i = this.GetCommandNbr(salon, 256);
}
    }
}
