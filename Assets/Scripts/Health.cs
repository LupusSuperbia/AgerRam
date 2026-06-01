using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 3f;
    public float _currentHealth { get; private set; }
    public Action OnDeath;

    void OnEnable() {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount) {
        _currentHealth -= amount;

        if(_currentHealth <= 0) {
            OnDeath?.Invoke();
        }
    }

}
