using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ObjectPoolingData { 
    public string nombreReferencia;
    public ObjectType type; 
    public Transform spawn;
    [Range(0f, 1f)] public float probability  = 1f; 
}

public class SC_PlatformTile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform startPoint; 
    public Transform endPoint; 
    public List<ObjectPoolingData> objectData; 
    private List<GameObject> objectActivateList = new List<GameObject>(); 

    public void PrepareTile() { 
        DeactivateAllObjects();
        foreach(ObjectPoolingData objUnique in objectData) 
        {
            if(Random.value <= objUnique.probability) { 
                GameObject objectPool = ObjectPooling.instance.GetObjectPooling(objUnique.type);
                if(objectPool != null) { 
                    objectPool.SetActive(true);
                    objectPool.transform.SetParent(objUnique.spawn);
                    objectPool.transform.localPosition = Vector3.zero;
                    objectPool.transform.localRotation = Quaternion.Euler(0, 90, 0);
                    objectPool.transform.localScale = Vector3.one;
                    objectPool.transform.position = objUnique.spawn.position;
                    objectActivateList.Add(objectPool);
                }
            }
        }



    } 

    public void DeactivateAllObjects() { 
         foreach(GameObject obj in objectActivateList){ 
            if(obj != null) { 
                obj.SetActive(false);
                obj.transform.SetParent(ObjectPooling.instance.transform);
            }  
         }
         objectActivateList.Clear();
    }

    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
