using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FortBuildZApc
{
    public class Screen
    {
        public static PlayerIndex playerOne;
        public static PlayerIndex playerTwo;
        public static bool pickaxeActive;
        protected EventHandler screenEvent;

        public Screen()
        {

        }

        public Screen(EventHandler theScreenEvent)
        {
            screenEvent = theScreenEvent;
        }

        public virtual void Update(GameTime theTime)
        {
            //This can be overridded by other methods. Hence the "virtual"
        }

        public virtual void Draw(SpriteBatch theBatch)
        {
            //This can be overridded by other methods. Hence the "virtual"
        }
    }
}