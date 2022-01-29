using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FortBuildZApc
{
    public class Rock1 : Screen
    {
        public GamePadState gpsT;
        public GamePadState gpsT2;
        public Texture2D rock1Texture;
        public Vector2 rock1Position;
       
        public Rectangle rock1ColRec;
        public Rectangle rock1SouRec;
        public Rectangle handsColRec;
        public Rectangle handsColRec2;

        

        public int rock1Damage = 0;
        public int rock1Frames = 10;

        public bool isBreakingT = false;
        public bool isrock1Broken;

        public Rock1 ( Texture2D r, Vector2 rp)
        {
            rock1Texture = r;
            rock1Position = rp;
            isrock1Broken = false;
        }

        public void Update()
        {
            gpsT = GamePad.GetState(playerOne);
            gpsT2 = GamePad.GetState(playerTwo);
            rock1ColRec = new Rectangle((int)rock1Position.X + 10, (int)rock1Position.Y + 10, rock1Texture.Width / rock1Frames - 20, rock1Texture.Height - 20);
            rock1SouRec = new Rectangle((int)((rock1Texture.Width / rock1Frames)) * rock1Damage, 0, rock1Texture.Width / rock1Frames, rock1Texture.Height);
            

            if (isBreakingT == false)
            {
                if (((handsColRec.Intersects(rock1ColRec)) && (gpsT.Triggers.Left >= .5f) ||
                    ((handsColRec2.Intersects(rock1ColRec)) && (gpsT2.Triggers.Left >= .5f))))
                {
                    isBreakingT = true;
                    if (pickaxeActive == false)
                        rock1Damage++;
                    else
                        rock1Damage += 2; 
                }

                if (rock1Damage >= 11)
                {
                    isrock1Broken = true;
                }
            }

            if (isBreakingT == true)
            {
                if (((handsColRec.Intersects(rock1ColRec)) && (gpsT.Triggers.Left < .5f) ||
                    ((handsColRec2.Intersects(rock1ColRec)) && (gpsT2.Triggers.Left < .5f))))
                {
                    isBreakingT = false;
                }
            }

            


        }

        public override void Draw(SpriteBatch sprites)
        {
            sprites.Draw(rock1Texture, rock1Position, rock1SouRec, Color.White);
        }

        public void checkZombieCollision(List<Zombie1> nigger, int niggerRadius, int thisRadius, TimeSpan placementDamageTime, GameTime gameTime)
        {
            foreach (Zombie1 z in nigger)
            {
                if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(rock1Position.X + (rock1Texture.Width / rock1Frames / 2), rock1Position.Y + (rock1Texture.Height / 2)), thisRadius))
                {
                    z.maxSpeed = 0.5f;
                }
                else
                    z.maxSpeed = 1f; 
            }
        }

        public void checkZombie2Collision(List<Zombie2> nigger, int niggerRadius, int thisRadius, TimeSpan placementDamageTime, GameTime gameTime)
        {
            foreach (Zombie2 z in nigger)
            {
                if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(rock1Position.X + (rock1Texture.Width / rock1Frames / 2), rock1Position.Y + (rock1Texture.Height / 2)), thisRadius))
                {
                    z.maxSpeed = 0.5f;
                }
                else
                    z.maxSpeed = 1f; 
            }
        }

        public void checkZombie3Collision(List<Zombie3> nigger, int niggerRadius, int thisRadius, TimeSpan placementDamageTime, GameTime gameTime)
        {
            foreach (Zombie3 z in nigger)
            {
                if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(rock1Position.X + (rock1Texture.Width / rock1Frames / 2), rock1Position.Y + (rock1Texture.Height / 2)), thisRadius))
                {
                    z.maxSpeed = 0.5f;
                }
                else
                    z.maxSpeed = 1f; 
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