using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using DungeonsAndDungeons.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using Color = Microsoft.Xna.Framework.Color;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DungeonsGame : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Renderer renderer;
        private GUIRenderer GuiRenderer { get; set; }
        private Texture2D screen;
        private Texture2D gui;
        private readonly List<Texture2D> textures;
        private readonly int[,] tiles = new int[,] {
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
        private Level level;
        private TurnProcessor TurnProcessor { get; set; }
        Dictionary<string, string> KeyBinding { get; set; }
        private List<Command> Commands { get; set; }
        private SoundEffect song;
        private InputMapper InputMapper;
        private GameContext GameContext { get; set; }
        private SpriteFont defaultFont;
        private JObject Configuration { get; set; }
        private Camera camera;
        const string ConfigDirectory = "../../../../Config"; //todo find way of autoamically determining this

        public DungeonsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            textures = new List<Texture2D>();
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            GameContext = new GameContext(new GameTime());
            Commands = new List<Command>();
            TurnProcessor = new TurnProcessor();
        }

        protected override void Initialize()
        {
            string text = File.ReadAllText($"{ConfigDirectory}/Config.json");
            Configuration = JObject.Parse(text);

            string bindingsText = File.ReadAllText($"{ConfigDirectory}/Keybindings.json");
            KeyBinding = JsonConvert.DeserializeObject<Dictionary<string, string>>(bindingsText);

            for (int i = 1; i < 9; i++)
            {
                textures.Add(Content.Load<Texture2D>(i.ToString()));
            }

            InputMapper = new InputMapper(KeyBinding);

            Item knife = new Item(new Sprite(Content.Load<Texture2D>("knife")), new Vector2(17.5f, 6.5f));

            Player player = new Player(new Vector2(17.5f, 4.5f), new Vector2(0, 1), new Inventory() { knife,knife }, 100, new List<Sprite>() { });

            Entity demon = new Monster(new Vector2(17.5f, 8.5f),
                                       new Vector2(0, 1),
                                       new Inventory(),
                                       100,
                                       new List<Sprite>() { new Sprite(Content.Load<Texture2D>("demon")) });

            renderer = new Renderer(640, 480);

            screen = new Texture2D(graphics.GraphicsDevice, renderer.ScreenWidth, renderer.ScreenHeight);
            gui = new Texture2D(graphics.GraphicsDevice, ScreenWidth, ScreenHeight);

            level = new Level(new TexturedMap(tiles, textures), new List<Item>() { knife }, new List<Entity>() { demon }, player);

            camera = new Camera(new Vector2(17.5f, 4.5f), new Vector2(-1, 0), new Vector2(0, 0.66f));
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            song = Content.Load<SoundEffect>(Configuration.Value<string>("ambientsong"));
            MediaPlayer.Volume = float.Parse(Configuration.Value<string>("musicVolume"));
            song.Play(MediaPlayer.Volume, 0.0f, 0.0f);
            defaultFont = Content.Load<SpriteFont>("DefaultFont");

            GuiRenderer = new GUIRenderer(graphics, Window, defaultFont);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            GameContext.GameTime = gameTime;
            ProcessInput();

            TurnProcessor.RunCurrentTurn(level, GameContext);

            camera.Position = level.Player.Position;
            camera.SetDirection(level.Player.Direction);

            base.Update(gameTime);
        }

        /// <summary>
        /// Reads keyboard input from current frame and translates it to action descriptions
        /// </summary>
        public void ProcessInput()
        {

            Keys[] pressed = Keyboard.GetState().GetPressedKeys();
            InputState.Actions = InputMapper.Translate(pressed);

            if (InputState.HasAction("QUIT"))
            {
                Exit();
            }

            //TODO Add timeout period
            if (InputState.HasAction("TOGGLE_OVERLAY"))
            {
                Configuration["enableOverlay"] = !Configuration.Value<bool>("enableOverlay");
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);

            Color[] colors = renderer.Render(camera, level);
            screen.SetData<Color>(colors);
            spriteBatch.Draw(screen, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);

            if (Configuration.Value<bool>("enableOverlay"))
            {
                GuiRenderer.Render(spriteBatch, level);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

    }

}
