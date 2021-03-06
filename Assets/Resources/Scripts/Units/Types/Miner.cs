using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnitsActions;

public class Miner : MonoBehaviour, IUnit
{
    [SerializeField] private List<StoneEvents> _stones = new List<StoneEvents>();
    [SerializeField] private List<Stock> _stocks = new List<Stock>();
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
        //FindTargets();
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
        //if (_ibuilding != null)
        //{
        //    _ibuilding.Damage(this, _unitState.damage);
        //}
    }

    public void CalculateLogic()
    {
        //_noStocks = false;

        //SetNormalState();
        //FindTargets();
        //SetTarget();

        //if (_target == null)
        //{
        //    _target = _parent.transform;
        //}

        //float dist = Vector3.Distance(_parent.transform.position, _target.transform.position);

        //if (dist >= GlobalConstants.stopDistance)
        //{
        //    _coroutine = StartCoroutine(Walk(new SWalk()
        //    {
        //        navMeshAgent = _navMeshAgent,
        //        model = transform,
        //        from = _parent,
        //        target = _target,
        //        coordinates = FindRandCoordinates(),
        //        animator = _animator,
        //        anim = "Walk",
        //        unitState = _unitState,
        //        iunit = this,
        //    }));
        //}
        //else
        //{
        //    if ((_unitState.sp <= 0 && _unitState.items.Count == 0) || _noStocks)
        //    {
        //        Rest();
        //        return;
        //    }

        //    Work();

        //}
    }

    private void Rest()
    {
        //if (_restBuildings.Length > 0)
        //{
        //    _coroutine = StartCoroutine(Sit(new SSit()
        //    {
        //        navMeshAgent = _navMeshAgent,
        //        target = _target,
        //        animator = _animator,
        //        anim = "Sit",
        //        unitCollider = _unitCollider,
        //        time = GlobalConstants.sitTime,
        //        unitState = _unitState,
        //        restBuilding = _target.GetComponentInParent<RestBuilding>(),
        //        buildingState = _target.GetComponentInParent<BuildingState>(),
        //        iunit = this,
        //    }));
        //}
        //else
        //{
        //    int rand = Random.Range(0, 2);
        //    switch (rand)
        //    {
        //        case 0:
        //            _coroutine = StartCoroutine(Wait(new SWait()
        //            {
        //                animator = _animator,
        //                anim = "Wait",
        //                time = GlobalConstants.waitTime,
        //                unitState = _unitState,
        //                iunit = this,
        //            }));
        //            break;
        //        case 1:
        //            _coroutine = StartCoroutine(Walk(new SWalk()
        //            {
        //                navMeshAgent = _navMeshAgent,
        //                model = transform,
        //                from = _parent,
        //                coordinates = FindRandCoordinates(),
        //                animator = _animator,
        //                anim = "Walk",
        //                unitState = _unitState,
        //                iunit = this,
        //            }));
        //            break;
        //    }
        //}
    }

    //private void Work()
    //{
    //    float items = _unitState.items.Count;
    //    _ibuilding = _target.parent.GetComponent<IBuilding>();
    //    if (items <= 0)
    //    {
    //        _coroutine = StartCoroutine(Extract(new SExtract()
    //        {
    //            target = _target,
    //            unit = _parent,
    //            animator = _animator,
    //            anim = "Woodcut",
    //            time = GlobalConstants.mineStoneTime,
    //            unitState = _unitState,
    //            itemId = GlobalConstants.stoneId,
    //            itemCount = GlobalConstants.stoneCount,
    //            spMinus = GlobalConstants.mineStoneSpm,
    //            buildingState = _target.GetComponentInParent<BuildingState>(),
    //            iunit = this,
    //        }));;
    //    }
    //    else
    //    {
    //        _coroutine = StartCoroutine(Give(new SGive()
    //        {
    //            target = _target,
    //            animator = _animator,
    //            anim = "Working",
    //            time = GlobalConstants.giveTime,
    //            unitState = _unitState,
    //            stock = _target.parent.GetComponent<Stock>(),
    //            buildingState = _target.GetComponentInParent<BuildingState>(),
    //            iunit = this,
    //        }));
    //    }
    //}

    //private void SetNormalState()
    //{
    //    _navMeshAgent.enabled = true;
    //    _unitCollider.isTrigger = false;

    //    if (_coroutine != null)
    //    {
    //        StopCoroutine(_coroutine);
    //    }

    //    foreach (AnimatorControllerParameter parameter in _animator.parameters)
    //    {
    //        _animator.SetBool(parameter.name, false);
    //    }
    //}

    //private void SetTarget()
    //{
    //    if (_unitState.items.Count == 0)
    //    {
    //        if (_unitState.sp <= 0)
    //        {
    //            _target = FindNearestRestBuilding(new SFindNearestRestBuilding()
    //            {
    //                restBuildings = _restBuildings,
    //                transform = _parent.transform,
    //            });
    //            return;
    //        }

    //        _target = FindNearestStone(new SFindNearestStone()
    //        {
    //            stones = _stones.ToArray(),
    //            transform = _parent.transform,
    //        });
    //    }
    //    else
    //    {
    //        _target = FindNearestStoneStock(new SFindNearestStoneStock()
    //        {
    //            restBuildings = _restBuildings,
    //            transform = _parent.transform,
    //            stocks = _stocks.ToArray(),
    //            miner = GetComponent<Miner>()
    //        });
    //    }
    //}

    //private void FindTargets()
    //{
    //    StoneEvents[] stones = GameObject.Find("Environment").GetComponentsInChildren<StoneEvents>();
    //    _stones.Clear();
    //    foreach (StoneEvents item in stones)
    //    {
    //        if (!item.GetComponent<BuildingState>().isBusy)
    //        {
    //            _stones.Add(item.GetComponent<StoneEvents>());
    //        }
    //    }

    //    BuildingState[] stocks = GameObject.Find("Buildings").GetComponentsInChildren<BuildingState>();
    //    _stocks.Clear();
    //    foreach (BuildingState item in stocks)
    //    {
    //        BuildingState bs = item.GetComponentInParent<BuildingState>();
    //        if (bs.isReady && item.resources == "stone")
    //        {
    //            _stocks.Add(item.GetComponent<Stock>());
    //        }
    //    }
    //    _restBuildings = FindRestBuilding();
    //}

}
