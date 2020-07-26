using System.Collections.Generic;
using System.IO;
using DungeonsAndDungeons.Code;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using Color = Microsoft.Xna.Framework.Color;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DungeonsGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Renderer renderer;
        Texture2D screen;
        List<Texture2D> textures;

        int[,] tiles = new int[,] {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,7,7,7,7,7,7,7,7},
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,7},
                { 4,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7},
                { 4,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7},
                { 4,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,7},
                { 4,0,4,0,0,0,0,5,5,5,5,5,5,5,5,5,7,7,0,7,7,7,7,7},
                { 4,0,5,0,0,0,0,5,0,5,0,5,0,5,0,5,7,0,0,0,7,7,7,1},
                { 4,0,6,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,7},
                { 4,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,1},
                { 4,0,7,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,7},
                { 4,0,0,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,7,7,7,1},
                { 4,0,0,0,0,0,0,5,5,5,5,0,5,5,5,5,7,7,7,7,7,7,7,1},
                { 6,6,6,6,6,6,6,6,6,6,6,0,6,6,6,6,6,6,6,6,6,6,6,6},
                { 7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                { 6,6,6,6,6,6,0,6,6,6,6,0,6,6,6,6,6,6,6,6,6,6,6,6},
                { 4,4,4,4,4,4,0,4,4,4,6,0,6,2,2,2,2,2,2,2,3,3,3,3},
                { 4,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,0,0,0,2},
                { 4,0,0,0,0,0,0,0,0,0,0,0,6,2,0,0,5,0,0,2,0,0,0,2},
                { 4,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,2,0,2,2},
                { 4,0,6,0,6,0,0,0,0,4,6,0,0,0,0,0,5,0,0,0,0,0,0,2},
                { 4,0,0,5,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,2,0,2,2},
                { 4,0,6,0,6,0,0,0,0,4,6,0,6,2,0,0,5,0,0,2,0,0,0,2},
                { 4,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,0,0,0,2},
                { 4,4,4,4,4,4,4,4,4,4,1,1,1,2,2,2,2,2,2,3,3,3,3,3}
            };

        private const int ScreenWidth = 1920;
        private const int ScreenHeight = 1080;

        private double seconds;

        private Level level;

        private List<Sprite> sprites;

        SoundEffect song;

        private InputMapper InputMapper;

        private GameContext GameContext { get; set; }

        private SpriteFont defaultFont;

        Dictionary<string, string> Configuration { get; set; }

        Camera camera;

        public DungeonsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            textures = new List<Texture2D>();
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            seconds = 0;
            GameContext = new GameContext(new GameTime());
        }

        protected override void Initialize()
        {
            var text = File.ReadAllText("../../../../Code/Config.json");
            Configuration = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

            for (int i = 1; i < 9; i++)
            {
                textures.Add(Content.Load<Texture2D>(i.ToString()));
            }

            InputMapper = new InputMapper();

            Player player = new Player(new Vector2(17.5f, 4.5f), new Vector2(-1, 0), null, 100, new List<Sprite>() { });

            Entity demon = new Monster(new Vector2(17.5f, 8.5f),
                                       new Vector2(-1, 0),
                                       new Inventory(),
                                       100,
                                       new List<Sprite>() { new Sprite(Content.Load<Texture2D>("demon")) });

            renderer = new Renderer(640, 480);

            screen = new Texture2D(graphics.GraphicsDevice, renderer.ScreenWidth, renderer.ScreenHeight);

            level = new Level(new Map(tiles, textures), null, new List<Entity>() { demon }, player);

            camera = new Camera(new Vector2(17.5f, 4.5f), new Vector2(-1, 0), new Vector2(0, 0.66f));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            song = Content.Load<SoundEffect>("ambient");
            MediaPlayer.Volume = float.Parse(Configuration["musicVolume"]);
            defaultFont = Content.Load<SpriteFont>("DefaultFont");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            GameContext.GameTime = gameTime;
            ProcessInput(gameTime);

            if (gameTime.TotalGameTime.TotalSeconds - seconds > 0.5)
            {

                level.Player.Update(level, GameContext);
                camera.Position = level.Player.Position;
                camera.Rotate(level.Player.Rotation);

                foreach (Entity entity in level.Entities)
                {
                    entity.Update(level, GameContext);
                }
             
                seconds = gameTime.TotalGameTime.TotalSeconds;
            
            }

            song.Play(MediaPlayer.Volume, 0.0f, 0.0f);

            base.Update(gameTime);
        }

        public void ProcessInput(GameTime gameTime)
        {

            Keys[] pressed = Keyboard.GetState().GetPressedKeys();
            InputState.Actions = InputMapper.Translate(pressed);

            if (InputState.HasAction("Escape"))
            {
                Exit();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);

            Color[] colors = renderer.Render(camera, level);

            screen.SetData<Color>(colors);

            spriteBatch.Draw(screen, destinationRectangle: new Rectangle(0, 0, ScreenWidth, ScreenHeight));

            spriteBatch.DrawString(defaultFont, string.Join(" , ", InputState.Actions), new Vector2(100, 100), Color.LimeGreen);
            spriteBatch.DrawString(defaultFont, $"PLAYER QUADRANT {level.Player.GetDirectionQuadrant()}", new Vector2(100, 450), Color.LimeGreen);
            spriteBatch.DrawString(defaultFont, $"CAMERA QUADRANT {camera.GetDirectionQuadrant()}", new Vector2(100, 500), Color.LimeGreen);

            //spriteBatch.DrawString(defaultFont, $"PLAYER X {level.Player.Position.X}", new Vector2(100, 450), Color.LimeGreen);
            //spriteBatch.DrawString(defaultFont, $"PLAYER Y {level.Player.Position.Y}", new Vector2(100, 500), Color.LimeGreen);
            //spriteBatch.DrawString(defaultFont, $"CAMERA X {camera.Position.X}", new Vector2(100, 550), Color.LimeGreen);
            //spriteBatch.DrawString(defaultFont, $"CAMERA Y {camera.Position.Y}", new Vector2(100, 600), Color.LimeGreen);


            base.Draw(gameTime);

            spriteBatch.End();
        }

    }

}
