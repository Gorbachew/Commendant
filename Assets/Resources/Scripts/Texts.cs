public class Texts
{
    public static string get(string lang, string name)
    {
        return lang switch
        {
            "eng" => name switch
            {
                GlobalConstants.textDay => "Day",

                GlobalConstants.textStateGone => "Go to",
                GlobalConstants.textStateWalking => "Walking",
                GlobalConstants.textStateExtract => "Mines",
                GlobalConstants.textStateSit => "Sitting",
                GlobalConstants.textStateGive => "Puts",
                GlobalConstants.textStateWait => "Waiting",
                GlobalConstants.textStateBuilds => "Builds",

                GlobalConstants.textFoodStockName => "canteen",
                GlobalConstants.textDrovnitsaName => "wood warehouse",
                GlobalConstants.textStoneStockName => "stone warehouse",
                GlobalConstants.textIronStockName => "iron warehouse",
                GlobalConstants.textGoldStockName => "gold warehouse",
                GlobalConstants.textToolsStockName => "tools warehouse",
                GlobalConstants.textWeaponStockName => "weapon warehouse",

                GlobalConstants.textTreeName => "tree",
                GlobalConstants.textStoneName => "stone",

                GlobalConstants.textChairName => "chair",

                GlobalConstants.textGardenBedName => "garden bed",
                GlobalConstants.textMillName => "mill",
                GlobalConstants.textBakeryName => "bakery",

                _ => "N\\A",
            },
            "ru" => name switch
            {
                GlobalConstants.textDay => "День",

                GlobalConstants.textStateGone => "Идет к",
                GlobalConstants.textStateWalking => "Гуляет",
                GlobalConstants.textStateExtract => "Добывает",
                GlobalConstants.textStateSit => "Сидит",
                GlobalConstants.textStateGive => "Выкладывает",
                GlobalConstants.textStateWait => "Ждет",
                GlobalConstants.textStateBuilds => "Строит",

                GlobalConstants.textFoodStockName => "столовой",
                GlobalConstants.textDrovnitsaName => "складу с деревом",
                GlobalConstants.textStoneStockName => "складу с камнем",
                GlobalConstants.textIronStockName => "складу с железом",
                GlobalConstants.textGoldStockName => "складу с золотом",
                GlobalConstants.textToolsStockName => "складу с инструментами",
                GlobalConstants.textWeaponStockName => "складу с оружием",

                GlobalConstants.textTreeName => "дереву",
                GlobalConstants.textStoneName => "камню",

                GlobalConstants.textChairName => "стулу",

                GlobalConstants.textGardenBedName => "грядке",
                GlobalConstants.textMillName => "мельнице",
                GlobalConstants.textBakeryName => "пекарне",
                _ => "N\\A",
            },
            _ => "N\\A",
        };
    }
}