using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace FortBuildZApc
{
    class MultiplayerScreen : Screen
    {
        GamePadState currentPadState;
        GamePadState oldPadState;
        Texture2D bac, bac2, bac3, bac4, selector, selector2;

        public int currentSel;
        public int currentBac;
        public int gamemodeSel; 

        public bool goBack = false;
        public bool aUsed = false;

        public bool secondChoosen = false; 

        TimeSpan changeTime;
        TimeSpan prevChangeTime;
        TimeSpan DchangeTime;
        TimeSpan DprevChangeTime;

        SoundEffect click; 

        public MultiplayerScreen(ContentManager SIscreenContent, EventHandler SIscreenEvent) : base(SIscreenEvent)
        {
            changeTime = TimeSpan.FromSeconds(0.3f);
            DchangeTime = TimeSpan.FromSeconds(0.3f);

            bac = SIscreenContent.Load<Texture2D>("SinglePlayerMultiplayer");
            bac2 = SIscreenContent.Load<Texture2D>("MultiplayerSignInScreen");
            bac3 = SIscreenContent.Load<Texture2D>("MultiplayerGameModeScreen");
            bac4 = SIscreenContent.Load<Texture2D>("MultiplayerInstructions");
            selector = SIscreenContent.Load<Texture2D>("MultiplayerSelector");
            selector2 = SIscreenContent.Load<Texture2D>("GameModeSelector");
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

            if (secondChoosen == false)
            {
                for (int aPlayer = 0; aPlayer < 4; aPlayer++)
                {
                    if (GamePad.GetState((PlayerIndex)aPlayer).Buttons.Start == ButtonState.Pressed && (PlayerIndex)aPlayer != (PlayerIndex)playerOne)
                    {
                        playerTwo = (PlayerIndex)aPlayer;
                        //screenEvent.Invoke(this, new EventArgs());
                        secondChoosen = true;
                        Debug.WriteLine("Second Player Controller Choosen!"); 
                        return;
                    }
                }
            }

            if (currentBac == 0)
            {
                if ((currentPadState.Buttons.A == ButtonState.Pressed) && (oldPadState.Buttons.A == ButtonState.Released) || state.IsKeyDown(Keys.A))
                {
                
                        if (currentSel == 0)
                        {
                            screenEvent.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            currentBac = 2;
                        }
                }
            }
            else if (currentBac == 1)
            {
                //if ((currentPadState.DPad.Left == ButtonState.Pressed) && (oldPadState.DPad.Left == ButtonState.Released))
                //{
                //    gamemodeSel = 0;
                //    click.Play();
                //}
                //else if ((currentPadState.ThumbSticks.Left.X < -0.25))
                //{
                //    if (gameTime.TotalGameTime - prevChangeTime > changeTime)
                //    {
                //        prevChangeTime = gameTime.TotalGameTime;

                //        gamemodeSel = 0;
                //        click.Play();
                //    }
                //}

                //if ((currentPadState.DPad.Right == ButtonState.Pressed) && (oldPadState.DPad.Right == ButtonState.Released))
                //{
                //    gamemodeSel = 1;
                //    click.Play();
                //}
                //else if ((currentPadState.ThumbSticks.Left.X > 0.25))
                //{
                //    if (gameTime.TotalGameTime - DprevChangeTime > DchangeTime)
                //    {
                //        DprevChangeTime = gameTime.TotalGameTime;

                //        gamemodeSel = 1;
                //        click.Play();
                //    }
                //}

                if ((currentPadState.Buttons.A == ButtonState.Pressed) && (oldPadState.Buttons.A == ButtonState.Released) || state.IsKeyDown(Keys.A))
                {
                    currentBac = 3;
                }
            }
            else if (currentBac == 3)
            {
                if ((currentPadState.Buttons.A == ButtonState.Pressed) && (oldPadState.Buttons.A == ButtonState.Released) || state.IsKeyDown(Keys.A))
                {
                    screenEvent.Invoke(this, new EventArgs());
                }
            }
           else
            {
                if (secondChoosen == true)
                {
                    currentBac = 1;
                }
            }


            if ((currentPadState.Buttons.B == ButtonState.Pressed) && (oldPadState.Buttons.B == ButtonState.Released))
            {
                goBack = true;
                aUsed = false;
                screenEvent.Invoke(this, new EventArgs());
            }

            if (currentBac == 0)
            {
                if ((currentPadState.DPad.Up == ButtonState.Pressed) && (oldPadState.DPad.Up == ButtonState.Released))
                {
                    currentSel = 0;
                    click.Play();
                }
                else if ((currentPadState.ThumbSticks.Left.Y > 0.25))
                {
                    if (gameTime.TotalGameTime - prevChangeTime > changeTime)
                    {
                        prevChangeTime = gameTime.TotalGameTime;

                        currentSel = 0;
                        click.Play();
                    }
                }

                if ((currentPadState.DPad.Down == ButtonState.Pressed) && (oldPadState.DPad.Down == ButtonState.Released))
                {
                    currentSel = 1;
                    click.Play();
                }
                else if ((currentPadState.ThumbSticks.Left.Y < -0.25))
                {
                    if (gameTime.TotalGameTime - DprevChangeTime > DchangeTime)
                    {
                        DprevChangeTime = gameTime.TotalGameTime;

                        currentSel = 1;
                        click.Play();
                    }
                }
            }

            oldPadState = currentPadState;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (currentBac == 0)
            {
                spriteBatch.Draw(bac, Vector2.Zero, Color.White);
                spriteBatch.Draw(selector, new Vector2(325, 246 + (currentSel * 112)), Color.White);
            }
            else if (currentBac == 1)
            {
                spriteBatch.Draw(bac3, Vector2.Zero, Color.White);
                spriteBatch.Draw(selector2, new Vector2(257 + (gamemodeSel * 495), 191), Color.White);
            }
            else if (currentBac == 3)
            {
                spriteBatch.Draw(bac4, Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.Draw(bac2, Vector2.Zero, Color.White);
            }

            base.Draw(spriteBatch);
        }
    }
}