using System.Collections.Generic;
using UnityEngine;

public class BuildingState : MonoBehaviour
{
    public GameObject model;
    public string resources;

    public bool isBuild;
    public bool isWork;
    public bool isBusy;

    public int id;
    public int hp;
    public int level;

    public List<int> items = new List<int>();

    private void Awake()
    {
        model = transform.Find("Model").gameObject;
    }
}
