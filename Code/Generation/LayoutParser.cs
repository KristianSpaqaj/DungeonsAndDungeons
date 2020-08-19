﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DungeonsAndDungeons.Generation
{
    public class LayoutParser
    {
        private RoomLayout Layout { get; set; }
        private int RoomHeight { get; }
        private int RoomWidth { get; }
        private int MapHeight => RoomHeight * Layout.NumberOfRooms;
        private int MapWidth => RoomWidth * Layout.NumberOfRooms;
        public List<Room> Rooms { get; private set; }
        public int[,] Tiles { get;}
        public List<(int y, int x)> PathPoints { get; }

        public LayoutParser(RoomLayout layout, int roomHeight, int roomWidth)
        {
            Layout = layout;
            RoomHeight = roomHeight;
            RoomWidth = roomWidth;
            Rooms = new List<Room>();
            Tiles = GenUtils.CreateEmptyMap(Layout.MapHeight * RoomHeight, Layout.MapWidth * RoomWidth);
            PathPoints = new List<(int y, int x)>();
        }

        private void ParseRooms()
        {
            for (int i = 0; i < Layout.MapHeight; i++)
            {
                for (int j = 0; j < Layout.MapWidth; j++)
                {
                    if (Layout.Tiles[i, j] != 0)
                    {
                        Room room = new GenericRoom(j * RoomHeight, i * RoomWidth, RoomHeight, RoomWidth, Layout.Tiles[i, j]);
                        Rooms.Add(room);
                    }
                }
            }
        }

        private int[,] ParseTiles()
        {
            foreach(Room room in Rooms)
            {
                if (room.Type != 0)
                {
                    PathPoints.Add(room.Center);
                    for (int i = room.Top; i < room.Bottom; i++)
                    {
                        for (int j = room.Left; j < room.Right; j++)
                        {
                            if (i == room.Top && Tiles[i, j] == 0)
                            {
                                if (Tiles[Math.Max(0, i - 1), j] == 0)
                                {
                                    Tiles[i, j] = room.Type;
                                }
                            }
                            if (i == room.Bottom - 1)
                            {
                                Tiles[i, j] = room.Type;
                            }
                            if (j == room.Left && Tiles[i, j] == 0)
                            {
                                if (Tiles[i, Math.Max(0,j-1)] == 0)
                                {
                                    Tiles[i, j] = room.Type;
                                }
                            }
                            if (j == room.Right - 1)
                            {
                                Tiles[i, j] = room.Type;
                            }
                        }
                    }
                }
            }


            for (int i = 0; i < PathPoints.Count - 1; i++)
            {
                var start = PathPoints[i];
                var end = PathPoints[i + 1];

                (int x, int y) direction = (end.x - start.x, end.y - start.y);
                if (direction.x == 0 && direction.y == 0)
                {
                    throw new InvalidDataException("Points cannot be identical");
                }

                if (direction.y == 0) //horizontal
                {
                    if (start.x > 0)
                    {
                        for (int x = 0; x < direction.x; x++)
                        {
                            if (Tiles[start.y + 1, start.x + x] != 0 || Tiles[start.y - 1, start.x + x] != 0)
                            {
                                Tiles[start.y, start.x + x] = 7;
                                Tiles[start.y, start.x + x - 1] = 0;
                            }
                        }
                    }
                    else
                    {
                        for (int x = 0; x > direction.x; x--)
                        {
                            if (Tiles[start.y + 1, start.x + x] != 0 || Tiles[start.y - 1, start.x + x] != 0)
                            {
                                Tiles[start.y, start.x + x] = 7;
                                Tiles[start.y, start.x + x + 1] = 0;
                            }
                        }
                    }
                }
                else
                {
                    if (start.y > 0)
                    {
                        for (int y = 0; y < direction.y; y++)
                        {
                            if (Tiles[start.y + y, start.x + 1] != 0 || Tiles[start.y + y, start.x - 1] != 0)
                            {
                                Tiles[start.y + y, start.x] = 7;
                                Tiles[start.y + y - 1, start.x] = 0;
                            }
                        }
                    }
                    else
                    {
                        for (int y = 0; y > direction.y; y--)
                        {
                            if (Tiles[start.y + y, start.x + 1] != 0 || Tiles[start.y + y, start.x - 1] != 0)
                            {
                                Tiles[start.y + y, start.x] = 7;
                                Tiles[start.y + y + 1, start.x] = 0;

                            }
                        }
                    }
                }
            }


            return Tiles;

        }

        public override string ToString()
        {
            string s = "";
            int[,] Tiles = ToTiles();

            for (int i = 0; i < Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Tiles.GetLength(1); j++)
                {
                    s += Tiles[i, j].ToString().PadRight(4);
                }

                s += "\n";
            }

            return s;
        }

    }
}
