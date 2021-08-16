using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static IBuilding;

public class UnitsActions : MonoBehaviour
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
        public Transform unit, target;
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

    //public struct SBuild
    //{
    //    public Transform target;
    //    public Animator animator;
    //    public string anim;
    //    public float time;
    //    public UnitState unitState;
    //    public BuildingState buildingState;
    //    public Building building;
    //    public IUnit iunit;
    //}

    public static IEnumerator Walk(SWalk state)
    {
        if (state.navMeshAgent.enabled)
        {
            float dist = GlobalConstants.maxFindDistance;
            state.model.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            state.animator.SetBool(state.anim, true);
            state.navMeshAgent.isStopped = false;
            Vector3 oldPos = Vector3.zero;
            while (dist >= GlobalConstants.stopDistance)
            {
                yield return new WaitForSeconds(0.5f);
                if (state.target)
                {
                    if (oldPos != state.target.position)
                    {

                        oldPos = state.target.position;
                        state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateGone)
                        + " " +
                        state.target.GetComponentInParent<BuildingState>().nameGame;

                        
                        state.navMeshAgent.SetDestination(state.target.position);
                    }
                    dist = Vector3.Distance(state.from.transform.position, state.target.position);
                }
                else
                {
                    state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateWalking);
                    dist = Vector3.Distance(state.from.transform.position, state.coordinates);
                    state.navMeshAgent.SetDestination(state.coordinates);
                } 

            }
            state.navMeshAgent.isStopped = true;
            state.animator.SetBool(state.anim, false);
            state.iunit.CalculateLogic();
        }
    }

    public static IEnumerator Extract(SExtract state)
    {
        float time = state.time;
        state.unitState.transform.LookAt(state.target);
        state.animator.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        state.animator.SetBool(state.anim, true);
        state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateExtract);
        state.buildingState.isBusy = true;

        while (time > 0)
        {
            float dist = Vector3.Distance(state.unit.position, state.target.position);
            if (dist < GlobalConstants.stopDistance)
            {
                time--;
                yield return new WaitForSeconds(1);
                if (time <= 0)
                {
                    for (int i = 0; i < state.itemCount; i++)
                    {
                        state.unitState.items.Add(state.itemId);
                    }
                    state.unitState.sp -= state.spMinus;
                    state.buildingState.isBusy = false;
                    state.iunit.CalculateLogic();
                }
            }
            else
            {
                state.iunit.CalculateLogic();
                yield return null;
            }
        }

        
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

    //public static IEnumerator Build(SBuild state)
    //{
    //    state.unitState.transform.LookAt(state.target);
    //    state.animator.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    //    state.animator.SetBool(state.anim, true);
    //    state.unitState.state = Texts.get(GlobalState.language, GlobalConstants.textStateBuilds);
    //    yield return new WaitForSeconds(state.time);
    //    state.unitState.sp -= GlobalConstants.buildSpm;
    //    state.animator.SetBool(state.anim, false);
    //    state.building.Build(state.unitState.building);
    //    state.iunit.CalculateLogic();
    //}

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

    public struct SFindNearestNotReadyBuilding
    {
        public BuildingState[] buildingState;
        public Transform transform;
    }

    public static Transform FindNearestRestBuilding(SFindNearestRestBuilding state)
    {
        float dist = GlobalConstants.maxFindDistance;
        Transform nearestObj = null;
        foreach (RestBuilding item in state.restBuildings)
        {
            float newDist = Vector3.Distance(state.transform.position, item.transform.position);
            BuildingState bs = item.GetComponent<BuildingState>();
            List<int> items = bs.items;
            if (dist > newDist)
            {
                dist = newDist;
                nearestObj = item.transform;
            }
        }
        return nearestObj.Find("Model");
    }

    public static Transform FindNearestDrovnitsa(SFindNearestDrovnitsa state)
    {
        float dist = GlobalConstants.maxFindDistance;
        Transform nearestObj = null;
        foreach (Stock item in state.stocks)
        {
            float newDist = Vector3.Distance(state.transform.position, item.transform.position);
            List<int> items = item.GetComponent<BuildingState>().items;
            if (dist > newDist && items.Count < GlobalConstants.drownitsaMaxItems)
            {
                dist = newDist;
                nearestObj = item.transform;
            }
        }

        if (nearestObj == null)
        {
            state.woodcutter._noStocks = true;
            return FindNearestRestBuilding(new SFindNearestRestBuilding()
            {
                transform = state.transform,
                restBuildings = state.restBuildings,
            });
        }
        
        return nearestObj.Find("Model");
    }

    public static Transform FindNearestTree(SFindNearestTree state)
    {
        float dist = GlobalConstants.maxFindDistance;
        Transform nearestObj = null;
        foreach (TreeEvents item in state.trees)
        {
            float newDist = Vector3.Distance(state.transform.position, item.transform.position);
            if (dist > newDist)
            {
                dist = newDist;
                nearestObj = item.transform;
            }
        }
        return nearestObj.Find("Model");
    }

    public static RestBuilding[] FindRestBuilding()
    {
        RestBuilding[] rbArray = GameObject.Find("Buildings").GetComponentsInChildren<RestBuilding>();
        List<RestBuilding> rbList = new List<RestBuilding>();
        foreach (RestBuilding item in rbArray)
        {
            BuildingState bs = item.GetComponentInParent<BuildingState>();
            if (bs.isReady && !bs.isBusy)
            {
                rbList.Add(item);
            }
        }
        return rbList.ToArray();
    }

    public static Transform FindNearestStoneStock(SFindNearestStoneStock state)
    {
        float dist = GlobalConstants.maxFindDistance;
        Transform nearestObj = null;
        foreach (Stock item in state.stocks)
        {
            float newDist = Vector3.Distance(state.transform.position, item.transform.position);
            List<int> items = item.GetComponent<BuildingState>().items;
            if (dist > newDist && items.Count < GlobalConstants.drownitsaMaxItems)
            {
                dist = newDist;
                nearestObj = item.transform;
            }
        }

        if (nearestObj == null)
        {
            state.miner._noStocks = true;
            return FindNearestRestBuilding(new SFindNearestRestBuilding()
            {
                transform = state.transform,
                restBuildings = state.restBuildings,
            });
        }

        return nearestObj.Find("Model");
        
    }

    public static Transform FindNearestStone(SFindNearestStone state)
    {
        float dist = GlobalConstants.maxFindDistance;
        Transform nearestObj = null;
        foreach (StoneEvents item in state.stones)
        {
            float newDist = Vector3.Distance(state.transform.position, item.transform.position);
            if (dist > newDist)
            {
                dist = newDist;
                nearestObj = item.transform;
            }
        }
        return nearestObj.Find("Model");
    }

    public static Transform FindNearestNotReadyBuilding(SFindNearestNotReadyBuilding state)
    {
        float dist = GlobalConstants.maxFindDistance;
        Transform nearestObj = null;
        foreach (BuildingState item in state.buildingState)
        {
            float newDist = Vector3.Distance(state.transform.position, item.transform.position);
            if (!item.isReady && dist > newDist)
            {
                dist = newDist;
                nearestObj = item.transform;
            }
        }
        return nearestObj.Find("Model");
    }


    public static Vector3 FindRandCoordinates()
    {
        return new Vector3(
            Random.Range(0, 10),
            0,
            Random.Range(0, 10)
            );
    }

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


    public static IEnumerator Peasant (SUnitAction state)
    {
       
        while (state.unitState.hp > 0)
        {
            yield return new WaitForSeconds(1);
            state.unitState.state = "";
            if (state.unitState.state == "") yield return Harvest(state);
            if (state.unitState.state == "") yield return MakeBread(state);
            if (state.unitState.state == "") yield return SowHarvest(state);
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
        BuildingState[] buildingsBakery = FindBuildings(GlobalConstants.bakery, GlobalConstants.checkProgress, false);
        BuildingState[] buildingsMill = FindBuildings(GlobalConstants.mill, GlobalConstants.checkItems, false);
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
                    checkBuilding = GlobalConstants.checkItems,
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
                    checkBuilding = GlobalConstants.checkProgress,
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
        BuildingState[] buildingsMill = FindBuildings(GlobalConstants.mill, GlobalConstants.checkProgress, false);
        BuildingState[] buildingsGardenBed = FindBuildings(GlobalConstants.gardenBed, GlobalConstants.checkItems, false);
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
                    checkBuilding = GlobalConstants.checkProgress,
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
            yield return WalkNew(new SWalkNew()
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
            if ((item.nameTech == buildingType || all) && CheckStateBuilding(item, checkState))
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


    private static bool CheckStateBuilding(BuildingState buildingState, string action)
    {
        return action switch
        {
            GlobalConstants.checkProdStart => !buildingState.isProdStart,
            GlobalConstants.checkProdOver => buildingState.isProdOver && buildingState.items.Count > 0,
            GlobalConstants.checkProgress => buildingState.progress < buildingState.maxProgress,
            GlobalConstants.checkItems => buildingState.progress > 0,
            GlobalConstants.checkNotReady => !buildingState.isReady,
            _ => true,
        };
    }

    public struct SWalkNew
    {
        public Transform model;
        public Transform target;
        public Animator animator;
        public string animate;
        public NavMeshAgent navMeshAgent;
    }

    private static IEnumerator WalkNew(SWalkNew state)
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
        ibuilding.Using(state.sBuildingUsing);
        yield return new WaitForSeconds(state.time);
        state.sBuildingUsing.start = false;
        SBuildingReturndUsing sEndUsing = ibuilding.Using(state.sBuildingUsing);
        state.unitState.sp -= sEndUsing.spm;
        if (sEndUsing.items != null)
        {
            state.unitState.items = sEndUsing.items;
        }
        state.animator.SetBool(state.animate, false);
    }

}
