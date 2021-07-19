using UnityEngine;

public class UnitState : MonoBehaviour
{
    public GameObject obj;
    public int id;
    public string type;
    public int hp;
    public int level;

    private void Awake()
    {
        obj = transform.Find("Object").gameObject;
    }
}
