using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FortBuildZApc
{
    public class MiniInventory
    {
        public GamePadState ogps;
        public Texture2D miniInventoryTexture;
        public Vector2 miniInventoryPosition;

        public Rectangle miniSourceRectangle;
        public int miniFrames = 12;
        public int currentWeapon = 0;

        public MiniInventory(Texture2D it, Vector2 ip)
        {
            miniInventoryTexture = it;
            miniInventoryPosition = ip;
        }

        public void Update()
        {
            GamePadState gps = GamePad.GetState(PlayerIndex.One);
            miniSourceRectangle = new Rectangle((int)((miniInventoryTexture.Width / miniFrames)) * currentWeapon, 0, miniInventoryTexture.Width / miniFrames, miniInventoryTexture.Height);

            //if (gps.Buttons.RightShoulder == ButtonState.Pressed && ogps.Buttons.RightShoulder == ButtonState.Released)
            //{
            //    currentWeapon++;
            //}

            if (currentWeapon > 11)
            {
                currentWeapon = 0;
            }

            if (currentWeapon < 0)
            {
                currentWeapon = 11;
            }

            //if (gps.Buttons.LeftShoulder == ButtonState.Pressed && ogps.Buttons.LeftShoulder == ButtonState.Released)
            //{
            //    currentWeapon--;
            //}



            ogps = gps;

        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(miniInventoryTexture, miniInventoryPosition, miniSourceRectangle, Color.White);
        }


    }
}
