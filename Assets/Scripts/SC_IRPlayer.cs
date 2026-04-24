using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
 
// public interface PlayerState{
//     bool asdkas;
// }

public class SC_IRPlayer : MonoBehaviour 
{   
    // VAR FOR JUMP
    public float gravity = 20.0f; 
    public float jumpHeight = 2.5f;

    public float fov = 0f;  
    public float lateralSpeed = 10.0f;
    public float acceleration = 8f; 
    public float currentSpeed = 0f;
    public Transform orientation;
    public Transform cameraPos;

    public float coyoteTime = 0.46f;
    public float _currentCoyoteTime;
    public Rigidbody r; 
    bool useAdmin = false;
    public Vector3 defaultScale;
    public float crouchTime = 0.45f; 
    public float targetInput = 0f;
    public float jumpTime = 0.5f; 
    public float jumpTimer;
    public bool jumpTimeAction = false;
    public float bobSpeed = 7f;
    public float bobAmount = 0.2f;

    public  event Action<SC_IRPlayer> OnJumpedEvent;
    public  event Action<SC_IRPlayer> OnCrouchedEvent;

    public PlayerCam playerCam;


    public LayerMask groundLayerMask;
 
    
    public PlayerState _currentState;

    public WalkingState playerWalkingState = new WalkingState();
    public JumpingState playerJumpingState = new JumpingState();
    public FallingState playerFallingState = new FallingState();

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        r = GetComponent<Rigidbody>(); 
        r.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        r.freezeRotation = true; 
        r.useGravity = false; 
        r.mass = 100f;
        defaultScale = transform.localScale;  
        _currentCoyoteTime = coyoteTime;
        playerWalkingState.Initialize(this);
        playerJumpingState.Initialize(this);
        playerFallingState.Initialize(this);
        _currentState = playerWalkingState;
        _currentState.EnterState(this);

    }

    // Update is called once per frame
    void Update()
    {   
        InputFunction();
        if(!isGrounded()) { 
            _currentCoyoteTime -= Time.deltaTime;
        } else { 
            _currentCoyoteTime = coyoteTime;
        }
        _currentState?.UpdateState(this);
    }
    
    void FixedUpdate() { 
        currentSpeed = Mathf.Lerp(currentSpeed, targetInput * lateralSpeed, acceleration * Time.fixedDeltaTime);
        // transform.position += Vector3.right * currentSpeed * Time.fixedDeltaTime;
        Vector3 laterMove = Vector3.right * currentSpeed * Time.fixedDeltaTime;
        float newPosition = Mathf.Clamp(r.position.x + laterMove.x, -11f, 11f);
        Vector3 finalPosition = new Vector3(newPosition, r.position.y, r.position.z);
        r.MovePosition(finalPosition);
        r.linearVelocity = new Vector3(currentSpeed, r.linearVelocity.y, r.linearVelocity.z);
        if (targetInput == 0 && Mathf.Abs(currentSpeed) < 0.01f)
        {
            currentSpeed = 0;
        } 
        _currentState?.FixedUpdateState(this);
        r.AddForce(new Vector3(0, -gravity * r.mass, 0));
    } 
    void useDebug(){ 
         useAdmin = true;
         r.useGravity = false;
    }

    
    public bool isFalling() {
        float rayLength = 3f;
        Vector3 origin = transform.position;
        Debug.DrawRay(origin, Vector3.down * rayLength, Color.red); 
        return Physics.Raycast(origin, Vector3.down, rayLength, groundLayerMask); 
    }

    public bool isGrounded() { 
        float rayLength = 1.1f;
        Vector3 origin = transform.position;
        Debug.DrawRay(origin, Vector3.down * rayLength, Color.red); 
        return Physics.Raycast(origin, Vector3.down, rayLength, groundLayerMask); 
    }
    void InputFunction() { 
        var keyboard = Keyboard.current;
        if (keyboard == null) 
        {
            return;
        } 
        targetInput = keyboard.aKey.isPressed ? -1 : (keyboard.dKey.isPressed ? 1 : 0); 
        if((keyboard.wKey.isPressed || keyboard.spaceKey.isPressed) && isGrounded()) OnJumpedEvent?.Invoke(this);
        if (keyboard.cKey.isPressed) useDebug();
        if(keyboard.sKey.isPressed ) OnCrouchedEvent?.Invoke(this);
    }

    public void TransitionToState(PlayerState newState){ 
        if(newState == null) return;
        _currentState.ExitState(this);
        _currentState = newState; 
        _currentState.EnterState(this);
    }

    public float CalculateJumpVerticalSpeed() { 
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
    void OnTriggerEnter(Collider other){  
        if (other.CompareTag("Obstacle") || other.CompareTag("Trash")) {
            Rigidbody objRb = other.GetComponent<Rigidbody>(); 
            if (objRb != null) { 
                Vector3 direction = (other.transform.position - transform.position).normalized;
                
                objRb.AddForce(direction * (-3f) * SC_GroundGenerator.instance.movingSpeed, ForceMode.Impulse);
                r.AddForce(direction * (50f), ForceMode.Impulse);
            }

            SC_GroundGenerator.instance.ModifySpeed(-5f); 
            currentSpeed *= 0.4f;
        }
        if (other.CompareTag("Car")) { 
            Rigidbody objRb = other.GetComponent<Rigidbody>();
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
    void OnGUI(){
        GUI.Label(new Rect(20, 30, 200, 25), "Estado: " + (_currentState.GetType().Name));
        GUI.Label(new Rect(20, 60, 200, 25), "Grounded: " + (isGrounded()));
    }
}
