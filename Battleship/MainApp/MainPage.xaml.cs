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
        static public Grid seaGrid;

        static Board board;
        static public int boardSize = 10;

        public MainPage()
        {
            this.InitializeComponent();
            this.InitializeSea();
            
            this.InitializeBoats();
        }

        ////////////////////////////////////////// Initializer //////////////////////////////////////////

        public void InitializeSea()
        {
            // Init Grid Object
            seaGrid = new Grid();
            seaGrid.Name = "SeaGrid";
            double boardWidth = SeaBorder.Width;
            double boardHeight = SeaBorder.Height;

            // Init Board Object
            board = new Board(boardSize, boardSize);

            // Create Cols and Rows of our Grid (15x15)
            for (int i = 0; i < boardSize; i++){
                ColumnDefinition col = new ColumnDefinition();
                RowDefinition row = new RowDefinition();
                col.Width = new GridLength(boardWidth / boardSize);
                row.Height = new GridLength(boardHeight / boardSize);
                seaGrid.ColumnDefinitions.Add(col);
                seaGrid.RowDefinitions.Add(row);
            }

            // Fill Cols and Rows with Rectangles
            for (int i = 0; i < boardSize; i++){
                for (int j = 0; j < boardSize; j++){
                    // Initialize the Rectangle
                    Rectangle rect = new Rectangle();
                    rect.Width = boardWidth / boardSize;
                    rect.Height = boardHeight / boardSize;
                    rect.Name = (i + ":" + j);
                    rect.Fill = new SolidColorBrush(Colors.White);
                    rect.Stroke = new SolidColorBrush(Colors.Blue);
                    // Link with handlers
                    rect.PointerPressed += new PointerEventHandler(Rectangle_PointerPressed);
                    //Put it into the Board Object (state 0 == empty cell)
                    Board.Tile tile = new Board.Tile(rect, "0");
                    board.addTile(tile, i, j);
                    // Put it into the grid
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    seaGrid.Children.Add(rect);
                }
            }
            SeaBorder.Child = seaGrid;
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

        ////////////////////////////////////////// Event Handlers //////////////////////////////////////////

        public void Rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            String name = rect.Name;
            var splittedName = name.Split(':');
            int x = Int32.Parse(splittedName[0].ToString());
            int y = Int32.Parse(splittedName[1].ToString());

            textBox1_X.Text = x.ToString();
            textBox1_Y.Text = y.ToString();

            rect.Fill = new SolidColorBrush(Colors.Red);
        }

        public void Boat_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            rect.PointerExited += new PointerEventHandler(Boat_PointerExited);
            rect.Fill = new SolidColorBrush(Colors.Red);
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
            rect.Fill = new SolidColorBrush(Colors.Red);
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
    }
}
