using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FortBuildZApc
{
    class WarningScreen : Screen
    {
        GamePadState currentPadState;
        GamePadState oldPadState;
        Texture2D bac;

        public bool goBack;
        public bool aUsed;

        public WarningScreen(ContentManager SIscreenContent, EventHandler SIscreenEvent)
            : base(SIscreenEvent)
        {
            bac = SIscreenContent.Load<Texture2D>("warningScreen");
        }

        public override void Update(GameTime gameTime)
        {
            currentPadState = GamePad.GetState(playerOne);

            if (aUsed == false)
            {
                oldPadState = currentPadState;
                aUsed = true;
            }

            if ((currentPadState.Buttons.A == ButtonState.Pressed) && (oldPadState.Buttons.A == ButtonState.Released))
            {
                goBack = false;
                aUsed = false;
                screenEvent.Invoke(this, new EventArgs());
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
            spriteBatch.Draw(bac, Vector2.Zero, Color.White);
            base.Draw(spriteBatch);
        }
    }
}