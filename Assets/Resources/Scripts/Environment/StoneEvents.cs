using UnityEngine;

public class StoneEvents : MonoBehaviour, IBuilding
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Damage(IUnit damager, int count)
    {
        //_animator.SetTrigger("Damage");
    }
}
