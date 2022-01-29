
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;

namespace FortBuildZApc
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Screen currentScreen;
        SpriteFont regArial;
        GamePadState currentPadState;
        GameTime gameTime;

        SoundEffect menuMusic;
        SoundEffectInstance menuMusicI;

        bool isMusicOff = false; 

        Texture2D pleaseBuy;
        bool drawpb = false;
        bool deviceSelected = false;
        bool isSignInDone;
        public int screenSelectedFrom;
        StorageDevice permDevice;

        //Screens
        ControllerDetectScreen controllerScreen;
        TitleScreen titleScreen;
        StorageSelectScreen storageScreen;
        MainLevelScreen mainLevelScreen;
        HighScoreScreen highScoreScreen;
        WarningScreen warningScreen;
        CreditScreen creditScreen;
        AchievementScreen achievementScreen;

        SignInScreen signInScreen;
        preGameScreen siScreen;
        MultiplayerScreen multiScreen;
        MultiplayerMainLevelScreen multiplayerMainLevelScreen; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;

            this.Components.Add(new GamerServicesComponent(this));

            //this.graphics.PreferMultiSampling = false;
        }


   
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            controllerScreen = new ControllerDetectScreen(this.Content, new EventHandler(controllerDetectScreenEvent));
            titleScreen = new TitleScreen(this.Content, new EventHandler(titleScreenEvent));
            storageScreen = new StorageSelectScreen(this.Content, new EventHandler(storageScreenEvent));
            mainLevelScreen = new MainLevelScreen(this.Content, new EventHandler(mainLevelScreenEvent));
            highScoreScreen = new HighScoreScreen(this.Content, new EventHandler(highScoreScreenEvent));
            warningScreen = new WarningScreen(this.Content, new EventHandler(warningScreenEvent));
            creditScreen = new CreditScreen(this.Content, new EventHandler(creditScreenEvent));
            achievementScreen = new AchievementScreen(this.Content, new EventHandler(achievementScreenEvent));

            siScreen = new preGameScreen(this.Content, new EventHandler(siScreenEvent));
            signInScreen = new SignInScreen(this.Content, new EventHandler(signInScreenEvent));
            multiScreen = new MultiplayerScreen(this.Content, new EventHandler(multiScreenEvent));
            multiplayerMainLevelScreen = new MultiplayerMainLevelScreen(this.Content, new EventHandler(multiplayerMainLevelScreenEvent));

            //Set the current screen
            currentScreen = controllerScreen;

            pleaseBuy = Content.Load<Texture2D>("pleasebuy");
            regArial = Content.Load<SpriteFont>("SpriteFont1");

            menuMusic = Content.Load<SoundEffect>("MenuMusic");
            menuMusicI = menuMusic.CreateInstance();
            menuMusicI.IsLooped = true;
            menuMusicI.Volume = 1f;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if ((menuMusicI.State == SoundState.Stopped || menuMusicI.State == SoundState.Paused) && isMusicOff == false)
            {
                menuMusicI.Play();
            }

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();

            currentPadState = GamePad.GetState(Screen.playerOne);

            currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            currentScreen.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        /*
         * My Methods
         */

        public void signInScreenEvent(object obj, EventArgs e)
        {
            if (signInScreen.goBack == false)
            {
                if (!Guide.IsVisible)
                {
                    try
                    {
                        Guide.ShowSignIn(1, true);
                        try
                        {
                            SignedInGamer currentGamer = Gamer.SignedInGamers[Screen.playerOne];

                            if (currentGamer != null && currentGamer.Privileges.AllowPurchaseContent)
                            {
                                Guide.ShowMarketplace(Screen.playerOne);
                                currentScreen = titleScreen;
                            }
                            else
                                currentScreen = signInScreen;
                        }
                        catch (Exception e3)
                        {
                            Debug.WriteLine("Error occured while purchasing: " + e3);
                        }
                    }
                    catch (Exception e2)
                    {
                        Debug.WriteLine("Error occured while signing in: " + e2);
                    }
                }
            }
            else
            {
                currentScreen = titleScreen;
            }
        }
      
        public void controllerDetectScreenEvent(object obj, EventArgs e)
        {
            if (Gamer.SignedInGamers != null)
            {
                isSignInDone = true;
            }

            if (Guide.IsTrialMode == true)
            {
                currentScreen = titleScreen;
            }
            else
            {
                if (!Guide.IsVisible && !isSignInDone)
                {
                    try
                    {
                        Guide.ShowSignIn(1, true);
                        isSignInDone = true;
                        currentScreen = storageScreen;
                    }
                    catch (Exception e4)
                    {
                        Debug.WriteLine("Error occured while signing in at start: " + e4);
                    }
                }
                else
                {
                    currentScreen = storageScreen;
                }
            }
        }


        public void storageScreenEvent(object obj, EventArgs e)
        {
            if (storageScreen.goBack == true)
            {
                deviceSelected = false;
                permDevice = null;
                currentScreen = warningScreen;
                Debug.WriteLine("The user quit the storage screen.");
            }
            else
            {
                if (storageScreen.saveDevice == true)
                {
                    deviceSelected = true;
                    permDevice = storageScreen.device;
                    highScoreScreen.device = permDevice;
                    storageScreen.firstPass = false;
                }

                Debug.WriteLine("This is the permanent device for the game. Make sure it is not null: " + permDevice);
                Debug.WriteLine("This is the storage device stored in the storage screen. Make sure it is not null: " + storageScreen.device);
                Debug.WriteLine("This is the storage device stored in the high scores screen. Make sure it is not null: " + highScoreScreen.device);

                if (permDevice != null)
                {
                    storageScreen.DoLoadGame(permDevice); //Creates a file bitchhoe. 
                }

                currentScreen = titleScreen;
            }
        }

        public void highScoreScreenEvent(object obj, EventArgs e)
        {
            if (highScoreScreen.gotoStorage == true)
            {
                highScoreScreen.gotoStorage = false;
                storageScreen.device = null;
                permDevice = null;
                highScoreScreen.device = null;
                deviceSelected = false;
                storageScreen.firstPass = true;
                currentScreen = storageScreen;
            }
            else
            {
                currentScreen = titleScreen;
            }
        }

        public void titleScreenEvent(object obj, EventArgs e)
        {
            if (Guide.IsTrialMode == false)
            {
                switch (titleScreen.buttonPressed)
                {
                    case 1: currentScreen = siScreen; break;
                    case 2: currentScreen = highScoreScreen; break;
                    case 3: { currentScreen = achievementScreen; screenSelectedFrom = 1; achievementScreen.DoLoadGame(permDevice); } break;
                    case 4: currentScreen = creditScreen; break;
                    case 5: Exit(); break;
                }
            }
            else
            {
                switch (titleScreen.buttonPressed)
                {
                    case 1: currentScreen = siScreen; break;
                    case 2:
                        {
                            if (Gamer.SignedInGamers != null)
                            {
                                isSignInDone = true;
                            }

                            if (!Guide.IsVisible && !isSignInDone)
                            {
                                try
                                {
                                    Guide.ShowSignIn(1, true);
                                    isSignInDone = true;
                                    try
                                    {
                                        SignedInGamer currentGamer = Gamer.SignedInGamers[Screen.playerOne];

                                        if (currentGamer != null && currentGamer.Privileges.AllowPurchaseContent)
                                        {
                                            Guide.ShowMarketplace(Screen.playerOne);
                                            currentScreen = titleScreen;
                                        }
                                        else
                                            currentScreen = signInScreen;
                                    }
                                    catch (Exception e3)
                                    {
                                        Debug.WriteLine("Error occured while purchasing: " + e3);
                                    }
                                    currentScreen = titleScreen;
                                }
                                catch (Exception e2)
                                {
                                    Debug.WriteLine("Error occured while signing in: " + e2);
                                }
                            }
                            else
                            {
                                try
                                {
                                    SignedInGamer currentGamer = Gamer.SignedInGamers[Screen.playerOne];

                                    if (currentGamer != null && currentGamer.Privileges.AllowPurchaseContent)
                                    {
                                        Guide.ShowMarketplace(Screen.playerOne);
                                        currentScreen = titleScreen;
                                    }
                                    else
                                        currentScreen = signInScreen;
                                }
                                catch (Exception e3)
                                {
                                    Debug.WriteLine("Error occured while purchasing: " + e3);
                                }
                            }
                        } break;
                    case 3: Exit(); break;
                }
            }
        }

        public void mainLevelScreenEvent(object obj, EventArgs e)
        {
            if (mainLevelScreen.pauseSelection == 4)
            {
                if (Guide.IsTrialMode == false)
                {
                    storageScreen.realKills = mainLevelScreen.kills;
                    storageScreen.realScore = mainLevelScreen.score;
                    storageScreen.realNumberOfNights = mainLevelScreen.numberOfNights;

                    for (int i = 0; i < 13; i++)
                    {
                        storageScreen.realAchievements[i] = mainLevelScreen.achievementsUnlocked[i];
                    }

                    if (permDevice != null)
                    {
                        storageScreen.DoSaveGame(permDevice);
                        storageScreen.DoLoadGame(permDevice);
                        Debug.WriteLine("NIGGER");
                        currentScreen = titleScreen;
                        isMusicOff = false;
                    }
                    else
                    {
                        currentScreen = titleScreen;
                        isMusicOff = false;
                    }

                }
                else
                {
                    currentScreen = titleScreen;
                    isMusicOff = false;
                }

                //currentScreen = titleScreen;
                //mainLevelScreen.resetAll(gameTime);
            }
            else if (mainLevelScreen.pauseSelection == 3)
            {
                if (Guide.IsTrialMode == false)
                {
                    storageScreen.realKills = mainLevelScreen.kills;
                    storageScreen.realScore = mainLevelScreen.score;
                    storageScreen.realNumberOfNights = mainLevelScreen.numberOfNights;

                    for (int i = 0; i < 13; i++)
                    {
                        storageScreen.realAchievements[i] = mainLevelScreen.achievementsUnlocked[i];
                    }

                    if (permDevice != null)
                    {
                        storageScreen.DoSaveGame(permDevice);
                        storageScreen.DoLoadGame(permDevice);
                        currentScreen = achievementScreen;
                        achievementScreen.DoLoadGame(permDevice);
                        isMusicOff = false;
                    }
                    else
                    {
                        currentScreen = achievementScreen;
                        achievementScreen.DoLoadGame(permDevice);
                        isMusicOff = false;
                    }

                }
                else
                {
                    currentScreen = achievementScreen;
                    achievementScreen.DoLoadGame(permDevice);
                    isMusicOff = false;
                }
            }
            else
            {
                if (mainLevelScreen.dead == true)
                {
                    if (Guide.IsTrialMode == false)
                    {
                        currentScreen = highScoreScreen;
                        isMusicOff = false;
                    }
                    else
                    {
                        currentScreen = titleScreen;
                        isMusicOff = false;
                    }
                }
            }
        }

        public void siScreenEvent(object obj, EventArgs e)
        {
            if (siScreen.goBack == true)
            {
                siScreen.goBack = false;
                currentScreen = titleScreen;
            }
            else
            {
                mainLevelScreen = new MainLevelScreen(this.Content, new EventHandler(mainLevelScreenEvent));

                currentScreen = multiScreen;
            }
        }

        public void warningScreenEvent(object obj, EventArgs e)
        {
            if (warningScreen.goBack == true)
                currentScreen = storageScreen;
            else
                currentScreen = titleScreen;
        }

        public void creditScreenEvent(object obj, EventArgs e)
        {
            currentScreen = titleScreen;
        }

        public void multiplayerMainLevelScreenEvent(object obj, EventArgs e)
        {
            if (multiplayerMainLevelScreen.pauseSelection == 4)
            {
                if (Guide.IsTrialMode == false)
                {
                    storageScreen.realScore2 = multiplayerMainLevelScreen.score1;
                    storageScreen.realScore3 = multiplayerMainLevelScreen.score2;
                    storageScreen.realNumberOfNights2 = multiplayerMainLevelScreen.waveNum;

                    if (permDevice != null)
                    {
                        storageScreen.DoSaveGame(permDevice);
                        storageScreen.DoLoadGame(permDevice);
                        currentScreen = titleScreen;
                        isMusicOff = false;
                    }
                    else
                    {
                        currentScreen = titleScreen;
                        isMusicOff = false;
                    }

                }
                else
                {
                    currentScreen = titleScreen;
                    isMusicOff = false;
                }

                //currentScreen = titleScreen;
                //mainLevelScreen.resetAll(gameTime);
            }
            else if (multiplayerMainLevelScreen.pauseSelection == 3)
            {
                if (Guide.IsTrialMode == false)
                {
                    storageScreen.realScore2 = multiplayerMainLevelScreen.score1;
                    storageScreen.realScore3 = multiplayerMainLevelScreen.score2;
                    storageScreen.realNumberOfNights2 = multiplayerMainLevelScreen.waveNum;

                    if (permDevice != null)
                    {
                        storageScreen.DoSaveGame(permDevice);
                        storageScreen.DoLoadGame(permDevice);
                        currentScreen = achievementScreen;
                        achievementScreen.DoLoadGame(permDevice);
                        isMusicOff = false;
                    }
                    else
                    {
                        currentScreen = achievementScreen;
                        achievementScreen.DoLoadGame(permDevice);
                        isMusicOff = false;
                    }

                }
                else
                {
                    currentScreen = achievementScreen;
                    achievementScreen.DoLoadGame(permDevice);
                    isMusicOff = false;
                }
            }
            else
            {
                if (multiplayerMainLevelScreen.dead == true)
                {
                    if (Guide.IsTrialMode == false)
                    {
                        currentScreen = highScoreScreen;
                        isMusicOff = false;
                    }
                    else
                    {
                        currentScreen = titleScreen;
                        isMusicOff = false;
                    }
                }
            }
        }

        public void multiScreenEvent(object obj, EventArgs e)
        {
            if (multiScreen.goBack == true)
            {
                multiScreen.goBack = false;
                multiScreen.currentBac = 0; 
                currentScreen = titleScreen;
            }
            else
            {
                if (multiScreen.currentSel == 0)
                {
                    multiplayerMainLevelScreen.loadingAlphaValue = 255;
                    multiplayerMainLevelScreen.isLoading = true; 
                    currentScreen = mainLevelScreen;
                    multiScreen.currentBac = 0; 
                }
                else
                {
                    multiplayerMainLevelScreen = new MultiplayerMainLevelScreen(this.Content, new EventHandler(multiplayerMainLevelScreenEvent));
                    multiplayerMainLevelScreen.loadingAlphaValue = 255; 
                    multiplayerMainLevelScreen.isLoading = true; 
                    currentScreen = multiplayerMainLevelScreen;
                    multiScreen.currentBac = 0; 
                }


                menuMusicI.Stop();
                isMusicOff = true;
            }
        }

        public void achievementScreenEvent(object obj, EventArgs e)
        {
            if (screenSelectedFrom == 1)
            {
                currentScreen = titleScreen;
                screenSelectedFrom = 0;
            }
            else
            {
                currentScreen = mainLevelScreen;
                menuMusicI.Stop();
                isMusicOff = true; 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GraphicsDevice.Clear(Color.YellowGreen);

            if (drawpb == true)
                spriteBatch.Draw(pleaseBuy, Vector2.Zero, Color.White);
        }
    }
}