using System.Collections;
using System.Linq;
using UnityEngine;
using static MainData;

public class Building : MonoBehaviour
{
    public Renderer[] MainRenderers;
    private BuildingState _buildingState;
    public Vector2Int Size = Vector2Int.one;


    private void Awake()
    {
        _buildingState = GetComponent<BuildingState>();
    }

    public void SetNormal()
    {
        StartCoroutine(BuildDelay());
    }

    public void Enrichment(SBuild build)
    {
        _buildingState.id = build.id;
        _buildingState.hp = build.hp;
        _buildingState.level = build.level;
        _buildingState.items = build.items.ToList<int>();

        _buildingState.isBuild = build.isBouild;
        _buildingState.isWork = build.isWork;

        _buildingState.transform.position = new Vector3(
                build.pos.x,
                build.pos.y,
                build.pos.z
            );
        _buildingState.transform.rotation = Quaternion.Euler(
                build.rot.x,
                build.rot.y,
                build.rot.z
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

    //public void AddItems(int id, int count)
    //{
    //    _ibuilding.AddItems(id, count);
    //}
}
