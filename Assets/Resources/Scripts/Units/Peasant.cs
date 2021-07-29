using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Peasant : MonoBehaviour
{

    private Transform _buildsFolder;
    private GardenBed[] _gardenBeds;
    private Mill[] _mills;
    private NavMeshAgent _navMeshAgent;
    private Transform _obj;

    private Transform target;

    private void Awake()
    {
        _buildsFolder = GameObject.Find("/Buildings").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _obj = transform.Find("Object").transform;
    }

    private void Start()
    {
        FindGardenBeds();
        FindMills();
        target = _gardenBeds[0].transform;
        _navMeshAgent.SetDestination(target.position);
    }

    private void FixedUpdate()
    {
        if (target)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist <= 2.5f)
            {
                Using();
            } else
            {
                Moving();
            } 
        }
    }

    private void Using()
    {
        if (target.GetComponent(typeof(Building)))
        {
            //target.GetComponent<Building>().UsingBuilding();
        }
    }

    private void Moving()
    {
        if (Vector3.Distance(transform.position, target.position) <= 2.5f )
        {
            _navMeshAgent.isStopped = true;
        }
    }

    private void FindGardenBeds()
    {
        _gardenBeds = _buildsFolder.GetComponentsInChildren<GardenBed>();
    }

    private void FindMills()
    {
        _mills = _buildsFolder.GetComponentsInChildren<Mill>();
    }
}
