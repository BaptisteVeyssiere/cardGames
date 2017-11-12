using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class BidCalculator
    {
        int[] team = new int[2];
        int[] score = new int[2];
        BidManager.BidInfo bid;

        public BidCalculator()
        {
            score[0] = 0;
            score[1] = 0;
        }

        public void SetScore(int a, int b)
        {
            team[0] = a;
            team[1] = b;
        }

        public void SetBid(BidManager.BidInfo data)
        {
            bid = data;
        }

        public void ComputeBid()
        {
            int mult = 1;
            int biddingTeam;
            int defendTeam;

            if (bid.coinche)
                ++mult;
            if (bid.surcoinche)
                ++mult;
            if (bid.leadingPlayer == 0 || bid.leadingPlayer == 2)
                biddingTeam = 0;
            else
                biddingTeam = 1;

            defendTeam = (biddingTeam + 1) % 2;
            if (team[biddingTeam] >= bid.bidValue)
            {
                if (score[defendTeam] == 0)
                    score[biddingTeam] = 250;
                score[biddingTeam] += bid.bidValue * mult + score[biddingTeam];
                score[defendTeam] += score[defendTeam];
            } else
            {
                score[defendTeam] += 160 + bid.bidValue * mult;
            }
        }

           public int[] GetScore()
        {
            return (score);
        }
    }
}
