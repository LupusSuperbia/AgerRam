using UnityEngine;

public class PoolDamageable : MonoBehaviour, IDamageable
{   
    public void TakeDamage(DamageInfo info) {
        Debug.Log($"Daño: {info.amount} | Impacto: {info.hitPoint} | Dirección : {info.hitDirection} | Atacante: {info.attacker.name}");
    }
}
