using UnityEngine;

public class UnitState : MonoBehaviour
{
    public GameObject model;
    public int id;
    public string type;
    public int hp;
    public int level;

    private void Awake()
    {
        model = transform.Find("Model").gameObject;
    }
}
