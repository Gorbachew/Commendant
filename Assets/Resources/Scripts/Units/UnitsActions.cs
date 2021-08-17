using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static IBuilding;
using static UnitLogic;

public class UnitsActions : MonoBehaviour
{
    public static Vector3 FindRandCoordinates()
    {
        return new Vector3(
            Random.Range(0, 10),
            0,
            Random.Range(0, 10)
            );
    }

    public static IEnumerator Woodcut(SUnitAction state)
    {
        BuildingState[] trees = FindBuildings(GlobalConstants.tree, GlobalConstants.checkFullness, false);
        BuildingState[] buildingsDrovtisy = FindBuildings(GlobalConstants.drovnitsa, GlobalConstants.checkFullness, false);
        
        if (trees.Length > 0 && state.unitState.items.Count <= 0 && state.unitState.sp >= GlobalConstants.woodcutSpm)
        {
            state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textWoodcut);
            yield return ComeUpAndDo(new SComeUpAndDo()
            {
                SUnitAction = state,
                animationWalk = "Walk",
                animationUse = "Woodcut",
                spMinus = GlobalConstants.woodcutSpm,
                building = GlobalConstants.tree,
                checkBuilding = GlobalConstants.checkFullness,
                action = GlobalConstants.woodcutAction,
                actionTime = GlobalConstants.woodcutTime,
            });
        } else if (buildingsDrovtisy.Length > 0)
        {
            state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.text—arriesWood);
            if (state.unitState.items.Count <= 0)
            {
                yield return ComeUpAndDo(new SComeUpAndDo()
                {
                    SUnitAction = state,
                    animationWalk = "Walk",
                    animationUse = "Working",
                    spMinus = GlobalConstants.takeWoodSpm,
                    building = GlobalConstants.tree,
                    checkBuilding = GlobalConstants.checkEmpty,
                    action = GlobalConstants.takeWoodAction,
                    actionTime = GlobalConstants.takeWoodTime,
                });
            } else if (state.unitState.items.Count >= 1)
            {
                yield return ComeUpAndDo(new SComeUpAndDo()
                {
                    SUnitAction = state,
                    animationWalk = "Walk",
                    animationUse = "Working",
                    spMinus = GlobalConstants.putWoodSpm,
                    building = GlobalConstants.drovnitsa,
                    checkBuilding = GlobalConstants.checkFullness,
                    action = GlobalConstants.putWoodAction,
                    actionTime = GlobalConstants.putWoodTime,
                });
            }         
        }
        


        yield return null;
    }
    public static IEnumerator Build(SUnitAction state)
    {
        yield return ComeUpAndDo(new SComeUpAndDo()
        {
            SUnitAction = state,
            animationWalk = "Walk",
            animationUse = "Working",
            value = 20,
            spMinus = GlobalConstants.buildSpm,
            building = GlobalConstants.allBuildings,
            checkBuilding = GlobalConstants.checkNotReady,
            action = GlobalConstants.buildAction,
            actionTime = GlobalConstants.buildsTime,
        });
    }
    public static IEnumerator MakeBread(SUnitAction state)
    {
        BuildingState[] buildingsBakery = FindBuildings(GlobalConstants.bakery, GlobalConstants.checkFullness, false);
        BuildingState[] buildingsMill = FindBuildings(GlobalConstants.mill, GlobalConstants.checkEmpty, false);
        if (buildingsBakery.Length > 0 && buildingsMill.Length > 0)
        {
            state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textMakeBread);
            if (state.unitState.items.Count <= 0)
            {
                yield return ComeUpAndDo(new SComeUpAndDo()
                {
                    SUnitAction = state,
                    animationWalk = "Walk",
                    animationUse = "Working",
                    spMinus = 0,
                    building = GlobalConstants.mill,
                    checkBuilding = GlobalConstants.checkEmpty,
                    action = GlobalConstants.takeFlourAction,
                    actionTime = GlobalConstants.takeFlourTime,
                });
            }

            if (state.unitState.items.Count > 0 && state.unitState.items[0] == GlobalConstants.flourId)
            {
                yield return ComeUpAndDo(new SComeUpAndDo()
                {
                    SUnitAction = state,
                    animationWalk = "Walk",
                    animationUse = "Working",
                    spMinus = GlobalConstants.makeBreadSpm,
                    building = GlobalConstants.bakery,
                    checkBuilding = GlobalConstants.checkFullness,
                    action = GlobalConstants.makeBreadAction,
                    actionTime = GlobalConstants.makeBreadTime,
                });
            }

        }
         yield return null;
    }
    public static IEnumerator SowHarvest(SUnitAction state)
    {
        yield return ComeUpAndDo(new SComeUpAndDo()
        {
            SUnitAction = state,
            animationWalk = "Walk",
            animationUse = "PlantSeeds",
            spMinus = GlobalConstants.plantSeedSpm,
            building = GlobalConstants.gardenBed,
            checkBuilding = GlobalConstants.checkProdStart,
            action = GlobalConstants.plantSeedsAction,
            actionTime = GlobalConstants.plantSeedTime,
        });
    }
    public static IEnumerator Harvest(SUnitAction state)
    {
        BuildingState[] buildingsMill = FindBuildings(GlobalConstants.mill, GlobalConstants.checkFullness, false);
        BuildingState[] buildingsGardenBed = FindBuildings(GlobalConstants.gardenBed, GlobalConstants.checkEmpty, false);
        if (buildingsMill.Length > 0 && buildingsGardenBed.Length > 0)
        {
            state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textHarvest);
            if (state.unitState.items.Count <= 0)
            {
                yield return ComeUpAndDo(new SComeUpAndDo()
                {
                    SUnitAction = state,
                    animationWalk = "Walk",
                    animationUse = "Harvesting",
                    spMinus = GlobalConstants.harwestSpm,
                    building = GlobalConstants.gardenBed,
                    checkBuilding = GlobalConstants.checkProdOver,
                    action = GlobalConstants.harwestAction,
                    actionTime = GlobalConstants.harwestTime,
                });
            }

            if (state.unitState.items.Count >= 1 && state.unitState.items[0] == GlobalConstants.hayId)
            {
                yield return ComeUpAndDo(new SComeUpAndDo()
                {
                    SUnitAction = state,
                    animationWalk = "Walk",
                    animationUse = "Working",
                    spMinus = GlobalConstants.makeFlourSpm,
                    building = GlobalConstants.mill,
                    checkBuilding = GlobalConstants.checkFullness,
                    action = GlobalConstants.makeFlourAction,
                    actionTime = GlobalConstants.makeFlourTime,
                });
            }
        } 
       

        yield return null;
    }

    public struct SComeUpAndDo
    {
        public SUnitAction SUnitAction;
        public string building, checkBuilding, action, animationWalk, animationUse;
        public int value, spMinus, actionTime;
    }

    private static IEnumerator ComeUpAndDo(SComeUpAndDo state)
    {

        BuildingState[] buildings = FindBuildings(state.building, state.checkBuilding, state.building == GlobalConstants.allBuildings);
        if (buildings.Length > 0 && state.SUnitAction.unitState.sp >= state.spMinus)
        {
            Transform target = FindNearestBuilding(state.SUnitAction.model, buildings);
            yield return Walk(new SWalk()
            {
                model = state.SUnitAction.model,
                target = target.Find("Model"),
                animator = state.SUnitAction.animator,
                animate = state.animationWalk,
                navMeshAgent = state.SUnitAction.navMeshAgent,
            });
            state.SUnitAction.unitState.items.Clear();
            SBuildingUsing sBuildingUsing = new SBuildingUsing()
            {
                iunit = state.SUnitAction.iunit,
                action = state.action,
                value = state.value,
            };
            yield return Using(new SUsing()
            {
                sBuildingUsing = sBuildingUsing,
                unitState = state.SUnitAction.unitState,
                model = state.SUnitAction.model,
                target = target.Find("Model"),
                animator = state.SUnitAction.animator,
                animate = state.animationUse,
                time = state.actionTime,
            });
        }

        yield return null;
    }

    private static BuildingState[] FindBuildings(string buildingType, string checkState, bool all)
    {
        List<BuildingState> buildings = new List<BuildingState>();
        BuildingState[] allBuilding = GameObject.Find("Buildings").GetComponentsInChildren<BuildingState>();
        foreach (BuildingState item in allBuilding)
        {
            if ((item.name == buildingType || all) && CheckStateBuilding(item, checkState) )
            {
                buildings.Add(item);
            }
        }
        return buildings.ToArray();
    }

    private static Transform FindNearestBuilding(Transform unit, BuildingState[] buildings) 
    {
        if (!unit) return null;
        Transform nearestBuilding = null;
        float dist = GlobalConstants.maxFindDistance;
        foreach (BuildingState item in buildings)
        {
            float newDist = Vector3.Distance(unit.position, item.transform.position);

            if (dist > newDist)
            {
                dist = newDist;
                nearestBuilding = item.transform;
            }
            
        }
        return nearestBuilding;
    }

    public struct SWalk
    {
        public Transform model;
        public Transform target;
        public Animator animator;
        public string animate;
        public NavMeshAgent navMeshAgent;
    }

    private static IEnumerator Walk(SWalk state)
    {
        float dist = GlobalConstants.maxFindDistance;
        Vector3 oldPos = Vector3.zero;
        while (dist >= state.target.GetComponentInParent<BuildingState>().stopDistance)
        {
            yield return new WaitForSeconds(1f);
            if (oldPos != state.model.position)
            {
                oldPos = state.model.position;
                state.animator.SetBool(state.animate, true);
                state.model.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                state.navMeshAgent.isStopped = false;
                state.navMeshAgent.SetDestination(state.target.position);
            }
            dist = Vector3.Distance(state.model.position, state.target.position);
        }
        state.navMeshAgent.isStopped = true;
        state.animator.SetBool(state.animate, false);        
    }
    public struct SUsing
    {
        public SBuildingUsing sBuildingUsing;
        public UnitState unitState;
        public Transform model;
        public Transform target;
        public Animator animator;
        public string animate;
        public float time;
    }

    public static IEnumerator Using(SUsing state)
    {
        IBuilding ibuilding = state.target.GetComponentInParent<IBuilding>();
        state.model.parent.transform.LookAt(state.target);
        state.animator.SetBool(state.animate, true);
        state.sBuildingUsing.start = true;
        SBuildingReturndUsing sReturnUsing = ibuilding.Using(state.sBuildingUsing);
        state.unitState.target = sReturnUsing.building;
        yield return new WaitForSeconds(state.time);
        state.sBuildingUsing.start = false;
        sReturnUsing = ibuilding.Using(state.sBuildingUsing);
        state.unitState.sp -= sReturnUsing.spm;
        if (state.unitState.sp < 0)
        {
            state.unitState.sp = 0;
        }
        if (sReturnUsing.items != null)
        {
            state.unitState.items = sReturnUsing.items;
        }
        state.animator.SetBool(state.animate, false);
    }
    private static bool CheckStateBuilding(BuildingState buildingState, string action)
    {
        return action switch
        {
            GlobalConstants.checkProdStart => !buildingState.isProdStart && !buildingState.isDestroy,
            GlobalConstants.checkProdOver => buildingState.isProdOver && buildingState.items.Count > 0 && !buildingState.isDestroy,
            GlobalConstants.checkFullness => buildingState.progress < buildingState.maxProgress && !buildingState.isDestroy,
            GlobalConstants.checkEmpty => buildingState.progress > 0,
            GlobalConstants.checkNotReady => !buildingState.isReady && !buildingState.isDestroy,
            GlobalConstants.checkNotDied => !buildingState.isDestroy,
            GlobalConstants.checkAll => true,
            _ => true,
        };
    }
}
