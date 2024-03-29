using DotNetty.Transport.Channels;
// <copyright file="SalonTest.cs">Copyright ©  2017</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using server;

namespace server.Tests
{
    [TestClass]
    [PexClass(typeof(Salon))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SalonTest
    {

        [PexMethod]
        public Salon Constructor(
            Player player1,
            Player player2,
            Player player3,
            Player player4
        )
        {
            Salon target = new Salon(player1, player2, player3, player4);
            return target;
            // TODO: add assertions to method SalonTest.Constructor(Player, Player, Player, Player)
        }

        [PexMethod]
        public void DestroySalon([PexAssumeUnderTest]Salon target)
        {
            target.DestroySalon();
            // TODO: add assertions to method SalonTest.DestroySalon(Salon)
        }

        [PexMethod]
        public int GetClientNbr([PexAssumeUnderTest]Salon target)
        {
            int result = target.GetClientNbr();
            return result;
            // TODO: add assertions to method SalonTest.GetClientNbr(Salon)
        }

        [PexMethod]
        public string GetCommand([PexAssumeUnderTest]Salon target, int id)
        {
            string result = target.GetCommand(id);
            return result;
            // TODO: add assertions to method SalonTest.GetCommand(Salon, Int32)
        }

        [PexMethod]
        public int GetCommandNbr([PexAssumeUnderTest]Salon target, int id)
        {
            int result = target.GetCommandNbr(id);
            return result;
            // TODO: add assertions to method SalonTest.GetCommandNbr(Salon, Int32)
        }

        [PexMethod]
        public IChannel GetPlayer([PexAssumeUnderTest]Salon target, int id)
        {
            IChannel result = target.GetPlayer(id);
            return result;
            // TODO: add assertions to method SalonTest.GetPlayer(Salon, Int32)
        }

        [PexMethod]
        public bool PlayGame([PexAssumeUnderTest]Salon target)
        {
            bool result = target.PlayGame();
            return result;
            // TODO: add assertions to method SalonTest.PlayGame(Salon)
        }

        [PexMethod]
        public void SendCommand(
            [PexAssumeUnderTest]Salon target,
            int id,
            string command
        )
        {
            target.SendCommand(id, command);
            // TODO: add assertions to method SalonTest.SendCommand(Salon, Int32, String)
        }
    }
}
