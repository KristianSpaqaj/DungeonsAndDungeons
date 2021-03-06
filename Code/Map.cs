﻿using DungeonsAndDungeons.Entities;
using DungeonsAndDungeons.Generation;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;

namespace DungeonsAndDungeons
{
    public class Map<T> : ICollection where T : IComparable
    {
        protected T[,] Tiles { get; set; }
        public T EmptyTile { get; }
        public T DoorTile { get; }
        public List<Room> Rooms { get; }
        public int Height => Tiles.GetLength(0);
        public int Width => Tiles.GetLength(1);
        public int Count => Tiles.Length;
        public object SyncRoot => Tiles.SyncRoot;

        public bool IsSynchronized => Tiles.IsSynchronized;

        public Map(T[,] tiles, List<Room> rooms, T empty, T doorTile)
        {
            Tiles = tiles;
            Rooms = rooms;
            EmptyTile = empty;
            DoorTile = doorTile;
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
            get => this[(int)Math.Floor(pos.X), (int)Math.Floor(pos.Y)];
            set => this[(int)Math.Floor(pos.X), (int)Math.Floor(pos.Y)] = value;
        }

        public T this[int x, int y]
        {
            get => Tiles[y, x];
            set => Tiles[y, x] = value;
        }
    }
}