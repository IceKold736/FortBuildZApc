using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FortBuildZApc
{
    public class Character2 : Screen
    {
        GamePadState ogps;

        public Texture2D characterTexture;
        public Vector2 characterPosition;

        public Rectangle characterSourceRectangle;
        public Rectangle characterCollisionRectangle;

        public Vector2 characterVelocity;
        public Vector2 characterForward;

        public float maxSpeed = 9.0f;
        public float turnSpeed = 1f;

        public float orientation = 0;

        public float characterFriction = .3f;
        public int charHealth = 6;
        public int numberOfHearts = 3;

        public int currentWeapon = 0;
        public int characterFrames = 12;
        public bool isCurrentWeaponAvailable = false;


        public Character2(Vector2 cp, Texture2D c)
        {
            characterPosition = cp;
            characterTexture = c;
        }

        public void Update()
        {
            GamePadState gps = GamePad.GetState(playerTwo);

            characterSourceRectangle = new Rectangle(0, (int)((characterTexture.Height / characterFrames) * currentWeapon), characterTexture.Width, characterTexture.Height / characterFrames);
            characterCollisionRectangle = new Rectangle((int)characterPosition.X - characterTexture.Width / 2 + 25, (int)(characterPosition.Y) - characterTexture.Width / 2 + 25, characterTexture.Width - 50, characterTexture.Width - 50);

            if (Math.Abs(orientation) > MathHelper.ToRadians(360f))
                orientation = MathHelper.ToRadians(0f);

            if (gps.ThumbSticks.Right.Length() > 0)
            {
                orientation = TurnToFace(orientation, gps.ThumbSticks.Right, turnSpeed);
            }
            else
            {
                orientation = TurnToFace(orientation, gps.ThumbSticks.Left, turnSpeed);
            }

            characterForward = new Vector2((gps.ThumbSticks.Left).X, -(gps.ThumbSticks.Left).Y);
            characterVelocity += characterForward * maxSpeed;
            characterVelocity *= characterFriction;
            characterPosition += characterVelocity;

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

        private float TurnToFace(float rotation, Vector2 target, float turnRate)
        {
            if (target == Vector2.Zero)
            {
                return rotation;
            }

            float angle = (float)Math.Atan2(-target.Y, target.X);
            float difference = rotation - angle;

            while (difference > MathHelper.Pi)
            {
                difference -= MathHelper.TwoPi;
            }

            while (difference < -MathHelper.Pi)
            {
                difference += MathHelper.TwoPi;
            }

            turnRate *= Math.Abs(difference);

            if (difference < 0)
            {
                return rotation + Math.Min(turnRate, -difference);
            }
            else
            {
                return rotation - Math.Min(turnRate, difference);
            }
        }

        public override void Draw(SpriteBatch sprites)
        {

            sprites.Draw(characterTexture, characterPosition, characterSourceRectangle, Color.White, orientation, new Vector2(characterTexture.Width / 4, characterTexture.Height / characterFrames / 2), 1, SpriteEffects.None, 0);


        }

    }
}

