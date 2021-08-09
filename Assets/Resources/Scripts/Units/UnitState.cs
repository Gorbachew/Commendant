using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    public GameObject model;
    public int id;
    public string type;
    public int hp;
    public int sp;
    public int level;
    public int building;
    public int damage;
    public string state;
    public List<int> items = new List<int>();

    private void Awake()
    {
        model = transform.Find("Model").gameObject;
    }
}
