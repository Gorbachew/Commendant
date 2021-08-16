

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
    public const int treeMaxHp = 400;
    public const int stoneMaxHp = 1000;

    public const int drovnitsaMaxHp = 60;
    public const int stoneStockMaxHp = 60;

    public const int chairMaxHp = 20;

    
    //Items Id
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

    //Added Items
    public const int woodCount = 3;
    public const int stoneCount = 1;

    //MaxItems
    public const int drownitsaMaxItems = 50;
    public const int stoneStockMaxItems = 50;

    //TimesWork
    public const int giveTime = 3;
    public const int woodcutTime = 3;
    public const int mineStoneTime = 3;
    public const int sitTime = 3;
    public const int waitTime = 3;
    public const int buildsTime = 3;
    public const int plantSeedTime = 3;
    public const int harwestTime = 3;
    public const int makeFlourTime = 3;
    public const int takeFlourTime = 3;
    public const int makeBreadTime = 10;


    //Sp Minus
    public const int buildSpm = 10;
    public const int woodcutSpm = 10;
    public const int mineStoneSpm = 10;
    public const int plantSeedSpm = 5;
    public const int harwestSpm = 5;
    public const int makeFlourSpm = 5;
    public const int makeBreadSpm = 5;

    // Unit Actions
    public const string plantSeedsAction = "action_plant_seeds";
    public const string harwestAction = "action_harwest";
    public const string makeFlourAction = "action_make_flour";
    public const string takeFlourAction = "action_take_flour";
    public const string makeBreadAction = "action_make_bread";
    public const string takeBreadAction = "action_take_bread";

    public const string buildAction = "action_build";

    //Check
    public const string checkProdStart = "check_prod_start";
    public const string checkProdOver = "check_prod_over";
    public const string checkProgress = "check_progress";
    public const string checkItems = "check_items";
    public const string checkNotReady = "check_not_ready";

    //Buildings    
    public const string allBuildings = "all_buildings";

    public const string bakery = "Bakery";
    public const string textBakeryName = "name_bakery";
    public const int bakeryMaxHp = 80;
    public const int bakeryMaxProgress = 15;
    public const float bakeryStopDistance = 1.5f;

    public const string gardenBed = "GardenBed";
    public const string textGardenBedName = "name_garden_bed";
    public const int gardenBedMaxHp = 40;
    public const int gardenBedMaxProgress = 3;
    public const float gardenBedStopDistance = 0.5f;

    public const string mill = "Mill";
    public const string textMillName = "name_mill";
    public const int millMaxHp = 60;
    public const int millMaxProgress = 10;
    public const float millStopDistance = 1.5f;


    //Progress value
    public const int milletHayValue = 1;
    public const int flourBagValue = 1;
    public const int makeBreadValue = 3;

    //Texts
    public const string textDay = "day";

    public const string textStateGone = "state_gone";
    public const string textStateWalking = "state_walking";
    public const string textStateExtract = "state_extract";
    public const string textStateSit = "state_sit";
    public const string textStateGive = "state_give";
    public const string textStateWait = "state_wait";
    public const string textStateBuilds = "state_build";

    public const string textGoToStock = "state_go_to_stock";
    public const string textPlantingSeed = "state_planting_seed";
    public const string textHarvest = "state_harvest";
    public const string textMakeBread = "state_make_bread";

    public const string textFoodStockName = "name_food_stock";
    public const string textDrovnitsaName = "name_drovnitsa";
    public const string textStoneStockName = "name_stone_stock";
    public const string textIronStockName = "name_iron_stock";
    public const string textGoldStockName = "name_gold_stock";
    public const string textToolsStockName = "name_tools_stock";
    public const string textWeaponStockName = "name_weapon_stock";

    public const string textTreeName = "name_tree";
    public const string textStoneName = "name_stone";

    public const string textChairName = "name_chair";

  
}