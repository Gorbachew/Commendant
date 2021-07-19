using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{

    public static void Save (UnitState[] units, BuildingState[] buildings)
    {
        UnitsData unitsData = new UnitsData(units);
        BuildingsData buildingsData = new BuildingsData(buildings);

        MainData mainData = new MainData(buildingsData, unitsData);
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/CommendantSave.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, mainData);
        stream.Close();
        
    }

    public static MainData Loading ()
    {
        string path = Application.persistentDataPath + "/CommendantSave.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MainData data = formatter.Deserialize(stream) as MainData;
            stream.Close();

            return data;
        }else
        {
            Debug.Log("Save Units file not found in " + path);
            return null;
        }
    }
}
