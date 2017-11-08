namespace server
{
    using System.Collections.Generic;

    public class Server
    {
        private ServerHandler network;

        public Server(ServerHandler handler)
        {
            network = handler;
        }

        public void Loop()
        {
            List<Salon> salons = new List<Salon>();

            while(true)
            {
                if (network.GetClientNbr() > 3 && network.IsAvailable())
                {
                    salons.Add(network.MakeSalon());
                }
                for (int i = salons.Count - 1; i >= 0; --i)
                {
                    if (salons[i].GetClientNbr() < 4)
                    {
                        salons[i].DestroySalon();
                        salons.RemoveAt(i);
                    }
                }
                try
                {
                    for (int i = salons.Count - 1; i >= 0; --i)
                    {
                        try
                        {
                            // Play Game here
                        } catch (System.Exception)
                        {
                            salons[i].DestroySalon();
                            salons.RemoveAt(i);
                        }
                    }
                } catch (System.Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
