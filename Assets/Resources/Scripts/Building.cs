using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer[] MainRenderers;
    private Material[] OriginalRenderers;
    private Material _green, _red;
    private BuildingState _buildingState;
    public Vector2Int Size = Vector2Int.one;

    private void Awake()
    {
        _buildingState = GetComponent<BuildingState>();
        OriginalRenderers = new Material[MainRenderers.Length];
        for (int i = 0; i < MainRenderers.Length; i++)
        {
            OriginalRenderers[i] = MainRenderers[i].material;
        }
        _green = Resources.Load("Materials/Green") as Material;
        _red = Resources.Load("Materials/Red") as Material;
    }

    public void SetTransparent(bool available)
    {
        if (available)
        {
            foreach ( Renderer rend in MainRenderers )
                rend.material = _green;
        }
        else
        {
            foreach (Renderer rend in MainRenderers)
                rend.material = _red;
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
}
