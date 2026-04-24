using UnityEngine;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class PlayerCam : MonoBehaviour
{   
    public float sensY; 
    public float sensX; 

    public Transform orientation; 
    public Rigidbody r;
    public Camera mainCam;
    float xRotation;
    float yRotation; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() { 
        mainCam = GetComponent<Camera>();
    }
   
    void Start()
    {   
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {   
       Vector2 mousePosition = Mouse.current.delta.ReadValue();
       float mouseX =  mousePosition.x  * sensX; 
       float mouseY = mousePosition.y  * sensY; 

       yRotation += mouseX; 
       xRotation -= mouseY; 
       xRotation = Mathf.Clamp(xRotation, -90f, 90f);
       yRotation = Mathf.Clamp(yRotation, -90, 90f);

       transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
       orientation.rotation = Quaternion.Euler(0, yRotation, 0);
       rayCastTest();
    }


    public void rayCastTest(){ 
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Lanzamos el rayo a una distancia de 50 metros
        if (Physics.Raycast(ray, out hit, 50f))
        {
            // 'hit.collider' es el objeto que estás mirando
            Debug.DrawLine(ray.origin, hit.point);
            // Aquí puedes verificar etiquetas:
            if(hit.collider.CompareTag(ObjectType.Obstacle.ToString())) {
                
            }
        }
    }
}
