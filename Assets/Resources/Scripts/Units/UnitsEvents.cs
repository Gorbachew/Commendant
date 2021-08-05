using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class UnitsEvents : MonoBehaviour
{
    public struct SWalk
    {
        public NavMeshAgent navMeshAgent;
        public Transform model, from, target;
        public Vector3 coordinates;
        public string anim;
        public Animator animator;
        public UnitState unitState;
        public IUnit iunit;
    }

    public struct SExtract
    {
        public Transform target;
        public Animator animator;
        public string anim;
        public float time;
        public UnitState unitState;
        public int itemId, itemCount, spMinus;
        public BuildingState buildingState;
        public IUnit iunit;
    }

    public struct SGive
    {
        public Transform target;
        public Animator animator;
        public string anim;
        public float time;
        public UnitState unitState;
        public BuildingState buildingState;
        public Stock stock;
        public IUnit iunit;
    }

    public struct SSit
    {
        public NavMeshAgent navMeshAgent;
        public Transform target;
        public Animator animator;
        public string anim;
        public Collider unitCollider;
        public float time;
        public RestBuilding restBuilding;
        public BuildingState buildingState;
        public UnitState unitState;
        public IUnit iunit;
    }

    public struct SWait
    {
        public NavMeshAgent navMeshAgent;
        public UnitState unitState;
        public Animator animator;
        public string anim;
        public float time;
        public IUnit iunit;
    }

    public static IEnumerator Walk(SWalk state)
    {
        if (state.navMeshAgent.enabled)
        {
            float dist = GlobalConstants.maxFindDistance;
            state.model.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            state.animator.SetBool(state.anim, true);
            state.navMeshAgent.isStopped = false;
            while (dist >= GlobalConstants.stopDistance)
            {
                if (state.target)
                {
                    state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateGone) + " " + state.target.parent.name;
                    dist = Vector3.Distance(state.from.transform.position, state.target.position);
                    state.navMeshAgent.SetDestination(state.target.position);
                }
                else
                {
                    state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateWalking);
                    dist = Vector3.Distance(state.from.transform.position, state.coordinates);
                    state.navMeshAgent.SetDestination(state.coordinates);
                }

                yield return null;
            }
            state.navMeshAgent.isStopped = true;
            state.animator.SetBool(state.anim, false);
            state.iunit.CalculateLogic();
        }
    }

    public static IEnumerator Extract(SExtract state)
    {
        state.unitState.transform.LookAt(state.target);
        state.animator.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        state.animator.SetBool(state.anim, true);
        state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateExtract);
        state.buildingState.isBusy = true;
        yield return new WaitForSeconds(state.time);
        state.buildingState.isBusy = false;
        state.animator.SetBool(state.anim, false);
        for (int i = 0; i < state.itemCount; i++)
        {
            state.unitState.items.Add(state.itemId);
        }
        state.unitState.sp -= state.spMinus;
        state.iunit.CalculateLogic();
    }


    public static IEnumerator Give(SGive state)
    {
        state.unitState.transform.LookAt(state.target);
        state.animator.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        state.animator.SetBool(state.anim, true);
        state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateGive);
        state.buildingState.isBusy = true;
        yield return new WaitForSeconds(state.time);
        state.buildingState.isBusy = false;
        state.animator.SetBool(state.anim, false);

        if (state.unitState.items.Count > 0)
        {
            state.stock.AddItems(state.unitState.items[0], state.unitState.items.Count);
            state.unitState.items.Clear();
        }
        
        state.iunit.CalculateLogic();
    }


    public static IEnumerator Sit(SSit state)
    {
        if (state.buildingState.isBusy)
        {
            state.iunit.CalculateLogic();

        } else
        {
            Transform obj = state.unitState.transform;
            state.navMeshAgent.enabled = false;
            Transform lookTarget = state.target.parent.Find("Target");
            obj.position = state.target.parent.position;
            state.unitState.transform.LookAt(lookTarget);
            state.animator.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            state.unitCollider.isTrigger = true;
            state.animator.SetBool(state.anim, true);
            state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateSit);
            state.restBuilding.BusyBuilding(state.iunit);
            state.buildingState.isBusy = true;
            yield return new WaitForSeconds(state.time);
            state.buildingState.isBusy = false;
            state.unitCollider.isTrigger = false;
            state.navMeshAgent.enabled = true;
            state.animator.SetBool(state.anim, false);
            state.iunit.CalculateLogic();
        }
    }


    public static IEnumerator Wait(SWait state)
    {
        state.animator.SetBool(state.anim, true);
        state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateWait);
        yield return new WaitForSeconds(state.time);
        state.animator.SetBool(state.anim, false);
        state.iunit.CalculateLogic();
    }

    public struct SFindNearestRestBuilding
    {
        public RestBuilding[] restBuildings;
        public Transform transform;
    }

    public struct SFindNearestDrovnitsa
    {
        public RestBuilding[] restBuildings;
        public Transform transform;
        public Stock[] stocks;
        public Woodcutter woodcutter;
    }

    public struct SFindNearestTree
    {
        public TreeEvents[] trees;
        public Transform transform;
    }

    public struct SFindNearestStoneStock
    {
        public RestBuilding[] restBuildings;
        public Transform transform;
        public Stock[] stocks;
        public Miner miner;
    }

    public struct SFindNearestStone
    {
        public StoneEvents[] stones;
        public Transform transform;
    }

    public static Transform FindNearestRestBuilding(SFindNearestRestBuilding state)
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (RestBuilding d in state.restBuildings)
        {
            float newDist = Vector3.Distance(state.transform.position, d.transform.position);
            BuildingState bs = d.GetComponent<BuildingState>();
            List<int> items = bs.items;
            if (dist > newDist)
            {
                dist = newDist;
                return d.transform.Find("Model");
            }
        }
        return state.transform;
    }

    public static Transform FindNearestDrovnitsa(SFindNearestDrovnitsa state)
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (Stock d in state.stocks)
        {
            float newDist = Vector3.Distance(state.transform.position, d.transform.position);
            List<int> items = d.GetComponent<BuildingState>().items;
            if (dist > newDist && items.Count < GlobalConstants.drownitsaMaxItems)
            {
                dist = newDist;
                return d.transform.Find("Model");
            }
        }
        state.woodcutter._noStocks = true;
        return FindNearestRestBuilding(new SFindNearestRestBuilding()
        {
            transform = state.transform,
            restBuildings = state.restBuildings,
        });
    }

    public static Transform FindNearestTree(SFindNearestTree state)
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (TreeEvents t in state.trees)
        {
            float newDist = Vector3.Distance(state.transform.position, t.transform.position);
            if (dist > newDist)
            {
                dist = newDist;
                return t.transform.Find("Model");
            }
        }
        return state.transform;
    }

    public static RestBuilding[] FindRestBuilding()
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
        return rbList.ToArray();
    }

    public static Transform FindNearestStoneStock(SFindNearestStoneStock state)
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (Stock d in state.stocks)
        {
            float newDist = Vector3.Distance(state.transform.position, d.transform.position);
            List<int> items = d.GetComponent<BuildingState>().items;
            if (dist > newDist && items.Count < GlobalConstants.drownitsaMaxItems)
            {
                dist = newDist;
                return d.transform.Find("Model");
            }
        }
        state.miner._noStocks = true;
        return FindNearestRestBuilding(new SFindNearestRestBuilding()
        {
            transform = state.transform,
            restBuildings = state.restBuildings,
        });
    }

    public static Transform FindNearestStone(SFindNearestStone state)
    {
        float dist = GlobalConstants.maxFindDistance;
        foreach (StoneEvents item in state.stones)
        {
            float newDist = Vector3.Distance(state.transform.position, item.transform.position);
            if (dist > newDist)
            {
                dist = newDist;
                return item.transform.Find("Model");
            }
        }
        return state.transform;
    }


    public static Vector3 FindRandCoordinates()
    {
        return new Vector3(
            Random.Range(0, 10),
            0,
            Random.Range(0, 10)
            );
    }

}
