using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;

namespace DungeonsAndDungeons
{
    public class Map<T> : ICollection where T : IComparable
    {
        public T[,] Tiles { get; set; }
        public T EmptyTile { get; }
        public T DoorTile { get; }
        public List<Room> Rooms { get; }
        public int Height => Tiles.GetLength(0);
        public int Width => Tiles.GetLength(1);
        public int Count => Tiles.Length;
        public object SyncRoot => Tiles.SyncRoot;

        public bool IsSynchronized => Tiles.IsSynchronized;

        public Map(T[,] tiles, T empty, T doorTile)
        {
            Tiles = tiles;
            EmptyTile = empty;
            DoorTile = doorTile;
            ParseRooms();
        }

        public bool AreInSameRoom(Entity entity, params Entity[] entities)
        {
            return true;
        }

        private void ParseRooms()
        {
            int x = 0;
            int y = 0;
            int height = 0;
            int width = 0;
            bool parsingInProgress = true;

            for(int i = y+1; i < Height; i++)
            {
                for (int j = x + 1; j < Width; j++)
                {

                }

            }

        }

        public bool IsValid(int x, int y) => !IsOutOfBounds(x, y) && IsEmpty(x, y);
        public bool IsValid(Vector2 position)
        {
            return IsValid((int)Math.Floor(position.X), (int)Math.Floor(position.Y));
        }

        public bool IsEmpty(Vector2 pos) => Tiles[(int)pos.X, (int)pos.Y].CompareTo(EmptyTile) <= 0;
        public bool IsEmpty(int x, int y) => Tiles[y, x].CompareTo(EmptyTile) <= 0;

        public bool IsOutOfBounds(int x, int y)
        {
            return x < 0 || x > Width || y < 0 || y > Height;
        }

        public void CopyTo(Array array, int index)
        {
            Tiles.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return Tiles.GetEnumerator();
        }

        public T this[Vector2 pos]
        {
            get => this[(int)pos.Y, (int)pos.X];
            set => this[(int)pos.Y, (int)pos.X] = value;
        }

        public T this[int i, int j]
        {
            get => Tiles[i,j];
            set => Tiles[i,j] = value;
        }
    }
}