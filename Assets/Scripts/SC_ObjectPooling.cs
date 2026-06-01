using UnityEngine;

public class SC_ObjectPooling : MonoBehaviour
{
    Rigidbody r;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        r = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        if (r != null)
        {
            r.linearVelocity = Vector3.zero;
            r.angularVelocity = Vector3.zero;

        }
    }
    void Start()
    {
        if (r != null)
        {
            r.useGravity = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
