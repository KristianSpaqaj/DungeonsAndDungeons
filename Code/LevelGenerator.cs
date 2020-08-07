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

        }

        public Level Generate(string seed) //Currently only meant to simplify testing, not fully implemented
        {
            //seed format "XXYYZZ" where XX is number of entities, YY items and ZZ the size of the map

            if (seed.Length != 10) { throw new ArgumentException(); }

            int levelSeed = int.Parse(seed.Substring(0, 4));
            NumberOfEntities = int.Parse(seed.Substring(4, 2));
            NumberOfItems = int.Parse(seed.Substring(6, 2));

            RandomGenerator = new Random(levelSeed);

            Player player = new Player(new Vector2(17.5f, 3.5f),
                                      new Vector2(-1, 0),
                                      new Inventory(10,
                                      new Item[] { }),
                                      new Health(100),
                                      new List<Sprite>() { },
                                      new ActionPoints(2));

            Map = GenerateMap();
            Items = GenerateItems();
            Entities = GenerateEntities();

            return new Level(Map, Items, Entities, player);

        }


        private TexturedMap GenerateMap()
        {
            MapGenerator mg = new MapGenerator(25, 25);
            var tiles = mg.LeafsToTiles();

            return new TexturedMap(tiles, Textures);
        }


        private List<Entity> GenerateEntities()
        {
            List<Entity> entities = new List<Entity>();
            Monster prototype = new Monster(new Vector2(8.5f, 2.5f),
                                new Vector2(-1, 0),
                                new Inventory(10),
                                new Health(100),
                                new List<Sprite>() { new Sprite(Manager.Load<Texture2D>("demon")) },
                                new ActionPoints(2));

            for (int i = 0; i < NumberOfEntities; i++)
            {
                entities.Add(new Monster(GeneratePosition(), prototype.Direction, prototype.Inventory, prototype.Health, prototype.Stances, prototype.ActionPoints));
            }

            return entities;
        }

        private List<Item> GenerateItems()
        {
            List<Item> items = new List<Item>();
            List<Sprite> sprites = new List<Sprite>
            {
                new Sprite(Manager.Load<Texture2D>("key")),
                new Sprite(Manager.Load<Texture2D>("knife"))
            };

            int spriteIndex = RandomGenerator.Next(0, sprites.Count);

            for (int i = 0; i < NumberOfItems; i++)
            {
                items.Add(new Item(sprites[spriteIndex], GeneratePosition()));
            }

            return items;
        }

        private Vector2 GeneratePosition()
        {
            float x = -1;
            float y = -1;

            x = RandomGenerator.Next(0, Map.Width) + 0.5f;
            y = RandomGenerator.Next(0, Map.Height) + 0.5f;

            return new Vector2(x, y);
        }
    }
}