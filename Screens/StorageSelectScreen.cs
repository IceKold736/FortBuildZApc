using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Xml;
using System.Collections.Generic;

namespace FortBuildZApc
{
    public class StorageSelectScreen : Screen
    {
        public StorageDevice device;
        public StorageContainer container;
        public IAsyncResult result;
        public Object stateobj;

        SpriteFont regArial;

        public GamePadState currentState;

        public bool goBack;
        public bool GameSaveRequested = false;
        public bool saveDevice;
        public bool firstPass = true;

        Texture2D bac;

        public int realKills = 0;
        public int realScore = 0;
        public int realScore2 = 0;
        public int realScore3 = 0;
        public int realNumberOfNights = 0;
        public int realNumberOfNights2 = 0;
        public int realNumberOfNights3 = 0;
        public List<int> realAchievements;

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

        public StorageSelectScreen(ContentManager TscreenContent, EventHandler TScreenEvent) : base(TScreenEvent)
        {
            bac = TscreenContent.Load<Texture2D>("storageScreen");
            regArial = TscreenContent.Load<SpriteFont>("SpriteFont1");

            realAchievements = new List<int>();
            data.tempAchievements = new List<int>();
            
            for (int i = 0; i < 13; i++)
            {
                realAchievements.Add(0);
                data.tempAchievements.Add(0);
            }
        }

        public override void Update(GameTime gameTime)
        {
            GamePadState previousState = currentState;
            currentState = GamePad.GetState(playerOne);

            KeyboardState state = Keyboard.GetState();

            if ((currentState.Buttons.A == ButtonState.Pressed) && (previousState.Buttons.A == ButtonState.Released) || state.IsKeyDown(Keys.A))
            {
                if ((!Guide.IsVisible) && (GameSaveRequested == false))
                {
                    GameSaveRequested = true;

                    try
                    {
                        result = StorageDevice.BeginShowSelector(null, null);
                    }
                    catch (Exception e)
                    {
                        device = null;
                        Debug.WriteLine("An error occured while loading UPDATE: " + e);
                    }

                    goBack = false;
                    Debug.WriteLine("User selected device");
                }
            }

            if ((currentState.Buttons.B == ButtonState.Pressed) && (previousState.Buttons.B == ButtonState.Released))
            {
                if (!Guide.IsVisible)
                {
                    // Reset the device 
                    device = null;

                    goBack = true;
                    Debug.WriteLine("The User did not select a device. Make sure this is null: " + device);
                    screenEvent.Invoke(this, new EventArgs());
                }  
            }
            
            if ((GameSaveRequested) && (result.IsCompleted))
            {
                device = StorageDevice.EndShowSelector(result);

                if (firstPass == true)
                {
                    Debug.WriteLine("First pass done. Make sure this device is not null: " + device);
                    saveDevice = true;
                    screenEvent.Invoke(this, new EventArgs());
                }

                GameSaveRequested = false;
            }

            base.Update(gameTime);
        }

        public void DoSaveGame(StorageDevice device)
        {
                try
                {
                    Debug.WriteLine("About to save game");

                    if (realKills > data.tempKills)
                    {
                        data.tempKills = realKills;
                        Debug.WriteLine("Updated Kills");
                    }

                    if (realScore > data.tempScore)
                    {
                        data.tempScore = realScore;
                        Debug.WriteLine("Updated Score");
                    }
                    if (realScore2 > data.tempScore2)
                    {
                        data.tempScore2 = realScore2;
                        Debug.WriteLine("Updated Score");
                    }
                    if (realScore3 > data.tempScore3)
                    {
                        data.tempScore3 = realScore3;
                        Debug.WriteLine("Updated Score");
                    }

                    if (realNumberOfNights > data.tempNumberOfNights)
                    {
                        data.tempNumberOfNights = realNumberOfNights;
                        Debug.WriteLine("Updated NumberOfNights");
                    }

                    if (realNumberOfNights2 > data.tempNumberOfNights2)
                    {
                        data.tempNumberOfNights2 = realNumberOfNights2;
                        Debug.WriteLine("Updated NumberOfNights");
                    }
                    if (realNumberOfNights3 > data.tempNumberOfNights3)
                    {
                        data.tempNumberOfNights3 = realNumberOfNights3;
                        Debug.WriteLine("Updated NumberOfNights");
                    }

                    for (int i = 0; i < 13; i++)
                    {
                        if (realAchievements[i] > data.tempAchievements[i])
                        {
                            data.tempAchievements[i] = realAchievements[i];
                            Debug.WriteLine("Updated Achievements");
                        }
                    }

                    result = device.BeginOpenContainer("FortNightGameSave", null, null);
                    result.AsyncWaitHandle.WaitOne();
                    container = device.EndOpenContainer(result);

                    result.AsyncWaitHandle.Close();

                    string filename = "FortNightGameSave.sav";

                    if (container.FileExists(filename))
                    {
                        container.DeleteFile(filename);
                        Debug.WriteLine("Save file deleted.");
                    }
                    else
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

                        Debug.WriteLine("Created a new file, because another did not exist. SAVE");
                    }

                    Stream stream = container.CreateFile(filename);
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
                    serializer.Serialize(stream, data);
                    stream.Close();
                    container.Dispose();

                    //Debug.WriteLine("tempExampleScore1: " + data.tempExampleScore1);
                    //Debug.WriteLine("tempExampleScore2: " + data.tempExampleScore2);

                    Debug.WriteLine("Save file updated.");
                    Debug.WriteLine("Saving complete, but screen not finished. Make sure this is not null: " + device);
                }
                catch (Exception e)
                {
                    device = null;
                    Debug.WriteLine("An error occured while loading SAVE: " + e);
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
                        stream.Close();
                        container.Dispose();
                    }

                    Debug.WriteLine("Finished loading");
                }
                catch (Exception e)
                {
                    device = null;
                    Debug.WriteLine("An error occured while loading LOAD: " + e);
                }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bac, Vector2.Zero, Color.White);
        }
    }
}
