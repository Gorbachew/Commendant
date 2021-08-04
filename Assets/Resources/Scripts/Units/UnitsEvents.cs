using System.Collections;
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
        public IUnit iunit;
    }

    public struct SGive
    {
        public Transform target;
        public Animator animator;
        public string anim;
        public float time;
        public UnitState unitState;
        public IBuilding ibuilding;
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
        yield return new WaitForSeconds(state.time);
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
        yield return new WaitForSeconds(state.time);
        state.animator.SetBool(state.anim, false);

        if (state.unitState.items.Count > 0)
        {
            state.ibuilding.AddItems(state.unitState.items[0], state.unitState.items.Count);
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

}
