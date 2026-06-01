using UnityEngine;

public class ObjectBroken : MonoBehaviour
{
      private Health _health;

      private void Awake() => _health = GetComponent<Health>();

      private void OnEnable() => _health.OnDeath += HandleBreak;

      private void OnDisable() => _health.OnDeath -= HandleBreak;

      public void HandleBreak() {
          ObjectPooling.instance.Return(ObjectType.Obstacle, gameObject);
      }
}
