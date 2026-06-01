using UnityEngine;

[RequireComponent(typeof(Health))]
public class PoolDamageable : MonoBehaviour, IDamageable
{
    public Health _health;

    private void Awake() =>  _health = GetComponent<Health>();


    public void TakeDamage(DamageInfo info)
    {
        _health.TakeDamage(info.amount);
    }




}
