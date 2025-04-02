using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Topic_2___Lists_and_Loops
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
      

        Rectangle window;

        //load texture
        Texture2D bgTexture;
        //emoji list
        List<Texture2D> emojitextures;
        List<Vector2> emojiRect;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Random rand = new Random();

            //set resolution
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            //list texture
            emojitextures = new List<Texture2D>();
            emojiRect = new List<Vector2>();

            //bg texture
            bgTexture = Content.Load<Texture2D>("bg");

            //bg rect
            window = new Rectangle(0, 0, 1280, 720);

            //list for rect and counted loops
            //the textures
            for (int i = 1; i < 27; i++)
            {
                emojitextures.Add(Content.Load<Texture2D>($"emojipack/emoji({i})"));
            }

            //the rect
            for (int i = 1; i < 27; i++)
            {
                emojiRect.Add(new Vector2(rand.Next(0,50), rand.Next(0,50)));
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //draw bg
            _spriteBatch.Draw(bgTexture, window, Color.White);

            //draw emoji
            for (int i = 0; i < emojitextures.Count; i++)
            {
                _spriteBatch.Draw(emojitextures[i], emojiRect[i], Color.White);
            }


            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
