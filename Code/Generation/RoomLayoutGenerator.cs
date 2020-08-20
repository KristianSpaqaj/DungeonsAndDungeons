using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DungeonsAndDungeons.Generation
{
    class RoomLayoutGenerator
    {

        private enum Directions { N, S, E, W };

        private Dictionary<Directions, Point> Dirs;
        private const int START_ROOM = 1;
        private List<int> Types { get; }
        private int MapHeight { get; set; }
        private int MapWidth { get; set; }
        private int NumberOfRooms { get; set; }
        private int RoomsLeft { get; set; }
        private int[,] Map { get; set; }
        private Point LastRoom { get; set; }
        public int Tries { get; set; }
        private Random Random { get; }
        public RoomLayoutGenerator(int numberOfRooms)
        {
            Types = new List<int>() { 2, 3, 4 };
            MapHeight = (int)Math.Ceiling(Math.Sqrt(numberOfRooms));
            MapWidth = (int)Math.Ceiling(Math.Sqrt(numberOfRooms));
            NumberOfRooms = numberOfRooms;
            RoomsLeft = numberOfRooms;
            Map = GenUtils.CreateEmptyMap(MapHeight, MapWidth);
            LastRoom = new Point(-1, -1);
            Random = new Random();

            Dirs = new Dictionary<Directions, Point>()
            {
                { Directions.N, new Point(0,-1) },
                { Directions.S, new Point(0,1) },
                { Directions.E, new Point(1,0) },
                { Directions.W, new Point(-1,0) }
            };
        }

        public RoomLayout Generate()
        {
            CreateStartRoom();

            while (RoomsLeft > 0 )
            {
                CreateRoom();
            }

            return new RoomLayout(Map);
        }


        private void CreateStartRoom()
        {
            Point square = GetRandomSquare();
            Map[square.Y, square.X] = START_ROOM;
            RoomsLeft -= 1;
            LastRoom = square;
        }

        private void CreateRoom()
        {
            Point direction = GetRandomDirection();
            Point square = new Point(LastRoom.X + direction.X, LastRoom.Y + direction.Y);

            while (true)
            {
                if (IsValid(square) && Map[square.Y, square.X] == 0)
                {
                    break;
                }
                else
                {
                    direction = GetRandomDirection();
                    square = AddPoints(square,direction);
                }
            }

            Map[square.Y, square.X] = GetRandomRoomType();

            RoomsLeft -= 1;
            LastRoom = square;
        }

        private int CountNeighbors(Point square)
        {
            int count = 0;
            foreach(Point dir in Dirs.Values)
            {
                Point neighbor = AddPoints(square, dir);
                if (!IsValid(neighbor))
                {
                    continue;
                }

                if(Map[neighbor.Y,neighbor.X] != 0)
                {
                    count++;
                }
            }

            return count;
        }

        private int GetRandomRoomType()
        {
            int index = Random.Next(Types.Count);
            return Types[index];
        }

        private bool IsValid(Point square)
        {
            return square.Y >= 0 && square.Y <= MapHeight - 1 && square.X >= 0 && square.X <= MapWidth - 1;
        }

        private Point GetRandomDirection()
        {
            int index = Random.Next(Dirs.Count);
            return Dirs.Values.ToList()[index];
        }

        private Point GetRandomSquare()
        {
            int y = Random.Next(MapHeight);
            int x = Random.Next(MapWidth);

            while (Map[y, x] != 0)
            {
                y = Random.Next(MapHeight);
                x = Random.Next(MapWidth);
            }

            return new Point(x,y);
        }

        private Point AddPoints(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

    }
}
