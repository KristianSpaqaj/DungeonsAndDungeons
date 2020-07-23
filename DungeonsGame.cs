using DungeonsAndDungeons.Code;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
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
                { 4,0,6,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,8},
                { 4,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,1},
                { 4,0,8,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,8},
                { 4,0,0,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,7,7,7,1},
                { 4,0,0,0,0,0,0,5,5,5,5,0,5,5,5,5,7,7,7,7,7,7,7,1},
                { 6,6,6,6,6,6,6,6,6,6,6,0,6,6,6,6,6,6,6,6,6,6,6,6},
                { 8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
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

        private SpriteFont defaultFont;

        Dictionary<string, string> Configuration { get; set; }

        string inputString = "";

        Camera camera;

        public DungeonsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            textures = new List<Texture2D>();
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            seconds = 0;
        }

        protected override void Initialize()
        {
            var text = File.ReadAllText("../../../../Code/Config.json");
            Configuration = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

            for (int i = 1; i < 9; i++)
            {
                textures.Add(Content.Load<Texture2D>(i.ToString()));
            }

            sprites = new List<Sprite>() { new Sprite(Content.Load<Texture2D>("demon"), 17.5f, 8.5f) };
            
            renderer = new Renderer(640, 480);

            screen = new Texture2D(graphics.GraphicsDevice, renderer.ScreenWidth, renderer.ScreenHeight);
            
            level = new Level(new Map(tiles,textures), null, null);
            
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

            if(gameTime.TotalGameTime.TotalSeconds - seconds > 0.5)
            {
                ProcessInput(gameTime);
                seconds = gameTime.TotalGameTime.TotalSeconds;
            }

            song.Play(MediaPlayer.Volume, 0.0f, 0.0f);

            base.Update(gameTime);
        }

        public void ProcessInput(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            inputString = string.Join(" , ", Keyboard.GetState().GetPressedKeys());

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                camera.Move(gameTime, true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                camera.Move(gameTime, false);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                camera.Rotate(90);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                camera.Rotate(-90);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);

            Color[] colors = renderer.Render(camera, level, sprites);

            screen.SetData<Color>(colors);

            spriteBatch.Draw(screen, destinationRectangle: new Rectangle(0, 0, ScreenWidth, ScreenHeight));

            spriteBatch.DrawString(defaultFont, inputString, new Vector2(100, 100), Color.HotPink);

            base.Draw(gameTime);

            spriteBatch.End();
        }

    }

}
