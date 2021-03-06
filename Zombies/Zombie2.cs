using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FortBuildZApc
{
    public class Zombie2
    {

        public Texture2D zombie;
        public Vector2 zombiePosition;
        public Vector2 prevZombiePosition;
       
        public float maxSpeed = 2.3f;
        public float turnSpeed = .5f;

        public float orientation;
        public float rotate;
        public bool active;

        public int damageTaken = 10;
        public int damageDealth = 1;

        public Vector2 facePos;

        public Zombie2(Vector2 zp, Texture2D z)
        {
                zombiePosition = zp;
                zombie = z;
                active = true;
        }

        public void Update()
        {
            TurnToFace(facePos, turnSpeed);
                        
            Vector2 heading = new Vector2((float)Math.Cos(orientation), (float)Math.Sin(orientation));

            zombiePosition += heading * maxSpeed;
        }

        public void TurnToFace(Vector2 facePosition, float turnSpeed)
        {
            float x = facePosition.X - this.zombiePosition.X;
            float y = facePosition.Y - this.zombiePosition.Y;

            float desiredAngle = (float)Math.Atan2(y, x);
            float difference = WrapAngle(desiredAngle - this.orientation);

            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);
            this.orientation = WrapAngle(this.orientation + difference);
        }

        public static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }



            public void Draw(SpriteBatch sprites)
            {
                sprites.Draw(zombie, zombiePosition, null, Color.White, orientation, new Vector2(zombie.Width / 4, zombie.Height / 2), 1, SpriteEffects.None, 1);
            }
    } 
}
