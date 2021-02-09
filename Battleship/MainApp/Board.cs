using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace MainApp
{
    class Board
    {
        public struct Tile
        {
            public Rectangle rect;
            public string state;
            public Tile(Rectangle r, string s)
            {
                rect = r;
                state = s;
            }
        }

        static public Tile[,] b;

        public Board(int width, int height)
        {
            b = new Tile[width, height];
        }
        public void addTile(Tile t, int x, int y)
        {
            b[x, y] = t;
        }
    }
}
