using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace FortBuildZApc
{
    public class Tree2 : Screen
    {
        public GamePadState gpsT;
        public GamePadState gpsT2;
        public Texture2D tree2Texture;
        public Vector2 tree2Position;
       
        public Rectangle tree2CollisionRectangle;
        public Rectangle tree2SourceRectangle;
        public Rectangle handsCollisionRectangle;
        public Rectangle handsCollisionRectangle2;

        

        public int tree2Damage = 0;
        public int tree2Frames = 10;

        public bool isBreakingT = false;
        public bool isTree2Broken;

        public Tree2 ( Texture2D t, Vector2 tp)
        {
            tree2Texture = t;
            tree2Position = tp;
            isTree2Broken = false;
        }

        public void Update()
        {
            gpsT = GamePad.GetState(playerOne);
            gpsT2 = GamePad.GetState(playerTwo);
            tree2CollisionRectangle = new Rectangle((int)tree2Position.X + 10, (int)tree2Position.Y + 10, tree2Texture.Width / tree2Frames - 20, tree2Texture.Height - 20);
            tree2SourceRectangle = new Rectangle((int)((tree2Texture.Width / tree2Frames)) * tree2Damage, 0, tree2Texture.Width / tree2Frames, tree2Texture.Height);
            

            if (isBreakingT == false)
            {
                if (((handsCollisionRectangle.Intersects(tree2CollisionRectangle)) && (gpsT.Triggers.Left >= .5f) ||
                    ((handsCollisionRectangle2.Intersects(tree2CollisionRectangle)) && (gpsT2.Triggers.Left >= .5f))))
                {
                    isBreakingT = true;
                    tree2Damage++;  
                }

                if (tree2Damage >= 11) 
                    {
                        isTree2Broken = true;
                    }
            }

            if (isBreakingT == true)
            {
                if (((handsCollisionRectangle.Intersects(tree2CollisionRectangle)) && (gpsT.Triggers.Left < .5f) ||
                    ((handsCollisionRectangle2.Intersects(tree2CollisionRectangle)) && (gpsT2.Triggers.Left < .5f))))
                {
                    isBreakingT = false;
                }
            }

        }

        public override void Draw(SpriteBatch sprites)
        {
            sprites.Draw(tree2Texture, tree2Position, tree2SourceRectangle, Color.White);
        }


    }


}



