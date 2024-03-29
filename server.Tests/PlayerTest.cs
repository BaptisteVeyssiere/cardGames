using DotNetty.Transport.Channels;
// <copyright file="PlayerTest.cs">Copyright ©  2017</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using server;

namespace server.Tests
{
    [TestClass]
    [PexClass(typeof(Player))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PlayerTest
    {

        [PexMethod]
        public void AddCommand([PexAssumeUnderTest]Player target, string command)
        {
            target.AddCommand(command);
            // TODO: add assertions to method PlayerTest.AddCommand(Player, String)
        }

        [PexMethod]
        public void ClearCards([PexAssumeUnderTest]Player target)
        {
            target.ClearCards();
            // TODO: add assertions to method PlayerTest.ClearCards(Player)
        }

        [PexMethod]
        public Player Constructor(IChannel ch)
        {
            Player target = new Player(ch);
            return target;
            // TODO: add assertions to method PlayerTest.Constructor(IChannel)
        }

        [PexMethod]
        public void DisplayCards([PexAssumeUnderTest]Player target)
        {
            target.DisplayCards();
            // TODO: add assertions to method PlayerTest.DisplayCards(Player)
        }

        [PexMethod]
        public bool GetCard(
            [PexAssumeUnderTest]Player target,
            string cardname,
            out Card card
        )
        {
            bool result = target.GetCard(cardname, out card);
            return result;
            // TODO: add assertions to method PlayerTest.GetCard(Player, String, Card&)
        }

        [PexMethod]
        public IChannel GetChannel([PexAssumeUnderTest]Player target)
        {
            IChannel result = target.GetChannel();
            return result;
            // TODO: add assertions to method PlayerTest.GetChannel(Player)
        }

        [PexMethod]
        public string GetCommand([PexAssumeUnderTest]Player target)
        {
            string result = target.GetCommand();
            return result;
            // TODO: add assertions to method PlayerTest.GetCommand(Player)
        }

        [PexMethod]
        public int GetCommandNbr([PexAssumeUnderTest]Player target)
        {
            int result = target.GetCommandNbr();
            return result;
            // TODO: add assertions to method PlayerTest.GetCommandNbr(Player)
        }

        [PexMethod]
        public bool GetConnection([PexAssumeUnderTest]Player target)
        {
            bool result = target.GetConnection();
            return result;
            // TODO: add assertions to method PlayerTest.GetConnection(Player)
        }

        [PexMethod]
        public void GiveCard([PexAssumeUnderTest]Player target, Card card)
        {
            target.GiveCard(card);
            // TODO: add assertions to method PlayerTest.GiveCard(Player, Card)
        }

        [PexMethod]
        public bool HasBetterCard(
            [PexAssumeUnderTest]Player target,
            Card compare,
            Card.Atout value
        )
        {
            bool result = target.HasBetterCard(compare, value);
            return result;
            // TODO: add assertions to method PlayerTest.HasBetterCard(Player, Card, Atout)
        }

        [PexMethod]
        public bool HasColorInHand([PexAssumeUnderTest]Player target, Card.Color color)
        {
            bool result = target.HasColorInHand(color);
            return result;
            // TODO: add assertions to method PlayerTest.HasColorInHand(Player, Color)
        }

        [PexMethod]
        public void RemoveCard([PexAssumeUnderTest]Player target, string cardname)
        {
            target.RemoveCard(cardname);
            // TODO: add assertions to method PlayerTest.RemoveCard(Player, String)
        }

        [PexMethod]
        public void SendCommand([PexAssumeUnderTest]Player target, string command)
        {
            target.SendCommand(command);
            // TODO: add assertions to method PlayerTest.SendCommand(Player, String)
        }

        [PexMethod]
        public void SetConnection([PexAssumeUnderTest]Player target, bool status)
        {
            target.SetConnection(status);
            // TODO: add assertions to method PlayerTest.SetConnection(Player, Boolean)
        }
    }
}
