using Microsoft.Xna.Framework;
using System;
using System.Collections;

namespace DungeonsAndDungeons
{
    public class Map<T> : ICollection where T : IComparable
    {
        public T[,] Tiles { get; set; }
        public T EmptyTile { get; }
        public int Height => Tiles.GetLength(0);
        public int Width => Tiles.GetLength(1);
        public int Count => Tiles.Length;

        public object SyncRoot => Tiles.SyncRoot;

        public bool IsSynchronized => Tiles.IsSynchronized;

        public Map(T[,] tiles, T empty)
        {
            Tiles = tiles;
            EmptyTile = empty;
        }

        public bool IsValid(int x, int y) => !IsOutOfBounds(x, y) && IsEmpty(x, y);
        public bool IsValid(Vector2 position)
        {
            return IsValid((int)Math.Floor(position.X), (int)Math.Floor(position.Y));
        }

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

        public T this[int i, int j] => Tiles[i, j];
    }
}