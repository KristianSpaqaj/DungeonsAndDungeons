using DungeonsAndDungeons.Entities;
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
            Player player = null;

            foreach (Room room in Map.Rooms)
            {
                Items.AddRange(room.SpawnItems());
                if(room is StartRoom)
                {
                    player = (Player)room.SpawnEntities()[0];
                }
                else
                {
                    Entities.AddRange(room.SpawnEntities());
                }
            }

            var StartingPoint = mg.LayoutParser.PathPoints[0];

            return new Level(Map, Items, Entities, player);

        }

    }

}