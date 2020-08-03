using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class LevelGenerator
    {
        private List<Texture2D> Textures;
        private int NumberOfEntities { get; set; }
        private int NumberOfItems { get; set; }
        private TexturedMap Map { get; set; }
        private ContentManager Manager { get; }

        public LevelGenerator(ContentManager manager)
        {
            Manager = manager;
            Textures = new List<Texture2D>();

            for (int i = 1; i < 9; i++)
            {
                Textures.Add(Manager.Load<Texture2D>(i.ToString()));
            }
        }

        public Level Generate(string seed) //Currently only meant to simplify testing, not fully implemented
        {
            //seed format "XXYYZZ" where XX is number of entities, YY items and ZZ the size of the map

            if (seed.Length != 6) { throw new ArgumentException(); }

            Player player = new Player(new Vector2(17.5f, 3.5f),
                                      new Vector2(0, 1),
                                      new Inventory(10,
                                      new Item[] {}),
                                      new Health(100),
                                      new List<Sprite>() { },
                                      new ActionPoints(2));

            Map = GenerateMap();

            NumberOfEntities = int.Parse(seed.Substring(0, 2));
            NumberOfItems = int.Parse(seed.Substring(2, 2));

            return new Level(Map, new List<Item>(), GenerateEntities(NumberOfEntities), player);

        }


        private TexturedMap GenerateMap()
        {
            var tiles = new int[,] {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,7,7,7,7},
                { 4,0,0,5,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,2},
                { 4,0,6,0,6,0,0,0,0,0,0,0,0,0,0,0,5,0,0,2},
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                { 4,4,4,4,4,4,4,4,4,4,1,1,1,2,2,2,2,2,2,3}
            };

            return new TexturedMap(tiles, Textures);
        }


        private List<Entity> GenerateEntities(int amount)
        {
            List<Entity> entities = new List<Entity>();
            Monster prototype = new Monster(new Vector2(8.5f, 2.5f),
                                new Vector2(-1, 0),
                                new Inventory(10),
                                new Health(100),
                                new List<Sprite>() { new Sprite(Manager.Load<Texture2D>("demon")) },
                                new ActionPoints(2));

            Random random = new Random();

            float x = 0;
            float y = 0;

            for (int i = 0; i < amount; i++)
            {
                x = random.Next(0, Map.Width-1) + 0.5f;
                y = random.Next(0, Map.Height-1) + 0.5f;

                entities.Add(new Monster(new Vector2(x, y), prototype.Direction, prototype.Inventory, prototype.Health, prototype.Stances, prototype.ActionPoints));
            }

            return entities;
        }
    }
}