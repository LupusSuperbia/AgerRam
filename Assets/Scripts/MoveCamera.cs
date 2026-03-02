using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCamera : MonoBehaviour
{      
    public Transform cameraPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    
    {
        if (cameraPosition != null) 
        {// Un seguro extra para que no tire error
        transform.position = cameraPosition.position;
        }
    }
}
