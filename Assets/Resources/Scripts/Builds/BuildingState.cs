using System.Collections.Generic;
using UnityEngine;

public class BuildingState : MonoBehaviour
{
    public GameObject model;

    public string nameTech;
    public string nameGame;
    public string resources;
    
    public bool isBuild;
    public bool isWork;
    public bool isBusy;
    public bool isReady;

    public bool isProdStart;
    public bool isProdOver;

    public int id;
    public int maxHp, hp;
    public int level;
    public int progress, maxProgress;

    public List<int> items = new List<int>();

    private void Awake()
    {
        model = transform.Find("Model").gameObject;
    }
}
