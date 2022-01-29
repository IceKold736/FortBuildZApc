using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FortBuildZApc
{
    public class Hands 
    {
        public Vector2 handsPosition;
        public Vector2 handsTemp;
        public Texture2D handsTexture;
        public float handsRot;

        public float realCharacterRot = 0f;

        public bool active = true;

        public Character playerGuy;
        public GamePadState gpsH;

        public Rectangle handsRectangle; 

        public Hands(Character c, Texture2D ht)
        {
            playerGuy = c;
            handsTexture = ht;
        }

        public void Update()
        {
            if (active == true)
            {
                handsRectangle = new Rectangle((int)handsPosition.X - handsTexture.Width / 2, (int)handsPosition.Y - handsTexture.Width / 2, handsTexture.Width, handsTexture.Width); //WIDTH is used 2 times purposly to make it a square.

                handsPosition = playerGuy.characterPosition;
                handsTemp = new Vector2((float)Math.Cos(playerGuy.orientation), (float)Math.Sin(playerGuy.orientation)) * 4;

                handsPosition += handsTemp * 5; //this number (5) just happens to be the perfect placement. It is (should be) the original speed.
                handsRot = playerGuy.orientation;
            }

            else
                handsRectangle = new Rectangle(0, 0, 0, 0);
        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(handsTexture, handsPosition, null, Color.White, handsRot, new Vector2(handsTexture.Width / 2, handsTexture.Height / 2), 1,SpriteEffects.None, 0f);
        }

    }
}
