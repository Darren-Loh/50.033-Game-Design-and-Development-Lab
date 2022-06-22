using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastTriggerChecker : MonoBehaviour
{
    public KeyCode stats;
    public CustomCastEvent onCollected;

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         onCollected.Invoke(stats);
	// 		Destroy(this.gameObject);
    //     }
    // }
    // void Awake(){
    // if (Input.GetKeyDown("z")){
    // //   CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z,this.gameObject);
    // onCollected.Invoke(KeyCode.Z);
    // }

    // if (Input.GetKeyDown("x")){
    // //   CentralManager.centralManagerInstance.consumePowerup(KeyCode.X,this.gameObject);
    // onCollected.Invoke(KeyCode.X);
    // }
    // }
    void Update(){
        onCollected.Invoke(stats);
    }

}
