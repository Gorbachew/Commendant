using System;
using UnityEngine;
using static MainData;

public class GlobalEvents : MonoBehaviour
{
    public Transform _unitsFolder, _buildingsFolder;
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
        try
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
                UnitState unit = FindUnits(ud._units[i]._id);
                if (unit)
                {
                    unit.GetComponent<IUnit>().Enrichment(ud._units[i]);
                } else
                {
                    string name = OperationsHelper.CloneClearing(ud._units[i]._type);
                    GameObject obj = Instantiate(Resources.Load("Prefabs/Units/Citizen"), _unitsFolder) as GameObject;
                    obj.GetComponent<IUnit>().Enrichment(ud._units[i]);
                }
                
            }

            for (int i = 0; i < bd._builds.Length; i++)
            {
                BuildingState build = FindBuildings(bd._builds[i]._id);
                if (build)
                {
                    EnrichmentBuild(bd._builds[i], build);
                    continue;
                }
                string name = OperationsHelper.CloneClearing(bd._builds[i]._type);
                GameObject obj = Instantiate(Resources.Load("Prefabs/Builds/" + name), _buildingsFolder) as GameObject;
                EnrichmentBuild(bd._builds[i], obj.GetComponent<BuildingState>());
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    private void EnrichmentBuild(SBuild build, BuildingState buildState)
    {
        buildState.id = build._id;
        buildState.hp = build._hp;
        buildState.level = build._level;
        buildState.items = build._items;
        buildState.transform.position = new Vector3(
                build._pos._x,
                build._pos._y,
                build._pos._z
            );
        Transform obj = buildState.transform.Find("Object");
        obj.transform.localPosition = new Vector3(
                build._posObj._x,
                build._posObj._y,
                build._posObj._z
            );

        obj.transform.localRotation = Quaternion.Euler(
                build._rotObj._x,
                build._rotObj._y,
                build._rotObj._z
            );
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
