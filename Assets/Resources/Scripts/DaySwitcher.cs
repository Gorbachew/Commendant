using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DaySwitcher : MonoBehaviour
{
    [SerializeField] private Transform _sun;
    [SerializeField] private Text _dayText;

    [SerializeField] private IBuilding[] _ibuildings;
    private bool _isRotate;


    private void Start()
    {
        SetUI();
    }

    public void Switch()
    {
        if (!_isRotate)
        {
            if (_sun.eulerAngles.x > 100)
            {
                _sun.rotation = Quaternion.Euler(0, -30, 0);
                StartCoroutine(RotateSun(Quaternion.Euler(100, -30, 0)));
                NextDay();
            }
            else
            {
                StartCoroutine(RotateSun(Quaternion.Euler(200, -30, 0)));
                NextNight();
            }
        }
    }

    private void NextNight()
    {
        GlobalState.isNight = true;
    }


    private void NextDay()
    {
        GlobalState.isNight = false;
        GlobalState.day++;
        _ibuildings = GameObject.Find("Buildings").GetComponentsInChildren<IBuilding>();

        foreach (IBuilding item in _ibuildings)
        {
            item.NextDay();
        }
        SetUI(); 
    }

    private void SetUI()
    {
        _dayText.text = Texts.get(GlobalState.language, GlobalConstants.textDay) + ": " + GlobalState.day;

    }


    IEnumerator RotateSun(Quaternion rot)
    {
        _isRotate = true;
        while (_sun.rotation != rot)
        {
            _sun.rotation = Quaternion.RotateTowards
                (
                    _sun.rotation,
                    rot,
                    0.5f
                );
            yield return null;
        }
        _isRotate = false;
    }
}
