using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnitsActions;

public class UnitLogic : MonoBehaviour
{
    public struct SUnitAction
    {
        public IUnit iunit;
        public UnitState unitState;
        public Transform model;
        public Animator animator;
        public NavMeshAgent navMeshAgent;
    }
    public static IEnumerator Citizen(SUnitAction state)
    {

        while (state.unitState.hp > 0)
        {
            yield return new WaitForSeconds(1);
            state.unitState.state = "";
            if (state.unitState.state == "") yield return Build(state);
        }
        yield return null;
    }

    public static IEnumerator Peasant(SUnitAction state)
    {

        while (state.unitState.hp > 0)
        {
            yield return new WaitForSeconds(1);
            state.unitState.state = "";
            if (state.unitState.state == "") yield return SowHarvest(state);
            if (state.unitState.state == "") yield return Harvest(state);
            if (state.unitState.state == "") yield return MakeBread(state);
        }
        yield return null;
    }

    public static IEnumerator Woodcutter(SUnitAction state)
    {
        while (state.unitState.hp > 0)
        {
            yield return new WaitForSeconds(1);
            state.unitState.state = "";
            if (state.unitState.state == "") yield return Woodcut(state);
        }
        yield return null;
    }

    public static IEnumerator Miner(SUnitAction state)
    {

        while (state.unitState.hp > 0)
        {
            yield return new WaitForSeconds(1);
            state.unitState.state = "";
            //if (state.unitState.state == "") yield return Build(state);
        }
        yield return null;
    }
}
