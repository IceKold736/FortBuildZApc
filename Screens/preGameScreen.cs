using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace FortBuildZApc
{
    class preGameScreen : Screen
    {
        GamePadState currentPadState;
        GamePadState oldPadState;
        Texture2D bac, bac2, bac3, bac4;

        int screen = 1;

        public bool goBack = false;
        public bool aUsed = false;

        SoundEffect click; 

        public preGameScreen(ContentManager SIscreenContent, EventHandler SIscreenEvent): base(SIscreenEvent)
        {
            bac = SIscreenContent.Load<Texture2D>("preGameScreen");
            bac2 = SIscreenContent.Load<Texture2D>("IntroScreen3");
            bac3 = SIscreenContent.Load<Texture2D>("IntroScreen2");
            bac4 = SIscreenContent.Load<Texture2D>("IntroScreen4");
            click = SIscreenContent.Load<SoundEffect>("clickSound"); 
        }

        public override void Update(GameTime gameTime)
        {
            currentPadState = GamePad.GetState(playerOne);
            KeyboardState state = Keyboard.GetState();

            if (aUsed == false)
            {
                oldPadState = currentPadState;
                aUsed = true;
            }

            if ((currentPadState.Buttons.A == ButtonState.Pressed) && (oldPadState.Buttons.A == ButtonState.Released) || state.IsKeyDown(Keys.A))
            {
                screenEvent.Invoke(this, new EventArgs());
            }

            if ((currentPadState.Buttons.LeftShoulder == ButtonState.Pressed) && (oldPadState.Buttons.LeftShoulder == ButtonState.Released) || state.IsKeyDown(Keys.A))
            {
                if(screen > 1)
                screen--;

                click.Play(); 
            }

            if ((currentPadState.Buttons.RightShoulder == ButtonState.Pressed) && (oldPadState.Buttons.RightShoulder == ButtonState.Released) || state.IsKeyDown(Keys.A))
            {
                if(screen < 4)
                screen++;

                click.Play(); 
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
            switch(screen)
            {
                case 1: spriteBatch.Draw(bac, Vector2.Zero, Color.White); break;
                case 2: spriteBatch.Draw(bac2, Vector2.Zero, Color.White); break;
                case 3: spriteBatch.Draw(bac4, Vector2.Zero, Color.White); break;
                case 4: spriteBatch.Draw(bac3, Vector2.Zero, Color.White); break;
            }
            base.Draw(spriteBatch);
        }
    }
}