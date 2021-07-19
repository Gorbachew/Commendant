using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    Transform player;

    private void Start()
    {
        transform.SetParent(player);
        transform.localPosition = new Vector3(0, 10, -3);
        transform.LookAt(player);
    }
}
