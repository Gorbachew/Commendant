using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.AI;
using static UnitsEvents;

public class Peasant : MonoBehaviour, IUnit
{
    [SerializeField] private List<BuildingState> _gardenBeds = new List<BuildingState>();
    [SerializeField] private List<BuildingState> _mills = new List<BuildingState>();
    [SerializeField] private List<BuildingState> _bakerys = new List<BuildingState>();
    [SerializeField] private RestBuilding[] _restBuildings;
    [SerializeField] private Transform _target;
    [SerializeField] public bool _noStocks;

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
        _unitState.sp = 30;
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
        _noStocks = false;

        SetNormalState();
        FindTargets();
        SetTarget();

        if (_target == null)
        {
            _target = _parent.transform;
        } else
        {
            _target.Find("Model");
        }

        float dist = Vector3.Distance(_parent.transform.position, _target.transform.position);

        if (dist >= GlobalConstants.stopDistance)
        {
            _coroutine = StartCoroutine(Walk(new SWalk()
            {
                navMeshAgent = _navMeshAgent,
                model = transform,
                from = _parent,
                target = _target,
                coordinates = FindRandCoordinates(),
                animator = _animator,
                anim = "Walk",
                unitState = _unitState,
                iunit = this,
            }));
        }
        else
        {
            if ((_unitState.sp <= 0 && _unitState.items.Count == 0) || _noStocks)
            {
                Rest();
                return;
            }

            //Work();

        }
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

    private void Work()
    {
        float items = _unitState.items.Count;
        _ibuilding = _target.parent.GetComponent<IBuilding>();
        if (items <= 0)
        {
            _coroutine = StartCoroutine(Extract(new SExtract()
            {
                target = _target,
                unit = _parent,
                animator = _animator,
                anim = "Woodcut",
                time = GlobalConstants.woodcutTime,
                unitState = _unitState,
                itemId = GlobalConstants.woodId,
                itemCount = GlobalConstants.woodCount,
                spMinus = GlobalConstants.woodcutSpm,
                buildingState = _target.GetComponentInParent<BuildingState>(),
                iunit = this,
            }));
        }
        else
        {
            _coroutine = StartCoroutine(Give(new SGive()
            {
                target = _target,
                animator = _animator,
                anim = "Working",
                time = GlobalConstants.giveTime,
                unitState = _unitState,
                stock = _target.parent.GetComponent<Stock>(),
                buildingState = _target.GetComponentInParent<BuildingState>(),
                iunit = this,
            }));
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
       if (_gardenBeds.Count > 0)
        {
            _target = FindNearestBuilding(new SFindNearestBuilding
            {
                buildingStates = _gardenBeds.ToArray(),
                transform = transform,
            });
        }
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
