using UnityEngine;

public class StoneEvents : MonoBehaviour, IBuilding
{
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void Damage(IUnit damager, int count)
    {
        _particleSystem.Play();
    }

    public void Build(int count)
    {
        
    }
}
