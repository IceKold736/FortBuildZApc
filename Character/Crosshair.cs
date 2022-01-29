using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FortBuildZApc
{
    public class Crosshair
    {
        public Vector2 crosshairsPosition;
        public Vector2 crosshairsTemp;
        public Texture2D crosshairsTexture;
        public float crosshairsRot;

        public float realCharacterRot = 0f;

        public bool active = true;
        public bool available = true;
        
        public Character playerGuy;
        public GamePadState gpsH;

        public Rectangle crosshairsRectangle;

        public Crosshair(Character c, Texture2D ht)
        {
            playerGuy = c;
            crosshairsTexture = ht;
        }

        public void Update()
        {
            if (active == true)
            {
                crosshairsRectangle = new Rectangle((int)crosshairsPosition.X - crosshairsTexture.Width / 2, (int)crosshairsPosition.Y - crosshairsTexture.Width / 2, crosshairsTexture.Width, crosshairsTexture.Width); //WIDTH is used 2 times purposly to make it a square.

                crosshairsPosition = playerGuy.characterPosition;
                crosshairsTemp = new Vector2((float)Math.Cos(playerGuy.orientation), (float)Math.Sin(playerGuy.orientation)) * 10;

                crosshairsPosition += crosshairsTemp * 5; //this number (5) just happens to be the perfect placement. It is (should be) the original speed.
                crosshairsRot = playerGuy.orientation;
            }
            else
                crosshairsRectangle = new Rectangle(0, 0, 0, 0);
        }

        public void Draw(SpriteBatch sprites)
        {
            if (available == true)
            {
                sprites.Draw(crosshairsTexture, crosshairsPosition, null, Color.Black, crosshairsRot, new Vector2(crosshairsTexture.Width / 2, crosshairsTexture.Height / 2), 1, SpriteEffects.None, 0f);
            }

            else
            {
                sprites.Draw(crosshairsTexture, crosshairsPosition, null, Color.OrangeRed, crosshairsRot, new Vector2(crosshairsTexture.Width / 2, crosshairsTexture.Height / 2), 1, SpriteEffects.None, 0f);
            }
            
        }

    }
}