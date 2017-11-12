namespace server
{
    using DotNetty.Transport.Channels;
    using System.Collections.Generic;

    public class Salon
    {
        private bool new_game = true;
        private BidManager bidMng = new BidManager();
        private GameManager gameMng = new GameManager();

        private List<Player> players = new List<Player>();

        public Salon(Player player1, Player player2, Player player3, Player player4)
        {
            if (player1 == null || player2 == null || player3 == null || player4 == null)
            {
                throw new System.Exception("Null object");
            }
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);
        }

        public bool PlayGame()
        {

            if (new_game)
                InitGame();
            else if (bidMng.IsBidding())
            {
                if (bidMng.HandleBid())
                {
                    System.Console.WriteLine("Fin de l'enchère");
                    gameMng.Init(bidMng.GetBid(), players);
                    System.Console.WriteLine("Game init");
                }
            }
            else if (gameMng.PlayGame(out bool end))
            {
                if (end)
                    return (true);

                bidMng.Init(players);
            }
            return (false);
        }

        private void InitGame()
        {
            new_game = false;
            players[0].SendCommand("Bienvenue, la partie va commencer, vous êtes Joueur1 en équipe A avec Joueur3");
            players[1].SendCommand("Bienvenue, la partie va commencer, vous êtes Joueur2 en équipe B avec Joueur4");
            players[2].SendCommand("Bienvenue, la partie va commencer, vous êtes Joueur3 en équipe A avec Joueur1");
            players[3].SendCommand("Bienvenue, la partie va commencer, vous êtes Joueur14en équipe B avec Joueur2");
            bidMng.Init(players);
        }

        public int GetCommandNbr(int id)
        {
            if (players.Count <= id)
            {
                throw new System.Exception("Bad player ID requested");
            }
            return (players[id].GetCommandNbr());
        }

        public string GetCommand(int id)
        {
            if (players.Count <= id)
            {
                throw new System.Exception("Bad player ID requested");
            }
            return (players[id].GetCommand());
        }

        public IChannel GetPlayer(int id)
        {
            if (players.Count <= id)
            {
                throw new System.Exception("Bad player ID requested");
            }
            return (players[id].GetChannel());
        }

        public void SendCommand(int id, string command)
        {
            if (players.Count <= id)
            {
                throw new System.Exception("Bad player ID requested");
            }
            players[id].SendCommand(command);
        }

        public int GetClientNbr()
        {
            int i = 0;

            foreach (Player player in players)
            {
                if (player.GetConnection())
                {
                    ++i;
                }
            }
            return (i);
        }

        public void DestroySalon()
        {
            foreach (Player player in players)
            {
                if (player.GetConnection())
                {
                    player.GetChannel().CloseAsync();
                }
            }
            System.Console.WriteLine("A salon has been destroyed");
        }
    }
}
