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
        static public Grid grid;
        static public Grid carrierGrid;
        static public Grid submarineGrid;

        public MainPage()
        {
            this.InitializeComponent();
            this.InitializeSea();
            this.InitializeBoats();
        }

        ////////////////////////////////////////// Initializer //////////////////////////////////////////

        public void InitializeSea()
        {
            // Init grid object
            grid = new Grid();
            grid.Name = "SeaGrid";

            // Create Cols and Rows of our Grid (15x15)
            for (int i = 0; i < 15; i++){
                ColumnDefinition col = new ColumnDefinition();
                RowDefinition row = new RowDefinition();
                col.Width = new GridLength(42);
                row.Height = new GridLength(42);
                grid.ColumnDefinitions.Add(col);
                grid.RowDefinitions.Add(row);
            }

            // Fill Cols and Rows with Rectangles
            for (int i = 0; i < 15; i++){
                for (int j = 0; j < 15; j++){
                    Rectangle rect = new Rectangle();
                    rect.Width = 42;
                    rect.Height = 42;
                    rect.Name = (i + ":" + j);
                    rect.Fill = new SolidColorBrush(Colors.White);
                    rect.Stroke = new SolidColorBrush(Colors.Blue);
                    rect.PointerPressed += new PointerEventHandler(Rectangle_PointerPressed);
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    grid.Children.Add(rect);
                }
            }
            SeaBorder.Child = grid;
        }

        public void InitializeBoats()
        {
            carrierGrid = new Grid();
            carrierGrid.Name = "CarrierGrid";
            carrierGrid.VerticalAlignment = VerticalAlignment.Center;
            carrierGrid.HorizontalAlignment = HorizontalAlignment.Center;

            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(42);
            grid.RowDefinitions.Add(row);

            for (int i = 0; i < 5; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(42);
                carrierGrid.ColumnDefinitions.Add(col); 
            }

            for (int i = 0; i < 5; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = 42;
                rect.Height = 42;
                rect.Name = ("C:"+i); // C = Carrier + rectIdx
                rect.Fill = new SolidColorBrush(Colors.White);
                Grid.SetColumn(rect, i);
                Grid.SetRow(rect, 0);
                carrierGrid.Children.Add(rect);
            }

            CarrierBorder.Child = carrierGrid;
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
