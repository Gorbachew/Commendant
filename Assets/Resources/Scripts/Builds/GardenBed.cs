using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GardenBed : MonoBehaviour
{
    private BuildingState _buildingState;
    private Transform _hayField;
    private Image _endingImage;

    private void Awake()
    {
        _hayField = transform.Find("Object/HayField");
        _buildingState = GetComponent<BuildingState>();
        _endingImage = transform.Find("Canvas/EndingImage").GetComponent<Image>();
        _endingImage.enabled = false;
    }

    public void UsingBuilding()
    {
        if (!_buildingState.isWork)
        {
            StartWorking();
        }
    }

    private void StartWorking()
    {
        _buildingState.isWork = true;
        StartCoroutine(Working());
    }

    private void EndWorking()
    {
        _endingImage.enabled = true;
    }


    IEnumerator Working()
    {
        int counter = GlobalConstants.gardenBedSpeed;
        while (counter > 0)
        {
            _hayField.localPosition = Vector3.Lerp(
                _hayField.localPosition,
                new Vector3(0, _hayField.localPosition.y + 0.3f, 0),
                1);

            yield return new WaitForSeconds(1);
            counter--;
        }
        EndWorking();
    }
}
