using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsGrid : MonoBehaviour
{

    public Transform _buildingsFolder;
    public Building _flyingBuilding;

    [SerializeField] private Button[] _btnsControl = new Button[3];
    private Vector2Int _gridSize = new Vector2Int(GlobalConstants.placeSize, GlobalConstants.placeSize);
    private GameObject[,] _gridPlanes = new GameObject[GlobalConstants.placeSize, GlobalConstants.placeSize];
    private int _maxId = 0;
    private Building[,] _grid;
    private Camera _mainCamera;
    private Material _green, _red;

    private void Awake()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];
        _mainCamera = Camera.main;
        _green = Resources.Load("Materials/Green") as Material;
        _red = Resources.Load("Materials/Red") as Material;

        _btnsControl[0].onClick.AddListener(PlaceFlyingBuilding);
        _btnsControl[1].onClick.AddListener(RotateFlyingBuilding);
        _btnsControl[2].onClick.AddListener(CancelFlyingBuilding);

        ChangeBtnsControll(false);
    }

    private void Start()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Transform parent = GameObject.Find("Grid").transform;
                GameObject plane = Instantiate(Resources.Load("Prefabs/GridPlane"), new Vector3(x, parent.position.y, y), Quaternion.identity, parent) as GameObject;
                _gridPlanes[x, y] = plane;
            }
        }
        StartCoroutine(FindEmployedCells(false));
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }
 
        _flyingBuilding = Instantiate(buildingPrefab, new Vector3(-10, -10, -10), Quaternion.identity, _buildingsFolder);

        ChangeBtnsControll(true);
        StartCoroutine(FindEmployedCells(true));
    }

    public void ClickPlacingBuilding()
    {
        if (_flyingBuilding != null)
        {
            if (Application.platform == RuntimePlatform.Android && Input.touchCount > 0)
            {
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = _mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if (groundPlane.Raycast(ray, out float position))
                {
                    FlyingBuilding(ray, position);
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (groundPlane.Raycast(ray, out float position))
                {
                    FlyingBuilding(ray, position);
                }
            }
        }
    }

    public void CancelFlyingBuilding()
    {
        StartCoroutine(FindEmployedCells(false));
        ChangeBtnsControll(false);
        Destroy(_flyingBuilding.gameObject);
        _flyingBuilding = null;
    }

    private void FlyingBuilding(Ray ray, float position)
    {
        Vector3 worldPosition = ray.GetPoint(position);

        int x = Mathf.RoundToInt(worldPosition.x);
        int y = Mathf.RoundToInt(worldPosition.z);
        bool available = true;

        if (x < 0 || x > _gridSize.x - _flyingBuilding.Size.x) available = false;
        if (y < 0 || y > _gridSize.y - _flyingBuilding.Size.y) available = false;

        if (available && IsPlaceTaken(x, y)) available = false;

        if (available)
        {
            _flyingBuilding.transform.position = new Vector3(x, 0, y);
            _flyingBuilding.SetTransparent(available);
            StartCoroutine(FindEmployedCells(true));
        }
        
    }


    private void PlaceFlyingBuilding()
    {
        StartCoroutine(FindEmployedCells(false));
        ChangeBtnsControll(false);

        _flyingBuilding.SetNormal();
        _maxId++;
        _flyingBuilding.GetComponent<BuildingState>().id = _maxId;
        _flyingBuilding = null;
    }

    private void RotateFlyingBuilding()
    {
        _flyingBuilding.transform.position = new Vector3(-10, -10, -10);
        _flyingBuilding.Size = new Vector2Int(_flyingBuilding.Size.y, _flyingBuilding.Size.x);
        Transform obj = _flyingBuilding.transform.Find("Object");
        obj.Rotate(0, 90, 0);
        obj.localPosition = new Vector3(obj.localPosition.z, obj.localPosition.y, obj.localPosition.x);
        StartCoroutine(FindEmployedCells(true));
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.Size.y; y++)
            {
                if (_grid[placeX + x, placeY + y] != null) return true;
            }
        }

        return false;
    }

    IEnumerator FindEmployedCells(bool state)
    {
        yield return new WaitForSeconds(0.1f);

        foreach (GameObject plane in _gridPlanes)
        {
            plane.GetComponent<MeshRenderer>().material = _green;
            plane.SetActive(state);
        }

        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                _grid[x, y] = null;
            }
        }

        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                if (Physics.Raycast(new Vector3Int(x, -5, y), new Vector3(0, 10, 0), out RaycastHit hit))
                {
                    Building building = hit.collider.GetComponentInParent<Building>(); 
                    if (building)
                    {
                        _grid[x, y] = hit.collider.GetComponentInParent<Building>();
                        _gridPlanes[x, y].GetComponent<MeshRenderer>().material = _red;
                    }
                }
            }
        }

        
    }

    private void ChangeBtnsControll(bool state)
    {
        foreach (Button btn in _btnsControl)
        {
            btn.gameObject.SetActive(state);
        }
    }
}
