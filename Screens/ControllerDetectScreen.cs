using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FortBuildZApc
{
    class ControllerDetectScreen : Screen
    {
        Texture2D controllerDetectBacTex;
        Texture2D controllerDetectBacTex1;

        double fadingDelay = 0.35;
        int fadingAlphaValue = 0;
        int fadingFadeIncrement = 3;
        bool fading = true; 

        public ControllerDetectScreen(ContentManager CDscreenContent, EventHandler CDscreenEvent): base(CDscreenEvent)
        {
            controllerDetectBacTex = CDscreenContent.Load<Texture2D>("StartScreenSketch1");
            controllerDetectBacTex1 = CDscreenContent.Load<Texture2D>("StartScreenSketch2");
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            for (int aPlayer = 0; aPlayer < 4; aPlayer++)
            {
                if (GamePad.GetState((PlayerIndex)aPlayer).Buttons.Start == ButtonState.Pressed || state.IsKeyDown(Keys.A))
                {
                    playerOne = (PlayerIndex)aPlayer;
                    screenEvent.Invoke(this, new EventArgs());
                    return;
                }
            }

            fadingDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (fading == true)
            {
                if (fadingDelay <= 0)
                {
                    fadingDelay = .00005;

                    if (fadingAlphaValue < 255)
                        fadingAlphaValue += fadingFadeIncrement;
                }

                if (fadingAlphaValue == 255)
                    fading = false;
            }
            else
            {
                if (fadingDelay <= 0)
                {
                    fadingDelay = .00005;

                    if (fadingAlphaValue >= 0)
                        fadingAlphaValue -= fadingFadeIncrement;
                }

                if (fadingAlphaValue == 0)
                    fading = true;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(controllerDetectBacTex, Vector2.Zero, Color.White);
            spriteBatch.Draw(controllerDetectBacTex1, Vector2.Zero, new Color(255, 255, 255, (byte)MathHelper.Clamp(fadingAlphaValue, 0, 255)));
            base.Draw(spriteBatch);
        }
    }
}
