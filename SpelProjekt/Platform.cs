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
    class Platform
    {
        private Rectangle platformRect = new Rectangle(0, 0, 0, 0);
        public Texture2D platformTexture;

        public Platform(int _width, int _height, int _xPos, int _yPos)
        {
            platformRect.Width = _width;
            platformRect.Height = _height;
            platformRect.X = _xPos;
            platformRect.Y = _yPos;
        }

        //GET position
        public Vector2 GetPosition ()
        {
            Vector2 position = new Vector2(platformRect.X, platformRect.Y);
            return position;
        }

        //GET size
        public Vector2 GetSize()
        {
            Vector2 size = new Vector2(platformRect.Width, platformRect.Height);
            return size;
        }

        //SET
        public void SetPosition(Vector2 _newPos)
        {
            platformRect.X = (int)_newPos.X;
            platformRect.Y = (int)_newPos.Y;
        }

        //GET rect
        public Rectangle GetRect()
        {
            return platformRect;
        }

        public virtual void Draw(SpriteBatch _spritebatch)
        {
            _spritebatch.Draw(platformTexture, platformRect, Color.White);
        }
    }
}
