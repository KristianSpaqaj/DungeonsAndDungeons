﻿using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Generation
{
    public class LevelGenerator
    {
        private readonly List<Texture2D> Textures;
        private int NumberOfEntities { get; set; }
        private int NumberOfItems { get; set; }
        private TexturedMap Map { get; set; }
        private List<Item> Items { get; set; }
        private List<Entity> Entities { get; set; }
        private ContentManager Manager { get; }
        private Random RandomGenerator { get; set; }


        public LevelGenerator(ContentManager manager)
        {
            Manager = manager;
            Textures = new List<Texture2D>();

            for (int i = 1; i < 9; i++)
            {
                Textures.Add(Manager.Load<Texture2D>(i.ToString()));
            }

            Items = new List<Item>();
            Entities = new List<Entity>();

        }

        public Level Generate(string seed) //Currently only meant to simplify testing, not fully implemented
        {
            //seed format "XXYYZZ" where XX is number of entities, YY items and ZZ the size of the map

            if (seed.Length != 10) { throw new ArgumentException(); }

            int levelSeed = int.Parse(seed.Substring(0, 4));
            NumberOfEntities = int.Parse(seed.Substring(4, 2));
            NumberOfItems = int.Parse(seed.Substring(6, 2));

            RandomGenerator = new Random(levelSeed);

            MapGenerator mg = new MapGenerator(5, 7);
            Map = new TexturedMap(mg.LayoutParser.Tiles, mg.LayoutParser.Rooms, Textures);
            ItemGenerator ig = new ItemGenerator();
            Items = new List<Item>();
            Entities = new List<Entity>();

            foreach(Room room in Map.Rooms)
            {
                Items.AddRange(room.SpawnItems());
            }

            var StartingPoint = mg.LayoutParser.PathPoints[0];

            Player player = new Player(new Vector2(StartingPoint.x + 0.5f, StartingPoint.y + 0.5f),
                                      new Vector2(-1, 0),
                                      new Inventory(10,
                                      new Item[] { }),
                                      new Health(100),
                                      new List<Sprite>() { },
                                      new ActionPoints(2));
            return new Level(Map, Items, Entities, player);

        }


        //private List<Entity> GenerateEntities()
        //{
        //    List<Entity> entities = new List<Entity>();
        //    Monster prototype = new Monster(new Vector2(8.5f, 2.5f),
        //                        new Vector2(-1, 0),
        //                        new Inventory(10),
        //                        new Health(100),
        //                        new List<Sprite>() { new Sprite(Manager.Load<Texture2D>("demon")) },
        //                        new ActionPoints(2));

        //    for (int i = 0; i < NumberOfEntities; i++)
        //    {
        //        entities.Add(new Monster(GeneratePosition(), prototype.Direction, prototype.Inventory, prototype.Health, prototype.Stances, prototype.ActionPoints));
        //    }

        //    return entities;
        //}
    }
}