using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnitsActions;

public class Peasant : MonoBehaviour, IUnit
{
    [SerializeField] private List<BuildingState> _gardenBeds = new List<BuildingState>();
    [SerializeField] private List<BuildingState> _mills = new List<BuildingState>();
    [SerializeField] private List<BuildingState> _bakerys = new List<BuildingState>();
    [SerializeField] private RestBuilding[] _restBuildings;
    [SerializeField] private Transform _target;

    private UnitState _unitState;
    private NavMeshAgent _navMeshAgent;
    private Transform _parent;
    private Animator _animator;
    private Collider _unitCollider;
    private Coroutine _coroutine;
    private IBuilding _ibuilding;

    void Start()
    {
        _parent = transform.parent;
        FindTargets();
        _navMeshAgent = _parent.GetComponent<NavMeshAgent>();
        _unitState = _parent.GetComponent<UnitState>();
        _animator = GetComponent<Animator>();
        _unitCollider = _parent.GetComponent<CapsuleCollider>();
        //TODO debug
        _unitState.hp = 10;
        _unitState.sp = 40;
        _unitState.damage = 2;
        CalculateLogic();
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.zero;
    }

    public void AttackMoment()
    {
        if (_ibuilding != null)
        {
            _ibuilding.Damage(this, _unitState.damage);
        }
    }

    public void CalculateLogic()
    {
        _coroutine = StartCoroutine(Peasant(new SUnitAction()
        {
            iunit = this,
            unitState = _unitState,
            model = transform,
            animator = _animator,
            navMeshAgent = _navMeshAgent,
        }));

    }

    private void Rest()
    {
        if (_restBuildings.Length > 0)
        {
            _coroutine = StartCoroutine(Sit(new SSit()
            {
                navMeshAgent = _navMeshAgent,
                target = _target,
                animator = _animator,
                anim = "Sit",
                unitCollider = _unitCollider,
                time = GlobalConstants.sitTime,
                unitState = _unitState,
                restBuilding = _target.GetComponentInParent<RestBuilding>(),
                buildingState = _target.GetComponentInParent<BuildingState>(),
                iunit = this,
            }));
        }
        else
        {
            int rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0:
                    _coroutine = StartCoroutine(Wait(new SWait()
                    {
                        animator = _animator,
                        anim = "Wait",
                        time = GlobalConstants.waitTime,
                        unitState = _unitState,
                        iunit = this,
                    }));
                    break;
                case 1:
                    _coroutine = StartCoroutine(Walk(new SWalk()
                    {
                        navMeshAgent = _navMeshAgent,
                        model = transform,
                        from = _parent,
                        coordinates = FindRandCoordinates(),
                        animator = _animator,
                        anim = "Walk",
                        unitState = _unitState,
                        iunit = this,
                    }));
                    break;
            }
        }
    }

    private void SetNormalState()
    {
        _navMeshAgent.enabled = true;
        _unitCollider.isTrigger = false;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        foreach (AnimatorControllerParameter parameter in _animator.parameters)
        {
            _animator.SetBool(parameter.name, false);
        }
    }

    private void SetTarget()
    {
       //if (_gardenBeds.Count > 0)
       // {
       //     _target = FindNearestBuilding(new SFindNearestBuilding
       //     {
       //         buildingStates = _gardenBeds.ToArray(),
       //         transform = transform,
       //         check = GlobalConstants.checkProdStart,
       //     });
       // }
    }

    private void FindTargets()
    {
        Production[] productions = GameObject.Find("Buildings").GetComponentsInChildren<Production>();
        _mills.Clear();
        _bakerys.Clear();
        foreach (Production item in productions)
        {
            BuildingState bs = item.GetComponent<BuildingState>();
            if (!bs.isBusy)
            {
                switch (bs.nameTech)
                {
                    case "Mill":
                        _mills.Add(bs);
                        break;
                    case "Bakery":
                        _bakerys.Add(bs);
                        break;
                }
            }
        }

        _gardenBeds.Clear();
        GardenBed[] gardenBeds = GameObject.Find("Buildings").GetComponentsInChildren<GardenBed>();
        foreach (GardenBed item in gardenBeds)
        {
            BuildingState bs = item.GetComponent<BuildingState>();
            if (!bs.isBusy)
            {
                _gardenBeds.Add(bs);
            }
        }
        _restBuildings = FindRestBuilding();
    }

}
