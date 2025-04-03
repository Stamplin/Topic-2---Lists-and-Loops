using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
        List<Rectangle> emojiRect;

        //random
        Random random = new Random();

        int x, y;

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

            //set resolution
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            //list texture
            emojitextures = new List<Texture2D>();
            emojiRect = new List<Rectangle>();

            //bg rect
            window = new Rectangle(0, 0, 1280, 720);

            //the textures
            for (int i = 1; i < 27; i++)
            {
                emojitextures.Add(Content.Load<Texture2D>($"emojipack/emoji({i})"));
            }

            for (int i = 0; i < 26; i++)
            {
                emojiRect.Add(new Rectangle(random.Next(0, 1280 - 100), random.Next(0, 720 - 100), 100, 100));
            }


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //bg texture
            bgTexture = Content.Load<Texture2D>("bg");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //mouse cursor position
            MouseState mouseState = Mouse.GetState();
            x = mouseState.X;
            y = mouseState.Y;

            //if "r" is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                //and left is pressed
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    for (int i = 0; i < emojiRect.Count; i++)
                    {
                        if (emojiRect[i].Contains(x, y))
                        {
                            emojiRect[i] = new Rectangle(random.Next(0, 1280 - 100), random.Next(0, 720 - 100), 100, 100);
                        }
                    }
                }
               
            }

            //if q is pressed remove emoji
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                //and if left mouse is held
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    for (int i = emojiRect.Count - 1; i >= 0; i--)
                    {
                        if (emojiRect[i].Contains(x, y))
                        {
                            emojiRect.RemoveAt(i);
                        }
                    }
                }
                
            }

            //if e is pressed spawn in a emoji in the mouse position
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                //and left is is pressed spawn in a emoji in the mouse position
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    emojiRect.Add(new Rectangle(x, y, 100, 100));
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //draw bg
            _spriteBatch.Draw(bgTexture, window, Color.White);

            //draw 
            int count = Math.Min(emojitextures.Count, emojiRect.Count);
            for (int i = 0; i < count; i++)
            {
                _spriteBatch.Draw(emojitextures[i], emojiRect[i], Color.White);
                //if space is pressed reset pos
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    emojiRect[i] = new Rectangle(random.Next(0, 1280 - 100), random.Next(0, 720 - 100), 100, 100);
                }
            }


            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
