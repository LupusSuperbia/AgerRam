using UnityEngine;

public class SC_Enemy : MonoBehaviour
{
    Rigidbody r;

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
            r.isKinematic = false;
            r.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        //float newPosition = Mathf.Clamp(r.position.x, -12f, 12f);
        //Vector3 finalPosition = new Vector3(newPosition, r.position.y, r.position.z);
        //r.MovePosition(finalPosition);
    }
}
