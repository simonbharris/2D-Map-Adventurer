using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MinimapRenderer
{
    class Map
    {
        const int mapSize = 6;
        int mapX = mapSize;
        int mapY = mapSize;
        public Location[,] grid;
        public Vector myLocation;

        // Generates a 2d grid and assigns all tiles a generic color.
        public void GenerateMap()
        {
            grid = new Location[mapX + 1, mapY + 1];
            for(int i = 0; i < mapX; i++)
            {
                for(int j = 0; j < mapY; j++)
                {
                    grid[i, j] = new Location(i, j);
                    grid[i, j].TileColor = Colors.LightGreen;
                }
            }

            PopulateSpecialLocations();
        }

        // Generates specially-colored tiles.
        public void PopulateSpecialLocations()
        {
            grid[1, 1].TileColor = Colors.Brown;
            grid[1, 3].TileColor = Colors.Azure;
            grid[4, 2].TileColor = Colors.DarkOrchid;
            grid[3, 4].TileColor = Colors.Blue;
            grid[4, 5].TileColor = Colors.Red;
        }

        // Movement methods on cardinals.
        public void MoveSouth()
        {
            if((myLocation.Y < MapSize-1)){
                myLocation.Y++;
            }
        }

        public void MoveEast()
        {
            if ((myLocation.X < MapSize - 1))
            {
                myLocation.X++;
            }
        }

        public void MoveNorth()
        {
            if ((myLocation.Y - 1 >= 1))
            {
                myLocation.Y--;
            }
        }

        public void MoveWest()
        {
            if ((myLocation.X - 1 >= 1))
            {
                myLocation.X--;
            }
        }

        // Accessor / mutations -- Also myLocation should be tracked elsewhere.
        public int MapSize
        {
            get { return mapSize; }
        }
        public Vector MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        public Location GetLocation(int x, int y)
        {
            return grid[x, y];
        }

        public void SetLocation(Location loc, int x, int y)
        {
            grid[x, y] = loc;
        }
    }

    class Location
    {
        string name;
        int x, y;
        Color tileColor;
        public Location(int x, int y, string name)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }



        public Color TileColor
        {
            get { return tileColor; }
            set { tileColor = value; }
        }
    }
}
