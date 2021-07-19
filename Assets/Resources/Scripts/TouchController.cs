using UnityEngine;

public class TouchController : MonoBehaviour
{
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
            if (hit.collider.GetComponent(typeof(IBuilding)))
            {
                hit.collider.GetComponent<IBuilding>().UsingBuilding();
            }
        }
    }




    
}
