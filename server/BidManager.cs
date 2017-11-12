using System;
using System.Collections.Generic;
using System.Linq;

namespace server
{
    public class BidManager
    {
        public struct BidInfo
        {
            public bool coinche;
            public bool surcoinche;
            public int leadingPlayer;
            public int bidValue;
            public BidManager.Atout bidType;
        }

        public enum Atout
        {
            TA = 0,
            SA = 1,
            Trefle = 3,
            Pique = 2,
            Carreau = 4,
            Coeur = 5
        };

        private Deck deck = new Deck();
        private List<Player> players;
        private int skipInARow = 0;
        private bool waitingResponse = false;
        private bool coinche = false;
        private bool surcoinche = false;
        private int leadingPlayer = 0;
        private int playerTurn = 0;
        private bool bidding = false;
        private int bidValue = 0;
        private BidManager.Atout bidType = 0;

        public BidInfo GetBid()
        {
            BidInfo bid = new BidInfo
            {
                coinche = coinche,
                surcoinche = surcoinche,
                leadingPlayer = leadingPlayer,
                bidValue = bidValue,
                bidType = bidType
            };

            return (bid);
        }

        public void Init(List<Player> player_list)
        {
            deck.NewDeck();
            coinche = false;
            surcoinche = false;
            leadingPlayer = 0;
            bidValue = 0;
            skipInARow = 0;
            bidding = true;
            waitingResponse = false;
            players = player_list ?? throw new Exception("Null parameter");
            foreach (Player player in players)
            {
                player.ClearCards();
            }
            for (int i = 0; i < 8; ++i)
            {
                foreach (Player player in players)
                {
                    player.GiveCard(deck.PickACard());
                }
            }
            foreach (Player player in players)
            {
                player.DisplayCards();
            }
            SendToAll("Les enchères commencent");
        }

        public bool IsBidding()
        {
            return (bidding);
        }

        public bool HandleBid()
        {
            if (!waitingResponse)
                AskPlayer(playerTurn);
            else
            {
                for (int i = 0; i < players.Count(); ++i)
                {
                    if (players[i].GetCommandNbr() > 0)
                    {
                        if (AnalyseCommand(players[i].GetCommand(), i))
                            return (true);
                    }
                }
            }
            return (false);
        }

        private bool AnalyseCommand(String message, int player)
        {
            if (playerTurn != player || waitingResponse == false)
            {
                SendTo(player, "Ce n'est pas à vous de miser");
                return (false);
            }
            String[] words = message.Split(' ');
            switch (words[0])
            {
                case "Encherir":
                    ParseBid(message, player);
                    break;
                case "Coincher":
                    ParseCoinche(message, player);
                    break;
                case "Surcoincher":
                    return (ParseSurcoinche(message, player));
                case "Passer":
                    return (ParseSkip(message, player));
                default:
                    SendTo(player, "Commande inconnue, veuillez réessayer...");
                    break;
                   
            }
            return (false);
        }

        private void ParseCoinche(String message, int player)
        {
            if (coinche == true || bidValue == 0 || leadingPlayer == player || IsInSameTeam(player, leadingPlayer))
            {
                SendTo(player, "Vous ne pouvez pas coincher");
                return;
            }
            skipInARow = 0;
            SendToAllExcept(player, "Joueur" + (player + 1) + " coinche");
            SendTo(player, "Vous coinchez");
            waitingResponse = false;
            playerTurn = leadingPlayer;
            coinche = true;
        }

        private bool ParseSurcoinche(String message, int player)
        {
            if (coinche == false)
            {
                SendTo(player, "Vous ne pouvez pas surcoincher");
                return (false);
            }
            skipInARow = 0;;
            waitingResponse = false;
            SendToAllExcept(player, "Joueur" + (player + 1) + " surcoinche");
            SendTo(player, "Vous surcoinchez");
            SendToAll("Les enchères sont terminées, Joueur" + (leadingPlayer + 1) + " a la main.");
            bidding = false;
            return (true);
        }

