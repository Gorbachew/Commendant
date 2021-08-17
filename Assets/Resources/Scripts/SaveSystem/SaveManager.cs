using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Transform _unitsFolder, _buildingsFolder, _environmentsFolder;
    public BuildingsGrid _buildingsGrid;
    
    BuildingState[] _buildings;
    UnitState[] _units;

    private void Awake()
    {
        _units = _unitsFolder.GetComponentsInChildren<UnitState>();
        _buildings = _buildingsFolder.GetComponentsInChildren<BuildingState>();
        Loading(SaveSystem.Loading());
    }
    
    private void OnApplicationQuit()
    {
        _units = _unitsFolder.GetComponentsInChildren<UnitState>();
        _buildings = _buildingsFolder.GetComponentsInChildren<BuildingState>();
        SaveSystem.Save(_units, _buildings);
    }

    private void Loading(MainData data)
    {
        
        UnitsData ud = new UnitsData
        {
            _units = data._units
        };
        BuildingsData bd = new BuildingsData
        {
            _builds = data._builds
        };


        for (int i = 0; i < ud._units.Length; i++)
        {
            UnitState unit = FindUnits(ud._units[i].id);
            if (unit)
            {
                try
                {
                    unit.GetComponent<Unit>().Enrichment(ud._units[i]);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Don`t load unit! Error: " + e);
                }

            } else
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/Units/Citizen"), _unitsFolder) as GameObject;
                try
                {
                    obj.GetComponent<Unit>().Enrichment(ud._units[i]);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Don`t load unit! Error: " + e);
                }
            }
                
        }

        for (int i = 0; i < bd._builds.Length; i++)
        {
            
            BuildingState build = FindBuildings(bd._builds[i].id);
            if (build)
            {
                try
                {
                    build.GetComponent<Building>().Enrichment(bd._builds[i]);
                    
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Don`t load building! Error: " + e);
                }
                continue;
            }
            GameObject obj = Instantiate(Resources.Load("Prefabs/Builds/" + bd._builds[i].name), _buildingsFolder) as GameObject;
            try
            {
                obj.GetComponent<Building>().Enrichment(bd._builds[i]);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Don`t load building! Error: " + e);
            }
        }
    }

    private UnitState FindUnits(int id)
    {
        foreach (UnitState unit in _units)
        {
            if (unit.id == id)
                return unit;
        }
        return null;
    }

    private BuildingState FindBuildings(int id)
    {
        foreach (BuildingState build in _buildings)
        {
            if (build.id == id)
                return build;
        }
        return null;
    }
}
