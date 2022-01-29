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
    public class MainLevelScreen : Screen
    {
        SpriteFont font;
        SpriteFont fontBig;
        Rectangle levelBoundary;

        #region Sounds

        SoundEffect smgShot;
        SoundEffect smgCock;
        SoundEffect pistolShot;
        SoundEffect pistolCock;
        SoundEffect shotgunShot;
        SoundEffect shotgunCock;
        SoundEffect sniperShot;
        SoundEffect sniperCock;
        SoundEffect gunClick;
        public bool isGunClickPlaying = false;

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

        Shadow shadow;
        public Texture2D shadowTexture;
        public Vector2 shadowPosition;

        public int playerRadius = 24;
        public int playerHitRadius = 36;

        public Texture2D manos;
        public Hands hands;
        public Crosshair crosshair;
        public Texture2D reticle;

        public Texture2D deadScreen;
        public int deathAlphaValue = 1;

        public int playerGridPosX = 28;
        public int playerGridPosY = 34;
        public int crosshairGridPosX;
        public int crosshairGridPosY;

        public int crosshairPos;

        public Vector2 fixedPos = new Vector2(2048 , 2048);
        public Vector2 fixedPos2 = new Vector2(1408, 1688);
        public Vector2 crosshairCenter;

        int gridLocX;
        int gridLocY;

        public List<int> currentWeaponAavailable;

        public bool chooseWeapon = false;

        int choseWeapon = 0;

        Texture2D muzzleFlare1;
        bool drawMuzzleFlare1 = false;

        Texture2D muzzleFlare2;
        bool drawMuzzleFlare2 = false;

        Texture2D muzzleFlare3;
        bool drawMuzzleFlare3 = false;

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

        #region Chest

        public int chestRadius = 10;

        public Texture2D chestTexture;
        public Vector2 chestPosition;

        public int chestA; //this will always be one. 

        int chestUpgrade; // Ext. Mag, Scope, Shells, Extra Heart
        int chestDrop; // Wood, Sand, Stone, Metal, Glass, Zombie Drank

        bool chestPosIsSet = false;

        int chestIsSet = 1;

        bool foundThatDay = false;

        #endregion

        #region Health

        public Texture2D health;
        public Vector2 healthPosition = new Vector2(160, 40);
        //public int healthDamageTaken = 0;
        //public int healthFrames = 3;

        public Rectangle healthSourceRectangle;

        #endregion

        #region Terrain Tiles

        Texture2D grass;
        Vector2 grassPosition = new Vector2 (0,0);

        Texture2D verticalBoundaryTexture;
        Texture2D horizontalBoundaryTexture;

        Vector2 topPosition = new Vector2(0, 8); //add 8 for grid
        Vector2 leftPosition = new Vector2(16, 0); //add 16 for grid
        Vector2 bottomPosition = new Vector2(0, 3728); //subtract 8 for grid
        Vector2 rightPosition = new Vector2(3440, 0); //subtract 16 for grid

        #endregion

        #region Inventory

        public Texture2D inventoryScreen;
        public Texture2D inventorySelector;
        public Vector2 inventoryScreenPos = Vector2.Zero;
        public Vector2 inventorySelectorPos;
        public Texture2D inventoryMaterials;
        public Vector2 inventoryMaterialsPos = new Vector2 (464,40);
        public Texture2D inventoryWeapons;
        public Vector2 inventoryWeaponsPos = new Vector2(464, 40);
        public Texture2D inventoryMaterials2;
        public Vector2 inventoryMaterials2Pos = new Vector2(464, 40);

        public List<int> questionsList;
        Texture2D questionMarkTexture;

        public int selMax = 3;
        public int currentSelPos = 0;
        public int curInvList = 0;
        public int isShowing = 1;

        public Vector2 numberOfNightsPosition;
        public int numberOfNights = 0;        
        public int previousNumberOfNights;
        public int kills;
        public Vector2 killsPosition;
        public int score;
        public Vector2 scorePosition;
        
        
        

        public bool isInventoryShowing = false;
        public bool isInventoryMaterialsShowing = false;
        public bool isInventoryWeaponsShowing = false;
        public bool isInventoryMaterials2Showing = false;

        public Inventory inventory; 

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

        #region MiniInventory

        Texture2D miniTexture;
        Vector2 miniPosition;

        public MiniInventory miniInventory;

        #endregion

        #region Dpad Hotkey

        public Texture2D hotkeys;
        public Vector2 hotkeysPosition;

        #endregion

        #region Material Drops

        Texture2D woodDrop;
        Texture2D appleDrop;
        Texture2D rockDrop;
        Texture2D metalOreDrop;
        Texture2D sandDrop;        
        Texture2D clothDrop;
        Texture2D foodDrop;
        Texture2D energyDrop;
        Texture2D glassDrop;
        Texture2D metalDrop;

        Texture2D extendedMagDrop;
        Texture2D shellsDrop;
        Texture2D scopeDrop;

        public List<Drop> drops;

        float dropX;
        float dropY;
        int dropA;

        #endregion 

        #region Place-able Items

        Texture2D placedWoodTex;
        Texture2D placedStoneTex;
        Texture2D placedSandTex;
        Texture2D placedDoorTex;
        Texture2D placedWindowTex;
        Texture2D placedBedTex;
        Texture2D placedSeedTex;

        public List<Placement> placementList;

        int placementRadius = 22;
        bool isPlacing = false;
        int isOpen = 0;
        bool isOpen2 = false;
        bool Upass = false; 

        #endregion

        #region Achievements

        public List<int> achievementsUnlocked; 
        public bool iNeedaWeapon = false;
        public bool thankTheLord = false;
        public bool riotControl = false;
        public bool headShot = false;
        public bool killShot = false;
        public bool over9000 = false;
        public bool zombiecide = false;
        public bool countingSheep = false;
        public bool enjoyTheLittleThings = false;
        public bool survivor = false;
        public bool itTastesLikeLaser = false;
        public bool fortNight = false;

        public int killshots;
        public int zombieKills;
        public List<int> rareItems;

        

        #endregion

        public Vector2 screenCenter;
        public Vector2 scrollOffset;

        #region Bullets

        public Texture2D bullet;
        public Vector2 bulletPosition;

        #endregion

        public Texture2D collisionBox;

        #region Rectangles

        public Rectangle playerRectangle;

        #endregion


        #region Lists

        public List<Health> healthList;
        public List<Bullet> bulletList;
        public List<Zombie1> zombie1List;
        public List<Zombie2> zombie2List;
        public List<Zombie3> zombie3List;
        public List<Tree1> tree1List;
        public List<Tree2> tree2List;
        public List<Rock1> rock1List;
        public List<Dune1> dune1List;
        public List<Chest> chestList;
        public List<Blood> bloodList;
        public List<Vector2> seedList; 

        #endregion

        #region States

        KeyboardState ks;
        KeyboardState oks;
        // MouseState ms;
        public GamePadState gps;
        public GamePadState ogps;

        #endregion

        #region Times

        TimeSpan fireTime;

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

        //For pause menu
        TimeSpan changeTime;
        TimeSpan prevChangeTime;
        TimeSpan DchangeTime;
        TimeSpan DprevChangeTime;

        int dayTime = 175;
        int nightTime = 300;

        double nightDelay = 0.35;
        int nightAlphaValue = 1;
        int nightFadeIncrement = 3;

        bool isDayTime = true;
        bool isDayPassed = false;

        double loadingTime = 0;
        float loadingTimer = 7;

        double loadingDelay = 0.35;
        public int loadingAlphaValue = 1;
        int loadingFadeIncrement = 3;

        bool isLoading = false;
        bool isBlack = false;

        float timer; 

        Texture2D nightTex;
        Texture2D loadingTex;

        public bool isNightTexOn = true;
        public bool isLoadingTexOn = true;

        #endregion

        #region Grid
       
        Texture2D gridTex;
        public List<GridSquare> gridList;
        public List<bool> gridIsOccupied; 

        int gridY;
        int gridX;

        Vector2 gridCenter;

        #endregion

        public bool isShooting = false;
        public bool isBreaking = false;

        public bool drawBlood = false;
        public bool isVibrating = false;
        public float vibTime = 0f;

        public bool isReset = false; 

        public Random random = new Random();

        #region Constructor/LoadContent
        public MainLevelScreen(ContentManager MLscreenContent, EventHandler MLscreenEvent)
            : base(MLscreenEvent)
        {
            //Random
            levelBoundary = new Rectangle(0, 0, 1280, 720);

            pickaxeActive = false; 

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
            chestTexture = MLscreenContent.Load<Texture2D>("ChestSprite1");


            woodDrop = MLscreenContent.Load<Texture2D>("WoodDrop");
            appleDrop = MLscreenContent.Load<Texture2D>("AppleDrop");
            rockDrop = MLscreenContent.Load<Texture2D>("StoneDrop");
            metalOreDrop = MLscreenContent.Load<Texture2D>("MetalOreDrop");
            sandDrop = MLscreenContent.Load<Texture2D>("SandDrop");
            clothDrop = MLscreenContent.Load<Texture2D>("ClothDrop");
            foodDrop = MLscreenContent.Load<Texture2D>("FoodDrop");
            energyDrop = MLscreenContent.Load<Texture2D>("EnergyDrop");
            glassDrop = MLscreenContent.Load<Texture2D>("GlassDrop");
            metalDrop = MLscreenContent.Load<Texture2D>("MetalDrop");

            extendedMagDrop = MLscreenContent.Load<Texture2D>("ExtendedMagDrop");
            shellsDrop = MLscreenContent.Load<Texture2D>("ShellsDrop");
            scopeDrop = MLscreenContent.Load<Texture2D>("ScopeDrop");


            placedWoodTex = MLscreenContent.Load<Texture2D>("PlacedWood");
            placedStoneTex = MLscreenContent.Load<Texture2D>("PlacedStone");
            placedSandTex = MLscreenContent.Load<Texture2D>("PlacedSand");
            placedDoorTex = MLscreenContent.Load<Texture2D>("PlacedDoor");
            placedWindowTex = MLscreenContent.Load<Texture2D>("PlacedWindow");
            placedBedTex = MLscreenContent.Load<Texture2D>("PlacedBed");
            placedSeedTex = MLscreenContent.Load<Texture2D>("PlacedSeed");


            characterTex = MLscreenContent.Load<Texture2D>("CharacterSpriteSheet");
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

            inventoryScreen = MLscreenContent.Load<Texture2D>("InventorySprite");
            inventorySelector = MLscreenContent.Load<Texture2D>("InventorySelector");
            inventoryMaterials = MLscreenContent.Load<Texture2D>("InventoryMaterials");
            inventoryMaterials2 = MLscreenContent.Load<Texture2D>("InventoryMaterials2");
            inventoryWeapons = MLscreenContent.Load<Texture2D>("InventoryWeapons");
            miniTexture = MLscreenContent.Load<Texture2D>("MiniInventory");
            questionMarkTexture = MLscreenContent.Load<Texture2D>("QuestionMark");

            hotkeys = MLscreenContent.Load<Texture2D>("DpadHotkeys");

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

            #endregion

            #region Sounds

            click = MLscreenContent.Load<SoundEffect>("clickSound");
            hit1 = MLscreenContent.Load<SoundEffect>("hit1Sound");
            hit2 = MLscreenContent.Load<SoundEffect>("hit2Sound");

            smgShot = MLscreenContent.Load<SoundEffect>("SMGShot_Real");
            smgCock = MLscreenContent.Load<SoundEffect>("SmgCock");
            pistolShot = MLscreenContent.Load<SoundEffect>("PistolShot");
            pistolCock = MLscreenContent.Load<SoundEffect>("PistolCock");
            shotgunShot = MLscreenContent.Load<SoundEffect>("ShotgunShot");
            shotgunCock = MLscreenContent.Load<SoundEffect>("ShotgunCock");
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

            rareItems = new List<int>();

            for (int i = 0; i < 3; i++)
                rareItems.Add(0);

            changeTime = TimeSpan.FromSeconds(0.3f);
            DchangeTime = TimeSpan.FromSeconds(0.3f);

            achievementsUnlocked = new List<int>();

            for(int  i = 0; i < 13; i++)
                achievementsUnlocked.Add(0);

            zombie1List = new List<Zombie1>();
            zombie2List = new List<Zombie2>();
            zombie3List = new List<Zombie3>();

            zombie1A = 0; //CHANGE
            zombie2A = 0; //CHANGE
            zombie3A = 0; //CHANGE

            characterPos = new Vector2(2048, 2048);
            player = new Character(characterPos, characterTex);
            shadow = new Shadow(shadowTexture, shadowPosition);
            hands = new Hands(player, manos);

            crosshair = new Crosshair(player, reticle);
            crosshairCenter = new Vector2(crosshair.crosshairsPosition.X + crosshair.crosshairsTexture.Width / 2, crosshair.crosshairsPosition.Y + crosshair.crosshairsTexture.Height / 2);
            pistol = new Pistol(player, handGun);
            smg = new SMG(player, machineGun);
            shotgun = new Shotgun(player, shotty);
            sniper = new Sniper(player, barrett);

            currentWeaponAavailable = new List<int>();
            for (int i = 0; i < 12; i++)
                currentWeaponAavailable.Add(0);

            inventory = new Inventory(fontBig);
            miniPosition = new Vector2(30, 550);
            miniInventory = new MiniInventory(miniTexture, miniPosition);

            hotkeysPosition = new Vector2(1102, 550);

            pauseSelectorPosition = new Vector2(442, 268);
            numberOfNightsPosition = new Vector2(inventoryScreenPos.X + 190, inventoryScreenPos.Y + 350);
            killsPosition = new Vector2(inventoryScreenPos.X + 190, inventoryScreenPos.Y + 440);
            scorePosition = new Vector2(inventoryScreenPos.X + 190, inventoryScreenPos.Y + 530);

            questionsList = new List<int>();
            
            for (int i = 4; i >= 0; i--)
            {
                questionsList.Add(0);
            }

            tree1List = new List<Tree1>();
            tree2List = new List<Tree2>();

            seedList = new List<Vector2>();

            treeA = random.Next(1, 100); //amount of trees.

            rock1List = new List<Rock1>();
            //rock2List = new List<rock2>();
            rockA = random.Next(1, 50); //amount of rocks.

            dune1List = new List<Dune1>();
            duneA = random.Next(1, 25); //amount of dunes.

            chestList = new List<Chest>();
            chestA = 0; 

            bulletList = new List<Bullet>();

            healthTime = TimeSpan.FromSeconds(1f);
            placementDamageTime = TimeSpan.FromSeconds(1f);

            inventorySelectorPos = new Vector2(464, 40);

            drops = new List<Drop>();

            placementList = new List<Placement>();

            healthList = new List<Health>();

            gridList = new List<GridSquare>();

            chestList = new List<Chest>();

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


            if ((nightMusicI.State == SoundState.Stopped || nightMusicI.State == SoundState.Paused) && isMusicOff == false)
            {
                if(isDayTime == true)
                    dayMusicI.Play();
            }

            if (gps.IsConnected == false)
            {
                isMenuActive = true;
            }

            shadowPosition = new Vector2(player.characterPosition.X - shadowTexture.Width / 2, (int)player.characterPosition.Y - shadowTexture.Width / 2);

            if (player.charHealth > 0)
            {
                if (isMenuActive == false && !Guide.IsVisible)
                {
                    if (isInventoryShowing == false)
                    {
                        //Debug.WriteLine("Scroll: " + scrollOffset);
                        //Debug.WriteLine("fixedPos: " + fixedPos);

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
                        

                        #region Rectangles

                        foreach (Tree1 t in tree1List)
                            t.handsCollisionRectangle = hands.handsRectangle;

                        foreach (Tree2 t in tree2List)
                            t.handsCollisionRectangle = hands.handsRectangle;

                        foreach (Rock1 r in rock1List)
                            r.handsColRec = hands.handsRectangle;

                        foreach (Dune1 d in dune1List)
                            d.handsColRec = hands.handsRectangle;

                        foreach (Placement p in placementList)
                            p.handsCollisionRectangle = hands.handsRectangle;

                        foreach (Drop wd in drops)
                            wd.handsCollisionRectangle = hands.handsRectangle;

                        foreach (Chest c in chestList)
                        {
                            c.handsCollisionRectangle = hands.handsRectangle;
                            c.inventoryLockpickCount = inventory.lockpickCount;
                        }

                        //Collisions

                        //healthSourceRectangle = new Rectangle((int)((health.Width / healthFrames)) * healthDamageTaken, 0, health.Width / healthFrames, health.Height);
                        //playerRectangle = new Rectangle((int)player.characterPosition.X - (player.characterTexture.Width / 2), (int)player.characterPosition.Y - (player.characterTexture.Height / 2), player.characterTexture.Width, player.characterTexture.Height);

                        screenCenter = new Vector2(1280, 720) / 2;

                        scrollOffset = screenCenter - player.characterPosition;

                        previousCharacterPos = player.characterPosition;

                        #endregion


                        #region Update Lists

                        if (bulletList.Count > 0)
                            updateBullets();
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
                        //if (rock2List.Count > 0)
                        //    updateRock2s();
                        if (dune1List.Count > 0)
                            updateDune1s();
                        if (drops.Count > 0)
                            updateDrops();
                        if (placementList.Count > 0)
                            updatePlacements();
                        if (healthList.Count > 0)
                            updateHealth();
                        if (chestList.Count > 0)
                            updateChests();
                        if (bloodList.Count > 0)
                            updateBlood(gameTime);




                        inventory.Update();
                        miniInventory.Update();

                        #endregion


                        #region NIGHT SHIT

                        if (isDayTime == false)
                        {
                            if (nightMusicI.State != SoundState.Playing)
                            {
                                if ((nightMusicI.State == SoundState.Stopped || nightMusicI.State == SoundState.Paused) && isMusicOff == false)
                                {
                                    dayMusicI.Stop();
                                    nightMusicI.Play();
                                }
                            }

                            if (zombie1List.Count <= 0 && zombie2List.Count <= 0 && zombie3List.Count <= 0)
                            {
                                isDayTime = true;
                                timer = 0;
                                numberOfNights++;
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
                            isDayTime = false;
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

                        if (timer < dayTime - 1)
                        {
                            isDayTime = true;

                            nightDelay -= gameTime.ElapsedGameTime.TotalSeconds;

                            if (nightDelay <= 0)
                            {
                                nightDelay = .05;

                                if (nightAlphaValue >= 0)
                                    nightAlphaValue -= nightFadeIncrement;
                            }
                        }

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
                                for(int i = 0; i < placementList.Count; i++)
                                {
                                    if (placementList[i].type == 7)
                                    {
                                        if(random.Next(0, 2) == 0)
                                            tree1List.Add(new Tree1(tree1Tex, new Vector2(placementList[i].placementPosition.X - tree1Tex.Width/ 10 / 2, placementList[i].placementPosition.Y - tree1Tex.Height / 2)));
                                        else
                                            tree2List.Add(new Tree2(tree2Tex, new Vector2(placementList[i].placementPosition.X - tree2Tex.Width / 10 / 2, placementList[i].placementPosition.Y - tree2Tex.Height / 2)));

                                        placementList[i].isBroken = true;

                                        Debug.WriteLine("Tree Added, and seed removed"); 
                                        //placementList[i].damage = 10; 
                                        //placementList.RemoveAt(i); 
                                    }
                                }

                                rock1List.Clear();
                                rockA = random.Next(1, 50); //amount of rocks.

                                dune1List.Clear();
                                duneA = random.Next(1, 25); //amount of dunes.

                                chestList.Clear();
                                chestA = 1;

                                bulletList.Clear();
                                inventorySelectorPos = new Vector2(464, 40);

                                drops.Clear();

                                pistolClip = 16;
                                smgClip = 30;
                                shotgunClip = 8;
                                sniperClip = 5;

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

                        player.Update();
                        shadow.Update();

                        shadow.characterShadowRotation = player.orientation;

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

                        if (player.currentWeapon > 0)
                            hands.active = false;
                        else
                            hands.active = true;


                        if (ks.IsKeyDown(Keys.W))
                            player.characterPosition.Y -= 3;
                        if (ks.IsKeyDown(Keys.S))
                            player.characterPosition.Y += 3;
                        if (ks.IsKeyDown(Keys.D))
                            player.characterPosition.X += 3;
                        if (ks.IsKeyDown(Keys.A))
                            player.characterPosition.X -= 3;

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

                            if(noEnergy == 1)
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

                        #endregion

                        #region Character Actions

                        #region Seed Shit

                        //if (gps.Buttons.Y == ButtonState.Pressed && ogps.Buttons.Y == ButtonState.Released)
                        //{
                        //    seedList.Add(crosshairPos); 
                        //}

                        #endregion

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
                                                if(inventory.isGloveAvailable == 0)
                                                    addBullet(new Vector2(pistol.gunPosition.X, pistol.gunPosition.Y), pistol, pistol.damage * 2);
                                                else
                                                    addBullet(new Vector2(pistol.gunPosition.X, pistol.gunPosition.Y), pistol, pistol.damage);
                                                drawMuzzleFlare1 = true; 
                                                pistolShot.Play(.5f,0,0);
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
                                }
                            }
                            if (gps.Triggers.Right < .5f)
                            {
                                isGunClickPlaying = false;
                            }

                            if (reloadTimer >= 2.3f)
                            {
                                hasReloaded = true;
                                reloadTimer = 0;
                                pistolClip = 16;
                            }

                            if (reloadTimer == 0)
                            {
                                pistolCock.Play();
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
                                                    smgShot.Play(.5f,0,0);
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
                                }
                            }
                            if (gps.Triggers.Right < .5f)
                            {
                                isGunClickPlaying = false;
                            }

                            if (reloadTimer >= 2.3f)
                            {
                                hasReloaded = true;
                                reloadTimer = 0;
                                smgClip = 30;                          
                            }

                            if (reloadTimer == 0)
                            {
                                smgCock.Play();
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
                                    }
                                }
                                if (gps.Triggers.Right < .5f)
                                {
                                    isGunClickPlaying = false;
                                }

                                if (reloadTimer >= 3f)
                                {
                                    hasReloaded = true;
                                    reloadTimer = 0;
                                    shotgunClip = 8;                                    
                                }

                                if (reloadTimer == 0)
                                {
                                    shotgunCock.Play();
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
                                                if(inventory.isGloveAvailable == 0)
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

                        #region Shooting

                        //if (gameTime.TotalGameTime - previousTime > fireTime)
                        //{
                        //    previousTime = gameTime.TotalGameTime;
                        //if (player.currentWeapon >= 1)
                        //{
                        //    if (isShooting == false)
                        //    {
                        //        if ((ks.IsKeyDown(Keys.F) && oks.IsKeyUp(Keys.F) || (gps.Triggers.Right >= .5f)))
                        //        {
                        //            isShooting = true;
                        //            addBullet(player.characterPosition, player);
                        //        }
                        //    }

                        //    if (gps.Triggers.Right < .5f)
                        //    {
                        //        isShooting = false;
                        //    }
                        //fireTime = TimeSpan.FromSeconds(.5f);
                        // }
                        //}

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
                            if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(t.tree1Position.X + (t.tree1Texture.Width / t.tree1Frames / 2), t.tree1Position.Y + (t.tree1Texture.Height / 2)), treeRadius))
                            {
                                player.characterPosition = previousCharacterPos;
                            }

                            if (t.tree1Damage == 10)
                            {
                                dropA = random.Next(1, 3);

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(t.tree1Position.X), (int)(t.tree1Position.X + t.tree1Texture.Width / t.tree1Frames));
                                    dropY = random.Next((int)(t.tree1Position.Y), (int)(t.tree1Position.Y + t.tree1Texture.Height));

                                    addDrop(woodDrop, new Vector2(dropX, dropY), 1);
                                }

                                dropA = random.Next(0, 2);

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(t.tree1Position.X), (int)(t.tree1Position.X + t.tree1Texture.Width / t.tree1Frames));
                                    dropY = random.Next((int)(t.tree1Position.Y), (int)(t.tree1Position.Y + t.tree1Texture.Height));

                                    addDrop(appleDrop, new Vector2(dropX, dropY), 2);
                                }

                                t.tree1Damage += 2;

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
                            if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(t.tree2Position.X + (t.tree2Texture.Width / t.tree2Frames / 2), t.tree2Position.Y + (t.tree2Texture.Height / 2)), treeRadius))
                            {
                                player.characterPosition = previousCharacterPos;
                            }

                            if (t.tree2Damage == 10)
                            {
                                dropA = random.Next(1, 3);

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(t.tree2Position.X), (int)(t.tree2Position.X + t.tree2Texture.Width / t.tree2Frames));
                                    dropY = random.Next((int)(t.tree2Position.Y), (int)(t.tree2Position.Y + t.tree2Texture.Height));

                                    addDrop(woodDrop, new Vector2(dropX, dropY), 1);
                                }

                                dropA = random.Next(0, 2);

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(t.tree2Position.X), (int)(t.tree2Position.X + t.tree2Texture.Width / t.tree2Frames));
                                    dropY = random.Next((int)(t.tree2Position.Y), (int)(t.tree2Position.Y + t.tree2Texture.Height));

                                    addDrop(appleDrop, new Vector2(dropX, dropY), 2);
                                }

                                t.tree2Damage += 2;

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
                            //r.checkZombieCollision(zombie1List, zombieRadius, placementRadius, placementDamageTime, gameTime);
                            //r.checkZombie2Collision(zombie2List, zombieRadius, placementRadius, placementDamageTime, gameTime);
                            //r.checkZombie3Collision(zombie3List, zombieRadius, placementRadius, placementDamageTime, gameTime);

                            if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(r.rock1Position.X + (r.rock1Texture.Width / r.rock1Frames / 2), r.rock1Position.Y + (r.rock1Texture.Height / 2)), rockRadius))
                            {
                                player.characterPosition = previousCharacterPos;
                            }

                            if (r.rock1Damage == 10)
                            {
                                dropA = random.Next(1, 3);

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(r.rock1Position.X), (int)(r.rock1Position.X + r.rock1Texture.Width / r.rock1Frames));
                                    dropY = random.Next((int)(r.rock1Position.Y), (int)(r.rock1Position.Y + r.rock1Texture.Height));

                                    addDrop(rockDrop, new Vector2(dropX, dropY), 3);
                                }

                                dropA = random.Next(0, 2);

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(r.rock1Position.X), (int)(r.rock1Position.X + r.rock1Texture.Width / r.rock1Frames));
                                    dropY = random.Next((int)(r.rock1Position.Y), (int)(r.rock1Position.Y + r.rock1Texture.Height));

                                    addDrop(metalOreDrop, new Vector2(dropX, dropY), 4);
                                }

                                r.rock1Damage += 2;

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
                            //d.checkZombieCollision(zombie1List, zombieRadius, placementRadius, placementDamageTime, gameTime);
                            //d.checkZombie2Collision(zombie2List, zombieRadius, placementRadius, placementDamageTime, gameTime);
                            //d.checkZombie3Collision(zombie3List, zombieRadius, placementRadius, placementDamageTime, gameTime);

                            if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(d.dune1Position.X + (d.dune1Texture.Width / d.dune1Frames / 2), d.dune1Position.Y + (d.dune1Texture.Height / 2)), duneRadius))
                            {
                                player.characterPosition = previousCharacterPos;
                            }

                            if (d.dune1Damage == 10)
                            {
                                dropA = random.Next(1, 3);

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(d.dune1Position.X), (int)(d.dune1Position.X + d.dune1Texture.Width / d.dune1Frames));
                                    dropY = random.Next((int)(d.dune1Position.Y), (int)(d.dune1Position.Y + d.dune1Texture.Height));

                                    addDrop(sandDrop, new Vector2(dropX, dropY), 5);
                                }

                                d.dune1Damage += 2;

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

                        #region CHEST SHIT

                        gridLocX = random.Next(70);
                        gridLocY = random.Next(58);

                        if (foundThatDay == false)
                        {
                            if (numberOfNights % 5 == 0 && chestPosIsSet == false && chestIsSet == 1 && numberOfNights > 1)
                            {
                                chestList.Clear();
                                chestA = 1;
                                chestIsSet--;
                                foundThatDay = true;
                            }
                            
                        }

                        if (numberOfNights % 4 == 0)
                        {
                            foundThatDay = false;
                        }


                        if (chestA > 0)
                        {
                            chestList.Add(new Chest(chestTexture, gridList[gridLocY * 58 + gridLocX].gridPosition));
                            gridIsOccupied[gridLocY * 58 + gridLocX] = true;
                            chestPosIsSet = true;
                            chestA--;
                        }

                        foreach (Chest c in chestList)
                        {
                            if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(c.chestPosition.X + (chestTexture.Width / c.chestFrames / 2), c.chestPosition.Y + (chestTexture.Height / 2)), chestRadius))
                            {
                                player.characterPosition = previousCharacterPos;
                            }

                            if (c.chestDamage == 2)
                            {
                                inventory.lockpickCount--;

                                dropA = random.Next(2, 10);

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(c.chestPosition.X), (int)(c.chestPosition.X + c.chestTexture.Width / c.chestFrames));
                                    dropY = random.Next((int)(c.chestPosition.Y), (int)(c.chestPosition.Y + c.chestTexture.Height));

                                    chestDrop = random.Next(1, 10);
                                    
                                    switch (chestDrop)
                                    {
                                        case 1: addDrop(woodDrop, new Vector2(dropX, dropY), 1); break;
                                        case 2: addDrop(appleDrop, new Vector2(dropX, dropY), 2); break;
                                        case 3: addDrop(rockDrop, new Vector2(dropX, dropY), 3); break;
                                        case 4: addDrop(metalOreDrop, new Vector2(dropX, dropY), 4); break;
                                        case 5: addDrop(sandDrop, new Vector2(dropX, dropY), 5); break;
                                        case 6: addDrop(clothDrop, new Vector2(dropX, dropY), 6); break;
                                        case 7: addDrop(foodDrop, new Vector2(dropX, dropY), 7); break;
                                        case 8: addDrop(energyDrop, new Vector2(dropX, dropY), 8); break;
                                        case 9: addDrop(glassDrop, new Vector2(dropX, dropY), 9); break;
                                        case 10: addDrop(metalDrop, new Vector2(dropX, dropY), 10); break;
                                    }
                                }

                                chestUpgrade = random.Next(1, 3);

                                switch (chestUpgrade)
                                {
                                    case 1: addDrop(extendedMagDrop, new Vector2(dropX, dropY), 11); break;
                                    case 2: addDrop(shellsDrop, new Vector2(dropX, dropY), 12); break;
                                    case 3: addDrop(scopeDrop, new Vector2(dropX, dropY), 13); break;
                                }

                                c.collectedShit = true;
                                c.chestDamage += 2;

                            }

                        }

                        #endregion

                        #region MINI INVENTORY SHIT

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
                            
                        //if (pickAxe.active == true)
                        //{
                        //    currentWeaponAavailable[4] = 1;
                        //}

                        if (inventory.woodCount > 0)
                        {
                            currentWeaponAavailable[6] = 1;
                        }
                        else
                            currentWeaponAavailable[6] = 0;

                        if (inventory.rockCount > 0)
                        {
                            currentWeaponAavailable[7] = 1;
                        }
                        else
                            currentWeaponAavailable[7] = 0;

                        if (inventory.sandCount > 0)
                        {
                            currentWeaponAavailable[8] = 1;
                        }
                        else
                            currentWeaponAavailable[8] = 0;

                        if (inventory.windowCount > 0)
                        {
                            currentWeaponAavailable[9] = 1;
                        }
                        else
                            currentWeaponAavailable[9] = 0;

                        if (inventory.doorCount > 0)
                        {
                            currentWeaponAavailable[10] = 1;
                        }
                        else
                            currentWeaponAavailable[10] = 0;

                        if (inventory.bedCount > 0)
                        {
                            currentWeaponAavailable[11] = 1;
                        }
                        else
                            currentWeaponAavailable[11] = 0;

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
                                    for (int i = 0; i <= 11; i++)
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


                        #region EATING SHIT

                        //energyBoost += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (inventory.energyDrinkCount > 0 && gps.DPad.Right == ButtonState.Pressed && ogps.DPad.Right == ButtonState.Released && noEnergy == 1)
                        {
                            startEnergyBoost = true;
                            inventory.energyDrinkCount--;
                            itTastesLikeLaser = true;
                        }

                        if (startEnergyBoost == true)
                        {
                            energyBoost += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (energyBoost <= 10)
                            {
                                player.maxSpeed = 18.0f;
                                noEnergy = 0;
                            }
                            else
                            {
                                player.maxSpeed = 9.0f;
                                energyBoost = 0;
                                startEnergyBoost = false;
                                noEnergy = 1;
                            }
                        }

                        if (inventory.foodCount > 0 && gps.DPad.Up == ButtonState.Pressed && ogps.DPad.Up == ButtonState.Released)
                        {
                            if (player.charHealth <= 5)
                            {
                                if (player.charHealth % 2 == 0)
                                {
                                    //fills up the health for the heart after it.
                                    healthList[2 - (player.charHealth / 2)].damageTaken -= 2;
                                    healthList[2 - (player.charHealth / 2)].active = true;
                                    inventory.foodCount--;
                                    player.charHealth += 2;
                                }

                                else if (player.charHealth == 1)
                                {
                                    healthList[2].damageTaken--;
                                    healthList[1].damageTaken--;
                                    healthList[1].active = true;
                                    inventory.foodCount--;
                                    player.charHealth += 2;
                                }

                                else if (player.charHealth == 3)
                                {
                                    healthList[1].damageTaken--;
                                    healthList[0].damageTaken--;
                                    healthList[0].active = true;
                                    inventory.foodCount--;
                                    player.charHealth += 2;
                                }

                                else if (player.charHealth == 5)
                                {
                                    healthList[0].damageTaken--;
                                    inventory.foodCount--;
                                    player.charHealth ++;
                                }

                            }
                        }

                        if (inventory.appleCount > 0 && gps.DPad.Left == ButtonState.Pressed && ogps.DPad.Left == ButtonState.Released)
                        {
                            if (player.charHealth <= 5)
                            {
                                if (player.charHealth % 2 == 0)
                                {
                                    healthList[2 - (player.charHealth / 2)].damageTaken --;
                                    healthList[2 - (player.charHealth / 2)].active = true;
                                    inventory.appleCount--;
                                    player.charHealth ++;
                                }

                                else if (player.charHealth == 1)
                                {
                                    healthList[2].damageTaken--;
                                    inventory.appleCount--;
                                    player.charHealth ++;
                                }

                                else if (player.charHealth == 3)
                                {
                                    healthList[1].damageTaken--;
                                    inventory.appleCount--;
                                    player.charHealth ++;
                                }

                                else if (player.charHealth == 5)
                                {
                                    healthList[0].damageTaken--;
                                    inventory.appleCount--;
                                    player.charHealth++;
                                }

                            }
                        }

                        if (gps.DPad.Down == ButtonState.Pressed && ogps.DPad.Down == ButtonState.Released)
                        {
                            player.currentWeapon = 0;
                            miniInventory.currentWeapon = 0;
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


                        if (zombie1A > 0 && isDayTime == false)
                        {
                            zombie1List.Add(new Zombie1(zombie1Pos, zombie1));
                            zombie1A--;
                        }

                        if (isDayTime == true)
                        {
                            zombie1A = 10 + (numberOfNights * 2);
                        }

                        foreach (Zombie1 z in zombie1List)
                        {
                            z.facePos = player.characterPosition;

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

                            foreach (Bullet b in bulletList)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, z.zombiePosition, zombieRadius))
                                {
                                    z.damageTaken -= b.damage;
                                    addBlood(bloodTex, z.zombiePosition, z.orientation);
                                    if (player.currentWeapon != 4)
                                    b.active = false;
                                    if (killshots <= 50 && player.currentWeapon == 1)
                                    {
                                        killshots++;
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

                            if (z.damageTaken <= 0)
                            {
                                dropA = random.Next(0, 2);
                                score += 50;
                                kills += 1;

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(z.zombiePosition.X), (int)(z.zombiePosition.X + z.zombie.Width));
                                    dropY = random.Next((int)(z.zombiePosition.Y), (int)(z.zombiePosition.Y + z.zombie.Height));

                                    addDrop(foodDrop, new Vector2(dropX, dropY), 7);
                                }

                                z.active = false;
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

                        if (zombie2A > 0 && isDayTime == false)
                        {
                            zombie2List.Add(new Zombie2(zombie2Pos, zombie2));
                            zombie2A--;
                        }
                        if (isDayTime == true)
                        {
                            if(numberOfNights > 5)
                                zombie2A = 10 + (numberOfNights * 2);
                        }

                        foreach (Zombie2 z in zombie2List)
                        {
                            z.facePos = player.characterPosition;

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

                            foreach (Bullet b in bulletList)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, z.zombiePosition, zombieRadius))
                                {
                                    z.damageTaken -= b.damage;
                                    addBlood(bloodTex, z.zombiePosition, z.orientation);
                                    if (player.currentWeapon != 4)
                                    b.active = false;
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

                            if (z.damageTaken <= 0)
                            {
                                dropA = random.Next(0, 2);
                                score += 30;
                                kills += 1;

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(z.zombiePosition.X), (int)(z.zombiePosition.X + z.zombie.Width));
                                    dropY = random.Next((int)(z.zombiePosition.Y), (int)(z.zombiePosition.Y + z.zombie.Height));

                                    addDrop(clothDrop, new Vector2(dropX, dropY), 6);
                                }

                                z.active = false;
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

                        if (zombie3A > 0 && isDayTime == false)
                        {
                            zombie3List.Add(new Zombie3(zombie3Pos, zombie3));
                            zombie3A--;
                        }
                        if (isDayTime == true)
                        {
                            if(numberOfNights > 15)
                                zombie3A = 10 + (numberOfNights * 1);
                        }

                        foreach (Zombie3 z in zombie3List)
                        {
                            z.facePos = player.characterPosition;

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

                            foreach (Bullet b in bulletList)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, z.zombiePosition, zombieRadius))
                                {
                                    z.damageTaken -= b.damage;
                                    addBlood(bloodTex, z.zombiePosition, z.orientation);
                                    if(player.currentWeapon != 4)
                                    b.active = false;
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

                            if (z.damageTaken <= 0)
                            {
                                dropA = random.Next(0, 2);
                                score += 100;
                                kills += 1;

                                for (int i = 0; i < dropA; i++)
                                {
                                    dropX = random.Next((int)(z.zombiePosition.X), (int)(z.zombiePosition.X + z.zombie.Width));
                                    dropY = random.Next((int)(z.zombiePosition.Y), (int)(z.zombiePosition.Y + z.zombie.Height));

                                    addDrop(metalOreDrop, new Vector2(dropX, dropY), 4);
                                }

                                z.active = false;
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
                                isVibrating = false;
                                drawBlood = false; 
                            }
                        }

                    } // End Of Inventory
                    else
                    {
                        GamePad.SetVibration(playerOne, 0, 0);
                        scrollOffset = Vector2.Zero;
                    }
                    
                    #region INVENTORY SHIT

                    if ((gps.Buttons.Back == ButtonState.Pressed && ogps.Buttons.Back == ButtonState.Released) && isShowing == 1)
                    {
                        isInventoryShowing = true;
                        isShowing++;
                    }

                    else
                    {
                        if (isInventoryShowing == true)
                        {
                            if (gps.DPad.Right == ButtonState.Pressed && ogps.DPad.Right == ButtonState.Released)
                            {
                                if (currentSelPos < selMax)
                                {
                                    currentSelPos++;
                                }

                                else currentSelPos = 0;
                            }

                            if (gps.DPad.Left == ButtonState.Pressed && ogps.DPad.Left == ButtonState.Released)
                            {
                                if (currentSelPos > 0)
                                {
                                    currentSelPos--;
                                }

                                else currentSelPos = selMax;
                            }
                        }

                        if ((gps.Buttons.RightShoulder == ButtonState.Pressed && ogps.Buttons.RightShoulder == ButtonState.Released))
                        {
                            curInvList++;
                        }
                        else if ((gps.Buttons.LeftShoulder == ButtonState.Pressed && ogps.Buttons.LeftShoulder == ButtonState.Released))
                        {
                            curInvList--;
                        }

                        #region Material Trade

                        if (isInventoryMaterials2Showing == true && curInvList == 0 && currentSelPos == 0 && inventory.metalOreCount >= 2 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.metalOreCount -= 2;
                            inventory.metalCount++;
                        }

                        if (isInventoryMaterials2Showing == true && curInvList == 0 && currentSelPos == 1 && inventory.sandCount >= 2 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.sandCount -= 2;
                            inventory.glassCount++;
                        }

                        if (isInventoryMaterials2Showing == true && curInvList == 0 && currentSelPos == 2 && inventory.metalCount >= 5 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.metalCount -= 5;
                            inventory.lockpickCount++;
                        }

                        if (isInventoryMaterials2Showing == true && curInvList == 0 && currentSelPos == 3 && inventory.metalCount >= 25 && inventory.isGloveAvailable == 1 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.metalCount -= 25;
                            inventory.gloveCount++;
                            inventory.isGloveAvailable = 0;
                        }

                        #endregion

                        if (curInvList == 1)
                        {
                            isInventoryMaterialsShowing = true;
                        }
                        else isInventoryMaterialsShowing = false;

                        if (curInvList == 0)
                        {
                            isInventoryMaterials2Showing = true;
                        }
                        else isInventoryMaterials2Showing = false;

                        #region Material Purchasing

                        if (isInventoryMaterialsShowing == true && currentSelPos == 0 && inventory.woodCount >= 10 && inventory.glassCount >= 5 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.woodCount -= 5;
                            inventory.glassCount -= 2;
                            inventory.doorCount++;
                            //door.active = true;
                        }

                        if (isInventoryMaterialsShowing == true && currentSelPos == 1 && inventory.woodCount >= 5 && inventory.glassCount >= 10 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.woodCount -= 4;
                            inventory.glassCount -= 4;
                            inventory.windowCount++;
                            //window.active = true;
                        }

                        if (isInventoryMaterialsShowing == true && currentSelPos == 2 && inventory.woodCount >= 10 && inventory.clothCount >= 10 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.woodCount -= 10;
                            inventory.clothCount -= 10;
                            inventory.bedCount++;
                            //bed.active = true;
                        }

                        if (isInventoryMaterialsShowing == true && currentSelPos == 3 && inventory.rockCount >= 5 && inventory.clothCount >= 5 && pickaxeActive == false && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.rockCount -= 5;
                            inventory.clothCount -= 5;
                            pickaxeActive = true;
                        }

                        #endregion

                        if (curInvList == 2)
                        {
                            isInventoryWeaponsShowing = true;
                        }
                        else isInventoryWeaponsShowing = false;

                        #region Weapon Purchasing

                        if (isInventoryWeaponsShowing == true && currentSelPos == 0 && inventory.metalCount >= 10 && inventory.isPistolAvailable == 1 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.isPistolAvailable = 0;
                            inventory.metalCount -= 10;
                            pistol.active = true;
                            questionsList[0] = 1;
                            iNeedaWeapon = true;
                        }

                        if (isInventoryWeaponsShowing == true && currentSelPos == 1 && inventory.metalCount >= 25 && inventory.isSMGAvailable == 1 && inventory.extendedMagCount >= 1 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.isSMGAvailable = 0;
                            inventory.metalCount -= 25;
                            inventory.extendedMagCount -= 1;
                            smg.active = true;
                            questionsList[1] = 1;
                            iNeedaWeapon = true;
                            thankTheLord = true;
                        }

                        if (isInventoryWeaponsShowing == true && currentSelPos == 2 && inventory.metalCount >= 50 && inventory.isShotgunAvailable == 1 && inventory.shellsCount >= 1 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.isShotgunAvailable = 0;
                            inventory.metalCount -= 50;
                            inventory.shellsCount -= 1;
                            shotgun.active = true;
                            questionsList[2] = 1;
                            iNeedaWeapon = true;
                            riotControl = true;
                        }

                        if (isInventoryWeaponsShowing == true && currentSelPos == 3 && inventory.metalCount >= 50 && inventory.isSniperAvailable == 1 && inventory.scopeCount >= 1 && gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released)
                        {
                            inventory.isSniperAvailable = 0;
                            inventory.metalCount -= 50;
                            inventory.scopeCount -= 1;
                            sniper.active = true;
                            questionsList[3] = 1;
                            iNeedaWeapon = true;
                            headShot = true;
                        }

                        #endregion


                        if (curInvList > 2)
                        {
                            curInvList = 0;
                        }

                        if (curInvList < 0)
                        {
                            curInvList = 2;
                        }

                        #region Inventory Positions

                        if (score >= 10)
                            scorePosition = new Vector2(185, scorePosition.Y);
                        if (score >= 100)
                            scorePosition = new Vector2(180, scorePosition.Y);
                        if (score >= 1000)
                            scorePosition = new Vector2(175, scorePosition.Y);
                        if (score >= 10000)
                            scorePosition = new Vector2(170, scorePosition.Y);

                        if (kills >= 10)
                            killsPosition = new Vector2(185, killsPosition.Y);
                        if (kills >= 100)
                            killsPosition = new Vector2(180, killsPosition.Y);
                        if (kills >= 1000)
                            killsPosition = new Vector2(175, killsPosition.Y);

                        if (numberOfNights >= 10)
                            numberOfNightsPosition = new Vector2(185, numberOfNightsPosition.Y);
                        if (numberOfNights >= 100)
                            numberOfNightsPosition = new Vector2(180, numberOfNightsPosition.Y);
                        if (numberOfNights >= 1000)
                            numberOfNightsPosition = new Vector2(175, numberOfNightsPosition.Y);

                        #endregion

                        if ((gps.Buttons.Back == ButtonState.Pressed && ogps.Buttons.Back == ButtonState.Released && isShowing == 2))
                        {
                            isShowing--;
                            isInventoryShowing = false;
                        }
                    }

                    #endregion

                    #region GRID SHIT

                    //find grid square that player is in.

                    foreach (GridSquare r in gridList)
                    {
                        int tempX = (int)fixedPos.X;
                        int tempY = (int)fixedPos.Y;

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

                    #endregion

                    #region PLACEMENT SHIT


                    if ((gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released) && isOpen == 0)
                    {
                        isOpen++;
                        isOpen2 = true;
                    }

                    //Upass id for updateing gps, it means update pass
                    if (Upass == false)
                    {
                        if ((gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released) && isOpen == 1)
                        {
                            isOpen--;
                            isOpen2 = false;
                            Upass = true;
                        }
                    }

                    if (isOpen == 1)
                    {
                        Upass = false; 
                    }

                    foreach (Placement p in placementList)
                    {
                        //p.checkSand(playerRadius, placementRadius);

                            #region Zombies

                            p.checkZombieCollision(zombie1List, zombieRadius, placementRadius, placementDamageTime, gameTime);
                            p.checkZombie2Collision(zombie2List, zombieRadius, placementRadius, placementDamageTime, gameTime);
                            p.checkZombie3Collision(zombie3List, zombieRadius, placementRadius, placementDamageTime, gameTime);

                            #endregion

                            #region Bullets

                            foreach (Bullet b in bulletList)
                            {
                                if (BoundingCircle(b.bulletPosition, bulletRadius, p.placementPosition, placementRadius) && p.type != 4 && p.type != 6)
                                {
                                    b.active = false;
                                }
                            }


                            #endregion

                           

                            if (p.type == 3 || p.type == 7 || (p.type == 5 && isOpen == 1))
                            {
                                
                            }
                            else
                            {
                                if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(p.placementPosition.X, p.placementPosition.Y), placementRadius))
                                {
                                    player.characterPosition = previousCharacterPos;

                                    if (p.type == 6 && (gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released))
                                    {
                                        isReset = false; 
                                        resetAll(gameTime);
                                        countingSheep = true;
                                    }

                                    
                                }

                                if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(p.placementPosition.X, p.placementPosition.Y), placementRadius - 3))
                                {
                                    player.characterPosition -= new Vector2((float)Math.Cos(player.orientation), (float)Math.Sin(player.orientation));
                                }
                            }

                        //if (p.placementCollisionRectangle.Intersects(player.characterCollisionRectangle))
                        //{
                        //    
                        //}
                    }

                    #region Placement Snap

                    if (scrollOffset.X < 20 && scrollOffset.X > -20)
                    {
                        fixedPos -= scrollOffset;
                        fixedPos2 -= scrollOffset;
                    }

                    #endregion

                    if (crosshairGridPosX >= 0)
                        crosshairPos = crosshairGridPosX * 70 + crosshairGridPosY;
                    else
                        crosshairPos = 0;

                    if(crosshairPos > 0 && crosshairPos < 4060)
                    {
                        if (gridIsOccupied[crosshairPos] == false)
                        {
                            crosshair.available = true;
                        }
                        else
                        {
                            crosshair.available = false;
                        }
                    }

                    //Seed
                    if (isPlacing == false && gps.Buttons.Y == ButtonState.Pressed && ogps.Buttons.Y == ButtonState.Released && isInventoryShowing == false && player.currentWeapon == 0 && inventory.seedCount > 0)
                    {
                        isPlacing = true;

                        if (crosshairPos > 0 && crosshairPos < 4060 && gridIsOccupied[crosshairPos] == false)
                        {
                            gridCenter = new Vector2(gridList[crosshairPos].gridPosition.X + gridList[crosshairPos].gridTexture.Width / 2, gridList[crosshairPos].gridPosition.Y + gridList[crosshairPos].gridTexture.Height / 2);

                            addPlacement(placedSeedTex, player, gridCenter, true, 7);
                            gridIsOccupied[crosshairPos] = true;
                            inventory.seedCount--;
                        }
                    }

                    if (isPlacing == false && gps.Triggers.Right >= .7f && isInventoryShowing == false)
                    {
                        isPlacing = true;

                        if (crosshairPos > 0 && crosshairPos < 4060 && gridIsOccupied[crosshairPos] == false)
                        {
                            gridCenter = new Vector2(gridList[crosshairPos].gridPosition.X + gridList[crosshairPos].gridTexture.Width / 2, gridList[crosshairPos].gridPosition.Y + gridList[crosshairPos].gridTexture.Height / 2);

                            //Wood
                            if (player.currentWeapon == 6 && inventory.woodCount > 0)
                            {
                                addPlacement(placedWoodTex, player, gridCenter, true, 1);
                                gridIsOccupied[crosshairPos] = true;
                                inventory.woodCount--;
                            }

                            //Stone
                            if (player.currentWeapon == 7 && inventory.rockCount > 0)
                            {
                                addPlacement(placedStoneTex, player, gridCenter, true, 2);
                                gridIsOccupied[crosshairPos] = true;
                                inventory.rockCount--;
                            }

                            //Sand
                            if (player.currentWeapon == 8 && inventory.sandCount > 0)
                            {
                                addPlacement(placedSandTex, player, gridCenter, true, 3);
                                gridIsOccupied[crosshairPos] = true;
                                inventory.sandCount--;
                            }

                            //Window
                            if (player.currentWeapon == 9 && inventory.windowCount > 0)
                            {
                                addPlacement(placedWindowTex, player, gridCenter, true, 4);
                                gridIsOccupied[crosshairPos] = true;
                                inventory.windowCount--;
                            }

                            //Door
                            if (player.currentWeapon == 10 && inventory.doorCount > 0)
                            {
                                addPlacement(placedDoorTex, player, gridCenter, true, 5);
                                gridIsOccupied[crosshairPos] = true;
                                inventory.doorCount--;
                            }

                            //Bed
                            if (player.currentWeapon == 11 && inventory.bedCount > 0)
                            {
                                addPlacement(placedBedTex, player, gridCenter, true, 6);
                                gridIsOccupied[crosshairPos] = true;
                                inventory.bedCount--;
                            }
                            
                        }
                    }

                    if (gps.Triggers.Right < .7f)
                    {
                        isPlacing = false;
                    }

                    #endregion

                    #region ACHIEVEMENT SHIT

                    if (iNeedaWeapon == true)
                    {
                        achievementsUnlocked[0] = 1;
                    }

                    if (thankTheLord == true)
                    {
                        achievementsUnlocked[1] = 1;
                    }

                    if (riotControl == true)
                    {
                        achievementsUnlocked[2] = 1;
                    }

                    if (headShot == true)
                    {
                        achievementsUnlocked[3] = 1;
                    }

                    #region Killshot

                    if (killshots >= 50)
                    {
                        killShot = true;
                    }

                    if (killShot == true)
                    {
                        achievementsUnlocked[4] = 1;
                    }

                    #endregion

                    #region Over9000

                    if (score > 9000)
                    {
                        over9000 = true;
                    }

                    if (over9000 == true)
                    {
                        achievementsUnlocked[5] = 1;
                    }

                    #endregion

                    #region Zombiecide

                    if (zombieKills >= 100)
                    {
                        zombiecide = true;
                    }

                    if (zombiecide == true)
                    {
                        achievementsUnlocked[6] = 1;
                    }

                    #endregion

                    if (countingSheep == true)
                    {
                        achievementsUnlocked[7] = 1;
                    }

                    #region EnjoyTheLittleThings

                    if (inventory.extendedMagCount >= 1)
                    {
                        rareItems[0] = 1;
                    }

                    if (inventory.shellsCount >= 1)
                    {
                        rareItems[1] = 1;
                    }

                    if (inventory.scopeCount >= 1)
                    {
                        rareItems[2] = 1;
                    }

                    if (rareItems[0] == 1 && rareItems[1] == 1 && rareItems[2] == 1)
                    {
                        enjoyTheLittleThings = true;
                    }                    

                    if (enjoyTheLittleThings == true)
                    {
                        achievementsUnlocked[8] = 1;
                    }

                    #endregion

                    #region Survivor

                    if (numberOfNights >= 100)
                    {
                        survivor = true;
                    }

                    if (survivor == true)
                    {
                        achievementsUnlocked[9] = 1;
                    }

                    #endregion

                    if (itTastesLikeLaser == true)
                    {
                        achievementsUnlocked[10] = 1;
                    }

                    #region FortNight

                    if (numberOfNights == 14)
                    {
                        fortNight = true;
                    }

                    if (fortNight == true)
                    {
                        achievementsUnlocked[11] = 1;
                    }

                    #endregion

                    #endregion

                } // If Paused

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
                            pauseSelection++;
                            pauseSelectorPosition.Y += 69;
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
                                pauseSelection++;
                                pauseSelectorPosition.Y += 69;
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
                            pauseSelection--;
                            pauseSelectorPosition.Y -= 69;
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
                                pauseSelection--;
                                pauseSelectorPosition.Y -= 69;
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
                                isMusicNum=2;
                                pistolShot.Play();
                            }

                            else if (gps.Buttons.A == ButtonState.Pressed && ogps.Buttons.A == ButtonState.Released && isMusicNum == 2)
                            {
                                isMusicOff = false;
                                isMusicNum= 1;
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
                }

                #region Also PAUSE SHIT

                if ((gps.Buttons.Start == ButtonState.Pressed && ogps.Buttons.Start == ButtonState.Released) && isMenuOn == 1 && isInventoryShowing == false)
                {
                    isMenuActive = true;
                    isMenuOn++;
                }
                else
                {
                    if ((gps.Buttons.Start == ButtonState.Pressed && ogps.Buttons.Start == ButtonState.Released && isMenuOn == 2) && isInventoryShowing == false)
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
                        score += 150;
                        isDayPassed = true;
                    }
                }

                if ((int)timer == 0)
                    isDayPassed = false;

                previousNumberOfNights = numberOfNights;
                oks = ks;
                ogps = gps;
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
                    screenEvent.Invoke(this, new EventArgs());
                }
            }

            if (healthList.Count > 0)
                updateHealth();

            if (player.charHealth <= 0)
                healthList[healthList.Count - 1].damageTaken = 2;

           




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

            //foreach (rock2 t in rock2List)
            //{
            //    t.rock2Position += scrollOffset;
            //    t.Draw(spriteBatch);
            //}

            foreach (Dune1 t in dune1List)
            {
                t.dune1Position += scrollOffset;
                t.Draw(spriteBatch);
            }

            foreach (Placement p in placementList)
            {
                if (p.isPlaced == true)
                {
                    if (p.type == 3 || p.type == 7)
                    {
                        p.placementPosition += scrollOffset;
                        p.Draw(spriteBatch);
                    }
                }
            }

            foreach (Blood b in bloodList)
            {
                b.bloodPosition += scrollOffset;
                b.Draw(spriteBatch);
            }

            foreach (Drop d in drops)
            {
                if (d.active == true)
                {
                    d.dropPosition += scrollOffset;
                    d.Draw(spriteBatch);
                }
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
            //shadow.Draw(spriteBatch);

            player.characterPosition += scrollOffset;
            player.Draw(spriteBatch);
            
            #endregion

            foreach (Placement p in placementList)
            {
                if (p.isPlaced == true)
                {
                    if (p.type != 3 && p.type != 7)
                    {
                        p.placementPosition += scrollOffset;
                        p.Draw(spriteBatch);
                    }
                }
            }

            foreach (Chest c in chestList)
            {
                if (c.active == false)
                {
                    c.chestPosition += scrollOffset;
                    c.Draw(spriteBatch);
                }
            }

            crosshair.crosshairsPosition += scrollOffset;

            if (player.currentWeapon != 4)
                crosshair.Draw(spriteBatch);
            //else is in player.draw area

            // Dev Item

            

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

            //Want to go over night. 
            foreach (Bullet b in bulletList)
            {
                b.bulletPosition += scrollOffset;
                b.Draw(spriteBatch);
            }

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

            foreach (Health h in healthList)
            {
                h.Draw(spriteBatch);
            }

            miniInventory.Draw(spriteBatch);
            
            spriteBatch.Draw(hotkeys, hotkeysPosition, Color.White);

            if (isLoadingTexOn == true)
                spriteBatch.Draw(loadingTex, Vector2.Zero, new Color(255, 255, 255, (byte)MathHelper.Clamp(loadingAlphaValue, 0, 255)));   
            
            if (player.charHealth <= 0)
            {
                isNightTexOn = false;
                spriteBatch.Draw(deadScreen, Vector2.Zero, new Color(255, 255, 255, (byte)MathHelper.Clamp(deathAlphaValue, 0, 255)));
            }

            #region Inventory

            if (isInventoryShowing == true)
            {
                spriteBatch.Draw(inventoryScreen, inventoryScreenPos, Color.White);

                if (isInventoryMaterialsShowing == true)
                {
                    spriteBatch.Draw(inventoryMaterials, inventoryMaterialsPos, Color.White);
                }

                if (isInventoryMaterials2Showing == true)
                {
                    spriteBatch.Draw(inventoryMaterials2, inventoryMaterials2Pos, Color.White);
                }

                if (isInventoryWeaponsShowing == true)
                {
                    spriteBatch.Draw(inventoryWeapons, inventoryWeaponsPos, Color.White);
                    if (questionsList[0] == 0)
                    {
                        spriteBatch.Draw(questionMarkTexture, new Vector2(inventoryWeaponsPos.X + 12, inventoryWeaponsPos.Y + 12), Color.White);
                    }

                    if (questionsList[1] == 0)
                    {
                        spriteBatch.Draw(questionMarkTexture, new Vector2(inventoryWeaponsPos.X + 185, inventoryWeaponsPos.Y + 12), Color.White);
                    }

                    if (questionsList[2] == 0)
                    {
                        spriteBatch.Draw(questionMarkTexture, new Vector2(inventoryWeaponsPos.X + 356, inventoryWeaponsPos.Y + 12), Color.White);
                    }

                    if (questionsList[3] == 0)
                    {
                        spriteBatch.Draw(questionMarkTexture, new Vector2(inventoryWeaponsPos.X + 529, inventoryWeaponsPos.Y + 12), Color.White);
                    }
                }

                spriteBatch.Draw(inventorySelector, new Vector2(inventorySelectorPos.X + (currentSelPos * (inventorySelector.Width + 24)), inventorySelectorPos.Y), Color.White);

                spriteBatch.DrawString(font, numberOfNights.ToString(), numberOfNightsPosition, Color.White);
                spriteBatch.DrawString(font, kills.ToString(), killsPosition, Color.White);
                spriteBatch.DrawString(font, score.ToString(), scorePosition, Color.White);

                inventory.Draw(spriteBatch);

                isNightTexOn = false;
                isLoadingTexOn = false;
            }
            else
            {
                isNightTexOn = true;
                isLoadingTexOn = true;
            }

            #endregion

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

            #region Devpoop

            //if (gps.Buttons.A == ButtonState.Pressed)
            //{


            //    foreach (Drop d in drops)
            //        spriteBatch.Draw(collisionBox, d.dropCollisionRectangle, Color.White);

            //    foreach (Placement p in placementList)
            //        spriteBatch.Draw(collisionBox, p.placementCollisionRectangle, Color.White);

            //    spriteBatch.Draw(collisionBox, hands.handsRectangle, Color.White);
            //    spriteBatch.Draw(collisionBox, player.characterCollisionRectangle, Color.White);
            //    spriteBatch.Draw(collisionBox, rightBRect, Color.White);
            //    spriteBatch.Draw(collisionBox, leftBRect, Color.White);
            //    spriteBatch.Draw(collisionBox, topBRect, Color.White);
            //    spriteBatch.Draw(collisionBox, botBRect, Color.White);

            //    spriteBatch.DrawString(font, "Grid Squares: " + gridList.Count.ToString(), new Vector2(100, 515), Color.White);
            //    //spriteBatch.DrawString(font, "", new Vector2(100, 530), Color.White);

            //    if (gridList.Count > 1)
            //    {
            //        spriteBatch.DrawString(font, "Grid (0,0) Pos:" + gridList[0].gridPosition.ToString(), new Vector2(100, 545), Color.White);
            //    }

            //    if (gridList.Count > 3)
            //    {
            //        spriteBatch.DrawString(font, "Grid (0,3) Pos:" + gridList[3].gridPosition.ToString(), new Vector2(100, 560), Color.White);
            //    }

            //    spriteBatch.DrawString(font, "Placement Count:" + placementList.Count.ToString(), new Vector2(100, 575), Color.White);
            //    spriteBatch.DrawString(font, "Timer:" + timer.ToString(), new Vector2(100, 590), Color.White);
            //    spriteBatch.DrawString(font, "Scroll Offset:" + scrollOffset.ToString(), new Vector2(100, 605), Color.White);

            //    if (chestList.Count > 0)
            //        spriteBatch.DrawString(font, "Chest Pos" + chestList[0].chestPosition.ToString(), new Vector2(100, 620), Color.White);

            //    spriteBatch.DrawString(font, "Dooropen:" + isOpen.ToString(), new Vector2(100, 635), Color.Purple);
            //    spriteBatch.DrawString(font, "CharacterPos:" + player.characterPosition.ToString(), new Vector2(100, 650), Color.Pink);
            //    spriteBatch.DrawString(font, "Character GridX:" + playerGridPosX.ToString(), new Vector2(100, 665), Color.White);
            //    spriteBatch.DrawString(font, "Character GridY:" + playerGridPosY.ToString(), new Vector2(100, 680), Color.White);
            //    spriteBatch.DrawString(font, "Crosshair GridX:" + crosshairGridPosX.ToString(), new Vector2(300, 665), Color.White);
            //    spriteBatch.DrawString(font, "Crosshair GridY:" + crosshairGridPosY.ToString(), new Vector2(300, 680), Color.White);
            //    spriteBatch.DrawString(font, "hands" + hands.handsPosition.ToString(), new Vector2(300, 500), Color.Red);
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

        public void updateBullets()
        {
            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                bulletList[i].Update();

                if (bulletList[i].active == false)
                    bulletList.RemoveAt(i);
            }
        }

        #endregion

        #region Zombies

        public void addZombie1(Vector2 zp, Texture2D z )
        {
            Zombie1 zombies = new Zombie1(zp,z);            
            zombie1List.Add(zombies);
        }

        public void updateZombie1s()
        {  
            for(int i = zombie1List.Count - 1; i >= 0; i--)
            {
                zombie1List[i].Update();
                if (zombie1List[i].active == false)
                {
                    zombie1List.RemoveAt(i);
                    zombieKills++;
                }   
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
                    zombieKills++;
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
                {
                    zombie3List.RemoveAt(i);
                    zombieKills++;
                }
            }
        }

        #endregion

        #region Trees

        public void updateTree1s()
        {
            for(int i = tree1List.Count -1; i >= 0; i--)
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

        public void updateChests()
        {
            for (int i = chestList.Count - 1; i >= 0; i--)
            {
                chestList[i].Update();
                if (chestList[i].active == true)
                {
                    gridIsOccupied[crosshairPos] = false;
                    chestPosIsSet = false;
                    chestIsSet++;
                    chestList.RemoveAt(i);
                }
            }
        }

        #endregion

        #region Placements

        public void addPlacement(Texture2D t, Character c, Vector2 p, bool i, int type)
        {
            Placement placements = new Placement(t, c, p, i, type);
            placementList.Add(placements);
            
        }

        public void updatePlacements()
        {
            for (int i = 0; i < placementList.Count; i++)
            {
                placementList[i].Update();
                if (placementList[i].isBroken == true)
                {
                    //if (placementList[i].type == 3)
                    //{
                    //    player.characterFriction = .3f;
                    //}

                    //gridIsOccupied[crosshairPos] = false;

                    Debug.WriteLine("CUNTNIGGER");

                    gridIsOccupied.RemoveAt(i);
                    placementList.RemoveAt(i);
                }
            }
        }

        #endregion

        #region Drops

        public void addDrop( Texture2D dt, Vector2 dp, int type)
        {
            Drop dr = new Drop(dt, dp, type);
            drops.Add(dr);
        }

        public void updateDrops()
        {
            for (int i = drops.Count - 1; i >= 0; i--)
            {
                drops[i].Update();
                if (drops[i].active == false)
                {
                    if(drops[i].type == 1)
                        inventory.woodCount++;
                    if (drops[i].type == 2)
                        inventory.appleCount++;
                    if (drops[i].type == 3)
                        inventory.rockCount++;
                    if (drops[i].type == 4)
                        inventory.metalOreCount++;
                    if (drops[i].type == 5)
                        inventory.sandCount++;
                    if (drops[i].type == 6)
                        inventory.clothCount++;
                    if (drops[i].type == 7)
                        inventory.foodCount++;
                    if (drops[i].type == 8)
                        inventory.energyDrinkCount++;
                    if (drops[i].type == 9)
                        inventory.glassCount++;
                    if (drops[i].type == 10)
                        inventory.metalCount++;
                    if (drops[i].type == 11)
                        inventory.extendedMagCount++;
                    if (drops[i].type == 12)
                        inventory.shellsCount++;
                    if (drops[i].type == 13)
                        inventory.scopeCount++;
                    if (drops[i].type == 14)
                        inventory.seedCount++;
                    
                    drops.RemoveAt(i);
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

        public void updateHealth()
        {
            for (int i = healthList.Count - 1; i >= 0; i--)
            {
                healthList[i].Update();
                //if (healthList[i].active == false)
                //    healthList.RemoveAt(i);
            }
        }

        #endregion

        #region Seed

        public void addSeed(Vector2 gp)
        {
            seedList.Add(gp); //WE FINISHED ALMOST HERE ON THIS NIGHT THE FORTH KNIGHT! 4/27/2012 at fucking 6:40 its fucking bright outside too nigga shit, real nigga shit. 
            //Why are there are so many jugs of water?!? HAHAHAHA. 
            //Mr. Castillo says programmers leave jokes in their comments. 
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
               if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(t.tree2Position.X + (t.tree2Texture.Width / t.tree2Frames / 2), t.tree2Position.Y + (t.tree2Texture.Height / 2)), 60))
               {
                   player.characterPosition = previousCharacterPos;
               }
           }

           //foreach (Placement p in placementList)
           //{
           //    if (BoundingCircle(player.characterPosition, playerRadius, new Vector2(p.placementPosition.X + (p.placementTexture.Width / p.placementFrames / 2), p.placementPosition.Y + (p.placementTexture.Height / 2)), placementRadius))
           //    {
           //        player.characterPosition = previousCharacterPos;
           //    }
           //}
       }

        #endregion

    }
}