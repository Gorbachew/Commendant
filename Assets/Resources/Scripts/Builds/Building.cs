using System.Collections;
using System.Linq;
using UnityEngine;
using static MainData;

public class Building : MonoBehaviour
{
    public Renderer[] MainRenderers;
    private Material[] OriginalRenderers;
    private BuildingState _buildingState;
    private IBuilding _ibuilding;
    public Vector2Int Size = Vector2Int.one;

    private void Awake()
    {
        _buildingState = GetComponent<BuildingState>();
        _ibuilding = GetComponent<IBuilding>();
        OriginalRenderers = new Material[MainRenderers.Length];
        for (int i = 0; i < MainRenderers.Length; i++)
        {
            OriginalRenderers[i] = MainRenderers[i].material;
        }
    }

    private void Start()
    {
        if (_ibuilding != null)
        {
            _ibuilding.ChangeItems();
        }
    }

    public void SetNormal()
    {
        for (int i = 0; i < MainRenderers.Length; i++)
        {
            MainRenderers[i].material = OriginalRenderers[i];
        }
        StartCoroutine(BuildDelay());
    }

    public void Enrichment(SBuild build)
    {
        _buildingState.id = build._id;
        _buildingState.hp = build._hp;
        _buildingState.level = build._level;
        _buildingState.items = build._items.ToList<int>();

        _buildingState.isBuild = build._isBouild;
        _buildingState.isWork = build._isWork;

        _buildingState.transform.position = new Vector3(
                build._pos._x,
                build._pos._y,
                build._pos._z
            );
        Transform obj = _buildingState.transform.Find("Model");
        obj.transform.localPosition = new Vector3(
                build._posObj._x,
                build._posObj._y,
                build._posObj._z
            );

        obj.transform.localRotation = Quaternion.Euler(
                build._rotObj._x,
                build._rotObj._y,
                build._rotObj._z
            );
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }

    private IEnumerator BuildDelay()
    {
        yield return new WaitForSeconds(1);
        _buildingState.isBuild = true;
    }

    public void AddItems(int id, int count)
    {
        _ibuilding.AddItems(id, count);
    }
}
