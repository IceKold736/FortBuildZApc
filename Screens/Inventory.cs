using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace FortBuildZApc
{
    public class Inventory  
    {
        SpriteFont font;

        public int isPistolAvailable = 1;
        public int isSMGAvailable = 1;
        public int isShotgunAvailable = 1;
        public int isSniperAvailable = 1;
        public int isGloveAvailable = 1;
        


        //row 1
        Vector2 woodSlotPos = new Vector2(520, 325);
        Vector2 rockSlotPos = new Vector2(672, 325);
        Vector2 metalOreSlotPos = new Vector2(824, 325);
        Vector2 sandSlotPos = new Vector2(976, 325);
        Vector2 appleSlotPos = new Vector2(1128, 325);
        //row2
        Vector2 doorSlotPos = new Vector2(520, 477);
        Vector2 seedSlotPos = new Vector2(672, 477);
        Vector2 metalSlotPos = new Vector2(824, 477);
        Vector2 glassSlotPos = new Vector2(976, 477);
        Vector2 foodSlotPos = new Vector2(1128, 477);
        //row3
        Vector2 bedSlotPos = new Vector2(520, 629);
        Vector2 clothSlotPos = new Vector2(672, 629);
        Vector2 lockpickSlotPos = new Vector2(824, 629);
        Vector2 windowSlotPos = new Vector2(976, 629);
        Vector2 energyDrinkSlotPos = new Vector2(1128, 629);

        //row1
        public int woodCount = 10;
        public int rockCount = 10;
        public int metalOreCount = 10;
        public int sandCount = 10;
        public int appleCount = 0;
        
        //row2
        public int doorCount = 1;
        public int seedCount = 0;
        public int metalCount = 10; 
        public int glassCount = 12;
        public int foodCount = 0;
        
        //row3
        public int bedCount = 0; 
        public int clothCount = 0;
        public int lockpickCount = 1;
        public int windowCount = 0;
        public int energyDrinkCount = 0;
        
        //misc
        public int extendedMagCount = 0;
        public int shellsCount = 0;
        public int scopeCount = 0;
        public int gloveCount = 0;

        public Inventory(SpriteFont s)
        {
            font = s; 
        }

        public void Update()
        {

            //row 1
            if (woodCount < 9)
                woodSlotPos = new Vector2(520, 325);
            if (woodCount > 9)
                woodSlotPos = new Vector2(497, 325);
            if (woodCount > 99)
                woodSlotPos = new Vector2(474, 325);
                          

            if (rockCount < 9)
                rockSlotPos = new Vector2(672, 325);
            if (rockCount > 9)
                rockSlotPos = new Vector2(649, 325);
            if (rockCount > 99)
                rockSlotPos = new Vector2(626, 325);

            if(metalOreCount < 9)
                metalOreSlotPos = new Vector2(824, 325);
            if (metalOreCount > 9)
                metalOreSlotPos = new Vector2(801, 325);
            if (metalOreCount > 99)
                metalOreSlotPos = new Vector2(778, 325);

            if (sandCount < 9)
                sandSlotPos = new Vector2(976, 325);
            if (sandCount > 9)
                sandSlotPos = new Vector2(953, 325);
            if (sandCount > 99)
                sandSlotPos = new Vector2(930, 325);

            if (appleCount < 9)
                appleSlotPos = new Vector2(1128, 325);
            if (appleCount > 9)
                appleSlotPos = new Vector2(1105, 325);
            if (appleCount > 99)
                appleSlotPos = new Vector2(1082, 325);

            //row2
            if (doorCount < 9)
                doorSlotPos = new Vector2(520, 477);
            if (doorCount > 9)
                doorSlotPos = new Vector2(497, 477);
            if (doorCount > 99)
                doorSlotPos = new Vector2(474, 477);

            if (seedCount < 9)
                seedSlotPos = new Vector2(672, 477);
            if (seedCount > 9)
                seedSlotPos = new Vector2(649, 477);
            if (seedCount > 99)
                seedSlotPos = new Vector2(626, 477);

            if (metalCount < 9)
                metalSlotPos = new Vector2(824, 477);
            if (metalCount > 9)
                metalSlotPos = new Vector2(801, 477);
            if (metalCount > 99)
                metalSlotPos = new Vector2(778, 477);

            if (glassCount < 9)
                glassSlotPos = new Vector2(976, 477);
            if (glassCount > 9)
                glassSlotPos = new Vector2(953, 477);
            if (glassCount > 99)
                glassSlotPos = new Vector2(930, 477);

            if (foodCount > 9)
                foodSlotPos = new Vector2(1128, 477);
            if (foodCount > 9)
                foodSlotPos = new Vector2(1105, 477);
            if (foodCount > 9)
                foodSlotPos = new Vector2(1082, 477);

            //row3
            if (bedCount < 9)
                bedSlotPos = new Vector2(522, 629);
            if (bedCount > 9)
                bedSlotPos = new Vector2(499, 629);
            if (bedCount > 99)
                bedSlotPos = new Vector2(476, 629);

            if (clothCount < 9)
                clothSlotPos = new Vector2(662, 629);
            if (clothCount > 9)
                clothSlotPos = new Vector2(649, 629);
            if (clothCount > 99)
                clothSlotPos = new Vector2(626, 629);

            if(lockpickCount < 9)
                lockpickSlotPos = new Vector2(824, 629);
            if (lockpickCount > 9)
                lockpickSlotPos = new Vector2(801, 629);
            if (lockpickCount > 99)
                lockpickSlotPos = new Vector2(778, 629);

            if (windowCount < 9)
                windowSlotPos = new Vector2(976, 629);
            if (windowCount > 9)
                windowSlotPos = new Vector2(953, 629);
            if (windowCount > 99)
                windowSlotPos = new Vector2(930, 629);

            if(energyDrinkCount < 9)
                energyDrinkSlotPos = new Vector2(1128, 629);
            if (energyDrinkCount > 9)
                energyDrinkSlotPos = new Vector2(1105, 629);
            if (energyDrinkCount > 99)
                energyDrinkSlotPos = new Vector2(1082, 629);
        }

        public void Draw(SpriteBatch sprites)
        {
            //row1
            sprites.DrawString(font, woodCount.ToString(), woodSlotPos, Color.White);
            sprites.DrawString(font, rockCount.ToString(), rockSlotPos, Color.White);
            sprites.DrawString(font, metalOreCount.ToString(), metalOreSlotPos, Color.White);
            sprites.DrawString(font, sandCount.ToString(), sandSlotPos, Color.White);
            sprites.DrawString(font, appleCount.ToString(), appleSlotPos, Color.White);
            //row2
            sprites.DrawString(font, doorCount.ToString(), doorSlotPos, Color.White);
            sprites.DrawString(font, metalCount.ToString(), metalSlotPos, Color.White);
            sprites.DrawString(font, seedCount.ToString(), seedSlotPos, Color.White);
            sprites.DrawString(font, glassCount.ToString(), glassSlotPos, Color.White);
            sprites.DrawString(font, foodCount.ToString(), foodSlotPos, Color.White);
            //row3
            sprites.DrawString(font, bedCount.ToString(), bedSlotPos, Color.White);
            sprites.DrawString(font, clothCount.ToString(), clothSlotPos, Color.White);
            sprites.DrawString(font, lockpickCount.ToString(), lockpickSlotPos, Color.White);
            sprites.DrawString(font, windowCount.ToString(), windowSlotPos, Color.White);
            sprites.DrawString(font, energyDrinkCount.ToString(), energyDrinkSlotPos, Color.White);
        }

        public void resetShit()
        {
        isPistolAvailable = 1;
        isSMGAvailable = 1;
        isShotgunAvailable = 1;
        isSniperAvailable = 1;
        isGloveAvailable = 1;
        
        woodCount = 10;
        rockCount = 10;
        metalOreCount = 10;
        sandCount = 10;
        appleCount = 0;
        
        doorCount = 1;
        seedCount = 10;
        metalCount = 20; //cahnge
        glassCount = 10;
        foodCount = 0;
        
        bedCount = 1; 
        clothCount = 10;
        lockpickCount = 1;
        windowCount = 10;
        energyDrinkCount = 1;

        
        extendedMagCount = 0;
        shellsCount = 0;
        scopeCount = 0;
        gloveCount = 0;
        }
    }
}
