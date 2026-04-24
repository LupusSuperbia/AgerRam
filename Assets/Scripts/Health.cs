using UnityEngine;

public class Heath : MonoBehaviour
{   
    public float maxHealth = 100f; 
    public float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Die(){ 
        Debug.Log("Se murióoo");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
