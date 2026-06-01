using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectConfig {
    public ObjectType type;
    public GameObject prefab;
    public int quantity;
    //public float mass;

}

public enum ObjectType {Obstacle, Money, Enemy, Cars, Walls, Trash, Bullet}

public class ObjectPooling : MonoBehaviour
{
    List<GameObject> spawnedObject = new List<GameObject>();
    public static ObjectPooling instance;
    public List<ObjectConfig> configuration;
    public Dictionary<ObjectType, List<GameObject>> poolDictionary;

    private void Awake() {
        if(instance != null && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poolDictionary = new Dictionary<ObjectType, List<GameObject>>();


        foreach(ObjectConfig obj in configuration){
            List<GameObject> objectsList = new List<GameObject>();

            for(int i = 0; i < obj.quantity; i++) {
                GameObject newObj = Instantiate(obj.prefab);
                newObj.SetActive(false);
                objectsList.Add(newObj);
            }
        if(!poolDictionary.ContainsKey(obj.type)){
            poolDictionary.Add(obj.type, objectsList);
        }
        }
    }
        // for(int i = 0; i  > spawnedObject.Count; i++){

        // }


    // Update is called once per frame
    void Update()
    {

    }

    public void Return(ObjectType type, GameObject obj){
        obj.SetActive(false);
    }

    public GameObject GetObjectPooling(ObjectType typeToGet) {

        if(poolDictionary.ContainsKey(typeToGet))
        {
            foreach(GameObject obj in poolDictionary[typeToGet])
            {
                if(!obj.activeInHierarchy) {
                    return obj;
                }
           }
        }
        ObjectConfig config = configuration.Find(c => c.type == typeToGet);
        if(config == null) return null;
        GameObject newObj = Instantiate(config.prefab);
        newObj.SetActive(false);
        poolDictionary[typeToGet].Add(newObj);
        return newObj;

}
}
