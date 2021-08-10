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
                GlobalConstants.textDay => "����",

                GlobalConstants.textStateGone => "���� �",
                GlobalConstants.textStateWalking => "������",
                GlobalConstants.textStateExtract => "��������",
                GlobalConstants.textStateSit => "�����",
                GlobalConstants.textStateGive => "�����������",
                GlobalConstants.textStateWait => "����",
                GlobalConstants.textStateBuilds => "������",

                GlobalConstants.textFoodStockName => "��������",
                GlobalConstants.textDrovnitsaName => "������ � �������",
                GlobalConstants.textStoneStockName => "������ � ������",
                GlobalConstants.textIronStockName => "������ � �������",
                GlobalConstants.textGoldStockName => "������ � �������",
                GlobalConstants.textToolsStockName => "������ � �������������",
                GlobalConstants.textWeaponStockName => "������ � �������",

                GlobalConstants.textTreeName => "������",
                GlobalConstants.textStoneName => "�����",

                GlobalConstants.textChairName => "�����",

                GlobalConstants.textGardenBedName => "������",
                GlobalConstants.textMillName => "��������",
                GlobalConstants.textBakeryName => "�������",
                _ => "N\\A",
            },
            _ => "N\\A",
        };
    }
}