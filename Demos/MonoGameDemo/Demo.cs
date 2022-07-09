using System.Linq;
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
        private bool spacePressed;
        private SpriteBatch spriteBatch;

        public Demo()
        {
            Instance = this;
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public static SpriteFont Font { get; private set; }

        protected override void Initialize()
        {
            base.Initialize(); // this creates the graphics device
        }

        protected override void LoadContent()
        {
            Demo.Font = Content.Load<SpriteFont>("Font");
            this.slideDeck = new SlideDeck();
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            this.spacePressed = keyboard.GetPressedKeys().Contains(Keys.Space);

            if (!this.hasStarted)
            {
                this.slideDeck.Begin();
                this.hasStarted = true;
            }
            else
            {
                this.slideDeck.UpdateCurrentSlide(1 / 60f);
            }
        }

        public int Height => GraphicsDevice.Viewport.Bounds.Height;
        public int Width => GraphicsDevice.Viewport.Bounds.Width;

        public static Demo Instance { get; private set; }
        
        protected override void Draw(GameTime gameTime)
        {
            this.graphics.GraphicsDevice.Clear(Color.LightBlue);

            this.spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null,
                Matrix.CreateTranslation(
                    new Vector3(
                        Demo.Instance.Width / 2f,
                        Demo.Instance.Height / 2f,
                        0
                    )));
            this.slideDeck.DrawCurrentSlide(this.spriteBatch);
            this.spriteBatch.End();
        }
    }
}
