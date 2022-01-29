using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FortBuildZApc
{
    public class Health
    {
        public Texture2D healthTexture;
        public Vector2 healthPosition;
       
        public Rectangle healthSourceRectangle;

        public int damageTaken = 0;
        public int healthFrames = 3;

        public bool active = true;

        public Health ( Texture2D ht, Vector2 hp)
        {
            healthTexture = ht;
            healthPosition = hp;
        }

        public void Update()
        {
            if (damageTaken == 2)
            {
                active = false;
            }
            
            healthSourceRectangle = new Rectangle((int)((healthTexture.Width / healthFrames)) * damageTaken, 0, healthTexture.Width / healthFrames, healthTexture.Height);
        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(healthTexture, healthPosition, healthSourceRectangle, Color.White);
        }


    }
}
