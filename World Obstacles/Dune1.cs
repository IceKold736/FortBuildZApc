using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace FortBuildZApc
{
    public class Dune1 : Screen
    {
        public GamePadState gpsT;
        public GamePadState gpsT2;
        public Texture2D dune1Texture;
        public Vector2 dune1Position;
       
        public Rectangle dune1ColRec;
        public Rectangle dune1SouRec;
        public Rectangle handsColRec;
        public Rectangle handsColRec2;

        

        public int dune1Damage = 0;
        public int dune1Frames = 10;

        public bool isBreakingT = false;
        public bool isdune1Broken;

        public Dune1 ( Texture2D d, Vector2 dp)
        {
            dune1Texture = d;
            dune1Position = dp;
            isdune1Broken = false;
        }

        public void Update()
        {
            gpsT = GamePad.GetState(playerOne);
            gpsT2 = GamePad.GetState(playerTwo);
            dune1ColRec = new Rectangle((int)dune1Position.X + 10, (int)dune1Position.Y + 10, dune1Texture.Width / dune1Frames - 20, dune1Texture.Height - 20);
            dune1SouRec = new Rectangle((int)((dune1Texture.Width / dune1Frames)) * dune1Damage, 0, dune1Texture.Width / dune1Frames, dune1Texture.Height);
            

            if (isBreakingT == false)
            {
                if (((handsColRec.Intersects(dune1ColRec)) && (gpsT.Triggers.Left >= .5f) ||
                    ((handsColRec2.Intersects(dune1ColRec)) && (gpsT2.Triggers.Left >= .5f))))
                {
                    isBreakingT = true;
                    dune1Damage++;
                }

                if (dune1Damage >= 11)
                {
                    isdune1Broken = true;
                }
            }

            if (isBreakingT == true)
            {
                if (((handsColRec.Intersects(dune1ColRec)) && (gpsT.Triggers.Left < .5f) ||
                    ((handsColRec2.Intersects(dune1ColRec)) && (gpsT2.Triggers.Left < .5f))))
                {
                    isBreakingT = false;
                }
            }

            


        }

        public override void Draw(SpriteBatch sprites)
        {
            sprites.Draw(dune1Texture, dune1Position, dune1SouRec, Color.White);
        }

        public void checkZombieCollision(List<Zombie1> nigger, int niggerRadius, int thisRadius, TimeSpan placementDamageTime, GameTime gameTime)
        {
            foreach (Zombie1 z in nigger)
            {
                if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(dune1Position.X + (dune1Texture.Width / dune1Frames / 2), dune1Position.Y + (dune1Texture.Height / 2)), thisRadius))
                {
                    z.maxSpeed = 0.5f;
                }
            }
        }

        public void checkZombie2Collision(List<Zombie2> nigger, int niggerRadius, int thisRadius, TimeSpan placementDamageTime, GameTime gameTime)
        {
            foreach (Zombie2 z in nigger)
            {
                if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(dune1Position.X + (dune1Texture.Width / dune1Frames / 2), dune1Position.Y + (dune1Texture.Height / 2)), thisRadius))
                {
                    z.maxSpeed = 0.5f;
                }
            }
        }

        public void checkZombie3Collision(List<Zombie3> nigger, int niggerRadius, int thisRadius, TimeSpan placementDamageTime, GameTime gameTime)
        {
            foreach (Zombie3 z in nigger)
            {
                if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(dune1Position.X + (dune1Texture.Width / dune1Frames / 2), dune1Position.Y + (dune1Texture.Height / 2)), thisRadius))
                {
                    z.maxSpeed = 0.5f;
                }
            }
        }

        public static bool BoundingCircle(Vector2 cir1, int radius1, Vector2 cir2, int radius2)
        {
            if (Vector2.Distance(cir1, cir2) <= radius1 + radius2)
                return true;

            return false;
        }
    }
}
