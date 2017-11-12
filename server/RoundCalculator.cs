using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class RoundCalculator
    {
        private int teamA = 0;
        private int teamB = 0;
        private int tableValue = 0;

        public void AddCardValue(int value)
        {
            tableValue += value;
        }

        public void Reset()
        {
            teamA = 0;
            teamB = 0;
            tableValue = 0;
        }

        public void StoreRoundScore(int winning_player)
        {
            if (winning_player == 1 || winning_player == 3)
                teamB += tableValue;
            else
                teamA += tableValue;
            tableValue = 0;
        }

        public int GetTeamAValue()
        {
            return (teamA);
        }

        public int GetTeamBValue()
        {
            return (teamB);
        }
    }
}
