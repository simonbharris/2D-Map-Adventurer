using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinimapRenderer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Map worldMap;
        Rectangle[,] mmGrid = new Rectangle[3, 3];

        public MainWindow()
        {
            InitializeComponent();
            worldMap = new Map();
            worldMap.GenerateMap();
            worldMap.MyLocation = new Vector(1, 1);
            BindRectGrid();

            // Debug (Identify current tile position.)
            lbldebug.Content = worldMap.myLocation.X + ", " + worldMap.myLocation.Y; 

            RenderSurroundings();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        // Does all the color
        private void fillRect(Rectangle rect, Color col)
        {
            rect.Fill = new SolidColorBrush(col);
        }

        private void RenderSurroundings()
        {
            int centerX = (int)worldMap.myLocation.X, centerY = (int)worldMap.myLocation.Y;

            // mi, mj = map tiles index // i, j track grid coordinates.
            // Iterates so that all map tiles are assigned the appropriate color or black of the tile does not exist.
            for (int i = centerX - 1, mi = 0; i <= centerX + 1; i++, mi++)
            {
                for (int j = centerY - 1, mj = 0; j <= centerY + 1; j++, mj++)
                {
                    if (j <= 0 || i <= 0 || worldMap.grid[j, i] == null)
                    {
                        fillRect(mmGrid[mj, mi], Colors.Black);
                    }
                    else
                    {
                        fillRect(mmGrid[mj, mi], worldMap.grid[j, i].TileColor);
                    }
                }
            }
        }

        // Binds GUI rects to mmGrid array.
        private void BindRectGrid()
        {
            mmGrid[0, 0] = rctNW;
            mmGrid[0, 1] = rctNorth;
            mmGrid[0, 2] = rctNE;
            mmGrid[1, 0] = rctWest;
            mmGrid[1, 1] = rctCenter;
            mmGrid[1, 2] = rctEast;
            mmGrid[2, 0] = rctSW;
            mmGrid[2, 1] = rctSouth;
            mmGrid[2, 2] = rctSE;
        }

        // Register GUI clicks.
        private void rctSouth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            worldMap.MoveSouth();
            RenderSurroundings();
            lbldebug.Content = worldMap.myLocation.X + ", " + worldMap.myLocation.Y;
        }

        private void rctEast_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            worldMap.MoveEast();
            RenderSurroundings();
            lbldebug.Content = worldMap.myLocation.X + ", " + worldMap.myLocation.Y;
        }

        private void rctNorth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            worldMap.MoveNorth();
            RenderSurroundings();
            lbldebug.Content = worldMap.myLocation.X + ", " + worldMap.myLocation.Y;
        }

        private void rctWest_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            worldMap.MoveWest();
            RenderSurroundings();
            lbldebug.Content = worldMap.myLocation.X + ", " + worldMap.myLocation.Y;
        }
    }
}
