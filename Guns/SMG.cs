using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FortBuildZApc
{
    public class SMG
     {

        public Texture2D gunTexture;
        public Vector2 gunPosition;
        public Vector2 gunTemp;
        public float gunRot;

        public int damage = 4;

        public float rotate;
        public bool active;
       

        public GamePadState gpsG;

        public float realCharacterRot = 0f;

        public Character playerGuy;

        public SMG(Character c, Texture2D gt)
        {
            playerGuy = c;
            gunTexture = gt;
        }

        public void Update()
        {
            gpsG = GamePad.GetState(PlayerIndex.One);

            //if (gpsG.Buttons.X == ButtonState.Pressed)
            //{
            //    active = true;               
            //}
            //if (gpsG.Buttons.B == ButtonState.Pressed)
            //{
            //    active = false;
            //}
            

            gunPosition = playerGuy.characterPosition;
            gunTemp = new Vector2((float)Math.Cos(playerGuy.orientation), (float)Math.Sin(playerGuy.orientation))*5f;

            gunPosition += gunTemp * 5; //this number (5) just happens to be the perfect placement. It is (should be) the original speed.
            gunRot = playerGuy.orientation;
        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(gunTexture, gunPosition, null, Color.White, gunRot, new Vector2(gunTexture.Width / 2, gunTexture.Height / 2), 1,SpriteEffects.None, 0f);
        }


    }
}
