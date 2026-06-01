using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
   private Health _health;

   private void Awake() => _health = GetComponent<Health>();

   private void OnEnable() => _health.OnDeath += HandleDeath;

   private void OnDisable() => _health.OnDeath -= HandleDeath;

   public void HandleDeath() {
       ObjectPooling.instance.Return(ObjectType.Enemy, gameObject);
   }
}
