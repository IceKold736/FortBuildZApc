using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace FortBuildZApc
{
    class TitleScreen : Screen
    {
        Texture2D titleBacTex;
        Texture2D titleBacTexTrial;
        Texture2D treeOverlay;
        Texture2D titleSelector;
        Vector2 titlePosition;
        GamePadState currentPadState;
        GamePadState oldPadState;
        SpriteFont smallArial;

        TimeSpan changeTime;
        TimeSpan prevChangeTime;
        TimeSpan DchangeTime;
        TimeSpan DprevChangeTime;

        public int buttonPressed;
        public int menuSelection = 1;

        int min;
        int max;

        SoundEffect click; 

        public TitleScreen(ContentManager TscreenContent, EventHandler TScreenEvent) : base(TScreenEvent)
        {
            changeTime = TimeSpan.FromSeconds(0.3f);
            DchangeTime = TimeSpan.FromSeconds(0.3f);

            titleBacTex = TscreenContent.Load<Texture2D>("titleBacTex");
            titleBacTexTrial = TscreenContent.Load<Texture2D>("TrialScreen");
            smallArial = TscreenContent.Load<SpriteFont>("SpriteFont1");
            treeOverlay = TscreenContent.Load<Texture2D>("TreeOverlay");
            titleSelector = TscreenContent.Load<Texture2D>("TitleSelector");
            click = TscreenContent.Load<SoundEffect>("clickSound"); 
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            oldPadState = currentPadState;
            currentPadState = GamePad.GetState(playerOne);

            if (Guide.IsTrialMode != true)
            {
                min = 1;
                max = 5;
            }
            else
            {
                min = 1;
                max = 3;
            }

            if ((currentPadState.Buttons.A == ButtonState.Pressed) && (oldPadState.Buttons.A == ButtonState.Released) || state.IsKeyDown(Keys.A))
            {
                screenEvent.Invoke(this, new EventArgs());
            }

            if ((currentPadState.DPad.Up == ButtonState.Pressed) && (oldPadState.DPad.Up == ButtonState.Released))
            {
                if (menuSelection > min)
                    menuSelection--;
                else
                    menuSelection = max;

                click.Play(); 
            }
            else if ((currentPadState.ThumbSticks.Left.Y > 0.25))
            {
                if (gameTime.TotalGameTime - prevChangeTime > changeTime)
                {
                    prevChangeTime = gameTime.TotalGameTime;

                    if (menuSelection > min)
                        menuSelection--;
                    else
                        menuSelection = max;

                    click.Play();
                }

            }

            if ((currentPadState.DPad.Down == ButtonState.Pressed) && (oldPadState.DPad.Down == ButtonState.Released))
            {
                if (menuSelection < max)
                    menuSelection++;
                else
                    menuSelection = min;

                click.Play();
            }
            else if ((currentPadState.ThumbSticks.Left.Y < -0.25))
            {
                if (gameTime.TotalGameTime - DprevChangeTime > DchangeTime)
                {
                    DprevChangeTime = gameTime.TotalGameTime;

                    if (menuSelection < max)
                        menuSelection++;
                    else
                        menuSelection = min;

                    click.Play();
                }
            }

            switch (menuSelection)
            {
                case 1: buttonPressed = 1; break;
                case 2: buttonPressed = 2; break;
                case 3: buttonPressed = 3; break;
                case 4: buttonPressed = 4; break;
                case 5: buttonPressed = 5; break;
            }

            if (Guide.IsTrialMode != true)
            {
                titlePosition = new Vector2(156, 225 + (menuSelection - 1) * 55);
            }
            else
            {
                titlePosition = new Vector2(156, 225 + (menuSelection - 1) * 110);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Guide.IsTrialMode == false)
            {
                spriteBatch.Draw(titleBacTex, Vector2.Zero, Color.White);
                spriteBatch.Draw(titleSelector, titlePosition, Color.White);
                spriteBatch.Draw(treeOverlay, Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.Draw(titleBacTexTrial, Vector2.Zero, Color.White);
                spriteBatch.Draw(titleSelector, titlePosition, Color.White);
                spriteBatch.Draw(treeOverlay, Vector2.Zero, Color.White);
            }

            base.Draw(spriteBatch);
        }
    }
}