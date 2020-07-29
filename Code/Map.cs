using System;
using System.Collections;

namespace DungeonsAndDungeons
{
    public class Map<T> : ICollection
    {
        public T[,] Tiles { get; set; }
        public int Height => Tiles.GetLength(0);
        public int Width => Tiles.GetLength(1);
        public T EmptyTile { get; }
        public int Count => Tiles.Length;

        public object SyncRoot => Tiles.SyncRoot;

        public bool IsSynchronized => Tiles.IsSynchronized;

        public Map(T[,] tiles)
        {
            Tiles = tiles;
            EmptyTile = default;
        }

        public Map(T[,] tiles, T empty) : this(tiles)
        {
            EmptyTile = empty;
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