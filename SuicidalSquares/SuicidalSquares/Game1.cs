using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SuicidalSquares
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState previousKeyboardState = Keyboard.GetState();
        MouseState previousMouseState = Mouse.GetState();
        Rectangle ViewportRect;
        SoundEffect laser;
        GameItem Tbarrier;
        GameItem Bbarrier;
        GameItem Lbarrier;
        GameItem Rbarrier;
        GameItem circle;
        GameItem Level0;
        GameItem Level1;
        GameItem Level2;
        GameItem Level3;
        GameItem GameOver;
        GameItem medkit;
        GameItem[] SuperSquares;
        GameItem[] squares;
        GameItem[] MegaSquares;
        GameItem[] Rounds;
        const int MAXROUND = 30;

        bool BarrierCollision = false;
        bool SquareCollision = false;
        bool SuperSquareCollision = false;
        bool MegaSquareCollision = false;

        bool MedKitPowerUp = false;
        bool MedKitPowerUp2 = true;

        const int MAXSQUARES = 20;
        const float MAXSQUAREHEIGHT = 0f;
        const float MINSQUAREHEIGHT = 1f;
        const float MAXSQUAREVELOCITY = 6.0f;
        const float MINSQUAREVELOCITY = 2.0f;
        Random random = new Random();

        const int MAXSUPERSQUARES = 15;
        const float MAXSUPERSQUAREHEIGHT = 1f;
        const float MINSUPERSQUAREHEIGHT = 0.01f;
        const float MAXSUPERSQUAREVELOCITY = 5f;
        const float MINSUPERSQUAREVELOCITY = 1f;

        const int MAXMEGASQUARES = 10;
        const float MAXMEGASQUAREHEIGHT = 1f;
        const float MINMEGASQUAREHEIGHT = 0.01f;
        const float MAXMEGASQUAREVELOCITY = 5f;
        const float MINMEGASQUAREVELOCITY = 1f;

        int health = 100;
        SpriteFont font2;
        Vector2 healthDrawPoint = new Vector2(
            0.1f,
            0.1f);
        
        int score;
        SpriteFont font;
        Vector2 scoreDrawPoint = new Vector2(
            0.1f,
            0.1f);
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ViewportRect = new Rectangle(0, 0,
                graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height);

                laser = Content.Load<SoundEffect>("SoundEffects\\laser");
            
                Tbarrier = new GameItem(Content.Load<Texture2D>("Sprite\\Tbarrier"));
                Tbarrier.position = new Vector2(-3, GraphicsDevice.Viewport.Height - 500);

                Bbarrier = new GameItem(Content.Load<Texture2D>("Sprite\\Bbarrier"));
                Bbarrier.position = new Vector2(-30, GraphicsDevice.Viewport.Height - 30);

                Lbarrier = new GameItem(Content.Load<Texture2D>("Sprite\\Lbarrier"));
                Lbarrier.position = new Vector2(-20, GraphicsDevice.Viewport.Height - 500);

                Rbarrier = new GameItem(Content.Load<Texture2D>("Sprite\\Rbarrier"));
                Rbarrier.position = new Vector2(770, GraphicsDevice.Viewport.Height - 512);

                circle = new GameItem(Content.Load<Texture2D>("Sprite\\circle"));
                circle.position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

                Level0 = new GameItem(Content.Load<Texture2D>("Sprite\\Level0"));
                Level0.position = new Vector2(590, GraphicsDevice.Viewport.Height - 417);

                Level1 = new GameItem(Content.Load<Texture2D>("Sprite\\Level1"));
                Level1.position = new Vector2(590, GraphicsDevice.Viewport.Height - 417);

                Level2 = new GameItem(Content.Load<Texture2D>("Sprite\\Level2"));
                Level2.position = new Vector2(590, GraphicsDevice.Viewport.Height - 417);

                Level3 = new GameItem(Content.Load<Texture2D>("Sprite\\Level3"));
                Level3.position = new Vector2(590, GraphicsDevice.Viewport.Height - 417);

                GameOver = new GameItem(Content.Load<Texture2D>("Sprite\\GameOverSprite"));
                GameOver.position = new Vector2(225, GraphicsDevice.Viewport.Height - 240);

                font = Content.Load<SpriteFont>("SuicidalSquaresFont\\SuicidalSquaresFont");

                font2 = Content.Load<SpriteFont>("SuicidalSquaresFont\\SuicidalSquaresFont");

                medkit = new GameItem(Content.Load<Texture2D>("Sprite\\MedKit"));
                medkit.position = new Vector2(350, GraphicsDevice.Viewport.Height - 325);

                Rounds = new GameItem[MAXROUND];
                for (int i = 0; i < MAXROUND; i++)
                {
                    Rounds[i] = new GameItem(Content.Load<Texture2D>("Sprite\\round"));
                }

                squares = new GameItem[MAXSQUARES];
                for (int s = 0; s < MAXSQUARES; s++)
                {
                    squares[s] = new GameItem(
                        Content.Load<Texture2D>("Sprite\\square"));
                }

                SuperSquares = new GameItem[MAXSUPERSQUARES];
                for (int g = 0; g < MAXSUPERSQUARES; g++)
                {
                    SuperSquares[g] = new GameItem(
                        Content.Load<Texture2D>("Sprite\\SuperSquare"));
                }

                MegaSquares = new GameItem[MAXMEGASQUARES];
                for (int m = 0; m < MAXMEGASQUARES; m++)
                {
                    MegaSquares[m] = new GameItem(
                        Content.Load<Texture2D>("Sprite\\MegaSquare"));
                }
            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();

            if (keyboardState.IsKeyDown(Keys.W))
                circle.position.Y -= 6.5f;

            if (keyboardState.IsKeyDown(Keys.S))
                circle.position.Y += 6.5f;

            if (keyboardState.IsKeyDown(Keys.A))
                circle.position.X -= 6.5f;

            if (keyboardState.IsKeyDown(Keys.D))
                circle.position.X += 6.5f;

            if (keyboardState.IsKeyDown(Keys.Right))
                circle.rotation += .125f;

            if (keyboardState.IsKeyDown(Keys.Left))
                circle.rotation -= .125f;

            if (keyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
            {
                FireRound();
                laser.Play();
            }
            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                FireRound();
                laser.Play();
            }

            BarrierCollision = false;
            SquareCollision = false;
            SuperSquareCollision = false;
            MegaSquareCollision = false;

            MedKitPowerUp = false;
            MedKitPowerUp2 = true;

            Rectangle circleRect =
                new Rectangle((int)circle.position.X, (int)circle.position.Y,
                circle.sprite.Width, circle.sprite.Height);

            Rectangle LbarrierRect =
                new Rectangle((int)Lbarrier.position.X, (int)Lbarrier.position.Y,
                    Lbarrier.sprite.Width, Lbarrier.sprite.Height);

            Rectangle RbarrierRect =
                new Rectangle((int)Rbarrier.position.X, (int)Rbarrier.position.Y,
                    Rbarrier.sprite.Width, Rbarrier.sprite.Height);

            Rectangle TbarrierRect =
                new Rectangle((int)Tbarrier.position.X, (int)Tbarrier.position.Y,
                    Tbarrier.sprite.Width, Tbarrier.sprite.Height);

            Rectangle BbarrierRect =
                new Rectangle((int)Bbarrier.position.X, (int)Bbarrier.position.Y,
                    Bbarrier.sprite.Width, Bbarrier.sprite.Height);

            if (score >= 1000)
            {
                Rectangle medkitRect =
                new Rectangle((int)medkit.position.X, (int)medkit.position.Y,
                    medkit.sprite.Width, medkit.sprite.Height);

                if (circleRect.Intersects(medkitRect))
                    MedKitPowerUp = true;
                    MedKitPowerUp2 = true;

            }
            
            foreach (GameItem square in squares)
            {
                Rectangle squareRect = new Rectangle((int)square.position.X, (int)square.position.Y,
                            square.sprite.Width, square.sprite.Height);

                if (circleRect.Intersects(squareRect))
                    SquareCollision = true;
            }

            if (score >= 500)
            foreach (GameItem SuperSquare in SuperSquares)
            {
                Rectangle supersquareRect = new Rectangle((int)SuperSquare.position.X, (int)SuperSquare.position.Y,
                               SuperSquare.sprite.Width, SuperSquare.sprite.Height);

                if (circleRect.Intersects(supersquareRect))
                    SuperSquareCollision = true;
            }
            
            if (score >= 1000)
                foreach (GameItem MegaSquare in MegaSquares)
                {
                    Rectangle megasquareRect = new Rectangle((int)MegaSquare.position.X, (int)MegaSquare.position.Y,
                        MegaSquare.sprite.Width, MegaSquare.sprite.Height);

                    if (circleRect.Intersects(megasquareRect))
                        MegaSquareCollision = true;
                }
            
            if (circleRect.Intersects(LbarrierRect))
                BarrierCollision = true;

            if (circleRect.Intersects(RbarrierRect))
                BarrierCollision = true;

            if (circleRect.Intersects(TbarrierRect))
                BarrierCollision = true;

            if (circleRect.Intersects(BbarrierRect))
                BarrierCollision = true;

            

            // TODO: Add your update logic here

            UpdateRounds();
            UpdateSquares();
            UpdateSuperSquares();
            UpdateMegaSquares();
            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;
            base.Update(gameTime);
        }
        
        public void FireRound()
        {
            foreach (GameItem round in Rounds)
            {
                if (!round.alive)
                {
                    round.alive = true;
                    round.position = circle.position - round.center;
                    round.velocity = new Vector2(
                        (float)Math.Cos(circle.rotation),
                        (float)Math.Sin(circle.rotation))
                        * 8.75f;
                    return;
                }
            }
        }

        public void UpdateRounds()
        {
            foreach (GameItem round in Rounds)
            {
                if (round.alive)
                {
                    round.position += round.velocity;
                    if (!ViewportRect.Contains(new Point(
                        (int)round.position.X,
                        (int)round.position.Y)))
                    {
                        round.alive = false;
                        continue;
                    }
                    Rectangle roundRect = new Rectangle(
                        (int)round.position.X,
                        (int)round.position.Y,
                        round.sprite.Width,
                        round.sprite.Height);
                    foreach (GameItem square in squares)
                    {
                        Rectangle squareRect = new Rectangle(
                            (int)square.position.X,
                            (int)square.position.Y,
                            square.sprite.Width,
                            square.sprite.Height);
                        if (roundRect.Intersects(squareRect))
                        {
                            round.alive = false;
                            square.alive = false;
                            score += 25;
                            break;
                        }
                    }

                    if (score >= 500)
                        foreach (GameItem SuperSquare in SuperSquares)
                        {
                            Rectangle supersquareRect = new Rectangle(
                                (int)SuperSquare.position.X,
                                (int)SuperSquare.position.Y,
                                SuperSquare.sprite.Width,
                                SuperSquare.sprite.Height);
                            if (roundRect.Intersects(supersquareRect))
                            {
                                round.alive = false;
                                SuperSquare.alive = false;
                                score += 50;
                                break;
                            }
                        }

                    if (score >= 1000)
                        foreach (GameItem MegaSquare in MegaSquares)
                        {
                            Rectangle megasquareRect = new Rectangle(
                                (int)MegaSquare.position.X,
                                (int)MegaSquare.position.Y,
                                MegaSquare.sprite.Width,
                                MegaSquare.sprite.Height);
                            if (roundRect.Intersects(megasquareRect))
                            {
                                round.alive = false;
                                MegaSquare.alive = false;
                                score += 100;
                                break;
                            }
                        }
                }
            }
        }
        
        public void UpdateSuperSquares()
        {
            foreach (GameItem SuperSquare in SuperSquares)
            {
                if (SuperSquare.alive)
                {
                    SuperSquare.position += SuperSquare.velocity;
                    if (!ViewportRect.Contains(new Point(
                        (int)SuperSquare.position.X,
                        (int)SuperSquare.position.Y)))
                    {
                        SuperSquare.alive = false;
                    }
                }
                else
                {
                    SuperSquare.alive = true;
                    SuperSquare.position = new Vector2(
                        ViewportRect.Right,
                        MathHelper.Lerp(
                        (float)ViewportRect.Height * MINSQUAREHEIGHT,
                        (float)ViewportRect.Width * MAXSQUAREHEIGHT,
                        (float)random.NextDouble()));
                    SuperSquare.velocity = new Vector2(
                        MathHelper.Lerp(
                        -MINSQUAREVELOCITY,
                        -MAXSQUAREVELOCITY,
                        (float)random.NextDouble()), 0);
                }
            }
        }

         public void UpdateSquares()
        {
            foreach (GameItem square in squares)
            {
                if (square.alive)
                {
                    square.position += square.velocity;
                    if (!ViewportRect.Contains(new Point(
                        (int)square.position.X,
                        (int)square.position.Y)))
                    {
                        square.alive = false;
                    }
                }
                else
                {
                    square.alive = true;
                    square.position = new Vector2(
                        ViewportRect.Right,
                        MathHelper.Lerp(
                        (float)ViewportRect.Height * MINSQUAREHEIGHT,
                        (float)ViewportRect.Width * MAXSQUAREHEIGHT,
                        (float)random.NextDouble()));
                    square.velocity = new Vector2(
                        MathHelper.Lerp(
                        -MINSQUAREVELOCITY,
                        -MAXSQUAREVELOCITY,
                        (float)random.NextDouble()), 0);
                }
            }
        }

         public void UpdateMegaSquares()
         {
             foreach (GameItem MegaSquare in MegaSquares)
             {
                 if (MegaSquare.alive)
                 {
                     MegaSquare.position += MegaSquare.velocity;
                     if (!ViewportRect.Contains(new Point(
                         (int)MegaSquare.position.X,
                         (int)MegaSquare.position.Y)))
                     {
                         MegaSquare.alive = false;
                     }
                 }
                 else
                 {
                     MegaSquare.alive = true;
                     MegaSquare.position = new Vector2(
                         ViewportRect.Right,
                         MathHelper.Lerp(
                         (float)ViewportRect.Height * MINSQUAREHEIGHT,
                         (float)ViewportRect.Width * MAXSQUAREHEIGHT,
                         (float)random.NextDouble()));
                     MegaSquare.velocity = new Vector2(
                         MathHelper.Lerp(
                         -MINSQUAREVELOCITY,
                         -MAXSQUAREVELOCITY,
                         (float)random.NextDouble()), 0);
                 }
             }
         }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
         {

            GraphicsDevice.Clear(Color.DarkBlue);
            
            if (SuperSquareCollision)
                 {
                     GraphicsDevice.Clear(Color.Green);
                     health -= 1;
                 }
               
                 
             if (BarrierCollision)
                 {
                     GraphicsDevice.Clear(Color.Gray);
                     health -= 1;
                 }
              
                 
              if (SquareCollision)
                 {
                     GraphicsDevice.Clear(Color.Red);
                     health -= 1;
                 }

              if (MegaSquareCollision)
                 {
                  GraphicsDevice.Clear(Color.Purple);
                  health -= 1;
                 }

              if (MedKitPowerUp)
              {
                  health = 100;
              }

              if (score >= 1000)
              {
                  MedKitPowerUp2 = false;
              }

            spriteBatch.Begin();

             spriteBatch.Draw(Tbarrier.sprite,
                 Tbarrier.position,
                 null,
                 Color.White);

             spriteBatch.Draw(Bbarrier.sprite,
                 Bbarrier.position,
                 null,
                 Color.White);

             spriteBatch.Draw(Lbarrier.sprite,
                 Lbarrier.position,
                 null,
                 Color.White);

             spriteBatch.Draw(Rbarrier.sprite,
                 Rbarrier.position,
                 null,
                 Color.White);

             spriteBatch.Draw(circle.sprite,
                circle.position,
                null,
                Color.White,
                circle.rotation,
                circle.center,
                1.0f,
                SpriteEffects.None,
                0);

            if (score <= -1)
             {
                 spriteBatch.Draw(Level0.sprite,
                     Level0.position,
                     null,
                     Color.White);
             }
            
            if (score >= 0 && score <= 499)
            {
                    spriteBatch.Draw(Level1.sprite,
                        Level1.position,
                        null,
                        Color.White);
            }

            if (score >= 500 && score <= 999)
            {
                    spriteBatch.Draw(Level2.sprite,
                        Level2.position,
                        null,
                        Color.White);
            }

            if (score >= 1000 && score <= 1999)
            {
                    spriteBatch.Draw(Level3.sprite,
                        Level3.position,
                        null,
                        Color.White);
            }

            if (score <= -500)
            {
                spriteBatch.Draw(GameOver.sprite,
                    GameOver.position,
                    null,
                    Color.White);
            }

            if (health <= 0)
            {
                spriteBatch.Draw(GameOver.sprite,
                    GameOver.position,
                    null,
                    Color.White);
            }

             foreach (GameItem round in Rounds)
             {
                 if (round.alive)
                 {
                     spriteBatch.Draw(round.sprite,
                         round.position,
                         null,
                         Color.White);
                 }
             }

             foreach (GameItem square in squares)
             {
                 if (square.alive)
                 {
                     spriteBatch.Draw(square.sprite,
                         square.position,
                         null,
                         Color.White);
                 }
             }

             if (score >= 500)
             {
                 foreach (GameItem SuperSquare in SuperSquares)
                 {
                     if (SuperSquare.alive)
                     {
                         spriteBatch.Draw(SuperSquare.sprite,
                             SuperSquare.position,
                             null,
                             Color.White);
                     }
                 }
             }

             if (score >= 1000)
             {
                 foreach (GameItem MegaSquare in MegaSquares)
                 {
                     if (MegaSquare.alive)
                     {
                         spriteBatch.Draw(MegaSquare.sprite,
                             MegaSquare.position,
                             null,
                             Color.White);
                     }
                 }
             }

             if (MedKitPowerUp2 == false)
             {
                 spriteBatch.Draw(medkit.sprite,
                     medkit.position,
                     null,
                     Color.White);
             }

             spriteBatch.DrawString(font, "Score: " + score.ToString(),
                 new Vector2(scoreDrawPoint.X * ViewportRect.Width,
                     scoreDrawPoint.Y * ViewportRect.Height),
                     Color.Blue);
             
            spriteBatch.DrawString(font, "Health: " + health.ToString(),
                  new Vector2(350, GraphicsDevice.Viewport.Height - 430),
                      Color.Blue);

             spriteBatch.End();

             // TODO: Add your drawing code here
             base.Draw(gameTime);
         }
    }
}
