using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Topic_2___Lists_and_Loops
{
    enum GameState
    {
        Menu,
        Game
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        //load texture
        Texture2D bgTexture, menuTexture;
        //emoji list
        List<Texture2D> emojitextures;
        List<Rectangle> emojiRect;

        MouseState mouseState, previousMouseState;
        KeyboardState keyboardState, previousKeyboardState;

        bool newClick;

        //load enum
        GameState gameState = GameState.Menu;
        GameState previousGameState = GameState.Menu;

        //random
        Random random = new Random();

        

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
            window = new Rectangle(0, 0, 1280, 720);

            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            //list texture
            emojitextures = new List<Texture2D>();
            emojiRect = new List<Rectangle>();

            //the textures
            for (int i = 1; i < 27; i++)
            {
                emojitextures.Add(Content.Load<Texture2D>($"emojipack/emoji({i})"));
            }

            for (int i = 0; i < 26; i++)
            {
                emojiRect.Add(new Rectangle(random.Next(0, window.Width - 100), random.Next(0, window.Height - 100), 100, 100));
            }


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //bg texture
            bgTexture = Content.Load<Texture2D>("bg");
            menuTexture = Content.Load<Texture2D>("menu");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //mouse cursor position
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            
            newClick = mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;


            if (gameState == GameState.Menu)
            {
                //if enter is pressed go to game
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    gameState = GameState.Game;
                }
            }
            if (gameState == GameState.Game)
            {
                //if esc is pressed go to menu
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    gameState = GameState.Menu;
                }

                //if "r" is pressed - Randomly moves emoji that already exists
                if (keyboardState.IsKeyDown(Keys.R))
                {
                    //and left is pressed
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        for (int i = 0; i < emojiRect.Count; i++)
                        {
                            if (emojiRect[i].Contains(mouseState.Position))
                            {
                                emojiRect[i] = new Rectangle(random.Next(window.X, window.Width - 100), random.Next(window.Y, window.Height - 100), 100, 100);
                            }
                        }
                    }

                }

                //if q is pressed remove emoji
                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    //and if left mouse is held
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        for (int i = 0; i < emojiRect.Count; i++)
                        {
                            if (emojiRect[i].Contains(mouseState.Position))
                            {
                                emojiRect.RemoveAt(i);
                                emojitextures.RemoveAt(i);
                                i--;
                            }
                        }
                    }

                }

                //if e is pressed spawn in a emoji in the mouse position
                if (keyboardState.IsKeyDown(Keys.E))
                    //if mouse is PRESSED
                    if (mouseState.LeftButton == ButtonState.Pressed && newClick)
                        if (emojitextures.Count < 26)
                        {
                            emojiRect.Add(new Rectangle(mouseState.X - 50, mouseState.Y - 50, 100, 100));
                            emojitextures.Add(Content.Load<Texture2D>($"emojipack/emoji({emojiRect.Count})"));
                        }

                //make space is pressed not held
                if (keyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                {
                    for (int i = 0; i < emojiRect.Count; i++)
                    {
                        emojiRect[i] = new Rectangle(random.Next(window.X, window.Width - 100), random.Next(window.Y, window.Height - 100), 100, 100);
                    }
                }

                // Update previous keyboard state
                previousKeyboardState = keyboardState;


            }
            
 
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //enum gamestate
            if (gameState == GameState.Menu)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(menuTexture, window, Color.White);
                _spriteBatch.End();
            }

            if (gameState == GameState.Game)
            {
                // TODO: Add your drawing code here
                _spriteBatch.Begin();
                //draw bg
                _spriteBatch.Draw(bgTexture, window, Color.White);

                //draw 
                int count = Math.Min(emojitextures.Count, emojiRect.Count);
                for (int i = 0; i < count; i++)
                {
                    _spriteBatch.Draw(emojitextures[i], emojiRect[i], Color.White);
                }


                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
