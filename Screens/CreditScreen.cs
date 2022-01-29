using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FortBuildZApc
{
    class CreditScreen : Screen
    {
        GamePadState currentPadState;
        GamePadState oldPadState;
        Texture2D bac, bac2;

        public bool goBack;
        public bool aUsed;

        public int currentBac = 1; 

        public float timer = 0;

        public CreditScreen(ContentManager SIscreenContent, EventHandler SIscreenEvent) : base(SIscreenEvent)
        {
            bac = SIscreenContent.Load<Texture2D>("Credits");
            bac2 = SIscreenContent.Load<Texture2D>("Credits1");
        }

        public override void Update(GameTime gameTime)
        {
            currentPadState = GamePad.GetState(playerOne);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 3.0f)
            {
                currentBac = 2; 
            }

            if (aUsed == false)
            {
                oldPadState = currentPadState;
                aUsed = true;
            }

            if ((currentPadState.Buttons.B == ButtonState.Pressed) && (oldPadState.Buttons.B == ButtonState.Released))
            {
                goBack = true;
                aUsed = false;
                screenEvent.Invoke(this, new EventArgs());
            }

            oldPadState = currentPadState;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (currentBac == 1)
                spriteBatch.Draw(bac, Vector2.Zero, Color.White);
            else
                spriteBatch.Draw(bac2, Vector2.Zero, Color.White);
            base.Draw(spriteBatch);
        }
    }
}