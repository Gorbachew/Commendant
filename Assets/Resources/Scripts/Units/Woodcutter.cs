using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnitsEvents;

public class Woodcutter : MonoBehaviour, IUnit
{
    [SerializeField] private Tree[] _trees;
    [SerializeField] private Drovnitsa[] _drovnitsy;
    [SerializeField] private RestBuilding[] _restBuildings;
    [SerializeField] private Transform _target;

    private UnitState _unitState;
    private NavMeshAgent _navMeshAgent;
    private Transform _parent;
    private Animator _animator;
    private Collider _unitCollider;

    void Start()
    {
        _parent = transform.parent;
        FindTargets();
        _navMeshAgent = _parent.GetComponent<NavMeshAgent>();
        _unitState = _parent.GetComponent<UnitState>();
        _animator = GetComponent<Animator>();
        _unitCollider = _parent.GetComponent<CapsuleCollider>();
        //TODO debug
        _unitState.sp = 0; 

        CalculateLogic();
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.zero;
    }


    public void CalculateLogic()
    {
        FindTargets();
        SetTarget();

        float dist = Vector3.Distance(_parent.transform.position, _target.transform.position);
        
        if (dist >= GlobalConstants.stopDistance)
        {
            StartCoroutine(Walk(new SWalk()
            {
                navMeshAgent = _navMeshAgent,
                model = transform,
                from = _parent,
                to = _target,
                animator = _animator,
                anim = "Walk",
                iunit = this,
            }));
        }
        else
        {
            if (_unitState.sp <= 0 && _unitState.items.Count == 0)
            {
                if (_restBuildings.Length > 0)
                {
                    StartCoroutine(Sit(new SSit()
                    {
                        navMeshAgent = _navMeshAgent,
                        target = _target,
                        animator = _animator,
                        anim = "Sit",
                        unitCollider = _unitCollider,
                        time = GlobalConstants.sitTime,
                        unitState = _unitState,
                        ibuilding = _target.parent.GetComponent<IBuilding>(),
                        iunit = this,
                    }));
                } else
                {
                    StartCoroutine(Wait(new SWait()
                    {
                        animator = _animator,
                        anim = "Wait",
                        time = GlobalConstants.sitTime,
                    }));
                }
                
                return;
            }

            Work();

        } 
    }

    private void Work() 
    {
        float items = _unitState.items.Count;
        if (items <= 0)
        {
            StartCoroutine(Extract(new SExtract()
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
            StartCoroutine(Give(new SGive()
            {
                target = _target,
                animator = _animator,
                anim = "Working",
                time = GlobalConstants.giveTime,
                unitState = _unitState,
                ibuilding = _target.parent.GetComponent<IBuilding>(),
                iunit = this,
            }));
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


    private Transform FindNearestRestBuilding()
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (RestBuilding d in _restBuildings)
        {
            float newDist = Vector3.Distance(_parent.transform.position, d.transform.position);
            List<int> items = d.GetComponent<BuildingState>().items;
            if (dist > newDist)
            {
                dist = newDist;
                return d.transform.Find("Model");
            }
        }
        return _parent.transform;
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
        return _parent.transform;
    }

    private Transform FindNearestTree()
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (Tree t in _trees)
        {
            float newDist = Vector3.Distance(_parent.transform.position, t.transform.position);
            if (dist > newDist)
            {
                dist = newDist;
                return t.transform.Find("Model");
            }
        }
        return _parent.transform;
    }
    private void FindTargets()
    {
        _trees = GameObject.Find("Environment").GetComponentsInChildren<Tree>();
        _drovnitsy = GameObject.Find("Buildings").GetComponentsInChildren<Drovnitsa>();
        _restBuildings = GameObject.Find("Buildings").GetComponentsInChildren<RestBuilding>();
    }
}
