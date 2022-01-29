using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FortBuildZApc
{
    public class Shadow
    {
        public Texture2D shadowTexture;
        public Vector2 shadowPosition;
        public float characterShadowRotation;
        public float zombieShadowRotation;



        public Shadow(Texture2D st, Vector2 sp)
        {

            shadowTexture = st;
            shadowPosition = sp;

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(shadowTexture, shadowPosition, null, Color.White);
        }



    }
}
