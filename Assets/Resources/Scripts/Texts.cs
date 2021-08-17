public class Texts
{
    public static string get(string lang, string name)
    {
        return lang switch
        {
            "eng" => name switch
            {
                GlobalConstants.textDay => "Day",

                GlobalConstants.textPlantingSeed => "Planting seed",
                GlobalConstants.textHarvest => "Harvesting",
                GlobalConstants.textMakeBread => "Bakes bread",
                GlobalConstants.textWoodcut => "Chopping wood",
                GlobalConstants.textÑarriesWood => "Ñarries Wood",
                GlobalConstants.textMining => "Mining",
                _ => "N\\A",
            },
            "ru" => name switch
            {
                GlobalConstants.textDay => "Äåíü",

                GlobalConstants.textPlantingSeed => "Ñàæàåò ñåìåíà",
                GlobalConstants.textHarvest => "Ñîáèðàåò óðîæàé",
                GlobalConstants.textMakeBread => "Ïå÷åò õëåá",
                GlobalConstants.textWoodcut => "Ðóáèò äðîâà",
                GlobalConstants.textÑarriesWood => "Íîñèò äðîâà",
                GlobalConstants.textMining => "Äîáûâàåò ìèíåðàëû",
                _ => "N\\A",
            },
            _ => "N\\A",
        };
    }
}