using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IBuilding;

public class TreeEvents : MonoBehaviour, IBuilding, IBattle
{
    [SerializeField] GameObject _model;
    private BuildingState _buildingState;
    private Animator _animator;
    private Building _building;
    private Collider _collider;

    private void Awake()
    {
        _building = GetComponent<Building>();
        _buildingState = GetComponent<BuildingState>();
        _buildingState.name = GlobalConstants.tree;
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _building.RenderItems();
        _building.IncreaseHP(this, 0);

        if (_buildingState.hp <= 0)
        {
            _collider.isTrigger = true;
            _model.SetActive(false);
        }
    }

    public SBuildingReturndUsing Using(SBuildingUsing sBuildingUsing)
    {
        SBuildingReturndUsing sEndUsing = new SBuildingReturndUsing
        {
            building = transform
        };

        if (!sBuildingUsing.start)
        {
            switch (sBuildingUsing.action)
            {
                case GlobalConstants.woodcutAction:
                    _building.IncreaseProgress(GlobalConstants.woodcutValue);
                    _building.IncreaseHP(this, GlobalConstants.woodcutHpm);
                    _building.RenderItems();
                    _buildingState.isProdStart = true;
                    sEndUsing = new SBuildingReturndUsing()
                    {
                        spm = GlobalConstants.woodcutSpm,
                    };
                    break;
                case GlobalConstants.takeWoodAction:
                    _buildingState.progress--;
                    _building.RenderItems();
                    sEndUsing = new SBuildingReturndUsing()
                    {
                        items = new List<int>(new int[] { GlobalConstants.woodId }),
                        spm = GlobalConstants.takeWoodSpm,
                    };
                    break;
            }
        }        
        return sEndUsing;
    }

    public void Damage(UnitState enemy)
    {
        _animator.SetTrigger("Damage");
    }

    public void Build(int count)
    {
        
    }

    public void Destroy()
    {
        _animator.SetBool("Destroyed", true);
        _buildingState.isProdOver = false;
        StartCoroutine(HideModel());
    }

    public void NextDay()
    {

    }

    IEnumerator HideModel()
    {
        _collider.isTrigger = true;
        yield return new WaitForSeconds(5);
        _model.SetActive(false);
    }

    
}
