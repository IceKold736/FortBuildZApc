using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FortBuildZApc
{
    public class Drop
    {
        public Texture2D dropTexture;
        public Vector2 dropPosition;
        public Rectangle dropCollisionRectangle;

        public Rectangle handsCollisionRectangle;
        public Rectangle handsCollisionRectangle2;

        public bool active = true;

        public int type;


        public Drop(Texture2D rdt, Vector2 rdp, int t)
        {
            dropTexture = rdt;
            dropPosition = rdp;
            type = t; 
        }

        public void Update()
        {
            dropCollisionRectangle = new Rectangle((int)dropPosition.X, (int)dropPosition.Y, dropTexture.Width, dropTexture.Height);

            if (handsCollisionRectangle.Intersects(dropCollisionRectangle) || handsCollisionRectangle2.Intersects(dropCollisionRectangle))
            {
                active = false;
            }

            /*
             * wood = 1
             * apples = 2
             * rock = 3
             * metal ore = 4
             * sand = 5
             * cloth = 6
             * food = 7  
             * energyDrink = 8
             * metal = 9
             * glass = 10
             * extendedMag = 11
             * shells = 12
             * scope = 13
             * seed = 14
             * 
             * */




        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(dropTexture, dropPosition, Color.White);
        }

    }
}
