using System;
using UnityEngine;

public class BuildingState : MonoBehaviour
{
    public GameObject obj;
    public string type;

    public bool isBuild;
    public bool isWork;

    public int id;
    public int hp;
    public int level;
    public int items;


    private void Awake()
    {
        obj = transform.Find("Object").gameObject;
    }
}
