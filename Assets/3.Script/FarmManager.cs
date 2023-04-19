using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    //Save dropped item data
    [HideInInspector]public List<GameObject> droppedItem = new List<GameObject>();
    //Prefab list
    [SerializeField]private List<GameObject> resPrefabList = new List<GameObject>();
    

    void SpawnItem(){
        
    }

    public void SpawnHoeDirt(float x, float y) {
        GameObject obj = Instantiate(resPrefabList[0], new Vector3(x, y, 0f), Quaternion.identity);
        obj.transform.SetParent(this.transform);
    }

    public void SpawnWateringDirt(float x, float y) {
        
    }
}
