using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnitsEvents;

public class Woodcutter : MonoBehaviour, IUnit
{
    [SerializeField] private TreeEvents[] _trees;
    [SerializeField] private Drovnitsa[] _drovnitsy;
    [SerializeField] private RestBuilding[] _restBuildings;
    [SerializeField] private Transform _target;
    [SerializeField] private bool _noStocks;

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

            Work();

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
                animator = _animator,
                anim = "Woodcut",
                time = GlobalConstants.woodcutTime,
                unitState = _unitState,
                itemId = GlobalConstants.woodId,
                itemCount = GlobalConstants.woodCount,
                spMinus = GlobalConstants.woodcutSpm,
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
                ibuilding = _ibuilding,
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
        if (_unitState.items.Count == 0)
        {
            if (_unitState.sp <= 0)
            {
               _target = FindNearestRestBuilding();
                return;
            }

            _target = FindNearestTree();
        }
        else
        {
            _target = FindNearestDrovnitsa();
        } 
    }

    private Vector3 FindRandCoordinates()
    {
        return new Vector3(
            Random.Range(0, 10),
            0,
            Random.Range(0, 10)
            );
    }

    private Transform FindNearestRestBuilding()
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (RestBuilding d in _restBuildings)
        {
            float newDist = Vector3.Distance(_parent.transform.position, d.transform.position);
            BuildingState bs = d.GetComponent<BuildingState>();
            List<int> items = bs.items;
            if (dist > newDist)
            {
                dist = newDist;
                return d.transform.Find("Model");
            }
        }
        return transform;
    }

    private Transform FindNearestDrovnitsa()
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (Drovnitsa d in _drovnitsy)
        {
            float newDist = Vector3.Distance(_parent.transform.position, d.transform.position);
            List<int> items = d.GetComponent<BuildingState>().items;
            if (dist > newDist && items.Count < GlobalConstants.drownitsaMaxItems)
            {
                dist = newDist;
                return d.transform.Find("Model");
            }
        }
        _noStocks = true;
        return FindNearestRestBuilding();
    }

    private Transform FindNearestTree()
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (TreeEvents t in _trees)
        {
            float newDist = Vector3.Distance(_parent.transform.position, t.transform.position);
            if (dist > newDist)
            {
                dist = newDist;
                return t.transform.Find("Model");
            }
        }
        return transform;
    }

    private void FindNotBusyRestBuilding()
    {
        RestBuilding[] rbArray = GameObject.Find("Buildings").GetComponentsInChildren<RestBuilding>();
        List<RestBuilding> rbList = new List<RestBuilding>();
        foreach (RestBuilding rb in rbArray)
        {
            if (!rb.GetComponentInParent<BuildingState>().isBusy)
            {
                rbList.Add(rb);
            }
        }
        _restBuildings = rbList.ToArray();
    }

    private void FindTargets()
    {
        _trees = GameObject.Find("Environment").GetComponentsInChildren<TreeEvents>();
        _drovnitsy = GameObject.Find("Buildings").GetComponentsInChildren<Drovnitsa>();
        FindNotBusyRestBuilding();
    }
        
}
