using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace FortBuildZApc
{
    public class Tree1 : Screen
    {
        public GamePadState gpsT;
        public GamePadState gpsT2;
        public Texture2D tree1Texture;
        public Vector2 tree1Position;
       
        public Rectangle tree1CollisionRectangle;
        public Rectangle tree1SourceRectangle;
        public Rectangle handsCollisionRectangle;
        public Rectangle handsCollisionRectangle2;

        

        public int tree1Damage = 0;
        public int tree1Frames = 10;

        public bool isBreakingT = false;
        public bool isTree1Broken;

        public Tree1 ( Texture2D t, Vector2 tp)
        {
            tree1Texture = t;
            tree1Position = tp;
            isTree1Broken = false;
        }

        public void Update()
        {
            gpsT = GamePad.GetState(playerOne);
            gpsT2 = GamePad.GetState(playerTwo);
            tree1CollisionRectangle = new Rectangle((int)tree1Position.X + 10, (int)tree1Position.Y + 10, tree1Texture.Width / tree1Frames - 20, tree1Texture.Height - 20);
            tree1SourceRectangle = new Rectangle((int)((tree1Texture.Width / tree1Frames)) * tree1Damage, 0, tree1Texture.Width / tree1Frames, tree1Texture.Height);
            

            if (isBreakingT == false)
            {
                if (((handsCollisionRectangle.Intersects(tree1CollisionRectangle)) && gpsT.Triggers.Left >= .5f) ||
                    ((handsCollisionRectangle2.Intersects(tree1CollisionRectangle)) && gpsT2.Triggers.Left >= .5f))
                {
                    isBreakingT = true;
                    tree1Damage++;
                }

                if (tree1Damage >= 11)
                {
                    isTree1Broken = true;
                }
            }

            if (isBreakingT == true)
            {
                if (((handsCollisionRectangle.Intersects(tree1CollisionRectangle)) && gpsT.Triggers.Left < .5f) ||
                    ((handsCollisionRectangle2.Intersects(tree1CollisionRectangle)) && gpsT2.Triggers.Left < .5f))
                {
                    isBreakingT = false;
                }
            }

            


        }

        public override void Draw(SpriteBatch sprites)
        {
            sprites.Draw(tree1Texture, tree1Position, tree1SourceRectangle, Color.White);
        }
    }
}
