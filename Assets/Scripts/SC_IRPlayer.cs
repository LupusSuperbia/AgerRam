using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class SC_IRPlayer : MonoBehaviour
{   
    public float gravity = 20.0f; 
    public float jumpHeight = 2.5f;
    public float lateralSpeed = 10.0f;
    public float acceleration = 8f; 
    private float currentSpeed = 0f;
    public Transform orientation;

    Rigidbody r; 
    bool grounded = false; 
    bool useAdmin = false;
    Vector3 defaultScale;
    bool crouch = false; 
    bool left = false;
    bool shouldJump = false;
    float targetInput = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        r = GetComponent<Rigidbody>(); 
        r.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        r.freezeRotation = true; 
        r.useGravity = false; 
        r.mass = 100f;
        defaultScale = transform.localScale;            
    }

    // Update is called once per frame
    void Update()
    {   
        var keyboard = Keyboard.current;
        if (keyboard == null) 
        {
            return;
        } 
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) targetInput = -1;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) targetInput = 1;
        if (keyboard.aKey.wasReleasedThisFrame || keyboard.dKey.wasReleasedThisFrame)
            {
                targetInput = 0; 
            }
        if (keyboard.cKey.isPressed) useDebug();
        // transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        if((Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame) && grounded){
            shouldJump = true;
        }

        crouch = Keyboard.current.sKey.wasPressedThisFrame; 
       
    }
    
    void FixedUpdate() { 
        currentSpeed = Mathf.Lerp(currentSpeed, targetInput * lateralSpeed, acceleration * Time.fixedDeltaTime);
        // transform.position += Vector3.right * currentSpeed * Time.fixedDeltaTime;
        Debug.Log(currentSpeed);
        Vector3 laterMove = Vector3.right * currentSpeed * Time.fixedDeltaTime;

        r.MovePosition(r.position + laterMove);
        r.linearVelocity = new Vector3(currentSpeed, r.linearVelocity.y, r.linearVelocity.z);
        if(shouldJump) {
            r.linearVelocity = new Vector3(r.linearVelocity.x, CalculateJumpVerticalSpeed(), r.linearVelocity.z);
            shouldJump = false;
        }
        if (targetInput == 0) Debug.Log("Frenando: " + currentSpeed);
        if (targetInput == 0 && Mathf.Abs(currentSpeed) < 0.01f)
        {
            currentSpeed = 0;
        }

        if (crouch){ 
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(defaultScale.x, defaultScale.y * 0.4f, defaultScale.z), Time.fixedDeltaTime * 7); 
        } 
        
        else { 
            transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, Time.fixedDeltaTime * 7);
        }
        r.AddForce(new Vector3(0, -gravity * r.mass, 0)); 
        grounded = false;

    } 
    void useDebug(){ 
         useAdmin = true;
         r.useGravity = false;
    }
    void OnCollisionStay() 
    { 
        grounded = true;
    }

    float CalculateJumpVerticalSpeed() { 
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
    void OnTriggerEnter(Collider other){ 
        Debug.Log("Si llegamos"); 
        Debug.Log(other.tag.ToString());
        if (other.CompareTag("Obstacle") || other.CompareTag("Trash")) {
            Rigidbody objRb = other.GetComponent<Rigidbody>(); 
            if (objRb != null) { 
                Vector3 direction = (other.transform.position - transform.position).normalized;
                
                objRb.AddForce(direction * (-15f) * SC_GroundGenerator.instance.movingSpeed, ForceMode.Impulse);
                r.AddForce(direction * (50f), ForceMode.Impulse);
            }

            SC_GroundGenerator.instance.ModifySpeed(-5f); 
            currentSpeed *= 0.4f;
        }
        if (other.CompareTag("Car")) { 
            Rigidbody objRb = other.GetComponent<Rigidbody>();
            Debug.Log("auto");
            if(objRb != null) { 
                currentSpeed = 0; 

            }
            SC_GroundGenerator.instance.ModifySpeed(-10f);
            currentSpeed *= 0.2f;
        }
    }
    void OnCollisionEnter(Collision collision) { 
        if(collision.gameObject.tag == "Finish") 
        { 
            SC_GroundGenerator.instance.gameOver = true;
        }
        // if(collision.rigidbody != null ) {
        //     if(collision.gameObject.tag == "Obstacle") { 
        //         Vector3 direction = (collision.gameObject.transform.position - transform.position).normalized;
        //         currentSpeed *= 0.4f;
        //         collision.rigidbody.AddForce(direction * (-15f) * 10, ForceMode.Impulse);
        //         SC_GroundGenerator.instance.ModifySpeed(-0.5f);
        //     }
        // }
    }
}
