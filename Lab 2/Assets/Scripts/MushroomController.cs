using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
private float maxOffset = 5.0f;
private float enemyPatroltime = 2.0f;
private int moveRight = 1;
private Vector2 velocity;

private Rigidbody2D mushroomBody;
private int[ ] randDirection;
private bool touchedPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        this.mushroomBody = GetComponent<Rigidbody2D>();
        ComputeVelocity();
        this.mushroomBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
        int randomNumber = Random.Range(0, 2);
        randDirection = new int[ ]{-1,1} ;
        moveRight = moveRight * randDirection[randomNumber];
        // Debug.Log(moveRight);

    }
    void ComputeVelocity(){
      velocity = new Vector2(7, 0);
  }
  void MoveMushroom(){
      this.mushroomBody.MovePosition(this.mushroomBody.position + velocity * Time.fixedDeltaTime * moveRight);
  }

    // Update is called once per frame
    void Update()
    {
      
        // change direction
        // moveRight *= -1;
        if (touchedPlayer == false){
            ComputeVelocity();
            MoveMushroom();
        }
        
      
    }
    void  OnBecameInvisible(){
	Destroy(gameObject);	
}
  void OnCollisionEnter2D(Collision2D col)
  {
      if (col.gameObject.CompareTag("MushroomObstacles")) {
          this.moveRight = this.moveRight * -1;
          MoveMushroom();
      }
      if (col.gameObject.CompareTag("Player")) {
          touchedPlayer = true;
      }

  }
}
