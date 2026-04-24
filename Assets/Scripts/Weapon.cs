using UnityEngine;

public class Weapon : MonoBehaviour
{   
    // [System.Serializable]
    public float damage;
    public int bullets;
    public int currentBullets;
    public GameObject owner;
    Ray ray;
    RaycastHit hit; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Rigidbody r;
    void Start()
    {   

        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // r = GetComponent<Rigidbody>(); 
        // r.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        // r.freezeRotation = true; 
        // r.useGravity = false; 
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(Physics.Raycast(ray, out hit, 50f)){ 
            if(hit.collider.TryGetComponent(out IDamageable objDamageable)) { 
                DamageInfo info = new DamageInfo(damage, hit.point, ray.origin, owner);
                objDamageable.TakeDamage(info);
            }
            
        }
    }
}
