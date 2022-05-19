using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour //every game class has to inherit
{
public float speed;
public float maxSpeed = 10;
private Rigidbody2D marioBody;
private float moveHorizontal;
private bool onGroundState = true;
public float upSpeed = 1;
private SpriteRenderer marioSprite;
private bool faceRightState = true;
public Transform enemyLocation;
public TextMeshProUGUI scoreText;
private int score = 0;
private bool countScoreState = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
	Application.targetFrameRate =  30;
	marioBody = GetComponent<Rigidbody2D>();
    marioSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
      if (Input.GetKeyDown("a") && faceRightState){
          faceRightState = false;
          marioSprite.flipX = true;
      }

      if (Input.GetKeyDown("d") && !faceRightState){
          faceRightState = true;
          marioSprite.flipX = false;
      }
           // when jumping, and Gomba is near Mario and we haven't registered our score
      if (!onGroundState && countScoreState)
      {
          if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
          {
              countScoreState = false;
              score++;
              Debug.Log(score);
          }
      }
    }

    void  FixedUpdate()
{
          // dynamic rigidbody
                if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
          // stop
        //   marioBody.velocity = Vector2.zero;
        marioBody.velocity = new Vector2(0, marioBody.velocity.y); // changing direction only reset velocity of x axis and keeps y velocity which might be falling
      }
      moveHorizontal = Input.GetAxis("Horizontal");
      if (Mathf.Abs(moveHorizontal) > 0){
          Vector2 movement = new Vector2(moveHorizontal, 0);
          if (marioBody.velocity.magnitude < maxSpeed)
                  marioBody.AddForce(movement * speed);
      }

      if (Input.GetKeyDown("space") && onGroundState){
          marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
          onGroundState = false;
          countScoreState = true; //check if Gomba is underneath
      }

  
    
}
 // called when the cube hits the floor
  void OnCollisionEnter2D(Collision2D col)
  {
      if (col.gameObject.CompareTag("Ground")) {
          onGroundState = true;
          countScoreState = false; // reset score state
          scoreText.text = "Score: " + score.ToString();
      };
  }

void OnTriggerEnter2D(Collider2D other)
  {
      if (other.gameObject.CompareTag("Enemy"))
      {
          Debug.Log("Collided with Gomba!");
          SceneManager.LoadScene("SampleScene");
      }
  }

}
