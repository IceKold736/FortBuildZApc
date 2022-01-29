using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace FortBuildZApc
{
    public class MultiplayerMainLevelScreen : Screen
    {
        SpriteFont font;
        SpriteFont fontBig;
        SpriteFont pixelfont;
        Rectangle levelBoundary;

        #region Sounds

        SoundEffect smgShot;
        SoundEffect pistolShot;
        SoundEffect shotgunShot;
        SoundEffect sniperShot;
        SoundEffect sniperCock;
        SoundEffect gunClick;
        public bool isGunClickPlaying = false;
        public bool isGunClickPlaying2 = false;

        SoundEffect nightMusic;
        SoundEffectInstance nightMusicI;

        SoundEffect dayMusic;
        SoundEffectInstance dayMusicI;

        SoundEffect click;
        SoundEffect hit1;
        SoundEffect hit2;

        #endregion

        #region Player Stuff

        public Character player;
        public Texture2D characterTex;
        public Vector2 characterPos;
        public Vector2 previousCharacterPos;

        public Character2 player2;
        public Texture2D characterTex2;
        public Vector2 characterPos2;
        public Vector2 previousCharacterPos2;

        Shadow shadow;
        Shadow shadow2;
        public Texture2D shadowTexture;
        public Vector2 shadowPosition;
        public Vector2 shadowPosition2;

        public int playerRadius = 24;
        public int playerHitRadius = 36;

        public Texture2D manos;
        public Hands hands;
        public Crosshair crosshair;
        public Texture2D reticle;

        public Hands2 hands2;
        public Crosshair2 crosshair2;

        public Texture2D deadScreen;
        public int deathAlphaValue = 1;

        public int playerGridPosX = 28;
        public int playerGridPosY = 34;
        public int crosshairGridPosX;
        public int crosshairGridPosY;

        public int playerGridPosX2 = 28;
        public int playerGridPosY2 = 34;
        public int crosshairGridPosX2;
        public int crosshairGridPosY2;

        public int crosshairPos;
        public int crosshairPos2;

        public Vector2 fixedPos = new Vector2(2048, 2048);
        public Vector2 fixedPos2 = new Vector2(1408, 1688);
        public Vector2 fixedPosP1 = new Vector2(2048, 2048);
        public Vector2 fixedPos2P1 = new Vector2(1408, 1688);
        public Vector2 fixedPosP2 = new Vector2(2048, 2048);
        public Vector2 fixedPos2P2 = new Vector2(1408, 1688);
        public Vector2 crosshairCenter;
        public Vector2 crosshairCenter2;

        int gridLocX;
        int gridLocY;

        public List<int> currentWeaponAavailable;

        public bool chooseWeapon = false;

        int choseWeapon = 0;

        public List<int> currentWeaponAavailable2;

        public bool chooseWeapon2 = false;

        int choseWeapon2 = 0;


        Texture2D muzzleFlare1;
        bool drawMuzzleFlare1 = false;

        Texture2D muzzleFlare2;
        bool drawMuzzleFlare2 = false;

        Texture2D muzzleFlare3;
        bool drawMuzzleFlare3 = false;

        bool drawMuzzleFlare12 = false;

        bool drawMuzzleFlare22 = false;

        bool drawMuzzleFlare32 = false;

        Texture2D bloodSplash;
        public bool dead = false;
        public float deadTime = 2f;

        #endregion

        #region Guns

        public Texture2D handGun;
        public Pistol pistol;
        public int pistolClip = 16;
        public int smgClip = 30;
        public int shotgunClip = 8;
        public int sniperClip = 5;
        public bool hasReloaded = true;

        public Texture2D machineGun;
        public SMG smg;
        public Texture2D machineGunFire;

        public Texture2D shotty;
        public Shotgun shotgun;

        public Texture2D barrett;
        public Sniper sniper;

        public Pistol2 pistol2;
        public int pistolClip2 = 16;
        public int smgClip2 = 30;
        public int shotgunClip2 = 8;
        public int sniperClip2 = 5;
        public bool hasReloaded2 = true;

        public SMG2 smg2;
        public Shotgun2 shotgun2;
        public Sniper2 sniper2;

        public int bulletRadius = 5;

        #endregion

        #region Zombies

        int zombieRadius = 24;
        int zombieSide;

        Texture2D zombie1;
        Vector2 zombie1Pos;

        float zombie1X;
        float zombie1Y;
        int zombie1A;

        Texture2D zombie2;
        Vector2 zombie2Pos;

        float zombie2X;
        float zombie2Y;
        int zombie2A;

        Texture2D zombie3;
        Vector2 zombie3Pos;

        float zombie3X;
        float zombie3Y;
        int zombie3A;

        Texture2D bloodTex;

        #endregion

        #region Trees

        public int treeRadius = 50;

        Texture2D tree1Tex;
        Texture2D tree2Tex;
        int treeA;

        Rectangle leftBRect;
        Rectangle rightBRect;
        Rectangle topBRect;
        Rectangle botBRect;

        #endregion

        #region Rocks

        public int rockRadius = 50;

        Texture2D rock1Tex;
        int rockA;

        #endregion

        #region Dunes

        public int duneRadius = 50;

        Texture2D dune1Tex;
        int duneA;

        #endregion

        #region Health

        public Texture2D health;
        public Vector2 healthPosition = new Vector2(160, 40);
        public Vector2 healthPosition2 = new Vector2(1150, 40);
        //public int healthDamageTaken = 0;
        //public int healthFrames = 3;

        public Rectangle healthSourceRectangle;

        #endregion

        #region Terrain Tiles

        Texture2D grass;
        Vector2 grassPosition = new Vector2(0, 0);

        Texture2D verticalBoundaryTexture;
        Texture2D horizontalBoundaryTexture;

        Vector2 topPosition = new Vector2(0, 8); //add 8 for grid
        Vector2 leftPosition = new Vector2(16, 0); //add 16 for grid
        Vector2 bottomPosition = new Vector2(0, 3728); //subtract 8 for grid
        Vector2 rightPosition = new Vector2(3440, 0); //subtract 16 for grid

        #endregion

        #region Screens

        public Texture2D pauseMenuTexture;
        public Texture2D musicOff;
        public Texture2D pauseSelector;
        public Vector2 pausePosition = Vector2.Zero;
        public Vector2 pauseSelectorPosition;

        public bool isMenuActive = false;
        public bool isMusicOff = false;
        public int isMenuOn = 1;
        public int pauseSelection = 1;
        public int pauseMax = 4;
        public int isMusicNum = 1;





        #endregion

        #region Inventory

        public Inventory inventory;

        public Inventory inventory2;

        public Vector2 numberOfNightsPosition;
        public int numberOfNights = 0;
        public int previousNumberOfNights;
        public int kills;
        public Vector2 killsPosition;
        public int score1;
        public Vector2 scorePosition1;
        public int score2;
        public Vector2 scorePosition2;

        # endregion

        #region MiniInventory

        Texture2D miniTexture;
        Vector2 miniPosition;
        Vector2 miniPosition2;

        public MiniInventory miniInventory;

        public MiniInventory miniInventory2;

        #endregion

        #region Bullets

        public Texture2D bullet;
        public Vector2 bulletPosition;

        #endregion

        public Texture2D collisionBox;
        public Vector2 scrollOffset;
        public Vector2 screenCenter;

        #region Rectangles

        public Rectangle playerRectangle;

        #endregion


        #region Lists

        public List<Health> healthList;
        public List<Health> healthList2;
        public List<Bullet> bulletList;
        public List<Bullet> bulletList2;
        public List<Zombie1> zombie1List;
        public List<Zombie2> zombie2List;
        public List<Zombie3> zombie3List;
        public List<Tree1> tree1List;
        public List<Tree2> tree2List;
        public List<Rock1> rock1List;
        public List<Dune1> dune1List;
        public List<Blood> bloodList;


        #endregion

        #region States

        KeyboardState ks;
        KeyboardState oks;
        // MouseState ms;
        public GamePadState gps;
        public GamePadState ogps;
        public GamePadState gps2;
        public GamePadState ogps2;

        #endregion

        #region Times

        TimeSpan fireTime;
        TimeSpan fireTime2;

        float reloadTimer;
        float shotgunTimer;
        float sniperTimer;
        float sniperCockTimer;

        float energyBoost;
        bool startEnergyBoost;
        int noEnergy = 1;

        TimeSpan previousTime;
        TimeSpan healthTime;
        TimeSpan previousHealthTime;
        TimeSpan placementDamageTime;

        float reloadTimer2;
        float shotgunTimer2;
        float sniperTimer2;
        float sniperCockTimer2;

        TimeSpan previousTime2;
        TimeSpan healthTime2;
        TimeSpan previousHealthTime2;

        //For pause menu
        TimeSpan changeTime;
        TimeSpan prevChangeTime;
        TimeSpan DchangeTime;
        TimeSpan DprevChangeTime;

        int dayTime = 10;
        int nightTime = 300;
        int newZombies = 5;
        int oldZombies;

        bool isHeald = false;
        bool isHeald2 = false; 

        double nightDelay = 0.35;
        int nightAlphaValue = 1;
        int nightFadeIncrement = 3;

        bool isWaveOver = false;
        bool isDayPassed = false;

        double loadingTime = 0;
        float loadingTimer = 7;

        double loadingDelay = 0.35;
        public int loadingAlphaValue = 1;
        int loadingFadeIncrement = 3;

        public bool isLoading = false;
        public bool isBlack = false;

        float timer;

        Texture2D nightTex;
        Texture2D loadingTex;

        public bool isNightTexOn = true;
        public bool isLoadingTexOn = true;

        public int waveNum = 0;

        #endregion

        #region Grid

        Texture2D gridTex;
        public List<GridSquare> gridList;
        public List<bool> gridIsOccupied;

        int gridY;
        int gridX;

        Vector2 gridCenter;
        Vector2 gridCenter2;

        #endregion

        public bool isShooting = false;
        public bool isBreaking = false;

        public bool isShooting2 = false;
        public bool isBreaking2 = false;

        public bool drawBlood = false;
        public bool isVibrating = false;
        public float vibTime = 0f;

        public bool isVibrating2 = false;
        public float vibTime2 = 0f;

        public bool isReset = false;

        public Random random = new Random();

        #region Constructor/LoadContent
        public MultiplayerMainLevelScreen(ContentManager MLscreenContent, EventHandler MLscreenEvent)
            : base(MLscreenEvent)
        {
            //Random
            levelBoundary = new Rectangle(0, 0, 1280, 720);

            #region Textures

            font = MLscreenContent.Load<SpriteFont>("spritefont1");
            fontBig = MLscreenContent.Load<SpriteFont>("bigFont");

            grass = MLscreenContent.Load<Texture2D>("PixelGrass"); //used to be "Grass"
            verticalBoundaryTexture = MLscreenContent.Load<Texture2D>("TopBoundary");
            horizontalBoundaryTexture = MLscreenContent.Load<Texture2D>("SideBoundary");

            //gridSquare = MLscreenContent.Load<Texture2D>("gridSquare");
            gridTex = MLscreenContent.Load<Texture2D>("grid");

            tree1Tex = MLscreenContent.Load<Texture2D>("Tree1");
            tree2Tex = MLscreenContent.Load<Texture2D>("Tree22");
            rock1Tex = MLscreenContent.Load<Texture2D>("Rock");
            //rock2Tex = MLscreenContent.Load<Texture2D>("rock2");
            dune1Tex = MLscreenContent.Load<Texture2D>("Dune");

            characterTex = MLscreenContent.Load<Texture2D>("CharacterSpriteSheet");
            characterTex2 = MLscreenContent.Load<Texture2D>("CharacterSpriteSheet2");
            shadowTexture = MLscreenContent.Load<Texture2D>("Shadow");
            manos = MLscreenContent.Load<Texture2D>("Hands");
            reticle = MLscreenContent.Load<Texture2D>("Reticle");
            handGun = MLscreenContent.Load<Texture2D>("Pistol");
            machineGun = MLscreenContent.Load<Texture2D>("SMG");
            machineGunFire = MLscreenContent.Load<Texture2D>("SMGFireAnimation");
            shotty = MLscreenContent.Load<Texture2D>("Shotgun");
            barrett = MLscreenContent.Load<Texture2D>("SniperRifle");
            bullet = MLscreenContent.Load<Texture2D>("Bullet");
            health = MLscreenContent.Load<Texture2D>("Health");

            zombie1 = MLscreenContent.Load<Texture2D>("Zombie");
            zombie2 = MLscreenContent.Load<Texture2D>("FastZombie");
            zombie3 = MLscreenContent.Load<Texture2D>("BigZombie");

            collisionBox = MLscreenContent.Load<Texture2D>("CollisionBox");

            pauseMenuTexture = MLscreenContent.Load<Texture2D>("PauseMenu");
            pauseSelector = MLscreenContent.Load<Texture2D>("PauseSelector3");
            musicOff = MLscreenContent.Load<Texture2D>("MusicOff");

            nightTex = MLscreenContent.Load<Texture2D>("Night");
            loadingTex = MLscreenContent.Load<Texture2D>("Black");
            deadScreen = MLscreenContent.Load<Texture2D>("YouDead");

            bloodTex = MLscreenContent.Load<Texture2D>("BloodSplatter");
            muzzleFlare1 = MLscreenContent.Load<Texture2D>("PistolFlare");
            muzzleFlare2 = MLscreenContent.Load<Texture2D>("ShotgunFlare");
            muzzleFlare3 = MLscreenContent.Load<Texture2D>("SniperFlare");
            bloodSplash = MLscreenContent.Load<Texture2D>("BloodSplash");

            miniTexture = MLscreenContent.Load<Texture2D>("MiniInventory");

            #endregion

            #region Sounds

            click = MLscreenContent.Load<SoundEffect>("clickSound");
            hit1 = MLscreenContent.Load<SoundEffect>("hit1Sound");
            hit2 = MLscreenContent.Load<SoundEffect>("hit2Sound");

            smgShot = MLscreenContent.Load<SoundEffect>("SMGShot_Real");
            pistolShot = MLscreenContent.Load<SoundEffect>("PistolShot");
            shotgunShot = MLscreenContent.Load<SoundEffect>("ShotgunShot");
            sniperShot = MLscreenContent.Load<SoundEffect>("SniperShot");
            sniperCock = MLscreenContent.Load<SoundEffect>("SniperCock");
            gunClick = MLscreenContent.Load<SoundEffect>("GunClick");

            nightMusic = MLscreenContent.Load<SoundEffect>("NightMusic");
            nightMusicI = nightMusic.CreateInstance();
            nightMusicI.IsLooped = true;
            nightMusicI.Volume = 1f;

            dayMusic = MLscreenContent.Load<SoundEffect>("DayMusic");
            dayMusicI = dayMusic.CreateInstance();
            dayMusicI.IsLooped = false;
            dayMusicI.Volume = 1f;

            #endregion

            #region Updating

            changeTime = TimeSpan.FromSeconds(0.3f);
            DchangeTime = TimeSpan.FromSeconds(0.3f);

            zombie1List = new List<Zombie1>();
            zombie2List = new List<Zombie2>();
            zombie3List = new List<Zombie3>();

            zombie1A = 0; //CHANGE
            zombie2A = 0; //CHANGE
            zombie3A = 0; //CHANGE

            characterPos = new Vector2(2248, 2048);
            characterPos2 = new Vector2(1848, 2048);

            player = new Character(characterPos, characterTex);
            shadow = new Shadow(shadowTexture, shadowPosition);
            hands = new Hands(player, manos);
            crosshair = new Crosshair(player, reticle);

            player2 = new Character2(characterPos2, characterTex2);
            shadow2 = new Shadow(shadowTexture, shadowPosition2);
            hands2 = new Hands2(player2, manos);
            crosshair2 = new Crosshair2(player2, reticle);

            crosshairCenter = new Vector2(crosshair.crosshairsPosition.X + crosshair.crosshairsTexture.Width / 2, crosshair.crosshairsPosition.Y + crosshair.crosshairsTexture.Height / 2);
            crosshairCenter2 = new Vector2(crosshair2.crosshairsPosition.X + crosshair2.crosshairsTexture.Width / 2, crosshair2.crosshairsPosition.Y + crosshair2.crosshairsTexture.Height / 2);
            
            pistol = new Pistol(player, handGun);
            smg = new SMG(player, machineGun);
            shotgun = new Shotgun(player, shotty);
            sniper = new Sniper(player, barrett);

            pistol2 = new Pistol2(player2, handGun);
            smg2 = new SMG2(player2, machineGun);
            shotgun2 = new Shotgun2(player2, shotty);
            sniper2 = new Sniper2(player2, barrett);

            currentWeaponAavailable = new List<int>();
            for (int i = 0; i < 12; i++)
                currentWeaponAavailable.Add(0);

            currentWeaponAavailable2 = new List<int>();
            for (int i = 0; i < 12; i++)
                currentWeaponAavailable2.Add(0);

            inventory = new Inventory(fontBig);
            miniPosition = new Vector2(30, 550);
            miniInventory = new MiniInventory(miniTexture, miniPosition);

            inventory2 = new Inventory(fontBig);
            miniPosition2 = new Vector2(1030, 550);
            miniInventory2 = new MiniInventory(miniTexture, miniPosition2);

            pauseSelectorPosition = new Vector2(442, 268);

            tree1List = new List<Tree1>();
            tree2List = new List<Tree2>();

            treeA = random.Next(1, 100); //amount of trees.

            rock1List = new List<Rock1>();
            //rock2List = new List<rock2>();
            rockA = random.Next(1, 50); //amount of rocks.

            dune1List = new List<Dune1>();
            duneA = random.Next(1, 25); //amount of dunes.

            bulletList = new List<Bullet>();
            bulletList2 = new List<Bullet>();

            healthTime = TimeSpan.FromSeconds(1f);
            healthTime2 = TimeSpan.FromSeconds(1f);
            placementDamageTime = TimeSpan.FromSeconds(1f);

            healthList = new List<Health>();
            healthList2 = new List<Health>();

            gridList = new List<GridSquare>();

            gridIsOccupied = new List<bool>();

            bloodList = new List<Blood>();

            //Loads it all once <3333
            for (gridX = 1; gridX <= 58; gridX++) //2816 + 16
            {
                for (gridY = 1; gridY <= 70; gridY++) //3376 + 32
                {
                    if (gridList.Count <= 4060)
                    {
                        addGrid(new Vector2(((int)(gridX * 48) + 608), ((int)(gridY * 48) + 320)), gridTex);
                    }
                }
            }

            #endregion
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            gps = GamePad.GetState(playerOne);
            gps2 = GamePad.GetState(playerTwo);


            if ((nightMusicI.State == SoundState.Stopped || nightMusicI.State == SoundState.Paused) && isMusicOff == false)
            {
                if (isWaveOver == true)
                    dayMusicI.Play();
            }

            if (gps.IsConnected == false)
            {
                isMenuActive = true;
            }

            //Multi - 1
            if (gps2.IsConnected == false)
            {
                isMenuActive = true;
            }

            //Multi - 2
            shadowPosition = new Vector2(player.characterPosition.X - shadowTexture.Width / 2, (int)player.characterPosition.Y - shadowTexture.Width / 2);
            shadowPosition2 = new Vector2(player2.characterPosition.X - shadowTexture.Width / 2, (int)player2.characterPosition.Y - shadowTexture.Width / 2);

            //Multi - 3
            if (player2.charHealth > 0 && player.charHealth > 0 && !Guide.IsVisible)
            {
                if (isMenuActive == false)
                {
                        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        foreach (Zombie1 z in zombie1List)
                        {
                            z.prevZombiePosition = z.zombiePosition;
                        }

                        foreach (Zombie2 z in zombie2List)
                        {
                            z.prevZombiePosition = z.zombiePosition;
                        }

                        foreach (Zombie3 z in zombie3List)
                        {
                            z.prevZombiePosition = z.zombiePosition;
                        }


                        if (player.characterPosition.X > 1280)
                        {
                            player.characterPosition.X = 1280; 
                        }

                        if (player.characterPosition.X < 0)
                        {
                            player.characterPosition.X = 0;
                        }
                        if (player2.characterPosition.X > 1280)
                        {
                            player2.characterPosition.X = 1280;
                        }
                        if (player2.characterPosition.X < 0)
                        {
                            player2.characterPosition.X = 0;
                        }

                        if (player.characterPosition.Y > 720)
                        {
                            player.characterPosition.Y = 720;
                        }

                        if (player.characterPosition.Y < 0)
                        {
                            player.characterPosition.Y = 0;
                        }
                        if (player2.characterPosition.Y > 720)
                        {
                            player2.characterPosition.Y = 720;
                        }
                        if (player2.characterPosition.Y < 0)
                        {
                            player2.characterPosition.Y = 0;
                        }


                        #region Rectangles

                        //Multi - 4 LOTS OF SHIT, WILL  HAVE TO CHANGE CLASSES
                        foreach (Tree1 t in tree1List)
                        {
                            t.handsCollisionRectangle = hands.handsRectangle;
                            t.handsCollisionRectangle2 = hands2.handsRectangle;
                        }

                        foreach (Tree2 t in tree2List)
                        {
                            t.handsCollisionRectangle = hands.handsRectangle;
                            t.handsCollisionRectangle2 = hands2.handsRectangle;
                        }

                        foreach (Rock1 r in rock1List)
                        {
                            r.handsColRec = hands.handsRectangle;
                            r.handsColRec2 = hands2.handsRectangle;
                        }

                        foreach (Dune1 d in dune1List)
                        {
                            d.handsColRec = hands.handsRectangle;
                            d.handsColRec2 = hands2.handsRectangle;
                        }

                        screenCenter = new Vector2(1280, 720) / 2;

                        //Multi - 6 NEED TO CHANGE STILL

                        //Found at: http://forums.create.msdn.com/forums/t/100861.aspx

                        //Create... 
                        Vector2 Start = player.characterPosition;
                        Vector2 End = player2.characterPosition;

                        //The Difference... (or distance)
                        Vector2 diff = End - Start;

                        //The Midpoint... 
                        Vector2 midpoint = diff * 0.5f;

                        //Set Origin... 
                        Vector2 final = midpoint + Start;

                        scrollOffset = screenCenter - final;

                        //Multi - 7
                        previousCharacterPos = player.characterPosition;
                        previousCharacterPos2 = player2.characterPosition;

                        #endregion


                        #region Update Lists

                        if (bulletList.Count > 0)
                            updateBullets();
                        if (bulletList2.Count > 0)
                            updateBullets2();
                        if (zombie1List.Count > 0)
                            updateZombie1s();
                        if (zombie2List.Count > 0)
                            updateZombie2s();
                        if (zombie3List.Count > 0)
                            updateZombie3s();
                        if (tree1List.Count > 0)
                            updateTree1s();
                        if (tree2List.Count > 0)
                            updateTree2s();
                        if (rock1List.Count > 0)
                            updateRock1s();
                        if (dune1List.Count > 0)
                            updateDune1s();
                        if (healthList.Count > 0)
                            updateHealth();
                        if (healthList2.Count > 0)
                            updateHealth2();
                        if (bloodList.Count > 0)
                            updateBlood(gameTime);




                        inventory.Update();
                        miniInventory.Update();
                        miniInventory2.Update();

                        #endregion


                        #region NIGHT SHIT

                        if (isWaveOver == false)
                        {
                            if (nightMusicI.State != SoundState.Playing)
                            {
                                if ((nightMusicI.State == SoundState.Stopped || nightMusicI.State == SoundState.Paused) && isMusicOff == false)
                                {
                                    dayMusicI.Stop();
                                    nightMusicI.Play();
                                }
                            }
                        }
                        else
                        {
                            if ((nightMusicI.State == SoundState.Playing) && isMusicOff == false)
                            {
                                nightMusicI.Stop();
                                dayMusicI.Play();
                            }

                            zombie1List.Clear();
                            zombie2List.Clear();
                            zombie3List.Clear(); 
                        }

                        if (timer > dayTime)
                        {
                            isWaveOver = false;
                            nightDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                            if (nightDelay <= 0)
                            {
                                nightDelay = .05;

                                if (nightAlphaValue <= 255)
                                    nightAlphaValue += nightFadeIncrement;
                            }

                            if (timer > dayTime + nightTime)
                            {
                                timer = 0;
                            }
                        }

                        //if (timer < dayTime - 1)
                        //{
                        //    isDayTime = true;

                        //    nightDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                        //    if (nightDelay <= 0)
                        //    {
                        //        nightDelay = .05;

                        //        if (nightAlphaValue >= 0)
                        //            nightAlphaValue -= nightFadeIncrement;
                        //    }
                        //}

                        #endregion

                        #region LOADING SHIT (almost, rest is in resetAll)

                        if (isLoading == true)
                        {
                            loadingTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                            if (isBlack == false)
                            {
                                if (timer > loadingTime)
                                {
                                    loadingDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                                    if (loadingDelay <= 0)
                                    {
                                        loadingDelay = .005;

                                        if (loadingAlphaValue <= 255)
                                            loadingAlphaValue += loadingFadeIncrement;
                                    }

                                    if (timer > loadingTime + 5)
                                    {
                                        timer = 0;
                                        isBlack = true;
                                    }
                                }
                            }
                            else
                            {
                                loadingDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                                if (loadingDelay <= 0)
                                {
                                    loadingDelay = .005;

                                    if (loadingAlphaValue >= 0)
                                        loadingAlphaValue -= loadingFadeIncrement;
                                }
                            }

                            if ((loadingTimer < 5.5 && loadingTimer > 2.0) && isReset == false)
                            {
                                zombie1List.Clear();
                                zombie2List.Clear();
                                zombie3List.Clear();

                                tree1List.Clear();
                                tree2List.Clear();

                                treeA = random.Next(1, 100); //amount of trees.

                                //ADD SEEDS

                                rock1List.Clear();
                                rockA = random.Next(1, 50); //amount of rocks.

                                dune1List.Clear();
                                duneA = random.Next(1, 25); //amount of dunes.

                                bulletList.Clear();
                                bulletList2.Clear();

                                pistolClip = 16;
                                smgClip = 30;
                                shotgunClip = 8;
                                sniperClip = 5;

                                //Multi - 8
                                pistolClip2 = 16;
                                smgClip2 = 30;
                                shotgunClip2 = 8;
                                sniperClip2 = 5;

                                isReset = true;
                            }

                            if (loadingTimer < 0)
                            {
                                isLoading = false;
                                loadingTimer = 7;
                            }
                        }

                        #endregion

                        #region Character Movement

                        //Multi - 9
                        player.Update();
                        shadow.Update();
                        player2.Update();
                        shadow2.Update();

                        //Multi - 10
                        shadow.characterShadowRotation = player.orientation;
                        shadow2.characterShadowRotation = player2.orientation;

                        //Multi - 11
                        hands.realCharacterRot = player.orientation;
                        hands.Update();
                        crosshair.realCharacterRot = player.orientation;
                        crosshair.Update();
                        pistol.realCharacterRot = player.orientation;
                        pistol.Update();
                        smg.realCharacterRot = player.orientation;
                        smg.Update();
                        shotgun.realCharacterRot = player.orientation;
                        shotgun.Update();
                        sniper.realCharacterRot = player.orientation;
                        sniper.Update();

                        hands2.realCharacterRot = player2.orientation;
                        hands2.Update();
                        crosshair2.realCharacterRot = player2.orientation;
                        crosshair2.Update();
                        pistol2.realCharacterRot = player2.orientation;
                        pistol2.Update();
                        smg2.realCharacterRot = player2.orientation;
                        smg2.Update();
                        shotgun2.realCharacterRot = player2.orientation;
                        shotgun2.Update();
                        sniper2.realCharacterRot = player2.orientation;
                        sniper2.Update();

                        //Multi - 12
                        if (player.currentWeapon > 0)
                            hands.active = false;
                        else
                            hands.active = true;

                        if (player2.currentWeapon > 0)
                            hands2.active = false;
                        else
                            hands2.active = true;

                        #endregion

                        #region BOUNDARY SHIT

                        leftBRect = new Rectangle((int)leftPosition.X + horizontalBoundaryTexture.Width - 75, (int)leftPosition.Y, horizontalBoundaryTexture.Width, horizontalBoundaryTexture.Height * 2);
                        rightBRect = new Rectangle((int)rightPosition.X, (int)rightPosition.Y, horizontalBoundaryTexture.Width, horizontalBoundaryTexture.Height * 2);
                        topBRect = new Rectangle((int)topPosition.X, (int)topPosition.Y, verticalBoundaryTexture.Width * 2, verticalBoundaryTexture.Height);
                        botBRect = new Rectangle((int)bottomPosition.X, (int)bottomPosition.Y, verticalBoundaryTexture.Width * 2, verticalBoundaryTexture.Height);

                        if (player.characterCollisionRectangle.Intersects(topBRect))
                        {
                            //player.characterPosition = previousCharacterPos;
                            //player.characterPosition -= new Vector2((float)Math.Cos(player.orientation), (float)Math.Sin(player.orientation)) * 1f;

                            if (noEnergy == 1)
                                player.characterPosition.Y += 3.86f; //This is the max speed you cann aproach it. 
                            else
                                player.characterPosition.Y += 7.72f; //This is the max speed you cann aproach it with energyDrink active. 
                        }

                        if (player.characterCollisionRectangle.Intersects(leftBRect))
                        {
                            if (noEnergy == 1)
                                player.characterPosition.X += 3.86f;
                            else
                                player.characterPosition.X += 7.72f;
                        }

                        if (player.characterCollisionRectangle.Intersects(botBRect))
                        {
                            if (noEnergy == 1)
                                player.characterPosition.Y -= 3.86f;
                            else
                                player.characterPosition.Y -= 7.72f;
                        }

                        if (player.characterCollisionRectangle.Intersects(rightBRect))
                        {
                            if (noEnergy == 1)
                                player.characterPosition.X -= 3.86f;
                            else
                                player.characterPosition.X -= 7.72f;
                        }

                        //Multi - 13
                        if (player2.characterCollisionRectangle.Intersects(topBRect))
                        {
                            //player2.characterPosition = previousCharacterPos;
                            //player2.characterPosition -= new Vector2((float)Math.Cos(player2.orientation), (float)Math.Sin(player2.orientation)) * 1f;

                            if (noEnergy == 1)
                                player2.characterPosition.Y += 3.86f; //This is the max speed you cann aproach it. 
                            else
                                player2.characterPosition.Y += 7.72f; //This is the max speed you cann aproach it with energyDrink active. 
                        }

                        if (player2.characterCollisionRectangle.Intersects(leftBRect))
                        {
                            if (noEnergy == 1)
                                player2.characterPosition.X += 3.86f;
                            else
                                player2.characterPosition.X += 7.72f;
                        }

                        if (player2.characterCollisionRectangle.Intersects(botBRect))
                        {
                            if (noEnergy == 1)
                                player2.characterPosition.Y -= 3.86f;
                            else
                                player2.characterPosition.Y -= 7.72f;
                        }

                        if (player2.characterCollisionRectangle.Intersects(rightBRect))
                        {
                            if (noEnergy == 1)
                                player2.characterPosition.X -= 3.86f;
                            else
                                player2.characterPosition.X -= 7.72f;
                        }

                        #endregion

                        #region Character Actions

                        #region Guns

                        #region Pistol

                        if (player.currentWeapon == 1)
                        {
                            if (pistol.active == true)
                            {
                                if (isShooting == false)
                                {
                                    if (pistolClip > 0)
                                    {
                                        if (hasReloaded == true)
                                        {
                                            if ((ks.IsKeyDown(Keys.F) && oks.IsKeyUp(Keys.F) || (gps.Triggers.Right >= .5f)))
                                            {
                                                isShooting = true;
                                                if (inventory.isGloveAvailable == 0)
                                                    addBullet(new Vector2(pistol.gunPosition.X, pistol.gunPosition.Y), pistol, pistol.damage * 2);
                                                else
                                                    addBullet(new Vector2(pistol.gunPosition.X, pistol.gunPosition.Y), pistol, pistol.damage);
                                                drawMuzzleFlare1 = true;
                                                pistolShot.Play(.5f, 0, 0);
                                                pistolClip--;
                                            }

                                        }
                                    }
                                }

                                if (gps.Triggers.Right < .5f)
                                {
                                    isShooting = false;
                                }

                                if (gps.Buttons.X == ButtonState.Pressed && ogps.Buttons.X == ButtonState.Released)
                                {
                                    pistolClip = 0;
                                }
                            }
                        }

                        if (pistolClip <= 0)
                        {
                            hasReloaded = false;

                            reloadTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (isGunClickPlaying == false)
                            {
                                if (gps.Triggers.Right >= .5f)
                                {
                                    isGunClickPlaying = true;
                                    gunClick.Play();
                                    if (reloadTimer >= 2.3f)
                                    {
                                        hasReloaded = true;
                                        reloadTimer = 0;
                                        pistolClip = 16;
                                    }
                                }
                            }
                            if (gps.Triggers.Right < .5f)
                            {
                                isGunClickPlaying = false;
                            }
                        }



                        #endregion

                        //Multi - 14
                        #region Pistol2

                        if (player2.currentWeapon == 1)
                        {
                            if (pistol2.active == true)
                            {
                                if (isShooting2 == false)
                                {
                                    if (pistolClip2 > 0)
                                    {
                                        if (hasReloaded2 == true)
                                        {
                                            if ((ks.IsKeyDown(Keys.F) && oks.IsKeyUp(Keys.F) || (gps2.Triggers.Right >= .5f)))
                                            {
                                                isShooting2 = true;
                                                if (inventory2.isGloveAvailable == 0)
                                                    add2Bullet(new Vector2(pistol2.gunPosition.X, pistol2.gunPosition.Y), pistol2, pistol2.damage * 2);
                                                else
                                                    add2Bullet(new Vector2(pistol2.gunPosition.X, pistol2.gunPosition.Y), pistol2, pistol2.damage);
                                                drawMuzzleFlare12 = true;
                                                pistolShot.Play(.5f, 0, 0);
                                                pistolClip2--;
                                            }

                                        }
                                    }
                                }

                                if (gps2.Triggers.Right < .5f)
                                {
                                    isShooting2 = false;
                                }

                                if (gps2.Buttons.X == ButtonState.Pressed && ogps2.Buttons.X == ButtonState.Released)
                                {
                                    pistolClip2 = 0;
                                }
                            }
                        }

                        if (pistolClip2 <= 0)
                        {
                            hasReloaded2 = false;

                            reloadTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (isGunClickPlaying2 == false)
                            {
                                if (gps2.Triggers.Right >= .5f)
                                {
                                    isGunClickPlaying2 = true;
                                    gunClick.Play();
                                    if (reloadTimer2 >= 2.3f)
                                    {
                                        hasReloaded2 = true;
                                        reloadTimer2 = 0;
                                        pistolClip2 = 16;
                                    }
                                }
                            }
                            if (gps2.Triggers.Right < .5f)
                            {
                                isGunClickPlaying2 = false;
                            }
                        }



                        #endregion

                        #region SMG

                        if (gameTime.TotalGameTime - previousTime > fireTime)
                        {
                            previousTime = gameTime.TotalGameTime;

                            if (player.currentWeapon == 2)
                            {
                                if (smg.active == true)
                                {
                                    if (isShooting == false)
                                    {
                                        if (smgClip > 0)
                                        {
                                            if (hasReloaded == true)
                                            {
                                                if ((ks.IsKeyDown(Keys.F) && oks.IsKeyUp(Keys.F) || (gps.Triggers.Right >= .5f)))
                                                {
                                                    isShooting = true;
                                                    if (inventory.isGloveAvailable == 0)
                                                        addBullet2(new Vector2(smg.gunPosition.X, smg.gunPosition.Y), smg, smg.damage * 2);
                                                    else
                                                        addBullet2(new Vector2(smg.gunPosition.X, smg.gunPosition.Y), smg, smg.damage);
                                                    drawMuzzleFlare1 = true;
                                                    smgShot.Play(.5f, 0, 0);
                                                    smgClip--;
                                                }
                                            }
                                        }
                                    }

                                    isShooting = false;
                                }

                                fireTime = TimeSpan.FromSeconds(.2f);
                            }
                        }

                        if (player.currentWeapon == 2)
                        {
                            if (smg.active == true)
                            {
                                if (gps.Buttons.X == ButtonState.Pressed && ogps.Buttons.X == ButtonState.Released)
                                {
                                    smgClip = 0;
                                }
                            }
                        }

                        if (smgClip <= 0)
                        {
                            hasReloaded = false;

                            reloadTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                            if (isGunClickPlaying == false)
                            {
                                if (gps.Triggers.Right >= .5f)
                                {
                                    isGunClickPlaying = true;
                                    gunClick.Play();
                                    if (reloadTimer >= 2.3f)
                                    {
                                        hasReloaded = true;
                                        reloadTimer = 0;
                                        smgClip = 30;
                                    }
                                }
                            }
                            if (gps.Triggers.Right < .5f)
                            {
                                isGunClickPlaying = false;
                            }
                        }

                        #endregion

                        //Multi - 15
                        #region SMG2

                        if (gameTime.TotalGameTime - previousTime2 > fireTime2)
                        {
                            previousTime2 = gameTime.TotalGameTime;

                            if (player2.currentWeapon == 2)
                            {
                                if (smg2.active == true)
                                {
                                    if (isShooting2 == false)
                                    {
                                        if (smgClip2 > 0)
                                        {
                                            if (hasReloaded2 == true)
                                            {
                                                if ((ks.IsKeyDown(Keys.F) && oks.IsKeyUp(Keys.F) || (gps2.Triggers.Right >= .5f)))
                                                {
                                                    isShooting2 = true;
                                                    if (inventory2.isGloveAvailable == 0)
                                                        add2Bullet2(new Vector2(smg2.gunPosition.X, smg2.gunPosition.Y), smg2, smg2.damage * 2);
                                                    else
                                                        add2Bullet2(new Vector2(smg2.gunPosition.X, smg2.gunPosition.Y), smg2, smg2.damage);
                                                    drawMuzzleFlare12 = true;
                                                    smgShot.Play(.5f, 0, 0);
                                                    smgClip2--;
                                                }
                                            }
                                        }
                                    }

                                    isShooting2 = false;
                                }

                                fireTime2 = TimeSpan.FromSeconds(.2f);
                            }
                        }

                        if (player2.currentWeapon == 2)
                        {
                            if (smg2.active == true)
                            {
                                if (gps2.Buttons.X == ButtonState.Pressed && ogps2.Buttons.X == ButtonState.Released)
                                {
                                    smgClip2 = 0;
                                }
                            }
                        }

                        if (smgClip2 <= 0)
                        {
                            hasReloaded2 = false;

                            reloadTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

                            if (isGunClickPlaying2 == false)
                            {
                                if (gps2.Triggers.Right >= .5f)
                                {
                                    isGunClickPlaying2 = true;
                                    gunClick.Play();
                                    if (reloadTimer2 >= 2.3f)
                                    {
                                        hasReloaded2 = true;
                                        reloadTimer2 = 0;
                                        smgClip2 = 30;
                                    }
                                }
                            }
                            if (gps2.Triggers.Right < .5f)
                            {
                                isGunClickPlaying2 = false;
                            }
                        }

                        #endregion

                        #region Shotgun

                        shotgunTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (player.currentWeapon == 3)
                        {
                            if (shotgun.active == true)
                            {
                                if (isShooting == false)
                                {
                                    if (shotgunClip > 0)
                                    {
                                        if (hasReloaded == true)
                                        {

                                            if ((ks.IsKeyDown(Keys.F) && oks.IsKeyUp(Keys.F) || (gps.Triggers.Right >= .5f)) && shotgunTimer >= 1)
                                            {
                                                isShooting = true;

                                                float rot = (shotgun.gunRot - .35f) + (float)random.NextDouble();
                                                if (inventory.isGloveAvailable == 0)
                                                {
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage * 2, rot);
                                                    rot = (shotgun.gunRot + .35f) - (float)random.NextDouble();
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage * 2, rot);

                                                    //perm shots
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage * 2, shotgun.gunRot);
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage * 2, shotgun.gunRot - .35f);
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage * 2, shotgun.gunRot + .35f);
                                                }
                                                else
                                                {
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage, rot);
                                                    rot = (shotgun.gunRot + .35f) - (float)random.NextDouble();
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage, rot);

                                                    //perm shots
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage, shotgun.gunRot);
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage, shotgun.gunRot - .35f);
                                                    addBullet3(new Vector2(shotgun.gunPosition.X, shotgun.gunPosition.Y), shotgun, shotgun.damage, shotgun.gunRot + .35f);
                                                }

                                                drawMuzzleFlare2 = true;
                                                shotgunShot.Play();
                                                shotgunTimer = 0;
                                                shotgunClip--;
                                            }
                                        }
                                    }

                                }

                                if (gps.Triggers.Right < .5f)
                                {
                                    isShooting = false;
                                }
                            }
                        }

                        if (player.currentWeapon == 3)
                        {
                            if (shotgun.active == true)
                            {
                                if (gps.Buttons.X == ButtonState.Pressed && ogps.Buttons.X == ButtonState.Released)
                                {
                                    shotgunClip = 0;
                                }
                            }
                        }

                        if (shotgunClip <= 0)
                        {
                            hasReloaded = false;

                            reloadTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (isGunClickPlaying == false)
                            {
                                if (gps.Triggers.Right >= .5f)
                                {
                                    isGunClickPlaying = true;
                                    gunClick.Play();
                                    if (reloadTimer >= 3f)
                                    {
                                        hasReloaded = true;
                                        reloadTimer = 0;
                                        shotgunClip = 8;
                                    }
                                }
                            }
                            if (gps.Triggers.Right < .5f)
                            {
                                isGunClickPlaying = false;
                            }
                        }


                        #endregion

                        //Multi - 16
                        #region Shotgun2

                        shotgunTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (player2.currentWeapon == 3)
                        {
                            if (shotgun2.active == true)
                            {
                                if (isShooting2 == false)
                                {
                                    if (shotgunClip2 > 0)
                                    {
                                        if (hasReloaded2 == true)
                                        {

                                            if ((ks.IsKeyDown(Keys.F) && oks.IsKeyUp(Keys.F) || (gps2.Triggers.Right >= .5f)) && shotgunTimer2 >= 1)
                                            {
                                                isShooting2 = true;

                                                float rot = (shotgun2.gunRot - .35f) + (float)random.NextDouble();
                                                if (inventory2.isGloveAvailable == 0)
                                                {
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage * 2, rot);
                                                    rot = (shotgun2.gunRot + .35f) - (float)random.NextDouble();
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage * 2, rot);

                                                    //perm shots
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage * 2, shotgun2.gunRot);
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage * 2, shotgun2.gunRot - .35f);
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage * 2, shotgun2.gunRot + .35f);
                                                }
                                                else
                                                {
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage, rot);
                                                    rot = (shotgun2.gunRot + .35f) - (float)random.NextDouble();
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage, rot);

                                                    //perm shots
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage, shotgun2.gunRot);
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage, shotgun2.gunRot - .35f);
                                                    add2Bullet3(new Vector2(shotgun2.gunPosition.X, shotgun2.gunPosition.Y), shotgun2, shotgun2.damage, shotgun2.gunRot + .35f);
                                                }

                                                drawMuzzleFlare22 = true;
                                                shotgunShot.Play();
                                                shotgunTimer2 = 0;
                                                shotgunClip2--;
                                            }
                                        }
                                    }

                                }

                                if (gps2.Triggers.Right < .5f)
                                {
                                    isShooting2 = false;
                                }
                            }
                        }

                        if (player2.currentWeapon == 3)
                        {
                            if (shotgun2.active == true)
                            {
                                if (gps2.Buttons.X == ButtonState.Pressed && ogps2.Buttons.X == ButtonState.Released)
                                {
                                    shotgunClip2 = 0;
                                }
                            }
                        }

                        if (shotgunClip2 <= 0)
                        {
                            hasReloaded2 = false;

                            reloadTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (isGunClickPlaying2 == false)
                            {
                                if (gps2.Triggers.Right >= .5f)
                                {
                                    isGunClickPlaying2 = true;
                                    gunClick.Play();
                                    if (reloadTimer2 >= 3f)
                                    {
                                        hasReloaded2 = true;
                                        reloadTimer2 = 0;
                                        shotgunClip2 = 8;
                                    }
                                }
                            }
                            if (gps2.Triggers.Right < .5f)
                            {
                                isGunClickPlaying2 = false;
                            }
                        }


                        #endregion

                        #region Sniper

                        sniperTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        sniperCockTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        //if (gameTime.TotalGameTime - previousSnipeTime > sniperFireTime)
                        //{
                        //    previousSnipeTime = gameTime.TotalGameTime;

                        if (player.currentWeapon == 4)
                        {
                            if (sniper.active == true)
                            {
                                if (isShooting == false)
                                {
                                    if (sniperClip > 0)
                                    {
                                        if (hasReloaded == true)
                                        {
                                            if ((ks.IsKeyDown(Keys.F) && oks.IsKeyUp(Keys.F) || (gps.Triggers.Right >= .5f)) && sniperTimer >= 1.5f)
                                            {
                                                sniperCockTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                                isShooting = true;
                                                if (inventory.isGloveAvailable == 0)
                                                    addBullet4(new Vector2(sniper.gunPosition.X, sniper.gunPosition.Y), sniper, sniper.damage * 2);
                                                else
                                                    addBullet4(new Vector2(sniper.gunPosition.X, sniper.gunPosition.Y), sniper, sniper.damage);
                                                drawMuzzleFlare3 = true;
                                                sniperShot.Play(.2f, 0, 0);
                                                sniperTimer = 0;
                                                sniperClip--;

                                                //if (sniperCockTimer >= 2)
                                                //{
                                                //    sniperCock.Play();
                                                //    sniperCockTimer = 0;
                                                //}
                                            }
                                        }
                                    }
                                }

                                if (gps.Triggers.Right < .5f)
                                {
                                    isShooting = false;
                                }
                            }
                        }

                        if (sniperClip <= 0)
                        {
                            hasReloaded = false;

                            reloadTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (isGunClickPlaying == false)
                            {
                                if (gps.Triggers.Right >= .5f)
                                {
                                    isGunClickPlaying = true;
                                    gunClick.Play();
                                }
                            }
                            if (reloadTimer >= 3f)
                            {
                                hasReloaded = true;
                                reloadTimer = 0;
                                sniperCock.Play();
                                sniperClip = 5;

                            }
                            if (gps.Triggers.Right < .5f)
                            {
                                isGunClickPlaying = false;
                            }
                        }

                        //        sniperFireTime = TimeSpan.FromSeconds(.5f);
                        //    }
                        //}

                        #endregion

                        //Multi - 17
                        #region Sniper

                        sniperTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        sniperCockTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (player2.currentWeapon == 4)
                        {
                            if (sniper2.active == true)
                            {
                                if (isShooting2 == false)
                                {
                                    if (sniperClip2 > 0)
                                    {
                                        if (hasReloaded2 == true)
                                        {
                                            if ((ks.IsKeyDown(Keys.F) && oks.IsKeyUp(Keys.F) || (gps2.Triggers.Right >= .5f)) && sniperTimer2 >= 1.5f)
                                            {
                                                sniperCockTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                                isShooting2 = true;
                                                if (inventory2.isGloveAvailable == 0)
                                                    add2Bullet4(new Vector2(sniper2.gunPosition.X, sniper2.gunPosition.Y), sniper2, sniper2.damage * 2);
                                                else
                                                    add2Bullet4(new Vector2(sniper2.gunPosition.X, sniper2.gunPosition.Y), sniper2, sniper2.damage);
                                                drawMuzzleFlare32 = true;
                                                sniperShot.Play(.2f, 0, 0);
                                                sniperTimer2 = 0;
                                                sniperClip2--;
                                            }
                                        }
                                    }
                                }

                                if (gps2.Triggers.Right < .5f)
                                {
                                    isShooting2 = false;
                                }
                            }
                        }

                        if (sniperClip2 <= 0)
                        {
                            hasReloaded2 = false;

                            reloadTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (isGunClickPlaying2 == false)
                            {
                                if (gps2.Triggers.Right >= .5f)
                                {
                                    isGunClickPlaying2 = true;
                                    gunClick.Play();
                                }
                            }
                            if (reloadTimer2 >= 3f)
                            {
                                hasReloaded2 = true;
                                reloadTimer2 = 0;
                                sniperCock.Play();
                                sniperClip2 = 5;

                            }
                            if (gps2.Triggers.Right < .5f)
                            {
                                isGunClickPlaying2 = false;
                            }
                        }

                        #endregion

                        #endregion

                        #region TREE SHIT

                        //chooses random grid
                        gridLocX = random.Next(70);
                        gridLocY = random.Next(58);

                        if (treeA > 0)
                        {
                            if ((gridLocX != playerGridPosX && gridLocX != playerGridPosX + 1 && gridLocX != playerGridPosX + 2 &&
                                gridLocX != playerGridPosX && gridLocX != playerGridPosX - 1 && gridLocX != playerGridPosX - 2) &&
                                (gridLocY != playerGridPosY && gridLocY != playerGridPosY + 1 && gridLocY != playerGridPosY + 2 &&
                                gridLocY != playerGridPosY && gridLocY != playerGridPosY - 1 && gridLocY != playerGridPosY - 2))
                            {
                                tree1List.Add(new Tree1(tree1Tex, gridList[gridLocY * 58 + gridLocX].gridPosition));
                                treeA--;
                            }
                        }


                        foreach (Tree1 t in tree1List)
                        {
                            //Multi - 18
                            if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(t.tree1Position.X + (t.tree1Texture.Width / t.tree1Frames / 2), t.tree1Position.Y + (t.tree1Texture.Height / 2)), treeRadius))
                            {
                                player.characterPosition = previousCharacterPos;
                            }

                            if (BoundingCircle(player2.characterPosition, playerRadius, new Vector2(t.tree1Position.X + (t.tree1Texture.Width / t.tree1Frames / 2), t.tree1Position.Y + (t.tree1Texture.Height / 2)), treeRadius))
                            {
                                player2.characterPosition = previousCharacterPos2;
                            }

                        }

                        //chooses random grid
                        gridLocX = random.Next(70);
                        gridLocY = random.Next(58);

                        if (treeA > 0)
                        {
                            if ((gridLocX != playerGridPosX && gridLocX != playerGridPosX + 1 && gridLocX != playerGridPosX + 2 &&
                                gridLocX != playerGridPosX && gridLocX != playerGridPosX - 1 && gridLocX != playerGridPosX - 2) &&
                                (gridLocY != playerGridPosY && gridLocY != playerGridPosY + 1 && gridLocY != playerGridPosY + 2 &&
                                gridLocY != playerGridPosY && gridLocY != playerGridPosY - 1 && gridLocY != playerGridPosY - 2))
                            {
                                tree2List.Add(new Tree2(tree2Tex, gridList[gridLocY * 58 + gridLocX].gridPosition));
                                treeA--;
                            }
                        }


                        foreach (Tree2 t in tree2List)
                        {
                            //Multi - 19
                            if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(t.tree2Position.X + (t.tree2Texture.Width / t.tree2Frames / 2), t.tree2Position.Y + (t.tree2Texture.Height / 2)), treeRadius))
                            {
                                player.characterPosition = previousCharacterPos;
                            }

                            if (BoundingCircle(player2.characterPosition, playerRadius, new Vector2(t.tree2Position.X + (t.tree2Texture.Width / t.tree2Frames / 2), t.tree2Position.Y + (t.tree2Texture.Height / 2)), treeRadius))
                            {
                                player2.characterPosition = previousCharacterPos2;
                            }

                        }

                        #endregion

                        #region ROCK SHIT

                        //chooses random grid
                        gridLocX = random.Next(70);
                        gridLocY = random.Next(58);

                        if (rockA > 0)
                        {
                            if ((gridLocX != playerGridPosX && gridLocX != playerGridPosX + 1 && gridLocX != playerGridPosX + 2 &&
                                gridLocX != playerGridPosX && gridLocX != playerGridPosX - 1 && gridLocX != playerGridPosX - 2) &&
                                (gridLocY != playerGridPosY && gridLocY != playerGridPosY + 1 && gridLocY != playerGridPosY + 2 &&
                                gridLocY != playerGridPosY && gridLocY != playerGridPosY - 1 && gridLocY != playerGridPosY - 2))
                            {
                                rock1List.Add(new Rock1(rock1Tex, gridList[gridLocY * 58 + gridLocX].gridPosition));
                                rockA--;
                            }
                        }

                        foreach (Rock1 r in rock1List)
                        {
                            //Multi - 20
                            if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(r.rock1Position.X + (r.rock1Texture.Width / r.rock1Frames / 2), r.rock1Position.Y + (r.rock1Texture.Height / 2)), rockRadius))
                            {
                                player.characterPosition = previousCharacterPos;
                            }

                            if (BoundingCircle(player2.characterPosition, playerRadius, new Vector2(r.rock1Position.X + (r.rock1Texture.Width / r.rock1Frames / 2), r.rock1Position.Y + (r.rock1Texture.Height / 2)), rockRadius))
                            {
                                player2.characterPosition = previousCharacterPos2;
                            }

                        }

                        for (int i = rock1List.Count - 1; i >= 0; i--)
                        {
                            for (int b = 0; b < i; b++)
                            {
                                if (rock1List[i].rock1ColRec.Intersects(rock1List[b].rock1ColRec))
                                {
                                    rock1List[i].rock1Position = rock1List[b].rock1Position + new Vector2(rock1List[i].rock1Texture.Width / rock1List[i].rock1Frames, rock1List[i].rock1Texture.Height);

                                }
                            }
                        }

                        #endregion

                        #region DUNE SHIT

                        gridLocX = random.Next(70);
                        gridLocY = random.Next(58);

                        if (duneA > 0)
                        {
                            if ((gridLocX != playerGridPosX && gridLocX != playerGridPosX + 1 && gridLocX != playerGridPosX + 2 &&
                                gridLocX != playerGridPosX && gridLocX != playerGridPosX - 1 && gridLocX != playerGridPosX - 2) &&
                                (gridLocY != playerGridPosY && gridLocY != playerGridPosY + 1 && gridLocY != playerGridPosY + 2 &&
                                gridLocY != playerGridPosY && gridLocY != playerGridPosY - 1 && gridLocY != playerGridPosY - 2))
                            {
                                dune1List.Add(new Dune1(dune1Tex, gridList[gridLocY * 58 + gridLocX].gridPosition));
                                duneA--;
                            }
                        }

                        foreach (Dune1 d in dune1List)
                        {
                            //Multi - 21
                            if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(d.dune1Position.X + (d.dune1Texture.Width / d.dune1Frames / 2), d.dune1Position.Y + (d.dune1Texture.Height / 2)), duneRadius))
                            {
                                player.characterPosition = previousCharacterPos;
                            }

                            if (BoundingCircle(player2.characterPosition, playerRadius, new Vector2(d.dune1Position.X + (d.dune1Texture.Width / d.dune1Frames / 2), d.dune1Position.Y + (d.dune1Texture.Height / 2)), duneRadius))
                            {
                                player2.characterPosition = previousCharacterPos2;
                            }

                        }

                        for (int i = dune1List.Count - 1; i >= 0; i--)
                        {
                            for (int b = 0; b < i; b++)
                            {
                                if (dune1List[i].dune1ColRec.Intersects(dune1List[b].dune1ColRec))
                                {
                                    dune1List[i].dune1Position = dune1List[b].dune1Position + new Vector2(dune1List[i].dune1Texture.Width / dune1List[i].dune1Frames, dune1List[i].dune1Texture.Height);

                                }
                            }
                        }

                        #endregion

                        
                        #region MINI INVENTORY SHIT

                        pistol.active = true;

                        if (pistol.active == true)
                        {
                            currentWeaponAavailable[1] = 1;
                        }
                        else
                            currentWeaponAavailable[1] = 0;

                        if (smg.active == true)
                        {
                            currentWeaponAavailable[2] = 1;
                        }
                        else
                            currentWeaponAavailable[2] = 0;

                        if (shotgun.active == true)
                        {
                            currentWeaponAavailable[3] = 1;
                        }
                        else
                            currentWeaponAavailable[3] = 0;

                        if (sniper.active == true)
                        {
                            currentWeaponAavailable[4] = 1;
                        }
                        else
                            currentWeaponAavailable[4] = 0;

                        if (gps.Buttons.RightShoulder == ButtonState.Pressed && ogps.Buttons.RightShoulder == ButtonState.Released && choseWeapon == 0)
                        {
                            bool found = false;


                            for (int i = 0; i < currentWeaponAavailable.Count; i++)
                            {
                                if (i > miniInventory.currentWeapon)
                                {
                                    if (currentWeaponAavailable[i] > 0)
                                    {
                                        miniInventory.currentWeapon = i;
                                        player.currentWeapon = i;
                                        choseWeapon++;
                                        chooseWeapon = true;
                                        found = true;
                                        break;
                                    }
                                }
                            }

                            if (found == false)
                            {
                                miniInventory.currentWeapon = 0;
                                player.currentWeapon = 0;
                                choseWeapon++;
                            }
                        }
                        else
                        {
                            choseWeapon = 0;
                            chooseWeapon = false;
                        }

                        if (gps.Buttons.LeftShoulder == ButtonState.Pressed && ogps.Buttons.LeftShoulder == ButtonState.Released && choseWeapon == 0)
                        {
                            bool found = false;


                            for (int i = currentWeaponAavailable.Count - 1; i >= 0; i--)
                            {
                                if (i < miniInventory.currentWeapon)
                                {
                                    if (currentWeaponAavailable[i] > 0)
                                    {
                                        miniInventory.currentWeapon = i;
                                        player.currentWeapon = i;
                                        choseWeapon++;
                                        chooseWeapon = true;
                                        found = true;
                                        break;
                                    }
                                }
                            }

                            if (found == false)
                            {
                                if (miniInventory.currentWeapon < 11 && miniInventory.currentWeapon > 0)
                                {
                                    miniInventory.currentWeapon = 0;
                                    player.currentWeapon = 0;
                                    choseWeapon++;
                                }
                                else
                                {
                                    for (int i = 0; i < 11; i++)
                                    {
                                        if (currentWeaponAavailable[i] == 1)
                                        {
                                            miniInventory.currentWeapon = i;
                                            player.currentWeapon = i;
                                            choseWeapon++;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            choseWeapon = 0;
                            chooseWeapon = false;
                        }

                        #endregion

                        #region MINI INVENTORY SHIT2

                        pistol2.active = true;

                        if (pistol2.active == true)
                        {
                            currentWeaponAavailable2[1] = 1;
                        }
                        else
                            currentWeaponAavailable2[1] = 0;

                        if (smg2.active == true)
                        {
                            currentWeaponAavailable2[2] = 1;
                        }
                        else
                            currentWeaponAavailable2[2] = 0;

                        if (shotgun2.active == true)
                        {
                            currentWeaponAavailable2[3] = 1;
                        }
                        else
                            currentWeaponAavailable2[3] = 0;

                        if (sniper2.active == true)
                        {
                            currentWeaponAavailable2[4] = 1;
                        }
                        else
                            currentWeaponAavailable2[4] = 0;

                        if (gps2.Buttons.RightShoulder == ButtonState.Pressed && ogps2.Buttons.RightShoulder == ButtonState.Released && choseWeapon2 == 0)
                        {
                            bool found2 = false;


                            for (int i = 0; i < currentWeaponAavailable2.Count; i++)
                            {
                                if (i > miniInventory2.currentWeapon)
                                {
                                    if (currentWeaponAavailable2[i] > 0)
                                    {
                                        miniInventory2.currentWeapon = i;
                                        player2.currentWeapon = i;
                                        choseWeapon2++;
                                        chooseWeapon2 = true;
                                        found2 = true;
                                        break;
                                    }
                                }
                            }

                            if (found2 == false)
                            {
                                miniInventory2.currentWeapon = 0;
                                player2.currentWeapon = 0;
                                choseWeapon2++;
                            }
                        }
                        else
                        {
                            choseWeapon2 = 0;
                            chooseWeapon2 = false;
                        }

                        if (gps2.Buttons.LeftShoulder == ButtonState.Pressed && ogps2.Buttons.LeftShoulder == ButtonState.Released && choseWeapon2 == 0)
                        {
                            bool found2 = false;


                            for (int i = currentWeaponAavailable2.Count - 1; i >= 0; i--)
                            {
                                if (i < miniInventory2.currentWeapon)
                                {
                                    if (currentWeaponAavailable2[i] > 0)
                                    {
                                        miniInventory2.currentWeapon = i;
                                        player2.currentWeapon = i;
                                        choseWeapon2++;
                                        chooseWeapon2 = true;
                                        found2 = true;
                                        break;
                                    }
                                }
                            }

                            if (found2 == false)
                            {
                                if (miniInventory2.currentWeapon < 11 && miniInventory2.currentWeapon > 0)
                                {
                                    miniInventory2.currentWeapon = 0;
                                    player2.currentWeapon = 0;
                                    choseWeapon2++;
                                }
                                else
                                {
                                    for (int i = 0; i < 11; i++)
                                    {
                                        if (currentWeaponAavailable2[i] == 1)
                                        {
                                            miniInventory2.currentWeapon = i;
                                            player2.currentWeapon = i;
                                            choseWeapon2++;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            choseWeapon2 = 0;
                            chooseWeapon2 = false;
                        }

                        #endregion

                        if (player.numberOfHearts > 0)
                        {
                            addHeart(health, healthPosition);
                            player.numberOfHearts--;
                        }

                        for (int i = 0; i < healthList.Count; i++)
                        {
                            healthList[i].healthPosition = new Vector2(healthPosition.X - (i * ((healthList[i].healthTexture.Width + 10) / healthList[i].healthFrames)), healthPosition.Y);
                        }

                        if (player2.numberOfHearts > 0)
                        {
                            addHeart2(health, healthPosition2);
                            player2.numberOfHearts--;
                        }

                        for (int i = 0; i < healthList2.Count; i++)
                        {
                            healthList2[i].healthPosition = new Vector2(healthPosition2.X - (i * ((healthList2[i].healthTexture.Width + 10) / healthList2[i].healthFrames)), healthPosition2.Y);
                        }

                        #endregion


                        #region ZOMBIE SHIT

                        #region Zombie1
                        zombie1X = random.Next(1280);
                        zombie1Y = random.Next(720);
                        zombieSide = random.Next(4);

                        switch (zombieSide)
                        {
                            case 1: zombie1Pos = new Vector2(zombie1X, -(zombieRadius * 2)); break; //Top
                            case 2: zombie1Pos = new Vector2(1280 + (zombieRadius * 2), zombie1Y); break; //Right
                            case 3: zombie1Pos = new Vector2(zombie1X, 720 + (zombieRadius * 2)); break; //Bottom
                            case 4: zombie1Pos = new Vector2(-(zombieRadius * 2), zombie1Y); break; //Left
                        }

                        if (loadingAlphaValue < 10)
                        {
                            if (zombie1A > 0)
                            {
                                zombie1List.Add(new Zombie1(zombie1Pos, zombie1));
                                zombie1A--;
                            }
                        }

                        foreach (Zombie1 z in zombie1List)
                        {
                            //Multi - 26
                            if(Vector2.Distance(z.zombiePosition, player.characterPosition) < Vector2.Distance(z.zombiePosition ,player2.characterPosition))
                                z.facePos = player.characterPosition;
                            else
                                z.facePos = player2.characterPosition;

                            foreach (Health h in healthList)
                            {
                                if (h.active == true)
                                {
                                    if (BoundingCircle(player.characterPosition, playerRadius, z.zombiePosition, zombieRadius))
                                    {
                                        if (gameTime.TotalGameTime - previousHealthTime > healthTime)
                                        {
                                            GamePad.SetVibration(playerOne, 0.5f, 0.5f);
                                            isVibrating = true;
                                            vibTime = 0.5f;

                                            drawBlood = true;

                                            h.damageTaken++;
                                            if (h.damageTaken >= 3)
                                            {
                                                h.damageTaken = 2;
                                            }

                                            previousHealthTime = gameTime.TotalGameTime;
                                            player.charHealth--;
                                        }
                                    }
                                }
                            }

                            //Multi - 27
                            foreach (Health h in healthList2)
                            {
                                if (h.active == true)
                                {
                                    if (BoundingCircle(player2.characterPosition, playerRadius, z.zombiePosition, zombieRadius))
                                    {
                                        if (gameTime.TotalGameTime - previousHealthTime2 > healthTime2)
                                        {
                                            GamePad.SetVibration(playerTwo, 0.5f, 0.5f);
                                            isVibrating2 = true;
                                            vibTime2 = 0.5f;

                                            drawBlood = true;

                                            h.damageTaken++;
                                            if (h.damageTaken >= 3)
                                            {
                                                h.damageTaken = 2;
                                            }

                                            previousHealthTime2 = gameTime.TotalGameTime;
                                            player2.charHealth--;
                                        }
                                    }
                                }
                            }

                            foreach (Bullet b in bulletList)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, z.zombiePosition, zombieRadius))
                                {
                                    z.damageTaken -= b.damage;
                                    addBlood(bloodTex, z.zombiePosition, z.orientation);
                                    
                                    //Multi - 28
                                    if (player.currentWeapon != 4 || player2.currentWeapon != 4)
                                        b.active = false;

                                    if (z.damageTaken <= 0)
                                    {
                                        score1 += 10;
                                        z.active = false;
                                    }
                                }
                            }

                            foreach (Bullet b in bulletList2)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, z.zombiePosition, zombieRadius))
                                {
                                    z.damageTaken -= b.damage;
                                    addBlood(bloodTex, z.zombiePosition, z.orientation);

                                    //Multi - 28
                                    if (player.currentWeapon != 4 || player2.currentWeapon != 4)
                                        b.active = false;

                                    if (z.damageTaken <= 0)
                                    {
                                        score2 += 10;
                                        z.active = false;
                                    }
                                }
                            }

                            //hitting
                            if (player.currentWeapon == 0)
                            {
                                if (gps.Buttons.B == ButtonState.Pressed && ogps.Buttons.B == ButtonState.Released)
                                {
                                    if (BoundingCircle(player.characterPosition, playerHitRadius, z.zombiePosition, zombieRadius))
                                    {
                                        z.damageTaken -= 5;
                                        z.zombiePosition += new Vector2((float)Math.Cos(player.orientation), (float)Math.Sin(player.orientation)) * 20f;

                                        if (random.Next(0, 2) == 0)
                                            hit1.Play(0.4f, 0, 0);
                                        else
                                            hit2.Play(0.4f, 0, 0);

                                    }
                                }
                            }

                            //Multi - 29
                            if (player2.currentWeapon == 0)
                            {
                                if (gps2.Buttons.B == ButtonState.Pressed && ogps2.Buttons.B == ButtonState.Released)
                                {
                                    if (BoundingCircle(player2.characterPosition, playerHitRadius, z.zombiePosition, zombieRadius))
                                    {
                                        z.damageTaken -= 5;
                                        z.zombiePosition += new Vector2((float)Math.Cos(player2.orientation), (float)Math.Sin(player2.orientation)) * 20f;

                                        if (random.Next(0, 2) == 0)
                                            hit1.Play(0.4f, 0, 0);
                                        else
                                            hit2.Play(0.4f, 0, 0);

                                    }
                                }
                            }
                        }

                        #endregion

                        #region Zombie2

                        zombie2X = random.Next(1280);
                        zombie2Y = random.Next(720);

                        switch (zombieSide)
                        {
                            case 1: zombie2Pos = new Vector2(zombie2X, -(zombieRadius * 2)); break; //Top
                            case 2: zombie2Pos = new Vector2(1280 + (zombieRadius * 2), zombie2Y); break; //Right
                            case 3: zombie2Pos = new Vector2(zombie2X, 720 + (zombieRadius * 2)); break; //Bottom
                            case 4: zombie2Pos = new Vector2(-(zombieRadius * 2), zombie2Y); break; //Left
                        }

                        if (loadingAlphaValue < 10)
                        {
                            if (zombie2A > 0)
                            {
                                zombie2List.Add(new Zombie2(zombie2Pos, zombie2));
                                zombie2A--;
                            }
                        }

                        foreach (Zombie2 z in zombie2List)
                        {
                            //Multi - 30
                            if (Vector2.Distance(z.zombiePosition, player.characterPosition) < Vector2.Distance(z.zombiePosition, player2.characterPosition))
                                z.facePos = player.characterPosition;
                            else
                                z.facePos = player2.characterPosition;

                            foreach (Health h in healthList)
                            {
                                if (h.active == true)
                                {
                                    if (BoundingCircle(player.characterPosition, playerRadius, z.zombiePosition, zombieRadius))
                                    {
                                        if (gameTime.TotalGameTime - previousHealthTime > healthTime)
                                        {
                                            GamePad.SetVibration(playerOne, 0.5f, 0.5f);
                                            isVibrating = true;
                                            vibTime = 0.5f;
                                            drawBlood = true;

                                            h.damageTaken++;
                                            if (h.damageTaken >= 3)
                                            {
                                                h.damageTaken = 2;
                                            }

                                            previousHealthTime = gameTime.TotalGameTime;
                                            player.charHealth--;
                                        }
                                    }
                                }
                            }

                            //Multi - 31
                            foreach (Health h in healthList2)
                            {
                                if (h.active == true)
                                {
                                    if (BoundingCircle(player2.characterPosition, playerRadius, z.zombiePosition, zombieRadius))
                                    {
                                        if (gameTime.TotalGameTime - previousHealthTime2 > healthTime2)
                                        {
                                            GamePad.SetVibration(playerTwo, 0.5f, 0.5f);
                                            isVibrating2 = true;
                                            vibTime2 = 0.5f;
                                            drawBlood = true;

                                            h.damageTaken++;
                                            if (h.damageTaken >= 3)
                                            {
                                                h.damageTaken = 2;
                                            }

                                            previousHealthTime2 = gameTime.TotalGameTime;
                                            player2.charHealth--;
                                        }
                                    }
                                }
                            }

                            foreach (Bullet b in bulletList)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, z.zombiePosition, zombieRadius))
                                {
                                    z.damageTaken -= b.damage;
                                    addBlood(bloodTex, z.zombiePosition, z.orientation);
                                    //Multi - 32
                                    if (player.currentWeapon != 4 || player2.currentWeapon != 4)
                                        b.active = false;

                                    if (z.damageTaken <= 0)
                                    {
                                        score1 += 30;
                                        z.active = false;
                                    }
                                }
                            }

                            foreach (Bullet b in bulletList2)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, z.zombiePosition, zombieRadius))
                                {
                                    z.damageTaken -= b.damage;
                                    addBlood(bloodTex, z.zombiePosition, z.orientation);
                                    //Multi - 32
                                    if (player.currentWeapon != 4 || player2.currentWeapon != 4)
                                        b.active = false;

                                    if (z.damageTaken <= 0)
                                    {
                                        score2 += 30;
                                        z.active = false;
                                    }
                                }
                            }

                            if (player.currentWeapon == 0)
                            {
                                if (gps.Buttons.B == ButtonState.Pressed && ogps.Buttons.B == ButtonState.Released)
                                {
                                    if (BoundingCircle(player.characterPosition, playerHitRadius, z.zombiePosition, zombieRadius))
                                    {
                                        z.damageTaken -= 5;

                                        z.zombiePosition += new Vector2((float)Math.Cos(player.orientation), (float)Math.Sin(player.orientation)) * 20f;
                                    }
                                }
                            }

                            //Multi - 33
                            if (player2.currentWeapon == 0)
                            {
                                if (gps2.Buttons.B == ButtonState.Pressed && ogps2.Buttons.B == ButtonState.Released)
                                {
                                    if (BoundingCircle(player2.characterPosition, playerHitRadius, z.zombiePosition, zombieRadius))
                                    {
                                        z.damageTaken -= 5;

                                        z.zombiePosition += new Vector2((float)Math.Cos(player2.orientation), (float)Math.Sin(player2.orientation)) * 20f;
                                    }
                                }
                            }

                        }

                        #endregion

                        #region Zombie3

                        zombie3X = random.Next(1280);
                        zombie3Y = random.Next(720);

                        switch (zombieSide)
                        {
                            case 1: zombie3Pos = new Vector2(zombie3X, -(zombieRadius * 2)); break; //Top
                            case 2: zombie3Pos = new Vector2(1280 + (zombieRadius * 2), zombie3Y); break; //Right
                            case 3: zombie3Pos = new Vector2(zombie3X, 720 + (zombieRadius * 2)); break; //Bottom
                            case 4: zombie3Pos = new Vector2(-(zombieRadius * 2), zombie3Y); break; //Left
                        }

                        if (loadingAlphaValue < 10)
                        {
                            if (zombie3A > 0)
                            {
                                zombie3List.Add(new Zombie3(zombie3Pos, zombie3));
                                zombie3A--;
                            }
                        }

                        foreach (Zombie3 z in zombie3List)
                        {
                            //Multi - 34
                            if (Vector2.Distance(z.zombiePosition, player.characterPosition) < Vector2.Distance(z.zombiePosition, player2.characterPosition))
                                z.facePos = player.characterPosition;
                            else
                                z.facePos = player2.characterPosition;

                            foreach (Health h in healthList)
                            {
                                if (h.active == true)
                                {
                                    if (BoundingCircle(player.characterPosition, playerRadius, z.zombiePosition, 30))
                                    {
                                        if (gameTime.TotalGameTime - previousHealthTime > healthTime)
                                        {
                                            GamePad.SetVibration(playerOne, 0.5f, 0.5f);
                                            isVibrating = true;
                                            vibTime = 0.5f;
                                            drawBlood = true;

                                            h.damageTaken += 2;
                                            if (h.damageTaken >= 3)
                                            {
                                                h.damageTaken = 2;
                                            }

                                            previousHealthTime = gameTime.TotalGameTime;
                                            player.charHealth -= 2;
                                        }
                                    }
                                }
                            }

                            //Multi - 35
                            foreach (Health h in healthList2)
                            {
                                if (h.active == true)
                                {
                                    if (BoundingCircle(player2.characterPosition, playerRadius, z.zombiePosition, 30))
                                    {
                                        if (gameTime.TotalGameTime - previousHealthTime2 > healthTime2)
                                        {
                                            GamePad.SetVibration(playerTwo, 0.5f, 0.5f);
                                            isVibrating2 = true;
                                            vibTime2 = 0.5f;
                                            drawBlood = true;

                                            h.damageTaken += 2;
                                            if (h.damageTaken >= 3)
                                            {
                                                h.damageTaken = 2;
                                            }

                                            previousHealthTime2 = gameTime.TotalGameTime;
                                            player2.charHealth -= 2;
                                        }
                                    }
                                }
                            }

                            foreach (Bullet b in bulletList)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, z.zombiePosition, zombieRadius))
                                {
                                    z.damageTaken -= b.damage;
                                    addBlood(bloodTex, z.zombiePosition, z.orientation);
                                    //Multi - 36
                                    if (player.currentWeapon != 4 || player2.currentWeapon != 4)
                                        b.active = false;

                                    if (z.damageTaken <= 0)
                                    {
                                        score1 += 50;
                                        z.active = false;
                                    }
                                }
                            }

                            foreach (Bullet b in bulletList2)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, z.zombiePosition, zombieRadius))
                                {
                                    z.damageTaken -= b.damage;
                                    addBlood(bloodTex, z.zombiePosition, z.orientation);
                                    //Multi - 36
                                    if (player.currentWeapon != 4 || player2.currentWeapon != 4)
                                        b.active = false;

                                    if (z.damageTaken <= 0)
                                    {
                                        score2 += 50;
                                        z.active = false;
                                    }
                                }
                            }

                            if (player.currentWeapon == 0)
                            {
                                if (gps.Buttons.B == ButtonState.Pressed && ogps.Buttons.B == ButtonState.Released)
                                {
                                    if (BoundingCircle(player.characterPosition, playerHitRadius, z.zombiePosition, zombieRadius))
                                    {
                                        z.damageTaken -= 5;

                                        z.zombiePosition += new Vector2((float)Math.Cos(player.orientation), (float)Math.Sin(player.orientation)) * 20f;
                                    }
                                }
                            }

                            //Multi - 37
                            if (player2.currentWeapon == 0)
                            {
                                if (gps2.Buttons.B == ButtonState.Pressed && ogps2.Buttons.B == ButtonState.Released)
                                {
                                    if (BoundingCircle(player2.characterPosition, playerHitRadius, z.zombiePosition, zombieRadius))
                                    {
                                        z.damageTaken -= 5;

                                        z.zombiePosition += new Vector2((float)Math.Cos(player2.orientation), (float)Math.Sin(player2.orientation)) * 20f;
                                    }
                                }
                            }
                        }

                        #endregion

                        #endregion

                        if (isVibrating == true)
                        {
                            vibTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                            if (vibTime <= 0)
                            {
                                GamePad.SetVibration(playerOne, 0, 0);
                                GamePad.SetVibration(playerTwo, 0, 0);
                                isVibrating = false;
                                drawBlood = false;
                            }
                        }

                        //Multi - 38
                        if (isVibrating2 == true)
                        {
                            vibTime2 -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                            if (vibTime2 <= 0)
                            {
                                GamePad.SetVibration(playerTwo, 0, 0);
                                isVibrating2 = false;
                                drawBlood = false;
                            }
                        }

                        if (loadingAlphaValue < 9)
                        {
                            if (zombie1List.Count <= 0 && zombie2List.Count <= 0 && zombie3List.Count <= 0)
                            {
                                waveNum++;
                                zombie1A = newZombies * waveNum;

                                if (waveNum >= 5)
                                {
                                    zombie2A = newZombies * waveNum;
                                }

                                if (waveNum >= 10)
                                {
                                    zombie3A = newZombies * waveNum;
                                }
                            }
                        }

                    #region GRID SHIT

                    //find grid square that player is in.

                    foreach (GridSquare r in gridList)
                    {
                        int tempX = (int)fixedPosP1.X;
                        int tempY = (int)fixedPosP1.Y;

                        if (tempX > (int)r.gridRectangle.X && tempX < (int)r.gridRectangle.X + r.gridTexture.Width)
                        {
                            playerGridPosX = ((int)r.gridRectangle.X / 48) - 13;
                        }

                        if (tempY > (int)r.gridRectangle.Y && tempY < (int)r.gridRectangle.Y + r.gridTexture.Height)
                        {
                            playerGridPosY = ((int)r.gridRectangle.Y / 48) - 7;
                        }

                        crosshairGridPosX = (int)(playerGridPosX + Math.Round((crosshair.crosshairsPosition.X - player.characterPosition.X) / 50));
                        crosshairGridPosY = (int)(playerGridPosY + Math.Round((crosshair.crosshairsPosition.Y - player.characterPosition.Y) / 50));
                    }

                    //Multi - 42
                    foreach (GridSquare r in gridList)
                    {
                        int tempX = (int)fixedPosP2.X;
                        int tempY = (int)fixedPosP2.Y;

                        if (tempX > (int)r.gridRectangle.X && tempX < (int)r.gridRectangle.X + r.gridTexture.Width)
                        {
                            playerGridPosX2 = ((int)r.gridRectangle.X / 48) - 13;
                        }

                        if (tempY > (int)r.gridRectangle.Y && tempY < (int)r.gridRectangle.Y + r.gridTexture.Height)
                        {
                            playerGridPosY2 = ((int)r.gridRectangle.Y / 48) - 7;
                        }

                        crosshairGridPosX2 = (int)(playerGridPosX2 + Math.Round((crosshair2.crosshairsPosition.X - player2.characterPosition.X) / 50));
                        crosshairGridPosY2 = (int)(playerGridPosY2 + Math.Round((crosshair2.crosshairsPosition.Y - player2.characterPosition.Y) / 50));
                    }

                    #endregion

                    #region UNLOCKING SHIT

                    if (score1 >= 100)
                        smg.active = true;

                    if (score2 >= 100)
                        smg2.active = true;

                    if (score1 >= 250)
                        shotgun.active = true;

                    if (score2 >= 250)
                        shotgun2.active = true;

                    if (score1 >= 500)
                        sniper.active = true;

                    if (score2 >= 500)
                        sniper2.active = true;

                    if (score1 >= 4000)
                        inventory.isGloveAvailable = 0;

                    if (score2 >= 4000)
                        inventory2.isGloveAvailable = 0;

                    if (waveNum % 5 == 0 && isHeald == false)
                    {
                        if (player.charHealth <= 5)
                        {
                            if (player.charHealth % 2 == 0)
                            {
                                //fills up the health for the heart after it.
                                healthList[2 - (player.charHealth / 2)].damageTaken -= 2;
                                healthList[2 - (player.charHealth / 2)].active = true;
                                player.charHealth += 2;
                            }

                            else if (player.charHealth == 1)
                            {
                                healthList[2].damageTaken--;
                                healthList[1].damageTaken--;
                                healthList[1].active = true;
                                player.charHealth += 2;
                            }

                            else if (player.charHealth == 3)
                            {
                                healthList[1].damageTaken--;
                                healthList[0].damageTaken--;
                                healthList[0].active = true;
                                player.charHealth += 2;
                            }

                            else if (player.charHealth == 5)
                            {
                                healthList[0].damageTaken--;
                                player.charHealth++;
                            }

                        }

                        isHeald = true;
                    }
                    else
                    {
                        if (waveNum % 5 != 0)
                        {
                            isHeald = false; 
                        }
                    }

                    if (waveNum % 5 == 0 && isHeald2 == false)
                    {
                        if (player2.charHealth <= 5)
                        {
                            if (player2.charHealth % 2 == 0)
                            {
                                //fills up the health for the heart after it.
                                healthList2[2 - (player2.charHealth / 2)].damageTaken -= 2;
                                healthList2[2 - (player2.charHealth / 2)].active = true;
                                player2.charHealth += 2;
                            }

                            else if (player2.charHealth == 1)
                            {
                                healthList2[2].damageTaken--;
                                healthList2[1].damageTaken--;
                                healthList2[1].active = true;
                                player2.charHealth += 2;
                            }

                            else if (player2.charHealth == 3)
                            {
                                healthList2[1].damageTaken--;
                                healthList2[0].damageTaken--;
                                healthList2[0].active = true;
                                player2.charHealth += 2;
                            }

                            else if (player2.charHealth == 5)
                            {
                                healthList[0].damageTaken--;
                                player2.charHealth++;
                            }

                        }

                        isHeald2 = true; 
                    }
                    else
                    {
                        if (waveNum % 5 != 0)
                        {
                            isHeald2 = false;
                        }
                    }

                    #endregion


                }

                else
                {
                    dayMusicI.Pause();
                    nightMusicI.Pause();
                    scrollOffset = Vector2.Zero; // Stops Pause Screen

                    #region PAUSE SHIT

                    if (gps.DPad.Down == ButtonState.Pressed && ogps.DPad.Down == ButtonState.Released)
                    {
                        if (pauseSelection < pauseMax)
                        {
                            if (pauseSelection == 2)
                            {
                                pauseSelection += 2;
                                pauseSelectorPosition.Y += 69 * 2;
                            }
                            else
                            {
                                pauseSelection++;
                                pauseSelectorPosition.Y += 69;
                            }
                        }

                        else
                        {
                            pauseSelection = 1;
                            pauseSelectorPosition.Y -= 207;
                        }

                        click.Play();

                    }
                    else if ((gps.ThumbSticks.Left.Y < -0.25))
                    {
                        if (gameTime.TotalGameTime - DprevChangeTime > DchangeTime)
                        {
                            DprevChangeTime = gameTime.TotalGameTime;

                            if (pauseSelection < pauseMax)
                            {
                                if (pauseSelection == 2)
                                {
                                    pauseSelection += 2;
                                    pauseSelectorPosition.Y += 69 * 2;
                                }
                                else
                                {
                                    pauseSelection++;
                                    pauseSelectorPosition.Y += 69;
                                }
                            }
                            else
                            {
                                pauseSelection = 1;
                                pauseSelectorPosition.Y -= 207;
                            }

                            click.Play();
                        }
                    }


                    if (gps.DPad.Up == ButtonState.Pressed && ogps.DPad.Up == ButtonState.Released)
                    {
                        if (pauseSelection > 1)
                        {
                            if (pauseSelection == 4)
                            {
                                pauseSelection -= 2;
                                pauseSelectorPosition.Y -= 69 * 2;
                            }
                            else
                            {
                                pauseSelection--;
                                pauseSelectorPosition.Y -= 69;
                            }
                        }

                        else
                        {
                            pauseSelection = pauseMax;
                            pauseSelectorPosition.Y += 207;
                        }

                        click.Play();
                    }
                    else if ((gps.ThumbSticks.Left.Y > 0.25))
                    {
                        if (gameTime.TotalGameTime - prevChangeTime > changeTime)
                        {
                            prevChangeTime = gameTime.TotalGameTime;

                            if (pauseSelection > 1)
                            {
                                if (pauseSelection == 4)
                                {
                                    pauseSelection -= 2;
                                    pauseSelectorPosition.Y -= 69 * 2;
                                }
                                else
                                {
                                    pauseSelection--;
                                    pauseSelectorPosition.Y -= 69;
                                }
                            }
                            else
                            {
                                pauseSelection = pauseMax;
                                pauseSelectorPosition.Y += 207;
                            }

                            click.Play();
                        }

                    }

                    //if(isMenuActive == true)
                    {
                        if (pauseSelection == 1)
                        {
                            //pauseSelectorPosition1 = new Vector2(484, 268);
                            if (gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                            {
                                isMenuActive = false;
                                isMenuOn--;
                            }
                        }

                        if (pauseSelection == 2)
                        {
                            if ((gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released) && isMusicNum == 1)
                            {
                                isMusicOff = true;
                                isMusicNum = 2;
                                pistolShot.Play();
                            }

                            else if (gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released && isMusicNum == 2)
                            {
                                isMusicOff = false;
                                isMusicNum = 1;
                                shotgunShot.Play();

                            }
                        }

                        if (pauseSelection == 3)
                        {
                            //pauseSelectorPosition1 = new Vector2(484, 406);
                            if (gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                            {
                                screenEvent.Invoke(this, new EventArgs());
                            }
                        }

                        if (pauseSelection == 4)
                        {
                            //pauseSelectorPosition1 = new Vector2(484, 406);
                            if (gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                            {
                                screenEvent.Invoke(this, new EventArgs());
                            }
                        }
                    }

                    #endregion

                    //Multi - 46
                    #region PAUSE SHIT2

                    if (gps2.DPad.Down == ButtonState.Pressed && ogps2.DPad.Down == ButtonState.Released)
                    {
                        if (pauseSelection < pauseMax)
                        {
                            if (pauseSelection == 2)
                            {
                                pauseSelection += 2;
                                pauseSelectorPosition.Y += 69 * 2;
                            }
                            else
                            {
                                pauseSelection++;
                                pauseSelectorPosition.Y += 69;
                            }
                        }

                        else
                        {
                            pauseSelection = 1;
                            pauseSelectorPosition.Y -= 207;
                        }

                        click.Play();

                    }
                    else if ((gps2.ThumbSticks.Left.Y < -0.25))
                    {
                        if (gameTime.TotalGameTime - DprevChangeTime > DchangeTime)
                        {
                            DprevChangeTime = gameTime.TotalGameTime;

                            if (pauseSelection < pauseMax)
                            {
                                if (pauseSelection == 2)
                                {
                                    pauseSelection += 2;
                                    pauseSelectorPosition.Y += 69 * 2;
                                }
                                else
                                {
                                    pauseSelection++;
                                    pauseSelectorPosition.Y += 69;
                                }
                            }
                            else
                            {
                                pauseSelection = 1;
                                pauseSelectorPosition.Y -= 207;
                            }

                            click.Play();
                        }
                    }


                    if (gps2.DPad.Up == ButtonState.Pressed && ogps2.DPad.Up == ButtonState.Released)
                    {
                        if (pauseSelection > 1)
                        {
                            if (pauseSelection == 4)
                            {
                                pauseSelection -= 2;
                                pauseSelectorPosition.Y -= 69 * 2;
                            }
                            else
                            {
                                pauseSelection--;
                                pauseSelectorPosition.Y -= 69;
                            }
                        }

                        else
                        {
                            pauseSelection = pauseMax;
                            pauseSelectorPosition.Y += 207;
                        }

                        click.Play();
                    }
                    else if ((gps2.ThumbSticks.Left.Y > 0.25))
                    {
                        if (gameTime.TotalGameTime - prevChangeTime > changeTime)
                        {
                            prevChangeTime = gameTime.TotalGameTime;

                            if (pauseSelection > 1)
                            {
                                if (pauseSelection == 4)
                                {
                                    pauseSelection -= 2;
                                    pauseSelectorPosition.Y -= 69 * 2;
                                }
                                else
                                {
                                    pauseSelection--;
                                    pauseSelectorPosition.Y -= 69;
                                }
                            }
                            else
                            {
                                pauseSelection = pauseMax;
                                pauseSelectorPosition.Y += 207;
                            }

                            click.Play();
                        }

                    }

                    //if(isMenuActive == true)
                    {
                        if (pauseSelection == 1)
                        {
                            //pauseSelectorPosition1 = new Vector2(484, 268);
                            if (gps2.Buttons.A == ButtonState.Pressed && ogps2.Buttons.A == ButtonState.Released)
                            {
                                isMenuActive = false;
                                isMenuOn--;
                            }
                        }

                        if (pauseSelection == 2)
                        {
                            if ((gps2.Buttons.A == ButtonState.Pressed && ogps2.Buttons.A == ButtonState.Released) && isMusicNum == 1)
                            {
                                isMusicOff = true;
                                isMusicNum = 2;
                                pistolShot.Play();
                            }

                            else if (gps2.Buttons.A == ButtonState.Pressed && ogps2.Buttons.A == ButtonState.Released && isMusicNum == 2)
                            {
                                isMusicOff = false;
                                isMusicNum = 1;
                                shotgunShot.Play();

                            }
                        }

                        if (pauseSelection == 3)
                        {
                            //pauseSelectorPosition1 = new Vector2(484, 406);
                            if (gps2.Buttons.A == ButtonState.Pressed && ogps2.Buttons.A == ButtonState.Released)
                            {
                                screenEvent.Invoke(this, new EventArgs());
                            }
                        }

                        if (pauseSelection == 4)
                        {
                            //pauseSelectorPosition1 = new Vector2(484, 406);
                            if (gps2.Buttons.A == ButtonState.Pressed && ogps2.Buttons.A == ButtonState.Released)
                            {
                                screenEvent.Invoke(this, new EventArgs());
                            }
                        }
                    }

                    #endregion
                }

                #region Also PAUSE SHIT

                if ((gps.Buttons.Start == ButtonState.Pressed && ogps.Buttons.Start == ButtonState.Released) && isMenuOn == 1)
                {
                    isMenuActive = true;
                    isMenuOn++;
                }
                else
                {
                    if ((gps.Buttons.Start == ButtonState.Pressed && ogps.Buttons.Start == ButtonState.Released && isMenuOn == 2))
                    {
                        isMenuOn--;
                        isMenuActive = false;
                    }
                }

                #endregion

                //Multi - 47
                #region Also PAUSE SHIT2

                if ((gps2.Buttons.Start == ButtonState.Pressed && ogps2.Buttons.Start == ButtonState.Released) && isMenuOn == 1)
                {
                    isMenuActive = true;
                    isMenuOn++;
                }
                else
                {
                    if ((gps2.Buttons.Start == ButtonState.Pressed && ogps2.Buttons.Start == ButtonState.Released && isMenuOn == 2))
                    {
                        isMenuOn--;
                        isMenuActive = false;
                    }
                }

                #endregion

                if (isDayPassed == false)
                {
                    if ((int)timer == dayTime + nightTime - 1)
                    {
                        numberOfNights++;
                        score1 += 150;
                        score2 += 150;
                        isDayPassed = true;
                    }
                }

                if ((int)timer == 0)
                    isDayPassed = false;

                previousNumberOfNights = numberOfNights;
                oks = ks;
                ogps = gps;
                ogps2 = gps2;
                base.Update(gameTime);

            } // If Health is 0

            else
            {
                scrollOffset = Vector2.Zero; // Stops Death Screen

                dead = true;
                dayMusicI.Stop();
                nightMusicI.Stop();

                deadTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (deadTime <= 0)
                {
                    GamePad.SetVibration(playerOne, 0, 0);
                    GamePad.SetVibration(playerTwo, 0, 0);
                    screenEvent.Invoke(this, new EventArgs());
                }
            }

            if (healthList.Count > 0)
                updateHealth();

            if (player.charHealth <= 0)
                healthList[healthList.Count - 1].damageTaken = 2;

            //Multi - 48
            if (healthList2.Count > 0)
                updateHealth2();

            if (player2.charHealth <= 0)
                healthList2[healthList2.Count - 1].damageTaken = 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            grassPosition += scrollOffset;
            topPosition += scrollOffset;
            bottomPosition += scrollOffset;
            leftPosition += scrollOffset;
            rightPosition += scrollOffset;

            spriteBatch.Draw(grass, grassPosition, Color.White); //T1
            spriteBatch.Draw(grass, new Vector2(grassPosition.X + 2048, grassPosition.Y), Color.White); //T2
            spriteBatch.Draw(grass, new Vector2(grassPosition.X, grassPosition.Y + 2048), Color.White); //T3
            spriteBatch.Draw(grass, new Vector2(grassPosition.X + 2048, grassPosition.Y + 2048), Color.White); //T4  

            /*
             * 
             * T1 T2
             * T3 T4
             * 
             */

            foreach (GridSquare g in gridList)
            {
                g.gridPosition += scrollOffset;
                g.Draw(spriteBatch);
            }

            foreach (Rock1 t in rock1List)
            {
                t.rock1Position += scrollOffset;
                t.Draw(spriteBatch);
            }

            foreach (Dune1 t in dune1List)
            {
                t.dune1Position += scrollOffset;
                t.Draw(spriteBatch);
            }

            foreach (Blood b in bloodList)
            {
                b.bloodPosition += scrollOffset;
                b.Draw(spriteBatch);
            }

            foreach (Zombie1 z in zombie1List)
            {
                z.zombiePosition += scrollOffset;
                z.Draw(spriteBatch);
            }

            foreach (Zombie2 z in zombie2List)
            {
                z.zombiePosition += scrollOffset;
                z.Draw(spriteBatch);
            }

            foreach (Zombie3 z in zombie3List)
            {
                z.zombiePosition += scrollOffset;
                z.Draw(spriteBatch);
            }

            #region Player Draw

            //Character Shadow

            shadowPosition += scrollOffset;
            spriteBatch.Draw(shadowTexture, shadowPosition, Color.White);

            shadowPosition2 += scrollOffset;
            spriteBatch.Draw(shadowTexture, shadowPosition2, Color.White);

            player.characterPosition += scrollOffset;
            player.Draw(spriteBatch);

            #endregion

            //Multi - 49
            #region Player Draw2

            player2.characterPosition += scrollOffset;
            player2.Draw(spriteBatch);

            #endregion


            crosshair.crosshairsPosition += scrollOffset;
            crosshair2.crosshairsPosition += scrollOffset;

            if (player.currentWeapon != 4 || player2.currentWeapon != 4)
                crosshair.Draw(spriteBatch);
            //else is in player.draw area

            if (drawBlood == true)
            {
                spriteBatch.Draw(bloodSplash, Vector2.Zero, Color.White);
            }

            foreach (Tree1 t in tree1List)
            {
                t.tree1Position += scrollOffset;
                t.Draw(spriteBatch);
            }

            foreach (Tree2 t in tree2List)
            {
                t.tree2Position += scrollOffset;
                t.Draw(spriteBatch);
            }

            //Boundaries
            spriteBatch.Draw(verticalBoundaryTexture, topPosition, Color.White);
            spriteBatch.Draw(verticalBoundaryTexture, new Vector2(topPosition.X + 2000, topPosition.Y), Color.White);

            spriteBatch.Draw(horizontalBoundaryTexture, leftPosition, Color.White);
            spriteBatch.Draw(horizontalBoundaryTexture, new Vector2(leftPosition.X, topPosition.Y + 2000), Color.White);

            spriteBatch.Draw(horizontalBoundaryTexture, new Vector2(leftPosition.X + horizontalBoundaryTexture.Width - 75, leftPosition.Y), Color.White);
            spriteBatch.Draw(horizontalBoundaryTexture, new Vector2(leftPosition.X + horizontalBoundaryTexture.Width - 75, topPosition.Y + 2000), Color.White);

            spriteBatch.Draw(verticalBoundaryTexture, bottomPosition, Color.White);
            spriteBatch.Draw(verticalBoundaryTexture, new Vector2(bottomPosition.X + 2000, bottomPosition.Y), Color.White);

            spriteBatch.Draw(horizontalBoundaryTexture, rightPosition, Color.White);
            spriteBatch.Draw(horizontalBoundaryTexture, new Vector2(rightPosition.X, topPosition.Y + 2000), Color.White);

            spriteBatch.Draw(horizontalBoundaryTexture, new Vector2(rightPosition.X + horizontalBoundaryTexture.Width - 48, rightPosition.Y), Color.White);
            spriteBatch.Draw(horizontalBoundaryTexture, new Vector2(rightPosition.X + horizontalBoundaryTexture.Width - 48, topPosition.Y + 2000), Color.White);

            if (isNightTexOn == true)
                spriteBatch.Draw(nightTex, Vector2.Zero, new Color(255, 255, 255, (byte)MathHelper.Clamp(nightAlphaValue, 0, 255)));

            foreach (Bullet b in bulletList2)
            {
                b.bulletPosition += scrollOffset;
                b.Draw(spriteBatch);
            }

            foreach (Bullet b in bulletList)
            {
                b.bulletPosition += scrollOffset;
                b.Draw(spriteBatch);
            }

            foreach (Health h in healthList)
            {
                h.Draw(spriteBatch);
            }

            foreach (Health h in healthList2)
            {
                h.Draw(spriteBatch);
            }

            spriteBatch.DrawString(fontBig, "PLAYER 1: " + score1, new Vector2(50, 100), Color.Purple);
            spriteBatch.DrawString(fontBig, "PLAYER 2: " + score2, new Vector2(50, 160), Color.Purple);
            spriteBatch.DrawString(fontBig, "WAVE: " + waveNum, new Vector2(1280/2 - 90, 30), Color.Purple);

            miniInventory.Draw(spriteBatch);
            miniInventory2.Draw(spriteBatch);

            if (isLoadingTexOn == true)
                spriteBatch.Draw(loadingTex, Vector2.Zero, new Color(255, 255, 255, (byte)MathHelper.Clamp(loadingAlphaValue, 0, 255)));
              //This draws the muzzle flare.
            if (drawMuzzleFlare1 == true)
            {
                spriteBatch.Draw(muzzleFlare1, player.characterPosition + (new Vector2((float)Math.Cos(player.orientation), (float)Math.Sin(player.orientation)) * 10) * 4, null, Color.White,
                                    crosshair.crosshairsRot, new Vector2(muzzleFlare1.Width / 2, muzzleFlare1.Height / 2), 1, SpriteEffects.None, 0f);
                drawMuzzleFlare1 = false;
            }

            if (drawMuzzleFlare2 == true)
            {
                spriteBatch.Draw(muzzleFlare2, player.characterPosition + (new Vector2((float)Math.Cos(player.orientation), (float)Math.Sin(player.orientation)) * 10) * 5, null, Color.White,
                                    crosshair.crosshairsRot, new Vector2(muzzleFlare2.Width / 2, muzzleFlare2.Height / 2), 1, SpriteEffects.None, 0f);
                drawMuzzleFlare2 = false;
            }

            if (drawMuzzleFlare3 == true)
            {
                spriteBatch.Draw(muzzleFlare3, player.characterPosition + (new Vector2((float)Math.Cos(player.orientation), (float)Math.Sin(player.orientation)) * 10) * 7, null, Color.White,
                                    crosshair.crosshairsRot, new Vector2(muzzleFlare3.Width / 2, muzzleFlare3.Height / 2), 1, SpriteEffects.None, 0f);
                drawMuzzleFlare3 = false;
            }

            //Multi - 50
            if (drawMuzzleFlare12 == true)
            {
                spriteBatch.Draw(muzzleFlare1, player2.characterPosition + (new Vector2((float)Math.Cos(player2.orientation), (float)Math.Sin(player2.orientation)) * 10) * 4, null, Color.White,
                                    crosshair2.crosshairsRot, new Vector2(muzzleFlare1.Width / 2, muzzleFlare1.Height / 2), 1, SpriteEffects.None, 0f);
                drawMuzzleFlare12 = false;
            }

            if (drawMuzzleFlare22 == true)
            {
                spriteBatch.Draw(muzzleFlare2, player2.characterPosition + (new Vector2((float)Math.Cos(player2.orientation), (float)Math.Sin(player2.orientation)) * 10) * 5, null, Color.White,
                                    crosshair2.crosshairsRot, new Vector2(muzzleFlare2.Width / 2, muzzleFlare2.Height / 2), 1, SpriteEffects.None, 0f);
                drawMuzzleFlare22 = false;
            }

            if (drawMuzzleFlare32 == true)
            {
                spriteBatch.Draw(muzzleFlare3, player2.characterPosition + (new Vector2((float)Math.Cos(player2.orientation), (float)Math.Sin(player2.orientation)) * 10) * 7, null, Color.White,
                                    crosshair2.crosshairsRot, new Vector2(muzzleFlare3.Width / 2, muzzleFlare3.Height / 2), 1, SpriteEffects.None, 0f);
                drawMuzzleFlare32 = false;
            }

            if (player.charHealth <= 0)
            {
                isNightTexOn = false;
                spriteBatch.Draw(deadScreen, Vector2.Zero, new Color(255, 255, 255, (byte)MathHelper.Clamp(deathAlphaValue, 0, 255)));
            }

            //Multi - 51
            if (player2.charHealth <= 0)
            {
                isNightTexOn = false;
                spriteBatch.Draw(deadScreen, Vector2.Zero, new Color(255, 255, 255, (byte)MathHelper.Clamp(deathAlphaValue, 0, 255)));
            }

            #region Pause

            if (isMenuActive == true)
            {
                spriteBatch.Draw(pauseMenuTexture, pausePosition, Color.White);
                spriteBatch.Draw(pauseSelector, pauseSelectorPosition, Color.White);

                if (isMusicNum == 2)
                {
                    spriteBatch.Draw(musicOff, new Vector2(625, 347), Color.White);
                }
            }

            #endregion

            // Dev Item

            #region Devpoop

            //if (gps.Buttons.A == ButtonState.Pressed || gps2.Buttons.A == ButtonState.Pressed)
            //{
            //    foreach (Tree1 t in tree1List)
            //        spriteBatch.Draw(collisionBox, t.tree1CollisionRectangle, Color.White);

            //    spriteBatch.Draw(collisionBox, hands.handsRectangle, Color.White);
            //    spriteBatch.Draw(collisionBox, player.characterCollisionRectangle, Color.White);
            //    spriteBatch.Draw(collisionBox, hands2.handsRectangle, Color.White);
            //    spriteBatch.Draw(collisionBox, player2.characterCollisionRectangle, Color.White);
            //    spriteBatch.Draw(collisionBox, rightBRect, Color.White);
            //    spriteBatch.Draw(collisionBox, leftBRect, Color.White);
            //    spriteBatch.Draw(collisionBox, topBRect, Color.White);
            //    spriteBatch.Draw(collisionBox, botBRect, Color.White);
            //    spriteBatch.Draw(collisionBox, scrollOffset, Color.White);

            //    spriteBatch.DrawString(font, "Scroll Offset:" + scrollOffset.ToString(), new Vector2(100, 440), Color.Black);
            //    spriteBatch.DrawString(font, "FixedPos:" + fixedPos.ToString(), new Vector2(100, 485), Color.Black);
            //    spriteBatch.DrawString(font, "FixedPosP1:" + fixedPosP1.ToString(), new Vector2(100, 500), Color.Black);
            //    spriteBatch.DrawString(font, "FixedPosP2:" + fixedPosP2.ToString(), new Vector2(100, 515), Color.Black);
            //    //spriteBatch.DrawString(font, "", new Vector2(100, 530), Color.Black);
            //    spriteBatch.DrawString(font, "Timer:" + timer.ToString(), new Vector2(100, 590), Color.Black);

            //    spriteBatch.DrawString(font, "CharacterPos:" + player.characterPosition.ToString(), new Vector2(100, 650), Color.Black);
            //    spriteBatch.DrawString(font, "Character GridX:" + playerGridPosX.ToString(), new Vector2(100, 665), Color.Black);
            //    spriteBatch.DrawString(font, "Character GridY:" + playerGridPosY.ToString(), new Vector2(100, 680), Color.Black);
            //    spriteBatch.DrawString(font, "Crosshair GridX:" + crosshairGridPosX.ToString(), new Vector2(300, 665), Color.Black);
            //    spriteBatch.DrawString(font, "Crosshair GridY:" + crosshairGridPosY.ToString(), new Vector2(300, 680), Color.Black);
            //    spriteBatch.DrawString(font, "Trees Count " + tree1List.Count.ToString(), new Vector2(300, 500), Color.Black);
            //}

            #endregion

            base.Draw(spriteBatch);
        }

        public void resetAll(GameTime gameTime)
        {
            timer = 0;
            loadingTime = 0;
            isLoading = true;
            isBlack = false;
            //The reset code should go here, but the code is being gay, so its located at : loading shit
        }

        #region Methods

        #region Bullets

        public void addBullet(Vector2 bp, Pistol p, int d)
        {
            Bullet bulletObj = new Bullet();
            bulletObj.rotate = p.gunRot;
            bulletObj.bulletFacts(bullet, bp, d);
            bulletList.Add(bulletObj);
        }



        public void addBullet2(Vector2 bp, SMG g, int d)
        {
            Bullet bulletObj = new Bullet();
            bulletObj.rotate = g.gunRot;
            bulletObj.bulletFacts(bullet, bp, d);
            bulletList.Add(bulletObj);
        }

        public void addBullet3(Vector2 bp, Shotgun g, int d, float br)
        {
            Bullet bulletObj = new Bullet();
            bulletObj.rotate = br;
            bulletObj.bulletFacts(bullet, bp, d);
            bulletList.Add(bulletObj);
        }

        public void addBullet4(Vector2 bp, Sniper g, int d)
        {
            Bullet bulletObj = new Bullet();
            bulletObj.rotate = g.gunRot;
            bulletObj.bulletFacts(bullet, bp, d);
            bulletList.Add(bulletObj);
        }

        public void add2Bullet(Vector2 bp, Pistol2 p, int d)
        {
            Bullet bulletObj = new Bullet();
            bulletObj.rotate = p.gunRot;
            bulletObj.bulletFacts(bullet, bp, d);
            bulletList2.Add(bulletObj);
        }

        public void add2Bullet2(Vector2 bp, SMG2 g, int d)
        {
            Bullet bulletObj = new Bullet();
            bulletObj.rotate = g.gunRot;
            bulletObj.bulletFacts(bullet, bp, d);
            bulletList2.Add(bulletObj);
        }

        public void add2Bullet3(Vector2 bp, Shotgun2 g, int d, float br)
        {
            Bullet bulletObj = new Bullet();
            bulletObj.rotate = br;
            bulletObj.bulletFacts(bullet, bp, d);
            bulletList2.Add(bulletObj);
        }

        public void add2Bullet4(Vector2 bp, Sniper2 g, int d)
        {
            Bullet bulletObj = new Bullet();
            bulletObj.rotate = g.gunRot;
            bulletObj.bulletFacts(bullet, bp, d);
            bulletList2.Add(bulletObj);
        }

        public void updateBullets()
        {
            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                bulletList[i].Update();

                if (bulletList[i].active == false)
                    bulletList.RemoveAt(i);
            }
        }

        public void updateBullets2()
        {
            for (int i = bulletList2.Count - 1; i >= 0; i--)
            {
                bulletList2[i].Update();

                if (bulletList2[i].active == false)
                    bulletList2.RemoveAt(i);
            }
        }

        #endregion

        #region Zombies

        public void addZombie1(Vector2 zp, Texture2D z)
        {
            Zombie1 zombies = new Zombie1(zp, z);
            zombie1List.Add(zombies);
        }

        public void updateZombie1s()
        {
            for (int i = zombie1List.Count - 1; i >= 0; i--)
            {
                zombie1List[i].Update();
                if (zombie1List[i].active == false)
                    zombie1List.RemoveAt(i);
            }
        }

        public void addZombie2(Vector2 zp, Texture2D z)
        {
            Zombie2 zombie2s = new Zombie2(zp, z);
            zombie2List.Add(zombie2s);
        }

        public void updateZombie2s()
        {
            for (int i = zombie2List.Count - 1; i >= 0; i--)
            {
                zombie2List[i].Update();
                if (zombie2List[i].active == false)
                {

                    zombie2List.RemoveAt(i);
                }
            }
        }

        public void addZombie3(Vector2 zp, Texture2D z)
        {
            Zombie3 zombie3s = new Zombie3(zp, z);
            zombie3List.Add(zombie3s);
        }

        public void updateZombie3s()
        {
            for (int i = zombie3List.Count - 1; i >= 0; i--)
            {
                zombie3List[i].Update();
                if (zombie3List[i].active == false)
                    zombie3List.RemoveAt(i);
            }
        }

        #endregion

        #region Trees

        public void updateTree1s()
        {
            for (int i = tree1List.Count - 1; i >= 0; i--)
            {
                tree1List[i].Update();
                if (tree1List[i].isTree1Broken == true)
                {
                    tree1List.RemoveAt(i);
                }
            }
        }

        public void updateTree2s()
        {
            for (int i = tree2List.Count - 1; i >= 0; i--)
            {
                tree2List[i].Update();
                if (tree2List[i].isTree2Broken == true)
                {
                    tree2List.RemoveAt(i);
                }
            }
        }

        #endregion

        #region Rocks

        public void updateRock1s()
        {
            for (int i = rock1List.Count - 1; i >= 0; i--)
            {
                rock1List[i].Update();
                if (rock1List[i].isrock1Broken == true)
                {
                    rock1List.RemoveAt(i);
                }
            }
        }

        #endregion

        #region Dune

        public void updateDune1s()
        {
            for (int i = dune1List.Count - 1; i >= 0; i--)
            {
                dune1List[i].Update();
                if (dune1List[i].isdune1Broken == true)
                {
                    dune1List.RemoveAt(i);
                }
            }
        }

        #endregion

        #region Health

        public void addHeart(Texture2D ht, Vector2 hp)
        {
            Health hearts = new Health(ht, hp);
            healthList.Add(hearts);
        }

        public void addHeart2(Texture2D ht, Vector2 hp)
        {
            Health hearts = new Health(ht, hp);
            healthList2.Add(hearts);
        }

        public void updateHealth()
        {
            for (int i = healthList.Count - 1; i >= 0; i--)
            {
                healthList[i].Update();
                //if (healthList[i].active == false)
                //    healthList.RemoveAt(i);
            }
        }

        public void updateHealth2()
        {
            for (int i = healthList2.Count - 1; i >= 0; i--)
            {
                healthList2[i].Update();
                //if (healthList[i].active == false)
                //    healthList.RemoveAt(i);
            }
        }

        #endregion

        #region Grid

        public void addGrid(Vector2 gp, Texture2D gt)
        {
            GridSquare gridObj = new GridSquare(gp, gt);
            gridList.Add(gridObj);
            gridIsOccupied.Add(false);
        }

        #endregion

        public void addBlood(Texture2D bt, Vector2 zp, float zr)
        {
            Blood bloodObj = new Blood(bt, zp, zr);
            bloodList.Add(bloodObj);
        }

        public void updateBlood(GameTime gt)
        {
            for (int i = bloodList.Count - 1; i >= 0; i--)
            {
                bloodList[i].Update(gt);

                if (bloodList[i].active == false)
                    bloodList.RemoveAt(i);
            }
        }

        public static bool BoundingCircle(Vector2 cir1, int radius1, Vector2 cir2, int radius2)
        {
            if (Vector2.Distance(cir1, cir2) <= radius1 + radius2)
                return true;

            return false;
        }

        public void playerCollision()
        {
            foreach (Tree1 t in tree1List)
            {
                if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(t.tree1Position.X + (t.tree1Texture.Width / t.tree1Frames / 2), t.tree1Position.Y + (t.tree1Texture.Height / 2)), 60))
                {
                    player.characterPosition = previousCharacterPos;
                }
            }

            foreach (Tree2 t in tree2List)
            {
                if (BoundingCircle(player2.characterPosition, playerRadius, new Vector2(t.tree2Position.X + (t.tree2Texture.Width / t.tree2Frames / 2), t.tree2Position.Y + (t.tree2Texture.Height / 2)), 60))
                {
                    player2.characterPosition = previousCharacterPos2;
                }
            }
        }

        #endregion

    }
}