using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace Warehouse_RTS
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TiledMap tileMap;
        private TiledMapRenderer mapRenderer;
        private OrthographicCamera orthoCamera;
        private Vector2 cameraPosition;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1280, 728);
            orthoCamera = new OrthographicCamera(viewportadapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
      
            
            tileMap = Content.Load<TiledMap>("TileMap");
            mapRenderer = new TiledMapRenderer(GraphicsDevice, tileMap);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MoveCamera(gameTime);
            orthoCamera.LookAt(cameraPosition);

            base.Update(gameTime);
        }

        private Vector2 GetMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                movementDirection += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                movementDirection += Vector2.UnitX;
            }

            // Can't normalize the zero vector so test for it before normalizing
            if (movementDirection != Vector2.Zero)
            {
                movementDirection.Normalize();
            }

            return movementDirection;
        }

        private void MoveCamera(GameTime gameTime)
        {
            var speed = 500;
            var seconds = gameTime.GetElapsedSeconds();
            var movementDirection = GetMovementDirection();
            cameraPosition += speed * movementDirection * seconds;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            mapRenderer.Draw(orthoCamera.GetViewMatrix());

            //_spriteBatch.Begin();
            //_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}