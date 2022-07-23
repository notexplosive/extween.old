using System.Linq;
using ExTween.Art.MonoGame;
using ExTween.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameDemo
{
    public class Demo : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private bool hasStarted;
        private SlideDeck slideDeck;
        private bool spacePressedLastFrame;
        private SpriteBatch spriteBatch;

        public Demo()
        {
            Instance = this;
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public static SpriteFont TitleFont { get; private set; }
        public static SpriteFont SubtitleFont { get; private set; }

        protected override void Initialize()
        {
            base.Initialize(); // this creates the graphics device
            
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;
            this.graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            Demo.TitleFont = Content.Load<SpriteFont>("Font");
            Demo.SubtitleFont = Content.Load<SpriteFont>("SubtitleFont");
            this.slideDeck = new SlideDeck();
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            var spacePressedThisFrame = keyboard.GetPressedKeys().Contains(Keys.Space);

            if (!this.spacePressedLastFrame && spacePressedThisFrame)
            {
                if (this.slideDeck.IsIdle() || keyboard.GetPressedKeys().Contains(Keys.LeftShift))
                {
                    this.slideDeck.NextSlide();
                }
            }
            
            this.spacePressedLastFrame = spacePressedThisFrame;

            if (!this.hasStarted)
            {
                this.slideDeck.Prepare();
                this.hasStarted = true;
            }
            else
            {
                this.slideDeck.Update(1 / 60f);
            }
        }

        public int Height => GraphicsDevice.Viewport.Bounds.Height;
        public int Width => GraphicsDevice.Viewport.Bounds.Width;

        public static Demo Instance { get; private set; }
        public static bool DebugMode { get; private set; }

        protected override void Draw(GameTime gameTime)
        {
            this.graphics.GraphicsDevice.Clear(Color.LightBlue);

            this.spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointWrap, null, null, null,
                Matrix.CreateTranslation(
                    new Vector3(
                        Demo.Instance.Width / 2f,
                        Demo.Instance.Height / 2f,
                        0
                    )));
            this.slideDeck.DrawPreservedSlides(new SpriteBatchPainter(this.spriteBatch));
            this.spriteBatch.End();
        }
    }
}
