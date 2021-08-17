using System.Collections;
using System.Linq;
using UnityEngine;
using static MainData;

public class Building : MonoBehaviour
{
    private BuildingState _buildingState;
    [SerializeField] private Scaffolding[] _scaffoldings;
    [SerializeField] private BuildPart[] _buildingParts;
    [SerializeField] private GameObject[] _items;

    public Vector2Int _size = Vector2Int.one;

    private void Awake()
    {
        _buildingState = GetComponent<BuildingState>();
        
        if (transform.Find("Scaffoldings"))
        {
            _scaffoldings = transform.Find("Scaffoldings").GetComponentsInChildren<Scaffolding>();

        }
        _buildingParts = transform.Find("Model").GetComponentsInChildren<BuildPart>();
    }

    private void Start()
    {
        SetOptions();
        ÑheckBuildingParts();
    }

    public void StartPlacingBuilding()
    {
        _buildingState.isReady = true;
        SetBbuildingParts(true);
    }

    public void SetNormal()
    {
        StartCoroutine(BuildDelay());
    }

    public void Enrichment(SBuild build)
    {
        _buildingState.id = build.id;
        _buildingState.hp = build.hp;
        _buildingState.level = build.level;
        _buildingState.items = build.items.ToList();
        _buildingState.progress = build.progress;

        _buildingState.isDestroy = build.isDestroy;
        _buildingState.isBuild = build.isBouild;
        _buildingState.isWork = build.isWork;
        _buildingState.isReady = build.isReady;
        _buildingState.isProdStart = build.isProdStart;
        _buildingState.isProdOver = build.isProdOver;

        _buildingState.transform.position = new Vector3(
                build.pos.x,
                build.pos.y,
                build.pos.z
            );
        _buildingState.transform.rotation = Quaternion.Euler(
                build.rot.x,
                build.rot.y,
                build.rot.z
            );
    }

    public void ÑheckBuildingParts()
    {
        if (_buildingState.isBuild)
        {
            SetBbuildingParts(false);
            int count = -1;
            for (int i = _buildingState.hp; i > 0; i -= 20)
            {
                count++;
                if (_buildingParts.Length > count)
                {
                    _buildingParts[count].gameObject.SetActive(true);
                }
            } 
        }
        CheckScaffoldings();
    }

    public void Build(int count)
    {
        if (_buildingState.hp < _buildingState.maxHp)
        {
            _buildingState.hp += count;
        }
        else if (_buildingState.hp > _buildingState.maxHp)
        {
            _buildingState.hp = _buildingState.maxHp;

        }
        if (_buildingState.hp >= _buildingState.maxHp)
        {
            _buildingState.isReady = true;
        }
        ÑheckBuildingParts();
    }

    public void IncreaseProgress(int value)
    {
        if (_buildingState.isProdStart && _buildingState.progress < _buildingState.maxProgress)
        {
            _buildingState.progress += value;
            if (_buildingState.progress == _buildingState.maxProgress)
            {
                _buildingState.isProdOver = true;
            }
        }
        else if (_buildingState.progress >= _buildingState.maxProgress)
        {
            _buildingState.progress = _buildingState.maxProgress;
        }
    }

    public void IncreaseHP(IBuilding ibuilding, int value)
    {
        if (_buildingState.hp >= 0)
        {
            _buildingState.hp -= value;
            if (_buildingState.hp <= 0)
            {
                _buildingState.isDestroy = true;
                ibuilding.Destroy();
            }
        }
        else if (_buildingState.hp <= 0)
        {
            _buildingState.hp = 0;
        }
    }

    public void RenderItems()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].SetActive(i < _buildingState.progress);

        }
    }

    private void SetBbuildingParts(bool value)
    {
        foreach (BuildPart item in _buildingParts)
        {
            item.gameObject.SetActive(value);
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }

    private IEnumerator BuildDelay()
    {   
        SetBbuildingParts(false);
        _buildingState.isReady = false;
        CheckScaffoldings();
        yield return new WaitForSeconds(1);
        _buildingState.isBuild = true;
    }

    private void CheckScaffoldings()
    {
        foreach (Scaffolding item in _scaffoldings)
        {
            item.gameObject.SetActive(!_buildingState.isReady);
        }
    }

    private void SetOptions()
    {
        switch (_buildingState.name)
        {
            case "Tree":
                _buildingState.maxHp = GlobalConstants.treeMaxHp;
                _buildingState.maxProgress = GlobalConstants.treeMaxProgress;
                _buildingState.stopDistance = GlobalConstants.treeStopDistance;
                break;
            case "Stone":
                _buildingState.maxHp = GlobalConstants.stoneMaxHp;
                _buildingState.maxProgress = GlobalConstants.stoneMaxProgress;
                _buildingState.stopDistance = GlobalConstants.gardenBedStopDistance;
                break;
            case "Drovnitsa":
                _buildingState.maxHp = GlobalConstants.drovnitsaMaxHp;
                _buildingState.maxProgress = GlobalConstants.drovnitsaMaxProgress;
                _buildingState.stopDistance = GlobalConstants.drovnitsaStopDistance;
                break;
            case "StoneStock":
                _buildingState.maxHp = GlobalConstants.stoneStockMaxHp;
                _buildingState.maxProgress = GlobalConstants.stoneStockMaxProgress;
                _buildingState.stopDistance = GlobalConstants.stoneStockStopDistance;
                break;
            case "IronStock":
                break;
            case "GoldStock":
                break;
            case "ToolsStock":
                break;
            case "WeaponStock":
                break;

            case "Chair":
                _buildingState.maxHp = GlobalConstants.chairMaxHp;
                break;

            case "GardenBed":
                _buildingState.maxHp = GlobalConstants.gardenBedMaxHp;
                _buildingState.maxProgress = GlobalConstants.gardenBedMaxProgress;
                _buildingState.stopDistance = GlobalConstants.gardenBedStopDistance;
                break;
            case "Mill":
                _buildingState.maxHp = GlobalConstants.millMaxHp;
                _buildingState.maxProgress = GlobalConstants.millMaxProgress;
                _buildingState.stopDistance = GlobalConstants.millStopDistance;
                break;
            case "Bakery":
                _buildingState.maxHp = GlobalConstants.bakeryMaxHp;
                _buildingState.maxProgress = GlobalConstants.bakeryMaxProgress;
                _buildingState.stopDistance = GlobalConstants.bakeryStopDistance;
                break;
        }
    }

    

}
