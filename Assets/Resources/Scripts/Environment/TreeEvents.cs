using UnityEngine;

public class TreeEvents : MonoBehaviour, IBuilding
{

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void AddItems(int id, int count){}
    

    public void RenderItems(){}
    

    public void Damage(IUnit damager, int count)
    {
        Debug.Log(count);
        _animator.SetTrigger("Damage");
    }
}
