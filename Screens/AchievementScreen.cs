using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Audio;

namespace FortBuildZApc
{
    public class AchievementScreen : Screen
    {
        //Storage Screen shit
        public StorageDevice device;
        public StorageContainer container;
        public IAsyncResult result;
        public Object stateobj;
        public bool deviceSelected;
        public bool gotoStorage = false;

        SoundEffect click; 

        public struct SaveGameData
        {
            public int tempKills;
            public int tempScore;
            public int tempNumberOfNights;
            public List<int> tempAchievements; 
        }

        SaveGameData data;

        Texture2D backgroundTexture;
        Texture2D backgroundTexture2;
        Texture2D A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12; 
        GamePadState gps;
        GamePadState ogps;

        public bool Ua1, Ua2, Ua3, Ua4, Ua5, Ua6, Ua7, Ua8, Ua9, Ua10, Ua11, Ua12;

        public bool changeScreen = false;
        public bool loaded = false; 

        public AchievementScreen(ContentManager AscreenContent, EventHandler AscreenEvent) : base(AscreenEvent)
        {
            backgroundTexture = AscreenContent.Load<Texture2D>("AchievementScreen");
            A1 = AscreenContent.Load<Texture2D>("INeedAWeapon");
            A2 = AscreenContent.Load<Texture2D>("ThankTheLord");
            A3 = AscreenContent.Load<Texture2D>("RiotControl");
            A4 = AscreenContent.Load<Texture2D>("BoomHeadShot");
            A5 = AscreenContent.Load<Texture2D>("KillShot");
            A6 = AscreenContent.Load<Texture2D>("Over9000");
            backgroundTexture2 = AscreenContent.Load<Texture2D>("AchievementsP2");
            A7 = AscreenContent.Load<Texture2D>("Zombiecide");
            A8 = AscreenContent.Load<Texture2D>("CountingSheep");
            A9 = AscreenContent.Load<Texture2D>("Enjoy");
            A10 = AscreenContent.Load<Texture2D>("INeedAMedic");
            A11 = AscreenContent.Load<Texture2D>("ItTasteLikeLaser");
            A12 = AscreenContent.Load<Texture2D>("Fortnight");
            click = AscreenContent.Load<SoundEffect>("clickSound"); 

            data = new SaveGameData();

            data.tempAchievements = new List<int>(); 

            //for (int i = 0; i < 13; i++)
            //{
            //    data.tempAchievements.Add(0);
            //}
        }

        public override void Update(GameTime gameTime)
        {
            gps = GamePad.GetState(playerOne);

            if (gps.Buttons.B == ButtonState.Pressed && ogps.Buttons.B == ButtonState.Released)
            {
                screenEvent.Invoke(this, new EventArgs());
            }

            if (gps.Buttons.RightShoulder == ButtonState.Pressed && ogps.Buttons.RightShoulder == ButtonState.Released && changeScreen == false)
            {
                changeScreen = true;
                click.Play();
            }

            if (gps.Buttons.LeftShoulder == ButtonState.Pressed && ogps.Buttons.LeftShoulder == ButtonState.Released && changeScreen == true)
            {
                changeScreen = false;
                click.Play(); 
            }


            if (loaded == true)
            {
                if (data.tempAchievements[0] == 1)
                    Ua1 = true;
                if (data.tempAchievements[1] == 1)
                    Ua2 = true;
                if (data.tempAchievements[2] == 1)
                    Ua3 = true;
                if (data.tempAchievements[3] == 1)
                    Ua4 = true;
                if (data.tempAchievements[4] == 1)
                    Ua5 = true;
                if (data.tempAchievements[5] == 1)
                    Ua6 = true;
                if (data.tempAchievements[6] == 1)
                    Ua7 = true;
                if (data.tempAchievements[7] == 1)
                    Ua8 = true;
                if (data.tempAchievements[8] == 1)
                    Ua9 = true;
                if (data.tempAchievements[9] == 1)
                    Ua10 = true;
                if (data.tempAchievements[10] == 1)
                    Ua11 = true;
                if (data.tempAchievements[11] == 1)
                    Ua12 = true;

                //DoLoadGame(device);
            }

            ogps = gps;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (changeScreen == false)
            {
                spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
                if (Ua1)
                    spriteBatch.Draw(A1, Vector2.Zero, Color.White);
                if (Ua2)
                    spriteBatch.Draw(A2, Vector2.Zero, Color.White);
                if (Ua3)
                    spriteBatch.Draw(A3, Vector2.Zero, Color.White);
                if (Ua4)
                    spriteBatch.Draw(A4, Vector2.Zero, Color.White);
                if (Ua5)
                    spriteBatch.Draw(A5, Vector2.Zero, Color.White);
                if (Ua6)
                    spriteBatch.Draw(A6, Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.Draw(backgroundTexture2, Vector2.Zero, Color.White);
                if (Ua7)
                    spriteBatch.Draw(A7, Vector2.Zero, Color.White);
                if (Ua8)
                    spriteBatch.Draw(A8, Vector2.Zero, Color.White);
                if (Ua9)
                    spriteBatch.Draw(A9, Vector2.Zero, Color.White);
                if (Ua10)
                    spriteBatch.Draw(A10, Vector2.Zero, Color.White);
                if (Ua11)
                    spriteBatch.Draw(A11, Vector2.Zero, Color.White);
                if (Ua12)
                    spriteBatch.Draw(A12, Vector2.Zero, Color.White);
            }
        }

        public void DoLoadGame(StorageDevice device)
        {
            try
            {
                result = device.BeginOpenContainer("FortNightGameSave", null, null);
                result.AsyncWaitHandle.WaitOne();
                container = device.EndOpenContainer(result);

                result.AsyncWaitHandle.Close();

                string filename = "FortNightGameSave.sav";

                if (!container.FileExists(filename))
                {
                    data = new SaveGameData();
                    data.tempKills = 0;
                    data.tempScore = 0;
                    data.tempNumberOfNights = 0;
                    data.tempAchievements = new List<int>();

                    for (int i = 0; i < 13; i++)
                    {
                        data.tempAchievements.Add(0);
                    }

                    Debug.WriteLine("Created a new file, because another did not exist. LOAD");

                    container.Dispose();
                    return;
                }
                else
                {
                    Stream stream = container.OpenFile(filename, FileMode.Open);
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
                    SaveGameData data2 = (SaveGameData)serializer.Deserialize(stream);

                    data = data2;
                    loaded = true;
                    stream.Close();
                    container.Dispose();
                }
            }
            catch (Exception ex)
            {
                device = null;
                Debug.WriteLine("An error occured while loading ACHIEVEMENTS: " + ex);
            }
        }
    }
}