using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{

    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private RectTransform _btnsUnitsTypes;
    [SerializeField] private ButtonsState _buttonsState;

    void FixedUpdate()
    {
       
        if (Application.platform == RuntimePlatform.Android && Input.touchCount > 0)
        {
            CheckTouch(Input.GetTouch(0).position);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            CheckTouch(Input.mousePosition);
        }
        
    }


    private void CheckTouch(Vector3 pos)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (_buildingsGrid._flyingBuilding != null)
                {
                    _buildingsGrid.ClickPlacingBuilding();
                } else
                {
                    if (hit.collider.GetComponent(typeof(Building)))
                    {
                        hit.collider.GetComponent<Building>().AddItems(GlobalConstants.woodId, 1);
                    }
                    else if (hit.collider.GetComponent(typeof(Unit)))
                    {
                        Unit unit = hit.collider.GetComponent<Unit>();
                        _buttonsState.OpenCategory(_btnsUnitsTypes, unit);
                    }
                }
            }
        }
    }

 



}
