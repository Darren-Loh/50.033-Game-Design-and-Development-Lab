using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMushroom : MonoBehaviour, ConsumableInterface
{
    public  Texture t;
    public GameConstants gameConstants;
	public  void  consumedBy(GameObject player){
		// give player jump boost
		player.GetComponent<PlayerController>().maxSpeed  *=  2;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator  removeEffect(GameObject player){
		yield  return  new  WaitForSeconds(5.0f);
		player.GetComponent<PlayerController>().maxSpeed  /=  2;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void  OnCollisionEnter2D(Collision2D col)
{
	if (col.gameObject.CompareTag("Player")){
		// update UI
		CentralManager.centralManagerInstance.addPowerup(t, 0, this);
		// GetComponent<Collider2D>().enabled  =  false;
        // StartCoroutine(usePowerup());
        this.transform.position  =  new  Vector3(this.transform.position.x, -7, this.transform.position.z);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
	}
}
//   IEnumerator  usePowerup(){
// 		Debug.Log("Flatten starts");
// 		int steps =  5;
// 		float stepper =  1.0f/(float) steps;

// 		for (int i =  0; i  <  steps; i  ++){
// 			// this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

// 			// make sure enemy is still above ground
// 			this.transform.position  =  new  Vector3(this.transform.position.x, -7, this.transform.position.z);
// 			yield  return  null;
// 		}
// 		Debug.Log("Flatten ends");
// 		// this.gameObject.SetActive(false);
// 		// Debug.Log("Enemy returned to pool");
// 		yield  break;
// 	}
}
