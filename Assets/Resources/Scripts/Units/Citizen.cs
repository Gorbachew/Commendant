using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnitsActions;

public class Citizen : MonoBehaviour, IUnit
{
    [SerializeField] private List<BuildingState> _notReadyBuildings;
    [SerializeField] private Transform _target;
    [SerializeField] private RestBuilding[] _restBuildings;

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
        _navMeshAgent = _parent.GetComponent<NavMeshAgent>();
        _unitState = _parent.GetComponent<UnitState>();
        _animator = GetComponent<Animator>();
        _unitCollider = _parent.GetComponent<CapsuleCollider>();

        _unitState.building = GlobalConstants.citizenBuildingParam;

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
        StartCoroutine(Citizen(new SUnitAction()
        {
            iunit = this,
            unitState = _unitState,
            model = transform,
            animator = _animator,
            navMeshAgent = _navMeshAgent,
        }));
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
        //    if (_unitState.sp <= 0 || _notReadyBuildings.Count <= 0)
        //    {
        //        Rest();
        //        return;
        //    }

        //    Work();

        //}


        //FindTargets();

        //if (_target != null)
        //{
        //    float dist = Vector3.Distance(_parent.transform.position, _target.transform.position);
        //    if (dist >= GlobalConstants.stopDistance)
        //    {
        //        _coroutine = StartCoroutine(Walk(new SWalk()
        //        {
        //            navMeshAgent = _navMeshAgent,
        //            model = transform,
        //            from = _parent,
        //            target = _target,
        //            coordinates = FindRandCoordinates(),
        //            animator = _animator,
        //            anim = "Walk",
        //            unitState = _unitState,
        //            iunit = this,
        //        }));
        //    } else
        //    {
        //        Work();
        //    }
        //} else
        //{
        //    int rand = Random.Range(0, 2);
        //    switch (rand)
        //    {
        //        case 0:
        //            SetTarget();
        //            CalculateLogic();
        //            break;
        //        case 1:
        //            Rest();
        //            break;
        //    }

        //}
    }


    //private void Rest()
    //{
    //    if (_restBuildings.Length > 0)
    //    {
    //        _coroutine = StartCoroutine(Sit(new SSit()
    //        {
    //            navMeshAgent = _navMeshAgent,
    //            target = _target,
    //            animator = _animator,
    //            anim = "Sit",
    //            unitCollider = _unitCollider,
    //            time = GlobalConstants.sitTime,
    //            unitState = _unitState,
    //            restBuilding = _target.GetComponentInParent<RestBuilding>(),
    //            buildingState = _target.GetComponentInParent<BuildingState>(),
    //            iunit = this,
    //        }));
    //    }
    //    else
    //    {
    //        int rand = Random.Range(0, 2);
    //        switch (rand)
    //        {
    //            case 0:
    //                _coroutine = StartCoroutine(Wait(new SWait()
    //                {
    //                    animator = _animator,
    //                    anim = "Wait",
    //                    time = GlobalConstants.waitTime,
    //                    unitState = _unitState,
    //                    iunit = this,
    //                }));
    //                break;
    //            case 1:
    //                _coroutine = StartCoroutine(Walk(new SWalk()
    //                {
    //                    navMeshAgent = _navMeshAgent,
    //                    model = transform,
    //                    from = _parent,
    //                    coordinates = FindRandCoordinates(),
    //                    animator = _animator,
    //                    anim = "Walk",
    //                    unitState = _unitState,
    //                    iunit = this,
    //                }));
    //                break;
    //        }
    //    }
    //}

    //private void Work()
    //{
    //    _coroutine = StartCoroutine(Build(new SBuild()
    //    {
    //        target = _target,
    //        animator = _animator,
    //        anim = "Working",
    //        time = GlobalConstants.buildsTime,
    //        unitState = _unitState,
    //        buildingState = _target.GetComponentInParent<BuildingState>(),
    //        building = _target.GetComponentInParent<Building>(),
    //        iunit = this,
    //    }));
    //}


    //private void Work()
    //{
    //    _coroutine = StartCoroutine(Build(new SBuild()
    //    {
    //        target = _target,
    //        animator = _animator,
    //        anim = "Working",
    //        time = GlobalConstants.buildsTime,
    //        unitState = _unitState,
    //        buildingState = _target.GetComponentInParent<BuildingState>(),
    //        building = _target.GetComponentInParent<Building>(),
    //        iunit = this,
    //    }));
    //    _target = null;
    //}

    //private void Rest()
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

    //private void SetTarget()
    //{

    //    if (_unitState.sp <= 0 || _notReadyBuildings.Count <= 0)
    //    {
    //        _target = FindNearestRestBuilding(new SFindNearestRestBuilding()
    //        {
    //            restBuildings = _restBuildings,
    //            transform = _parent.transform,
    //        });
    //        return;
    //    }

    //    if (_notReadyBuildings.Count > 0)
    //    {
    //        _target = FindNearestNotReadyBuilding(new SFindNearestNotReadyBuilding()
    //        {
    //            buildingState = _notReadyBuildings.ToArray(),
    //            transform = transform.parent,
    //        });
    //    }
        
    //}

    //private void FindTargets()
    //{
    //    BuildingState[] buildings = GameObject.Find("Buildings").GetComponentsInChildren<BuildingState>();
    //    _notReadyBuildings.Clear();
    //    foreach (BuildingState item in buildings)
    //    {
    //        if (!item.isReady && !item.isBusy)
    //        {
    //            _notReadyBuildings.Add(item);
    //        }
    //    }
    //    _restBuildings = FindRestBuilding();
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
}
