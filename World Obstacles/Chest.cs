using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FortBuildZApc
{
    public class Chest : Screen
    {
        public GamePadState gpsT;
        public GamePadState ogps;
        public GamePadState gpsT2;
        public GamePadState ogps2;

        public Texture2D chestTexture;
        public Vector2 chestPosition;
       
        public Rectangle chestCollisionRectangle;
        public Rectangle chestSourceRectangle;
        public Rectangle handsCollisionRectangle;
        public Rectangle handsCollisionRectangle2;
        public int inventoryLockpickCount;

        public int chestDamage = 0;
        public int chestFrames = 3;

        public bool isOpening = false;
        public bool active;
        public bool collectedShit = false;

        public Chest ( Texture2D c, Vector2 cp)
        {
            chestTexture = c;
            chestPosition = cp;
            active = false;
        }

        public void Update()
        {
            gpsT = GamePad.GetState(playerOne);
            gpsT2 = GamePad.GetState(playerTwo);
            chestCollisionRectangle = new Rectangle((int)chestPosition.X + 10, (int)chestPosition.Y + 10, chestTexture.Width / chestFrames - 20, chestTexture.Height - 20);

            if(chestDamage <= 2)
                chestSourceRectangle = new Rectangle((int)((chestTexture.Width / chestFrames)) * chestDamage, 0, chestTexture.Width / chestFrames, chestTexture.Height);

            if (isOpening == false)
            {
                if (((handsCollisionRectangle.Intersects(chestCollisionRectangle)) && (gpsT.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released && inventoryLockpickCount >= 1) ||
                    ((handsCollisionRectangle2.Intersects(chestCollisionRectangle)) && (gpsT2.Buttons.A == ButtonState.Pressed && ogps2.Buttons.A == ButtonState.Released && inventoryLockpickCount >= 1))))
                {
                    isOpening = true;
                    chestDamage++;
                }

                if (chestDamage >= 2 && collectedShit == true)
                {
                    active = true;
                }
            }

            if (isOpening == true)
            {
                if (((handsCollisionRectangle.Intersects(chestCollisionRectangle)) && gpsT.Triggers.Left < .5f) ||
                    ((handsCollisionRectangle2.Intersects(chestCollisionRectangle)) && gpsT2.Triggers.Left < .5f))
                {
                    isOpening = false;
                }
            }

            ogps = gpsT;
            ogps2 = gpsT2;
        }

        public override void Draw(SpriteBatch sprites)
        {
            sprites.Draw(chestTexture, chestPosition, chestSourceRectangle, Color.White);
        }
    }
}
