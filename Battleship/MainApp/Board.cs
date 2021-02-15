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

        public Tile[,] b;
        public List<Boat> boats = new List<Boat> { };

        public Tile[,] B => this.b;
        public List<Boat> Boats => boats;

        public Board(int width, int height) => this.b = new Tile[width, height];
        public void addTile(Tile t, int x, int y) => this.b[x, y] = t;
        public void addBoat(Boat boat) => boats.Add(boat);
    }

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

        public void rotateBoat()
        {
            int temp = width;
            width = height;
            height = temp;
        }
    }
}
