using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FortBuildZApc
{
    public class Bullet
    {
        public Texture2D bullet;
        public Vector2 bulletPosition;
        public Vector2 bulletVelocity;
        public float rotate;
        public float bulletSpeed = 50.0f;
        public bool active;
        public int damage;

        public void bulletFacts(Texture2D b, Vector2 bp, int d)
        {
            active = true;
            damage = d;
            bullet = b;
            bulletPosition = bp;
            bulletVelocity = new Vector2((float)Math.Cos(rotate), ((float)Math.Sin(rotate))) * 2;
        }

        public void Update()
        {
            bulletPosition += bulletVelocity * bulletSpeed;

            if ((bulletPosition.X > 1300 || bulletPosition.X < 0 || bulletPosition.Y > 800 || bulletPosition.Y < 0))
                active = false;
        }

        public void Draw(SpriteBatch sprites)
        {

            sprites.Draw(bullet, bulletPosition, null, Color.White, rotate, new Vector2(bullet.Width / 2, bullet.Height / 2), 1, SpriteEffects.None, 0f);


        }

        

    }
}
