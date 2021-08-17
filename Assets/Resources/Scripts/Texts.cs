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
                GlobalConstants.text�arriesWood => "�arries Wood",
                GlobalConstants.textMining => "Mining",
                _ => "N\\A",
            },
            "ru" => name switch
            {
                GlobalConstants.textDay => "����",

                GlobalConstants.textPlantingSeed => "������ ������",
                GlobalConstants.textHarvest => "�������� ������",
                GlobalConstants.textMakeBread => "����� ����",
                GlobalConstants.textWoodcut => "����� �����",
                GlobalConstants.text�arriesWood => "����� �����",
                GlobalConstants.textMining => "�������� ��������",
                _ => "N\\A",
            },
            _ => "N\\A",
        };
    }
}