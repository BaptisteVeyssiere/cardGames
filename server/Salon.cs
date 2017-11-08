namespace server
{
    using DotNetty.Transport.Channels;
    using System.Collections.Generic;

    public class Salon
    {
        private List<Player> players = new List<Player>();

        public Salon(Player player1, Player player2, Player player3, Player player4)
        {
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);
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