        private bool ParseSkip(String message, int player)
        {
            SendToAllExcept(player, "Joueur" + (player + 1) + " passe");
            SendTo(player, "Vous passez");
            skipInARow++;
            playerTurn = (playerTurn + 1) % 4;
            waitingResponse = false;
            if ((skipInARow == 3 && bidValue != 0) || coinche)
            {
                SendToAll("Les enchères sont terminées, Joueur" + (leadingPlayer + 1) + " a la main.");
                bidding = false;
                return (true);
            }
            if (skipInARow == 4)
            {
                SendToAll("Tout le monde passe, les cartes vont être redistribuées.");
                Init(players);
            }
            return (false);
        }

        private void ParseBid(String message, int player)
        {
            if (bidValue >= 160 || coinche)
            {
                SendTo(player, "Vous ne pouvez pas enchérir");
                return;
            }
            String[] word = message.Split(' ');
            if (word.Count() != 3)
            {
                SendTo(player, "Commande invalide, veuillez réessayer");
                return;
            }
            if (int.TryParse(word[1], out int value) == false)
                SendTo(player, "La valeur de l'enchère n'a pas pu être récupérer, veuillez réessayer");
            else if (value % 10 != 0 || value < (bidValue + 10) || (bidValue == 0 && value < 80) || value > 160)
                SendTo(player, "La valeur de l'enchère est incorrecte, veuillez réessayer");
            else
            {
                switch (word[2])
                {
                    case "SA":
                        bidType = BidManager.Atout.SA;
                        break;
                    case "TA":
                        bidType = BidManager.Atout.TA;
                        break;
                    case "Coeur":
                        bidType = BidManager.Atout.Coeur;
                        break;
                    case "Trefle":
                        bidType = BidManager.Atout.Trefle;
                        break;
                    case "Pique":
                        bidType = BidManager.Atout.Pique;
                        break;
                    case "Carreau":
                        bidType = BidManager.Atout.Carreau;
                        break;
                    default:
                        SendTo(player, "L'atout n'a pas été reconnu, veuillez réessayer");
                        return;
                }
                SendToAllExcept(player, "Joueur" + (player + 1) + " enchérit: " + word[1] + " " + word[2]);
                SendTo(player, "Vous enchérissez");
                leadingPlayer = player;
                bidValue = value;
                playerTurn = (playerTurn + 1) % 4;
                waitingResponse = false;
            }
        }

        private void AskPlayer(int player)
        {
            waitingResponse = true;
            SendToAllExcept(player, "Au tour de Joueur" + (player + 1) + " de faire une offre.");
            SendTo(player, "A votre tour de miser. Vous pouvez:\n");
            SendPlayerChoices(player);
        }

        private void SendPlayerChoices(int player)
        {
            if (bidValue < 160 && coinche == false)
            {
                int min = 80;
                if (bidValue != 0)
                    min = bidValue + 10;
                SendTo(player, "Encherir (min: " + min + ")");
            }
            if (bidValue > 0 && !IsInSameTeam(player, leadingPlayer) && coinche == false)
                SendTo(player, "Coincher");
            if (coinche)
                SendTo(player, "Surcoincher");
            SendTo(player, "Passer");
        }

        private bool IsInSameTeam(int player_a, int player_b)
        {
            if ((player_a == 0 && player_b == 2) ||
                (player_a == 1 && player_b == 3) ||
                (player_a == 2 && player_b == 0) ||
                (player_a == 3 && player_b == 1))
                return (true);
            return (false);
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
            foreach(Player player in players)
            {
                player.SendCommand(message);
            }
        }

        private void SendTo(int player, String message)
        {
            if (player >= players.Count() || player < 0)
            {
                throw new Exception("player index is not valid");
            }
            players[player].SendCommand(message);
        }
    }
}
