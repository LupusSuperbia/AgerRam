using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public enum PatternTile{ 
    CleanTile, 
    ZigZag,
    Cobertura,
    AllBlocked,
    Arena
}


public class SC_GroundGenerator : MonoBehaviour
{       
    
    public Camera mainCamera;  
    public Transform playerTransform;
    public Transform startPoint; 
    public SC_PlatformTile tilePrefab;
    public float movingSpeed = 12f; 
    public float normalSpeed = 15f;
    public float maxSpeed = 30f;
    public float recoveryRate = 2f;
    public int tilesToPreSpawn = 3; 
    public int tilesWithoutObstacles = 4; 
    List<SC_PlatformTile> spawnedTiles = new List<SC_PlatformTile>();
    int nextTileToActivate = -1; 
    [HideInInspector]
    public bool gameOver = false; 
    public bool gameStarted = false; 
    float score = 0;

    public static SC_GroundGenerator instance;

    private int randomPattern;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        Vector3 spawnPosition = startPoint.position; 
        int tilesWithNoObstacleTmp = tilesWithoutObstacles;

        for (int i = 0; i < tilesToPreSpawn; i++) 
        { 
            spawnPosition -= tilePrefab.startPoint.localPosition;

            SC_PlatformTile spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity) as SC_PlatformTile;
            if(tilesWithNoObstacleTmp > 0) { 
                // spawnedTile.DeactivateAllObstacles();
                tilesWithNoObstacleTmp--;
            } 
            else { 
                spawnedTile.PrepareTile();
                // spawnedTile.ActivateRandomObstacle();
            }

            spawnPosition = spawnedTile.endPoint.position; 
            // spawnedTile.PrepareTile();
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);


        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gameOver && gameStarted) { 
            transform.Translate(-spawnedTiles[0].transform.forward * Time.deltaTime * (movingSpeed + (score/500)), Space.World);
            score += Time.deltaTime * movingSpeed;
            movingSpeed = Mathf.Lerp(movingSpeed, normalSpeed, recoveryRate * Time.deltaTime);

        } 
        
        if(spawnedTiles[0].endPoint.position.z < playerTransform.position.z - 35f)
        {   
           RecycleTile();
        } 
        
    }
    public void RecycleTile() { 
            SC_PlatformTile tileTmp = spawnedTiles[0];
            
            tileTmp.PrepareTile();
            spawnedTiles.RemoveAt(0);
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count-1].endPoint.position - tileTmp.startPoint.localPosition;
            // tileTmp.ActivateRandomObstacle();
            spawnedTiles.Add(tileTmp);
    }
    public void ModifySpeed(float amount) {
        movingSpeed += amount;
        movingSpeed = Mathf.Clamp(movingSpeed, 5f, maxSpeed);         
         
    }

  
}
