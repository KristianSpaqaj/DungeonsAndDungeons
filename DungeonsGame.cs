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
using System.Linq;
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

        private const int ScreenWidth = 1920;
        private const int ScreenHeight = 1080;
        private Level level;
        private TurnProcessor TurnProcessor { get; set; }
        Dictionary<string, string> KeyBinding { get; set; }
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
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            GameContext = new GameContext(new GameTime());
            TurnProcessor = new TurnProcessor();
        }

        protected override void Initialize()
        {
            string text = File.ReadAllText($"{ConfigDirectory}/Config.json");
            Configuration = JObject.Parse(text);

            string bindingsText = File.ReadAllText($"{ConfigDirectory}/Keybindings.json");
            KeyBinding = JsonConvert.DeserializeObject<Dictionary<string, string>>(bindingsText);

            InputMapper = new InputMapper(KeyBinding);

            renderer = new Renderer(640, 480);

            screen = new Texture2D(graphics.GraphicsDevice, renderer.ScreenWidth, renderer.ScreenHeight);

            LevelGenerator levelGenerator = new LevelGenerator(Content);

            level = levelGenerator.Generate($"{SeedGenerator.Generate(4)}020300");

            camera = new Camera(level.Player.Position, level.Player.Direction, Configuration.Value<float>("fov"));

            if (Configuration.Value<bool>("isFullScreen")) //find way of moving this into constructor
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }

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

            List<string> pressed = Keyboard.GetState().GetPressedKeys().Select(k => k.ToString()).ToList();
            MouseInfo.Update(Mouse.GetState());
            pressed.AddRange(MouseInfo.GetPressed());
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

            if (InputState.HasAction("TOGGLE_FULLSCREEN"))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);

            Color[] colors = renderer.Render(camera, level);
            screen.SetData<Color>(colors);
            spriteBatch.Draw(screen, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
            spriteBatch.End();

            if (Configuration.Value<bool>("enableOverlay"))
            {
                GuiRenderer.Render(spriteBatch, level);
            }


            base.Draw(gameTime);

        }

    }

}
