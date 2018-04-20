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
        const int RENDER_SIZE = 5;
        Rectangle[,] mmGrid = new Rectangle[RENDER_SIZE, RENDER_SIZE];

        public MainWindow()
        {
            InitializeComponent();
            generateMapObjects();
            worldMap = new Map();
            worldMap.GenerateMap();
            worldMap.MyLocation = new Vector(1, 1);
            BindMovementButtons();

            //Debug (Identify current tile position.)
            lbldebug.Content = worldMap.myLocation.X + ", " + worldMap.myLocation.Y;
            try
            {
                RenderSurroundings();

            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
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
            for (int i = centerX - RENDER_SIZE / 2, mi = 0; i <= centerX + RENDER_SIZE / 2; i++, mi++)
            {
                for (int j = centerY - RENDER_SIZE/2, mj = 0; j <= centerY + RENDER_SIZE / 2; j++, mj++)
                {
                    if (j < 0 || j > 6 || i > 6 || i < 0 || worldMap.grid[j, i] == null)
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

        private void BindMovementButtons()
        {
            int mapCenter = RENDER_SIZE / 2;

            mmGrid[mapCenter + 1, mapCenter].MouseLeftButtonDown += SouthButton_MouseLeftButtonDown;
            mmGrid[mapCenter - 1, mapCenter].MouseLeftButtonDown += NorthButton_MouseLeftButtonDown;
            mmGrid[mapCenter, mapCenter + 1].MouseLeftButtonDown += EastButton_MouseLeftButtonDown;
            mmGrid[mapCenter, mapCenter - 1].MouseLeftButtonDown += WestButton_MouseLeftButtonDown;
        }

        private void WestButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            worldMap.MoveWest();
            RenderSurroundings();
        }

        private void EastButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            worldMap.MoveEast();
            RenderSurroundings();
        }

        private void SouthButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            worldMap.MoveSouth();
            RenderSurroundings();
        }

        private void NorthButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            worldMap.MoveNorth();
            RenderSurroundings();
        }

        private void generateMapObjects()
        {
            double canvasWidth = this.lineCanvas.Width;

            // Math may seem odd, but it all scales based off renderSize variable.
            // Odd numbers cause map to have a "centered" square.
            // Other functionality of the map probably won't work with even numbers.
            double mapHeight = lineCanvas.Height;
            double mapWidth = lineCanvas.Width;
            double tileSize = canvasWidth * 25 / 100 * (3 / (double) RENDER_SIZE);
            double tilePadding = 12.5 * (3 / RENDER_SIZE);

            for (int i = 0; i < RENDER_SIZE; i++)
            {
                for(int j = 0; j < RENDER_SIZE; j++)
                {
                    Rectangle rect = new Rectangle();
                    SolidColorBrush solidBrush = new SolidColorBrush(Colors.Black);
                    rect.Height = tileSize;
                    rect.Width = tileSize;

                    Canvas.SetLeft(rect, tilePadding + (j) * ((mapWidth / RENDER_SIZE) ));
                    Canvas.SetTop(rect, tilePadding + (i) * ((mapHeight / RENDER_SIZE) ));
                    rect.StrokeThickness = 1;
                    rect.Stroke = Brushes.Black;
                    this.lineCanvas.Children.Add(rect);

                    mmGrid[i, j] = rect;

                    rect.Fill = solidBrush;
                }
            }
        }
    }
}
