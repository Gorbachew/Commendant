using System.Collections;
using System.Linq;
using UnityEngine;
using static MainData;

public class Building : MonoBehaviour
{
    private BuildingState _buildingState;
    [SerializeField] private Scaffolding[] _scaffoldings;
    [SerializeField] private BuildPart[] _buildingParts;

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

        _buildingState.isBuild = build.isBouild;
        _buildingState.isWork = build.isWork;
        _buildingState.isReady = build.isReady;

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
                    Debug.Log(gameObject.name + " " + i);
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

    public void Damage(IUnit damager, int count)
    {
        _buildingState.hp -= count;
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
        switch (_buildingState.nameTech)
        {
            case "Tree":
                _buildingState.nameGame = Texts.get(GlobalState.language, GlobalConstants.textTreeName);
                _buildingState.maxHp = GlobalConstants.treeMaxHp;
                break;
            case "Stone":
                _buildingState.nameGame = Texts.get(GlobalState.language, GlobalConstants.textStoneName);
                _buildingState.maxHp = GlobalConstants.stoneMaxHp;
                break;
            case "Drovnitsa":
                _buildingState.nameGame = Texts.get(GlobalState.language, GlobalConstants.textDrovnitsaName);
                _buildingState.maxHp = GlobalConstants.drovnitsaMaxHp;
                break;
            case "StoneStock":
                _buildingState.nameGame = Texts.get(GlobalState.language, GlobalConstants.textStoneStockName);
                _buildingState.maxHp = GlobalConstants.stoneStockMaxHp;
                break;
            case "IronStock":
                _buildingState.nameGame = Texts.get(GlobalState.language, GlobalConstants.textIronStockName);
                break;
            case "GoldStock":
                _buildingState.nameGame = Texts.get(GlobalState.language, GlobalConstants.textGoldStockName);
                break;
            case "ToolsStock":
                _buildingState.nameGame = Texts.get(GlobalState.language, GlobalConstants.textToolsStockName);
                break;
            case "WeaponStock":
                _buildingState.nameGame = Texts.get(GlobalState.language, GlobalConstants.textWeaponStockName);
                break;
            case "Chair":
                _buildingState.nameGame = Texts.get(GlobalState.language, GlobalConstants.textChairName);
                _buildingState.maxHp = GlobalConstants.chairMaxHp;
                break;
        }
    }

    

}
