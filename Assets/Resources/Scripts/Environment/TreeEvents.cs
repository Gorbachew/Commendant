using UnityEngine;

public class TreeEvents : MonoBehaviour, IBuilding
{

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Damage(IUnit damager, int count)
    {
        _animator.SetTrigger("Damage");
    }

    public void Build(int count)
    {

    }

    public void Destroy()
    {

    }

    public void NextDay()
    {

    }

    public void Using(IUnit unit)
    {

    }
}
