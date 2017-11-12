using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class GameManager
    {
        BidManager.BidInfo bid;
        bool newRound = false;
        bool waitingResponse = false;
        int round = 0;
        int playerTurn;
        Card leadingCard;
        int leadingPlayer;
        bool firstCard = false;
        List<Player> players;
        List<Card> table = new List<Card>();
        RoundCalculator roundClc = new RoundCalculator();
        BidCalculator bidClc = new BidCalculator();

        public void Init(BidManager.BidInfo data, List<Player> playerList)
        {
            round = 0;
            waitingResponse = false;
            newRound = true;
            players = playerList;
            bid = data;
            playerTurn = bid.leadingPlayer;
            table.Clear();
        }

        public void AskPlayer()
        {
            if (newRound)
            {
                firstCard = true;
                SendToAll("\n-----------------------------\nRecapitulatif de vos cartes:");
                foreach(Player player in players)
                {
                    player.DisplayCards();
                }
                SendToAll("-----------------------------\n");
                newRound = false;
            }
            SendTo(playerTurn, "A vous de jouer");
            SendToAllExcept(playerTurn, "Joueur" + (playerTurn + 1) + " joue");
            waitingResponse = true;
        }

        public bool PlayGame(out bool end)
        {
            if (waitingResponse == false)
            {
                AskPlayer();
            }
            for (int i = 0; i < players.Count; ++i)
                if (players[i].GetCommandNbr() > 0)
                {
                    if (AnalyseCommand(players[i].GetCommand(), i, out end))
                        return (true);
                }
            end = false;
            return (false);
        }

        private bool AnalyseCommand(String message, int player, out bool end)
        {

            end = false;
            if (playerTurn != player || waitingResponse == false)
            {
                SendTo(player, "Ce n'est pas à vous de jouer");
                return (false);
            }
            if (players[player].GetCard(message, out Card tmp) == false)
            {
                SendTo(player, "La carte choisie n'est pas valable, veuillez réessayer");
                return (false);
            }
            if (IsCardLegal(tmp, player))
            {
                SendToAllExcept(player, "Joueur" + (playerTurn + 1) + " joue: " + message);
                SendTo(player, "Vous jouez: " + message);
                players[player].RemoveCard(message);
                playerTurn = (playerTurn + 1) % 4;
                table.Add(tmp);
                roundClc.AddCardValue(tmp.GetValue(BidTypeToAtout(tmp.GetColor())));
                waitingResponse = false;
                if (table.Count() >= 4)
                {
                    playerTurn = leadingPlayer;
                    roundClc.StoreRoundScore(leadingPlayer);
                    SendToAll("\n\nPli terminé, scores: Equipe A: " + roundClc.GetTeamAValue() + ", Equipe B: " + roundClc.GetTeamBValue() + "\n");
                    newRound = true;
                    table.Clear();
                    return (CheckEndBid(out end));
                }
                return (false);
            }
            SendTo(player, "Vous ne pouvez pas jouer cette carte");
            return (false);
        }

        private bool CheckEndBid(out bool end)
        {
            end = false;
            if (++round == 8)
            {
                bidClc.SetScore(roundClc.GetTeamAValue(), roundClc.GetTeamAValue());
                roundClc.Reset();
                bidClc.SetBid(bid);
                bidClc.ComputeBid();
                SendToAll("Fin du contrat, voici les scores: Equipe A: " + bidClc.GetScore()[0] + ", Equipe b: " + bidClc.GetScore()[1]);
                if (bidClc.GetScore()[0] >= 701)
                {
                    SendToAll("L'équipe A remporte la partie");
                    end = true;
                    return (true);
                } else if (bidClc.GetScore()[1] >= 701)
                {
                    SendToAll("L'équipe B remporte la partie");
                    end = true;
                    return (true);
                }
                return (true);
            }
            return (false);
        }

        private bool IsCardLegal(Card card, int player)
        {
            if (bid.bidType == BidManager.Atout.SA || bid.bidType == BidManager.Atout.TA)
                return (CheckTASACard(card, player));
            return (CheckAtoutCard(card, player));
        }

        private bool CheckAtoutCard(Card card, int player)
        {
            if (firstCard)
            {
                leadingCard = card;
                leadingPlayer = player;
                firstCard = false;
                return (true);
            }
            else if (BidTypeToAtout(leadingCard.GetColor()) == Card.Atout.Normal)
            {
                return (CheckAtoutNormalCard(card, player));
            } else
            {
                if (card.GetColor() == leadingCard.GetColor() && card.GetValue(Card.Atout.Atout) > leadingCard.GetValue(Card.Atout.Atout))
                {
                    leadingCard = card;
                    leadingPlayer = player;
                    return (true);
                } if (card.GetColor() != leadingCard.GetColor() && players[player].HasBetterCard(leadingCard, Card.Atout.Atout) == false)
                {
                    return (true);
                }
            }
            return (false);
        }

        private bool CheckAtoutNormalCard(Card card, int player)
        {
            if (card.GetColor() == leadingCard.GetColor() && card.GetValue(Card.Atout.Normal) > leadingCard.GetValue(Card.Atout.Normal))
            {
                leadingCard = card;
                leadingPlayer = player;
                return (true);
            }
            else if (BidTypeToAtout(card.GetColor()) == Card.Atout.Atout && players[player].HasBetterCard(leadingCard, Card.Atout.Normal) == false)
            {
                leadingCard = card;
                leadingPlayer = player;
                return (true);
            }
            else if (BidTypeToAtout(card.GetColor()) == Card.Atout.Normal && players[player].HasBetterCard(leadingCard, Card.Atout.Normal) == false &&
              players[player].HasColorInHand(((Card.Color)bid.bidType - 2)) == false)
            {
                return (true);
            }
            return (false);
        }

        private bool CheckTASACard(Card card, int player)
        {
            if (firstCard)
            {
                leadingCard = card;
                leadingPlayer = player;
                firstCard = false;
                return (true);
            } else if (card.GetColor() == leadingCard.GetColor() && card.GetValue((Card.Atout)bid.bidType) > leadingCard.GetValue((Card.Atout)bid.bidType))
            {
                leadingCard = card;
                leadingPlayer = player;
                return (true);
            } else if (players[player].HasBetterCard(leadingCard, BidTypeToAtout(leadingCard.GetColor())) == false)
                return (true);
            return (false);
        }

        private Card.Atout BidTypeToAtout(Card.Color color)
        {
            if (bid.bidType == BidManager.Atout.SA || bid.bidType == BidManager.Atout.TA)
            {
                return ((Card.Atout)bid.bidType);
            }
            if (((Card.Color)bid.bidType - 2) == color)
            {
                return (Card.Atout.Atout);
            }
            return (Card.Atout.Normal);
        }

        private void SendToAllExcept(int player, String message)
        {
            for (int i = 0; i < players.Count; ++i)
            {
                if (player != i)
                {
                    players[i].SendCommand(message);
                }
            }
        }

        private void SendToAll(String message)
        {
            foreach (Player player in players)
            {
                player.SendCommand(message);
            }
        }

        private void SendTo(int player, String message)
        {
            players[player].SendCommand(message);
        }
    }
}
