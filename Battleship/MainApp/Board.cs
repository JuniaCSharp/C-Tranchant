using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace MainApp
{
    public class Board
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
        static public List<Boat> boats;

        public Board(int width, int height) => b = new Tile[width, height];
        public void addTile(Tile t, int x, int y) => b[x, y] = t;
        public void addBoat(Boat boat)
        {
            // check if the boat placement is allowed
            for(int i = 0; i < boat.width; i++){
                for(int j = 0; j < boat.height; j++){
                    if(b[boat.topLeftPosX + i, boat.topLeftPosY + j].state != "0")
                    {
                        throw new Exception();
                    }
                    b[boat.topLeftPosX + i, boat.topLeftPosY + j].state = "1:" + boat.name;
                }
            }
            boats.Add(boat);
        }

    }

    public class Boat
    {
        public string name;
        public int width;
        public int height;
        public int topLeftPosX;
        public int topLeftPosY;
        public int[] damage;
        public Boat(string n, int w, int h)
        {
            name = n;
            width = w;
            height = h;
            damage = new int[Math.Max(w, h)];
            Array.Clear(damage, 0, Math.Max(w, h)); // Set to 0
        }
        public void setTopLeftPos(int x, int y)
        {
            topLeftPosX = x;
            topLeftPosY = y;
        }
    }
}
