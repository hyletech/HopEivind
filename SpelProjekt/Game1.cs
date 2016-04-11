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
using System.Xml.Serialization;
using System.IO;

namespace SpelProjekt
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Object
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlayerManager playerManager = new PlayerManager();
        WorldManager worldManager = new WorldManager();

        //Boost
        private int boost = 15;

        //Window
        private int screenWidth = 600;
        private int screenHeight = 800;

        //Player
        Vector2 playerPos = new Vector2(0,0);
        Vector2 playerSize = new Vector2(0,0);
        Rectangle playerRect = new Rectangle(0, 0, 0, 0);
        private bool jumpBool = false;                          //Allows the player to jump once

        //Textures
        Texture2D playerTexture;
        Texture2D platformTexture;
        Texture2D backgroundTexture;
        Texture2D grassTexture;

        //Fonts 
        SpriteFont Verdana35;

        //Score
        Vector2 scoreTPos = new Vector2(170, 0);
        Vector2 scorePos = new Vector2(340, 0);

        //Lose-Screen
        private bool lose;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            base.Initialize();

            //Window
            this.graphics.PreferredBackBufferWidth = screenWidth;
            this.graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            worldManager.Initialize(screenWidth, screenHeight, platformTexture, backgroundTexture, grassTexture);

        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("Images/Eivind");
            platformTexture = Content.Load<Texture2D>("Images/PizzaBox");
            grassTexture = Content.Load<Texture2D>("Images/Dirt");
            backgroundTexture = Content.Load<Texture2D>("Images/BackGround");

            Verdana35 = Content.Load<SpriteFont>("Fonts/Verdana35");
        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {

            KeyboardState keyboard = Keyboard.GetState();

            //Back or Escape Exits the game.
            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            if (!lose)
            {

                //Updates Playermanager
                playerManager.Update();
                playerPos = playerManager.GetPosition();
                playerSize = playerManager.GetPlayerSize();

                //Updates WorldManager
                if (jumpBool)
                {
                    worldManager.Update();
                }

                //PlayerRect
                playerRect = playerManager.GetRect();

                //Firstjump
                if (keyboard.IsKeyDown(Keys.Space) && !jumpBool)
                {
                    worldManager.ChangeSpeed(boost);
                    jumpBool = true;
                }


                //Collisions
                List<Platform> platList = worldManager.GetPlatforms();
                foreach (Platform plat in platList)
                {
                    if (plat.GetRect().Intersects(playerRect) && worldManager.GetWorldSpeed() < 0)
                    {
                        worldManager.ChangeSpeed(boost);
                    }
                }

                if (jumpBool)
                {
                    if (worldManager.GetGrass().Intersects(playerRect))
                    {
                        lose = true;
                    }
                }

                base.Update(gameTime);
            }

            //Start again
            if(keyboard.IsKeyDown(Keys.Enter))
            {
                jumpBool = false;
                worldManager.ResetWorld();
                lose = false;
            }

        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            worldManager.Draw(spriteBatch);
            spriteBatch.Draw(playerTexture, playerRect,Color.White);
            spriteBatch.DrawString(Verdana35, "Score: ", scoreTPos, Color.Gold);
            spriteBatch.DrawString(Verdana35, worldManager.GetScore().ToString(), scorePos, Color.Gold);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
