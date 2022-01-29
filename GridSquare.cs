using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FortBuildZApc
{
    public class GridSquare
    {
        public Rectangle gridRectangle;
        public Vector2 gridPosition;
        public Texture2D gridTexture; 

        public GridSquare(Vector2 p, Texture2D t)
        {
            gridPosition = p;
            gridTexture = t;
            gridRectangle = new Rectangle((int)p.X, (int)p.Y, t.Width, t.Height);
        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(gridTexture, gridPosition, Color.White);
        }
    }
}