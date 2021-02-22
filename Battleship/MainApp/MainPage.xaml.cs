using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MainApp
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static public Grid Grid;

        static public int boardSize = 10;
        public Board myBoard = new Board(boardSize, boardSize);
        public Board enemyBoard = new Board(boardSize, boardSize);

        static public Boat selectedBoat;
        static public int selectedBoatIdx;

        public int test;
        static public int playerTurn;

        public Dictionary<string, Border> BoatNameToBoatBorder = new Dictionary<string, Border>();

        public MainPage()
        {
            selectedBoat = null;
            selectedBoatIdx = -1;
            playerTurn = -1;

            this.InitializeComponent();
            this.enemyBoard = this.InitializeSea(EnemySeaBorder);
            this.myBoard = this.InitializeSea(MySeaBorder);
            this.InitializeBoats();
            this.InitializeBotBoard();

            // Init the dictionnary
            BoatNameToBoatBorder.Add("carrier", CarrierBorder);
            BoatNameToBoatBorder.Add("destroyer", DestroyerBorder);
            BoatNameToBoatBorder.Add("submarine1", Submarine1Border);
            BoatNameToBoatBorder.Add("submarine2", Submarine2Border);
            BoatNameToBoatBorder.Add("torpedo", TorpedoBorder);
        }

        ////////////////////////////////////////// Initializer //////////////////////////////////////////

        public Board InitializeSea(Border border)
        {
            // Init Grid Object
            Grid = new Grid();
            Board board = new Board(10, 10);
            Grid.Name = "SeaGrid" + border.Name;
            double boardWidth = border.Width;
            double boardHeight = border.Height;
            Rectangle rect;

            // Create Cols and Rows of our Grid (15x15)
            for (int i = 0; i < boardSize; i++) {
                ColumnDefinition col = new ColumnDefinition();
                RowDefinition row = new RowDefinition();
                col.Width = new GridLength(boardWidth / boardSize);
                row.Height = new GridLength(boardHeight / boardSize);
                Grid.ColumnDefinitions.Add(col);
                Grid.RowDefinitions.Add(row);
            }

            // Fill Cols and Rows with Rectangles
            for (int i = 0; i < boardSize; i++) {
                for (int j = 0; j < boardSize; j++) {
                    // Initialize the Rectangle
                    rect = new Rectangle();
                    rect.Width = boardWidth / boardSize;
                    rect.Height = boardHeight / boardSize;
                    rect.Fill = new SolidColorBrush(Colors.White);
                    rect.Stroke = new SolidColorBrush(Colors.Blue);
                    // Link with handlers
                    if (border.Name == "MySeaBorder")
                    {
                        rect.PointerPressed += new PointerEventHandler(Rectangle_PointerPressed);
                        rect.PointerEntered += new PointerEventHandler(Rectangle_PointerEntered);
                        rect.PointerExited += new PointerEventHandler(Rectangle_PointerExited);
                        rect.Name = (j + ":p1:" + i);
                    }
                    else
                    {
                        rect.Name = (j + ":bot:" + i);
                    }
                    //Put it into the Board Object (state 0 == empty cell)
                    board.addTile(new Tile(rect, "0"), i, j);
                    // Put it into the grid
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    Grid.Children.Add(rect);
                }
            }
            border.Child = Grid;
            return board;
        }

        public void InitializeBoats()
        {
            InitializeBoat("carrier", 5, 1, CarrierBorder);
            InitializeBoat("destroyer", 1, 4, DestroyerBorder);
            InitializeBoat("submarine1", 1, 3, Submarine1Border);
            InitializeBoat("submarine2", 3, 1, Submarine2Border);
            InitializeBoat("torpedo", 1, 2, TorpedoBorder);
        }

        public void InitializeBoat(string name, int sizeX, int sizeY, Border border)
        {
            Grid boatGrid = new Grid();
            boatGrid.Name = name + "Grid";
            boatGrid.VerticalAlignment = VerticalAlignment.Center;
            boatGrid.HorizontalAlignment = HorizontalAlignment.Center;

            for (int i = 0; i < sizeX; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(42);
                boatGrid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < sizeY; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(42);
                boatGrid.RowDefinitions.Add(row);
            }


            for (int i = 0; i < sizeX; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = 42;
                rect.Height = 42;
                rect.Name = (name + ":" + i); // C = Carrier + rectIdx
                rect.Fill = new SolidColorBrush(Colors.White);
                rect.Stroke = new SolidColorBrush(Colors.DarkGray);
                rect.PointerEntered += new PointerEventHandler(Boat_PointerEntered);
                rect.PointerPressed += new PointerEventHandler(Boat_PointerPressed);
                Grid.SetColumn(rect, i);
                boatGrid.Children.Add(rect);
            }
            for (int i = 0; i < sizeY; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = 42;
                rect.Height = 42;
                rect.Name = (name + ":" + i); // C = Carrier + rectIdx
                rect.Fill = new SolidColorBrush(Colors.White);
                rect.Stroke = new SolidColorBrush(Colors.DarkGray);
                rect.PointerEntered += new PointerEventHandler(Boat_PointerEntered);
                rect.PointerPressed += new PointerEventHandler(Boat_PointerPressed);
                Grid.SetRow(rect, i);
                boatGrid.Children.Add(rect);
            }

            border.Child = boatGrid;
        }

        public void InitializeBotBoard()
        {
            List<Boat> boatList = new List<Boat> { new Boat("carrier", 5, 1), new Boat("destroyer", 1, 4), new Boat("submarine1", 1, 3), new Boat("submarine2", 3, 1), new Boat("torpedo", 1, 2) };

            var rand = new Random();

            bool isMovePossible = false;

            foreach (Boat boat in boatList)
            {
                isMovePossible = false;

                while (isMovePossible == false)
                {
                    var x = rand.Next(10);
                    var y = rand.Next(10);
                    isMovePossible = true;

                    if (CheckBoatPlacement(enemyBoard.B, x, y, boat, 0))
                    {
                        if (boat.width == 1)
                        {
                            for (int i = 0; i < boat.height; i++)
                            {
                                if (enemyBoard.B[y + i, x].state != "0")
                                {
                                    isMovePossible = false;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < boat.width; i++)
                            {
                                if (enemyBoard.B[y, x + i].state != "0")
                                {
                                    isMovePossible = false;
                                }
                            }
                        }
                        if (isMovePossible)
                        {
                            if (boat.width == 1)
                            {
                                for (int i = 0; i < boat.height; i++)
                                {
                                    enemyBoard.B[y + i, x].rect.Fill = new SolidColorBrush(Colors.Red);
                                    enemyBoard.B[y + i, x].state = "1:" + boat.name + ":" + i;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < boat.width; i++)
                                {
                                    enemyBoard.B[y, x + i].rect.Fill = new SolidColorBrush(Colors.Red);
                                    enemyBoard.B[y, x + i].state = "1:" + boat.name + i;
                                }
                            }
                            boat.setTopLeftPos(x, y);
                            enemyBoard.addBoat(boat);
                            isMovePossible = true;
                        }
                    }
                    else { isMovePossible = false;  }
                }
            }
        }

        public bool CheckBoatPlacement(Tile[,] board, int x, int y, Boat boat, int idx)
        {
            // If the boat is vertical
            if (boat.width == 1)
            {
                // check if the boat placement is allowed
                if (x < boardSize && x >= 0 && y + (boat.height - selectedBoatIdx - 1) < boardSize && y - (selectedBoatIdx) >= 0)
                {
                    for (int i = 0; i < Math.Max(boat.width, boat.height); i++)
                    {
                        if (board[y - idx + i, x].state != "0")
                        {
                            return false;
                        }
                    }
                }
                else { return false; }
            }
            else if (boat.height == 1)
            {
                // check if the boat placement is allowed
                if (x + (boat.width - selectedBoatIdx - 1) < boardSize && x - (selectedBoatIdx) >= 0 && y  < boardSize && y >= 0)
                {
                    for (int i = 0; i < Math.Max(boat.width, boat.height); i++)
                    {
                        if (board[y , x - idx + i].state != "0")
                        {
                            return false;
                        }
                    }
                }
                else { return false; }
            }
                return true;
        }

        public bool CheckEndGame(Board board)
        {
            foreach (var boat in board.Boats)
            {
                for (int i = 0; i < boat.damage.Length; i++)
                {
                    if (boat.damage[i] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        ////////////////////////////////////////// Event Handlers //////////////////////////////////////////

        public void Rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            Windows.UI.Input.PointerPointProperties ptrPt = e.GetCurrentPoint(this).Properties;
            Rectangle rect = (Rectangle)sender;
            String name = rect.Name;
            var splittedName = name.Split(':');
            int x = Int32.Parse(splittedName[0].ToString());
            int y = Int32.Parse(splittedName[2].ToString());
            
            Tile[,] t = myBoard.B;

            // if the user want to place a boat
            if (ptrPt.IsLeftButtonPressed && selectedBoatIdx != -1 && CheckBoatPlacement(myBoard.B, x, y, selectedBoat, selectedBoatIdx))
            {
                Border boatBorder = BoatNameToBoatBorder[selectedBoat.name];
                Grid boatGrid = (Grid)boatBorder.Child;

                // Place the boat on the board
                if (selectedBoat.width == 1)
                {
                    for (int i = 0; i < selectedBoat.height; i++)
                    {
                        if (y - selectedBoatIdx + i >= 0 && y - selectedBoatIdx + i < boardSize)
                        {
                            t[y - selectedBoatIdx + i, x].rect.Fill = new SolidColorBrush(Colors.DimGray);
                            t[y - selectedBoatIdx + i, x].state = "1:" + selectedBoat.name + ":" + i;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < selectedBoat.width; i++)
                    {
                        if (x - selectedBoatIdx + i >= 0 && x - selectedBoatIdx + i < boardSize)
                        {
                            t[y, x - selectedBoatIdx + i].rect.Fill = new SolidColorBrush(Colors.DimGray);
                            t[y , x - selectedBoatIdx + i].state = "1:" + selectedBoat.name + ":" + i;
                        }
                    }
                }

                // Hide the boat if placement is succesful
                boatGrid.Visibility = Visibility.Collapsed;

                // Start the game if there is no more boat to put
                if(myBoard.boats.Count == 5)
                {
                    playerTurn = 0;
                }

                if (selectedBoat.width == 1){ selectedBoat.setTopLeftPos(x, y - selectedBoatIdx); }
                else { selectedBoat.setTopLeftPos(x - selectedBoatIdx, y); }
                myBoard.addBoat(selectedBoat);

                selectedBoat = null;
                selectedBoatIdx = -1;
            }
            else if (ptrPt.IsRightButtonPressed && selectedBoatIdx != -1)
            {
                selectedBoat.rotateBoat();
                Rectangle_PointerExited(sender, e);
                Rectangle_PointerEntered(sender, e);
            }
        }
        public void Rectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            String name = rect.Name;
            var splittedName = name.Split(':');
            int x = Int32.Parse(splittedName[0].ToString());
            int y = Int32.Parse(splittedName[2].ToString());
            Tile[,] t = myBoard.B;

            if (selectedBoatIdx != -1)
            {
                if(selectedBoat.width == 1)
                {
                    for(int i = 0; i < selectedBoat.height; i++)
                    {
                        if(CheckBoatPlacement(t, x, y, selectedBoat, selectedBoatIdx))
                        {
                            if (y - selectedBoatIdx + i >= 0 && y - selectedBoatIdx + i < boardSize && t[y - selectedBoatIdx + i, x].state == "0")
                            {
                                t[y - selectedBoatIdx + i, x].rect.Fill = new SolidColorBrush(Colors.LightGray);
                            }
                        }
                        else
                        {
                            if (y - selectedBoatIdx + i >= 0 && y - selectedBoatIdx + i < boardSize && t[y - selectedBoatIdx + i, x].state == "0")
                            {
                                t[y - selectedBoatIdx + i, x].rect.Fill = new SolidColorBrush(Colors.OrangeRed);
                            }
                        }
                        
                    }
                }
                else
                {
                    for (int i = 0; i < selectedBoat.width; i++)
                    {
                        if (CheckBoatPlacement(t, x, y, selectedBoat, selectedBoatIdx))
                        {
                            if (x - selectedBoatIdx + i >= 0 && x - selectedBoatIdx + i < boardSize && t[y, x - selectedBoatIdx + i].state == "0")
                            {
                                t[y, x - selectedBoatIdx + i].rect.Fill = new SolidColorBrush(Colors.LightGray);
                            }
                        }
                        else
                        {
                            if (x - selectedBoatIdx + i >= 0 && x - selectedBoatIdx + i < boardSize && t[y, x - selectedBoatIdx + i].state == "0")
                            {
                                t[y, x - selectedBoatIdx + i].rect.Fill = new SolidColorBrush(Colors.OrangeRed);
                            }
                        }
                    }
                }
            }
        }
        public void Rectangle_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            Tile[,] t = myBoard.B;
            SolidColorBrush fillBrush1 = new SolidColorBrush(Colors.LightGray);
            SolidColorBrush fillBrush2 = new SolidColorBrush(Colors.OrangeRed);

            foreach (Tile tile in t)
            {
                SolidColorBrush rectFillBrush = (SolidColorBrush)tile.rect.Fill;
                if (rectFillBrush.Color == fillBrush1.Color || rectFillBrush.Color == fillBrush2.Color)
                {
                    tile.rect.Fill = new SolidColorBrush(Colors.White);
                }
            }
        }

        public void Boat_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            rect.PointerExited += new PointerEventHandler(Boat_PointerExited);
            rect.Fill = new SolidColorBrush(Colors.DimGray);
        }
        public void Boat_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            rect.PointerExited -= new PointerEventHandler(Boat_PointerExited);
            rect.Fill = new SolidColorBrush(Colors.White);
        }
        public void Boat_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            rect.PointerExited -= new PointerEventHandler(Boat_PointerExited);

            // Get the name of the clicked boat
            Grid grid = (Grid)rect.Parent;
            string boatName = grid.Name.Split("Grid")[0];
            foreach (Rectangle item in grid.Children)
            {
                item.Fill = new SolidColorBrush(Colors.Green);
            }

            // Depending on the name we create the boat object and put it in selectedBoat
            selectedBoat = boatName == "carrier"  ? new Boat("carrier", 5, 1) : selectedBoat;
            selectedBoat = boatName == "destroyer" ? new Boat("destroyer", 1, 4) : selectedBoat;
            selectedBoat = boatName == "submarine1" ? new Boat("submarine1", 1, 3) : selectedBoat;
            selectedBoat = boatName == "submarine2" ? new Boat("submarine2", 3, 1) : selectedBoat;
            selectedBoat = boatName == "torpedo" ? new Boat("torpedo", 1, 2) : selectedBoat;

            selectedBoatIdx = Int32.Parse(rect.Name.Split(":")[1]);
            ClearBoatColor(selectedBoat);

        }

        public void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Play the game 42");
            Level.Text = "42";
        }

        private void KeyPressed(object sender, KeyRoutedEventArgs e)
        {
            Windows.System.VirtualKey k = e.Key;
        }

        private void ClearBoatColor(Boat boat)
        {
            List<string> boatList = new List<string> { "carrier", "destroyer", "submarine1", "submarine2", "torpedo" };

            foreach (string item in boatList)
            {
                if(item != boat.name)
                {
                    Border border = BoatNameToBoatBorder[item];
                    Grid grid = (Grid)border.Child;
                    foreach (Rectangle rect in grid.Children)
                    {
                        rect.Fill = new SolidColorBrush(Colors.White);
                    }
                }
            }

        }
    }
}
