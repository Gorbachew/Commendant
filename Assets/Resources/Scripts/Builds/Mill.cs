using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mill : MonoBehaviour
{
    private Animator _animator;
    private BuildingState _buildingState;
    private Image _endingImage;

    private void Awake()
    {
        _animator = transform.GetComponentInChildren<Animator>();
        _buildingState = GetComponent<BuildingState>();
        _endingImage = transform.Find("Canvas/EndingImage").GetComponent<Image>();
        _endingImage.enabled = false;
    }

    private void Start()
    {

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
        _animator.SetBool("isWork", true);
        _buildingState.isWork = true;
        StartCoroutine(Working());
    }

    private void EndWorking()
    {
        _animator.SetBool("isWork", false);
        _endingImage.enabled = true;
    }

    IEnumerator Working()
    {
        int counter = GlobalConstants.millSpeed;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        EndWorking();
    }

}
