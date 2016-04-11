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

namespace SpelProjekt
{
    class WorldManager
    {
        private List<Platform> platforms = new List<Platform>();

        //Size of platform
        private Vector2 platformSize = new Vector2(100,20);

        //texture for platform
        private Texture2D platformTexture;

        //travellingspeed
        private float worldSpeed = 0;
        private float speedDecrease = 0.3f;
        private float score = 0;
        private float height = 0;

        //Screen
        private int screenWidth = 600;
        private int screenHeight = 800;

        //Background
        Texture2D backgroundTexture;
        private Rectangle backgroundRect = new Rectangle(0, -1200, 600, 2000);
        private float backgroundPos = -1200;

        //Top coord
        private int topBoundary = 0;
        private int maxTopBoundary = -1500;

        //Bound rectangle
        private Rectangle bottomBoundary = new Rectangle(-500, 900, 2500, 1000);

        //platforms
        private int platFormCount = 10;
        private int platformYDistance = 100;

        //Grass
        private Rectangle grassRect = new Rectangle(0,700,600,600);
        private Texture2D grassTexture;


        //Initialize
        public void Initialize(int _screenWidth, int _screenHeight, Texture2D _platformTexture, Texture2D _backgroundTexture, Texture2D _grassTexture)
        {
            screenWidth = _screenWidth;
            screenHeight = _screenHeight;

            backgroundTexture = _backgroundTexture;
            platformTexture = _platformTexture;
            grassTexture = _grassTexture;

            InitPlatforms(platformTexture);
        }

        //GET platforms
        public List<Platform> GetPlatforms()
        {
            return platforms;
        }

        //GET worldSpeed
        public float GetWorldSpeed()
        {
            return worldSpeed;
        }

        //GET score
        public int GetScore()
        {
            return (int)score;
        }

        //GET grass
        public Rectangle GetGrass()
        {
            return grassRect;
        }

        //SET speed
        public void ChangeSpeed(int _speed)
        {
            worldSpeed = _speed;
        }

        public void InitPlatforms(Texture2D _platformTexture)
        {
            platforms.Clear();
            //Init of platforms
            for (int i = 0; i < platFormCount; i++)
            {
                Random rand = new Random();
                int _xPos = rand.Next(0, screenWidth - (int)platformSize.X);
                int _yPos = i * platformYDistance;

                platforms.Add(new Platform((int)platformSize.X, (int)platformSize.Y, _xPos, _yPos));
                platforms[i].platformTexture = _platformTexture;
            }
        }

        //Reset world
        public void ResetWorld()
        {
            score = 0;
            height = 0;
            worldSpeed = 0;
            topBoundary = 0;
            backgroundPos = -1200;
            grassRect.Y = 700;
        }

        public void Update()
        {
            //gravity
            worldSpeed -= speedDecrease;

            //Score
            if(worldSpeed > 0)
            {
                score += worldSpeed * 0.1f;
            }
            
            //Background
            height += worldSpeed;
            if (backgroundPos < 0)
            {
                backgroundPos = height * 0.02f - 1200;
                backgroundRect.Y = (int)backgroundPos;
            }

            //Difficulty
            if (topBoundary > maxTopBoundary)
            {
                float tempFloat = -height * 0.02f;
                topBoundary = (int)tempFloat;
            }

            //move platforms
            foreach (Platform plat in platforms)
            {
                Vector2 newPos = plat.GetPosition();
                newPos.Y += worldSpeed;
                plat.SetPosition(newPos);

                //Collision
                if (plat.GetRect().Intersects(bottomBoundary))
                {
                    Random rand = new Random();
                    int xPos = rand.Next(0, screenWidth - (int)platformSize.X);
                    plat.SetPosition(new Vector2(xPos, topBoundary));
                }
            }

            grassRect.Y += (int)worldSpeed;

        }

        //Draw
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);

            foreach (Platform plat in platforms)
            {
                plat.Draw(_spriteBatch);
            }

            _spriteBatch.Draw(grassTexture, grassRect, Color.White);
        }
    }
}
