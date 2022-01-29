using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FortBuildZApc
{
    public class Blood
    {
        public Vector2 bloodPosition;
        public Texture2D bloodTexture;
        public float bloodRotation;

        public Vector2 zombiePosition;
        public float zombieRotation;

        public Vector2 bloodTemp;

        public bool active = true;
        public bool onGround = false;

        double fadingDelay = 0.35;
        int fadingAlphaValue = 255;
        int fadingFadeIncrement = 3;

        public Blood(Texture2D bt, Vector2 zp, float zr)
        {
            bloodTexture = bt;
            zombiePosition = zp;
            zombieRotation = zr; 
        }

        public void Update(GameTime gameTime)
        {
            if (active == true)
            {
                if (fadingAlphaValue <= 0)
                {
                    active = false;
                }
            }

            fadingDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (fadingDelay <= 0)
            {
                fadingDelay = .035;

                if (fadingAlphaValue >= 0)
                    fadingAlphaValue -= fadingFadeIncrement;
            }

            if (onGround == false)
            {
                bloodPosition = zombiePosition;
                bloodTemp = new Vector2((float)Math.Cos(zombieRotation), (float)Math.Sin(zombieRotation)) * 4;

                bloodPosition += bloodTemp * -5; //this number (5) just happens to be the perfect placement. It is (should be) the original speed.
                bloodRotation = zombieRotation;
                onGround = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bloodTexture, bloodPosition, null, new Color(255, 255, 255, (byte)fadingAlphaValue), 
                bloodRotation, new Vector2(bloodTexture.Width / 2, bloodTexture.Height / 2), 1, SpriteEffects.None, 0f);
        }
    }
}