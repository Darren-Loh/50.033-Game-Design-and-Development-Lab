using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownMushroom : MonoBehaviour, ConsumableInterface
{
    public  Texture t;
	public  void  consumedBy(GameObject player){
		// give player jump boost
		player.GetComponent<PlayerController>().upSpeed  +=  10;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator  removeEffect(GameObject player){
		yield  return  new  WaitForSeconds(5.0f);
		player.GetComponent<PlayerController>().upSpeed  -=  10;
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
		CentralManager.centralManagerInstance.addPowerup(t, 1, this);
		// GetComponent<Collider2D>().enabled  =  false;
        this.transform.position  =  new  Vector3(this.transform.position.x, -7, this.transform.position.z);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
	
	}
}
}
