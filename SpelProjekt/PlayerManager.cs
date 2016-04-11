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
    class PlayerManager
    {

        GameTime gameTime = new GameTime();

        //Variables

        //PlayerRectangle
        Rectangle playerRect = new Rectangle(250, 600, 50, 75);

        //Speed
        private int xSpeed = 10;

        //Update
        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();

            //Movement
            if(keyboard.IsKeyDown(Keys.A))
            {
                playerRect.X -= xSpeed;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                playerRect.X += xSpeed;
            }


        }

        //Returns position vector
        public Vector2 GetPosition()
        {
            Vector2 position = new Vector2(playerRect.X,playerRect.Y);
            return position;
        }

        //Returns player size
        public Vector2 GetPlayerSize()
        {
            Vector2 size = new Vector2(playerRect.Width, playerRect.Height);
            return size;
        }

        public Rectangle GetRect()
        {
            return playerRect;
        }

    }
}
