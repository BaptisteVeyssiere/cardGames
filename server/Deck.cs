using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class Deck
    {
        Random rnd = new Random();
        private List<Card> cardList = new List<Card>();

        public Deck()
        {
            cardList.Clear();
        }

        public void NewDeck()
        {
            cardList.Clear();
            cardList.Add(new Card(0, 0, 0, 0, Card.Color.Trefle, "7 de Trefle"));
            cardList.Add(new Card(0, 0, 0, 0, Card.Color.Trefle, "8 de Trefle"));
            cardList.Add(new Card(9, 0, 14, 0, Card.Color.Trefle, "9 de Trefle"));
            cardList.Add(new Card(14, 2, 20, 2, Card.Color.Trefle, "Valet de Trefle"));
            cardList.Add(new Card(2, 3, 3, 3, Card.Color.Trefle, "Dame de Trefle"));
            cardList.Add(new Card(3, 4, 4, 4, Card.Color.Trefle, "Roi de Trefle"));
            cardList.Add(new Card(5, 10, 10, 10, Card.Color.Trefle, "10 de Trefle"));
            cardList.Add(new Card(7, 19, 11, 11, Card.Color.Trefle, "As de Trefle"));
            cardList.Add(new Card(0, 0, 0, 0, Card.Color.Pique, "7 de Pique"));
            cardList.Add(new Card(0, 0, 0, 0, Card.Color.Pique, "8 de Pique"));
            cardList.Add(new Card(9, 0, 14, 0, Card.Color.Pique, "9 de Pique"));
            cardList.Add(new Card(14, 2, 20, 2, Card.Color.Pique, "Valet de Pique"));
            cardList.Add(new Card(2, 3, 3, 3, Card.Color.Pique, "Dame de Pique"));
            cardList.Add(new Card(3, 4, 4, 4, Card.Color.Pique, "Roi de Pique"));
            cardList.Add(new Card(5, 10, 10, 10, Card.Color.Pique, "10 de Pique"));
            cardList.Add(new Card(7, 19, 11, 11, Card.Color.Pique, "As de Pique"));
            cardList.Add(new Card(0, 0, 0, 0, Card.Color.Carreau, "7 de Carreau"));
            cardList.Add(new Card(0, 0, 0, 0, Card.Color.Carreau, "8 de Carreau"));
            cardList.Add(new Card(9, 0, 14, 0, Card.Color.Carreau, "9 de Carreau"));
            cardList.Add(new Card(14, 2, 20, 2, Card.Color.Carreau, "Valet de Carreau"));
            cardList.Add(new Card(2, 3, 3, 3, Card.Color.Carreau, "Dame de Carreau"));
            cardList.Add(new Card(3, 4, 4, 4, Card.Color.Carreau, "Roi de Carreau"));
            cardList.Add(new Card(5, 10, 10, 10, Card.Color.Carreau, "10 de Carreau"));
            cardList.Add(new Card(7, 19, 11, 11, Card.Color.Carreau, "As de Carreau"));
            cardList.Add(new Card(0, 0, 0, 0, Card.Color.Coeur, "7 de Coeur"));
            cardList.Add(new Card(0, 0, 0, 0, Card.Color.Coeur, "8 de Coeur"));
            cardList.Add(new Card(9, 0, 14, 0, Card.Color.Coeur, "9 de Coeur"));
            cardList.Add(new Card(14, 2, 20, 2, Card.Color.Coeur, "Valet de Coeur"));
            cardList.Add(new Card(2, 3, 3, 3, Card.Color.Coeur, "Dame de Coeur"));
            cardList.Add(new Card(3, 4, 4, 4, Card.Color.Coeur, "Roi de Coeur"));
            cardList.Add(new Card(5, 10, 10, 10, Card.Color.Coeur, "10 de Coeur"));
            cardList.Add(new Card(7, 19, 11, 11, Card.Color.Coeur, "As de Coeur"));
        }

        public Card PickACard()
        {
            int nbr = rnd.Next(cardList.Count);
            Card elem;

            elem = cardList[nbr];
            cardList.RemoveAt(nbr);

            return (elem);
        }
    }
}
