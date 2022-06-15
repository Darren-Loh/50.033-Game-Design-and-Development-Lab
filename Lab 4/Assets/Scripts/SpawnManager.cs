using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;
    // Start is called before the first frame update
    void Start()
    {
                
        GameManager.OnEnemyDeath  +=  EnemySpawnSequence;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
        // spawn two goombaEnemy
        for (int j =  0; j  <  2; j++){
            spawnFromPooler(ObjectType.goombaEnemy);
        }
    }

void spawnFromPooler(ObjectType i){
	// static method access
	GameObject item =  ObjectPooler.SharedInstance.GetPooledObject(i);
	if (item  !=  null){
		//set position, and other necessary states
		item.transform.position  =  new  Vector3(Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
		item.SetActive(true);
	}
	else{
		Debug.Log("not enough items in the pool.");
	}
}

void EnemySpawnSequence(){
    ObjectType enemyType =  Random.Range(0, 2) ==  0  ?  ObjectType.goombaEnemy  :  ObjectType.greenEnemy;
    spawnFromPooler(enemyType);
    // spawnFromPooler(ObjectType.goombaEnemy);
}
}
