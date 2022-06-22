using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManagerEV : MonoBehaviour
{
    public GameConstants gameConstants;
    void Start()
    {
        Debug.Log("Spawnmanager start");
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.goombaEnemy);

    }

    void startSpawn(Scene scene, LoadSceneMode mode)
    {
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.goombaEnemy);
    }


    void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);

        if (item != null)
        {
            if (i == ObjectType.goombaEnemy){
            //set position
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f),  gameConstants.groundDistance + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
            }
            if (i == ObjectType.greenEnemy){
            //set position
            item.transform.localScale = new Vector3(0.35f, 0.35f, 1);
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), gameConstants.groundDistance + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
            }
            
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnNewEnemy()
    {
        ObjectType i = Random.Range(0, 2) == 0 ? ObjectType.goombaEnemy : ObjectType.greenEnemy;
        spawnFromPooler(i);
    }

}