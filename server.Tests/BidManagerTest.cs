using System.Collections.Generic;
// <copyright file="BidManagerTest.cs">Copyright ©  2017</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using server;

namespace server.Tests
{
    [TestClass]
    [PexClass(typeof(BidManager))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class BidManagerTest
    {

        [PexMethod]
        public bool IsBidding([PexAssumeUnderTest]BidManager target)
        {
            bool result = target.IsBidding();
            return result;
            // TODO: add assertions to method BidManagerTest.IsBidding(BidManager)
        }

        [PexMethod]
        public void Init([PexAssumeUnderTest]BidManager target, List<Player> player_list)
        {
            target.Init(player_list);
            // TODO: add assertions to method BidManagerTest.Init(BidManager, List`1<Player>)
        }

        [PexMethod]
        public bool HandleBid([PexAssumeUnderTest]BidManager target)
        {
            bool result = target.HandleBid();
            return result;
            // TODO: add assertions to method BidManagerTest.HandleBid(BidManager)
        }

        [PexMethod]
        public BidManager.BidInfo GetBid([PexAssumeUnderTest]BidManager target)
        {
            BidManager.BidInfo result = target.GetBid();
            return result;
            // TODO: add assertions to method BidManagerTest.GetBid(BidManager)
        }

        [PexMethod]
        public BidManager Constructor()
        {
            BidManager target = new BidManager();
            return target;
            // TODO: add assertions to method BidManagerTest.Constructor()
        }
    }
}
