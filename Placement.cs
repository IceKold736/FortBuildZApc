using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace FortBuildZApc
{
    public class Placement : Screen
    {
        GamePadState gps; 

        public Texture2D placementTexture;
        public Vector2 placementPosition;
        public Vector2 placementTemporaryPosition; 
        public float placementRotation;
         

        public Rectangle placementCollisionRectangle;
        public Rectangle placementSourceRectangle;
        public Rectangle handsCollisionRectangle;
        public Rectangle handsCollisionRectangle2;

        public int damage = 0;
        public int damage2 = 0;
        public int placementFrames = 10;
        public bool isBreaking = false;
        public bool isBroken = false;
        public bool isPlaced = false;
        public int type;

        public bool frontDone = false;

        TimeSpan previousDamageTime;

        Character playerGuy;
        Character2 playerGuy2; 

        public Placement(Texture2D t, Character c, Vector2 r, bool p, int i)
        {
            playerGuy = c;
            placementPosition = r;
            placementTexture = t;
            isPlaced = p;
            type = i;
        }

        public Placement(Texture2D t, Character2 c, Vector2 r, bool p, int i)
        {
            playerGuy2 = c;
            placementPosition = r;
            placementTexture = t;
            isPlaced = p;
            type = i;
        }

        public void Update()
        {
            gps = GamePad.GetState(PlayerIndex.One);

            placementCollisionRectangle = new Rectangle((int)placementPosition.X - placementTexture.Width / 2, (int)placementPosition.Y - (placementTexture.Height / placementFrames / 2), placementTexture.Width, placementTexture.Height / placementFrames);
            placementSourceRectangle = new Rectangle(0, (placementTexture.Height / placementFrames) * damage, placementTexture.Width, placementTexture.Height / placementFrames);

            if (type == 1) { damage2 = 2; } //wood
            if (type == 2) { damage2 = 1; } //stone
            if (type == 3) { damage2 = 2; } //sand
            if (type == 4) { damage2 = 5; } //window
            if (type == 5) { damage2 = 2; } //door
            if (type == 6) { damage2 = 5; } //bed
            if (type == 7) { damage2 = 5; } //seed

            if (isBreaking == false)
            {
                if ((handsCollisionRectangle.Intersects(placementCollisionRectangle) || handsCollisionRectangle2.Intersects(placementCollisionRectangle)) && gps.Triggers.Left >= .5f)
                {
                    isBreaking = true;
                    if (type == 1) { damage += 2; } //wood

                    if (pickaxeActive == false)
                    {
                        if (type == 2) { damage += 1; } //stone
                    }
                    else
                    {
                        if (type == 2) { damage += 2; } //stone
                    }

                    if (type == 3) { damage += 2; } //sand
                    if (type == 4) { damage += 5; } //window
                    if (type == 5) { damage += 2; } //door
                    if (type == 6) { damage += 5; } //bed
                    if (type == 7) { damage += 5; } //seed
                }

                if (damage >= 10)
                {
                    isBroken = true;
                }
            }

            if (isBreaking == true)
            {
                if ((handsCollisionRectangle.Intersects(placementCollisionRectangle) || (handsCollisionRectangle2.Intersects(placementCollisionRectangle))) && gps.Triggers.Left < .5f)
                {
                    isBreaking = false;
                }
            }

            if (isPlaced == true)
            {
                if (frontDone == false)
                {
                    frontDone = true; 
                    
                    if(playerGuy != null)
                        placementRotation = MathHelper.ToDegrees(playerGuy.orientation);
                    else
                        placementRotation = MathHelper.ToDegrees(playerGuy2.orientation);

                    placementRotation = Math.Abs(placementRotation);
                    
                   #region Rotation Snap

                    //Quardrant 1
                   if (placementRotation >= (45f) && placementRotation < (90f))
                   {
                       placementRotation = MathHelper.ToRadians(90f);
                   }
                   else if (placementRotation < (45f) && placementRotation >= (0f))
                   {
                       placementRotation = MathHelper.ToRadians(0f);
                   }

                   //Quardrant 2
                   else if (placementRotation >= (135f) && placementRotation < (180f))
                   {
                       placementRotation = MathHelper.ToRadians(180f);
                   }
                   else if (placementRotation < (135f) && placementRotation >= (90f))
                   {
                       placementRotation = MathHelper.ToRadians(90f);
                   }

                   //Quardrant 3
                   else if (placementRotation >= (225f) && placementRotation < (270f))
                   {
                       placementRotation = MathHelper.ToRadians(270f);
                   }
                   else if (placementRotation < (225f) && placementRotation >= (180f))
                   {
                       placementRotation = MathHelper.ToRadians(180f);
                   }

                   //Quardrant 4
                   else if (placementRotation >= (315f) && placementRotation <= (360f))
                   {
                       placementRotation = MathHelper.ToRadians(360f);
                   }
                   else if (placementRotation < (315f) && placementRotation >= (270f))
                   {
                       placementRotation = MathHelper.ToRadians(270f);
                   }

                   #endregion
                }

                
                //isPlaced = false;
            }

                //Rotation snap.

        }

        public void checkZombieCollision(List<Zombie1> nigger, int niggerRadius, int thisRadius, TimeSpan placementDamageTime, GameTime gameTime)
        {
            foreach (Zombie1 z in nigger)
            {
                //FUCKED UP STILL

                if (type == 3)
                {
                    if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(placementPosition.X, placementPosition.Y), thisRadius))
                    {
                        z.maxSpeed = 0.5f;
                    }
                }
                else
                {
                    if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(placementPosition.X, placementPosition.Y), thisRadius))
                    {
                        z.zombiePosition = z.prevZombiePosition;
                        if (gameTime.TotalGameTime - previousDamageTime > placementDamageTime)
                        {
                            damage += damage2;
                            previousDamageTime = gameTime.TotalGameTime;
                        }
                    }

                    if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(placementPosition.X, placementPosition.Y), thisRadius - 3))
                    {
                        z.zombiePosition -= new Vector2((float)Math.Cos(z.orientation), (float)Math.Sin(z.orientation));
                    }
                }
            }
        }

        public void checkZombie2Collision(List<Zombie2> nigger, int niggerRadius, int thisRadius, TimeSpan placementDamageTime, GameTime gameTime)
        {
            foreach (Zombie2 z in nigger)
            {
                if (type == 3)
                {
                    if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(placementPosition.X, placementPosition.Y), thisRadius))
                    {
                        z.maxSpeed = 0.5f;
                    }
                }
                else
                {
                    if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(placementPosition.X, placementPosition.Y), thisRadius))
                    {
                        z.zombiePosition = z.prevZombiePosition;
                        if (gameTime.TotalGameTime - previousDamageTime > placementDamageTime)
                        {
                            damage += damage2;
                            previousDamageTime = gameTime.TotalGameTime;
                        }
                    }

                    if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(placementPosition.X, placementPosition.Y), thisRadius - 3))
                    {
                        z.zombiePosition -= new Vector2((float)Math.Cos(z.orientation), (float)Math.Sin(z.orientation));
                    }
                }
            }
        }

        public void checkZombie3Collision(List<Zombie3> nigger, int niggerRadius, int thisRadius, TimeSpan placementDamageTime, GameTime gameTime)
        {
            foreach (Zombie3 z in nigger)
            {
                if (type == 3)
                {
                    if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(placementPosition.X, placementPosition.Y), thisRadius))
                    {
                        z.maxSpeed = 0.5f;
                    }
                }
                else
                {
                    if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(placementPosition.X, placementPosition.Y), thisRadius))
                    {
                        z.zombiePosition = z.prevZombiePosition;
                        if (gameTime.TotalGameTime - previousDamageTime > placementDamageTime)
                        {
                            damage += damage2;
                            previousDamageTime = gameTime.TotalGameTime;
                        }
                    }

                    if (BoundingCircle(z.zombiePosition, niggerRadius, new Vector2(placementPosition.X, placementPosition.Y), thisRadius - 3))
                    {
                        z.zombiePosition -= new Vector2((float)Math.Cos(z.orientation), (float)Math.Sin(z.orientation));
                    }
                }
            }
        }

        public static bool BoundingCircle(Vector2 cir1, int radius1, Vector2 cir2, int radius2)
        {
            if (Vector2.Distance(cir1, cir2) <= radius1 + radius2)
                return true;

            return false;
        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(placementTexture, placementPosition, placementSourceRectangle, Color.White, placementRotation, new Vector2(placementTexture.Width / 2, placementTexture.Height / placementFrames/ 2), 1, SpriteEffects.None, 0f);
        }
    }
}
