

public class GlobalConstants
{

    public const int placeSizeX = 50;
    public const int placeSizeY = 50;

    public const int maxFindDistance = 30;
    public const float stopDistance = 1.5f;

    //Building Param
    public const int citizenBuildingParam = 20;
    public const int woodcutterBuildingParam = 0;
    public const int minerBuildingParam = 0;

    //MaxHp
    public const int chairMaxHp = 20;

    //Items Id
    public const int allRes = 0;
    public const int woodId = 1;
    public const int stoneId = 2;
    public const int ironId = 3;
    public const int goldId = 4;
    public const int toolsId = 5;
    public const int armorId = 6;
    public const int weaponId = 7;
    public const int hayId = 8;
    public const int flourId = 9;
    public const int breadId = 10;

    //TimesWork
    public const int sitTime = 3;
    public const int waitTime = 3;


    //Check
    public const string checkProdStart = "check_prod_start";
    public const string checkProdOver = "check_prod_over";
    public const string checkFullness = "check_progress";
    public const string checkEmpty = "check_items";
    public const string checkNotReady = "check_not_ready";
    public const string checkNotDied = "check_not_died";
    public const string checkAll = "check_all";

    // Unit Actions
    public const string plantSeedsAction = "action_plant_seeds";
    public const int plantSeedTime = 3;
    public const int plantSeedSpm = 5;

    public const string harwestAction = "action_harwest";
    public const int harwestTime = 3;
    public const int harwestSpm = 5;

    public const string makeFlourAction = "action_make_flour";
    public const int makeFlourTime = 3;
    public const int makeFlourSpm = 5;
    public const string takeFlourAction = "action_take_flour";
    public const int takeFlourTime = 3;

    public const string makeBreadAction = "action_make_bread";
    public const int makeBreadTime = 10;
    public const int makeBreadSpm = 5;

    public const string takeBreadAction = "action_take_bread";
    public const int takeBreadTime = 3;

    public const string buildAction = "action_build";
    public const int buildsTime = 3;
    public const int buildSpm = 10;

    public const string woodcutAction = "action_woodcut";
    public const int woodcutTime = 10;
    public const int woodcutSpm = 10;
    public const int woodcutHpm = 5;

    public const string takeWoodAction = "action_take_wood";
    public const int takeWoodTime = 3;
    public const int takeWoodSpm = 0;

    public const string putWoodAction = "action_put_wood";
    public const int putWoodTime = 3;
    public const int putWoodSpm = 0;

    public const string mineAction = "action_mine";
    public const int mineTime = 3;
    public const int mineSpm = 10;

    //Buildings    
    public const string allBuildings = "all_buildings";

    public const string bakery = "Bakery";
    public const int bakeryMaxHp = 80;
    public const int bakeryMaxProgress = 15;
    public const float bakeryStopDistance = 1.5f;

    public const string gardenBed = "GardenBed";
    public const int gardenBedMaxHp = 40;
    public const int gardenBedMaxProgress = 3;
    public const float gardenBedStopDistance = 0.5f;

    public const string mill = "Mill";
    public const int millMaxHp = 60;
    public const int millMaxProgress = 10;
    public const float millStopDistance = 1.5f;

    public const string drovnitsa = "Drovnitsa";
    public const int drovnitsaMaxHp = 60;
    public const int drovnitsaMaxProgress = 50;
    public const float drovnitsaStopDistance = 1.5f;

    public const string tree = "Tree";
    public const int treeMaxHp = 100;
    public const int treeMaxProgress = 25;
    public const float treeStopDistance = 1.5f;

    public const string stone = "Stone";
    public const int stoneMaxHp = 1000;
    public const int stoneMaxProgress = 25;
    public const float stoneStopDistance = 1f;

    public const string stoneStock = "StoneStock";
    public const int stoneStockMaxHp = 60;
    public const int stoneStockMaxProgress = 50;
    public const float stoneStockStopDistance = 1.5f;

    //Progress value
    public const int milletHayValue = 1;
    public const int flourBagValue = 1;
    public const int makeBreadValue = 3;
    public const int woodcutValue = 3;
    public const int putWoodValue = 1;

    //Texts
    public const string textDay = "day";


    public const string textPlantingSeed = "state_planting_seed";
    public const string textHarvest = "state_harvest";
    public const string textMakeBread = "state_make_bread";
    public const string textWoodcut = "state_chopping_wood";
    public const string text—arriesWood = "state_carries_wood";
    public const string textMining = "state_mining";

}