using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Xml;
using System.Collections.Generic;

namespace FortBuildZApc
{
    public class HighScoreScreen : Screen
    {
        public StorageDevice device;
        public StorageContainer container;
        public IAsyncResult result;
        public Object stateobj;

        SpriteFont regArial;

        public GamePadState currentState;
        public GamePadState previousState;

        public bool deviceSelected;
        public bool gotoStorage = false;
        bool drawdraw = false;
        Texture2D bac;

        bool doneonce = false; 

        public struct SaveGameData
        {
            public int tempKills;
            public int tempScore;
            public int tempNumberOfNights;
            public int tempScore2;
            public int tempNumberOfNights2;
            public int tempScore3;
            public int tempNumberOfNights3;
            public List<int> tempAchievements;
        }

        SaveGameData data = new SaveGameData();

        public HighScoreScreen(ContentManager TscreenContent, EventHandler TScreenEvent) : base(TScreenEvent)
        {
            bac = TscreenContent.Load<Texture2D>("highScores");
            regArial = TscreenContent.Load<SpriteFont>("bigFont");
        }

        public override void Update(GameTime gameTime)
        {
            currentState = GamePad.GetState(playerOne);

            if ((currentState.Buttons.B == ButtonState.Pressed) && (previousState.Buttons.B == ButtonState.Released))
            {
                screenEvent.Invoke(this, new EventArgs());
            }

            if (device == null)
            {
                if ((currentState.Buttons.Y == ButtonState.Pressed) && (previousState.Buttons.Y == ButtonState.Released))
                {
                    gotoStorage = true;
                    screenEvent.Invoke(this, new EventArgs());
                }
            }

            if (device != null && device.IsConnected)
            {
                if (!device.IsConnected)
                {
                    gotoStorage = true;
                    screenEvent.Invoke(this, new EventArgs());
                }

                if (doneonce == false)
                {
                    DoLoadGame(device);
                    doneonce = true; 
                }

                drawdraw = true;
            }

            previousState = currentState;

            base.Update(gameTime);
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
                    data.tempScore2 = 0;
                    data.tempNumberOfNights2 = 0;
                    data.tempScore3 = 0;
                    data.tempNumberOfNights3 = 0;
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
                    Debug.WriteLine("Loaded!"); 
                    stream.Close();
                    container.Dispose();
                }
            }
            catch (Exception ex)
            {
                device = null;
                Debug.WriteLine("An error occured while loading: " + ex);
            }
        }

        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bac, Vector2.Zero, Color.White);

            if (device != null)
            {
                if (drawdraw == true)
                {
                    spriteBatch.DrawString(regArial, data.tempKills.ToString(), new Vector2(220, 300), Color.White);
                    spriteBatch.DrawString(regArial, data.tempNumberOfNights.ToString(), new Vector2(640, 300), Color.White);
                    spriteBatch.DrawString(regArial, data.tempScore.ToString(), new Vector2(915, 300), Color.White);

                    
                    spriteBatch.DrawString(regArial, data.tempNumberOfNights2.ToString(), new Vector2(600, 500), Color.White);
                    spriteBatch.DrawString(regArial, data.tempScore2.ToString(), new Vector2(180, 500), Color.White);
                    spriteBatch.DrawString(regArial, data.tempScore3.ToString(), new Vector2(870, 500), Color.White);
                }
                else
                {
                    spriteBatch.DrawString(regArial, "Loading...", new Vector2(300, 300), Color.White);
                    spriteBatch.DrawString(regArial, "DO NOT REMOVE STORAGE DEVICE!", new Vector2(300, 350), Color.White);
                }
            }
            else
            {
                spriteBatch.DrawString(regArial, "No storage device.", new Vector2(300, 260), Color.White);
                spriteBatch.DrawString(regArial, "Press Y to select one.", new Vector2(300, 300), Color.White);
            }
        }
    }
}