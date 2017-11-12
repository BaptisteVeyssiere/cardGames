namespace server
{
    using System.Collections.Generic;
    using DotNetty.Transport.Channels;
    using System.Collections.Concurrent;

    public class Player
    {
        private IChannel channel;
        private BlockingCollection<string> commands = new BlockingCollection<string>();
        private bool connected = true;
        private List<Card> cards = new List<Card>();

        public Player(IChannel ch)
        {
            channel = ch ?? throw new System.Exception("Null object");
        }

        public int GetCommandNbr()
        {
            return (commands.Count);
        }

        public string GetCommand()
        {
            if (commands.Count < 1)
            {
                return (null);
            }
            return (commands.Take());
        }

        public IChannel GetChannel()
        {
            return (channel);
        }

        public void AddCommand(string command)
        {
            commands.Add(command);
            System.Console.WriteLine("New command added: " + command);
        }

        public void SendCommand(string command)
        {
            try
            {
                ProtobufCommand.Command builder = new ProtobufCommand.Command
                {
                    Request = command
                };
                channel.WriteAndFlushAsync(builder);
            } catch (System.Exception)
            {
                throw new System.Exception("Error while reading data");
            }
        }

        public void SetConnection(bool status)
        {
            connected = status;
        }

        public bool GetConnection()
        {
            return (connected);
        }

        public void GiveCard(Card card)
        {
            cards.Add(card);
        }

        public void ClearCards()
        {
            cards.Clear();
        }

        public void DisplayCards()
        {
            SendCommand("Voici vos cartes:");
            foreach(Card card in cards)
            {
                SendCommand(card.GetName());
            }
        }

        public bool HasBetterCard(Card compare, Card.Atout value)
        {
            foreach(Card card in cards)
            {
                if (card.GetColor() == compare.GetColor() && card.GetValue(value) > compare.GetValue(value))
                {
                    return (true);
                }
            }
            return (false);
        }

        public void RemoveCard(string cardname)
        {
            foreach (Card card in cards)
            {
                if (card.GetName() == cardname)
                {
                    cards.Remove(card);
                    return;
                }
            }
        }

        public bool HasColorInHand(Card.Color color)
        {
            foreach(Card card in cards)
            {
                if (card.GetColor() == color)
                    return (true);
            }
            return (false);
        }

        public bool GetCard(string cardname, out Card card)
        {
            if (System.String.IsNullOrEmpty(cardname) || cardname.Equals("\0"))
            {
                throw new System.Exception("Null object");
            }
            foreach(Card hand in cards)
            {
                if (cardname == hand.GetName())
                {
                    card = hand;
                    return (true);
                }
            }
            card = cards[0];
            return (false);
        }
    }
}
