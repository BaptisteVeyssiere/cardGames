namespace server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Card
    {
        public enum Atout
        {
            TA = 0,
            SA = 1,
            Atout = 2,
            Normal = 3
        };

        public enum Color
        {
            Pique = 0,
            Trefle = 1,
            Carreau = 2,
            Coeur = 3
        };

        private int[] value = new int[4];
        private Color color;
        private string name;

        public Card(int ta, int sa, int atout, int normal, Color col, String cardName)
        {
            value[(int)Atout.TA] = ta;
            value[(int)Atout.SA] = sa;
            value[(int)Atout.Atout] = atout;
            value[(int)Atout.Normal] = normal;
            color = col;
            name = cardName;
        }

        public int GetValue(Atout at)
        {
            return (value[(int)at]);
        }

        public bool IsColor(Color col)
        {
            return (col == color);
        }

        public Color GetColor()
        {
            return (color);
        }

        public String GetName()
        {
            return (name);
        }
    }
}