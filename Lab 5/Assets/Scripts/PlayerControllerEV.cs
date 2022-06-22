using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    // public float speed;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;
    private bool isDead = false;
    private bool isADKeyUp=true;
    private bool isSpacebarUp=true;
    private bool faceRightState = true;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;
    private AudioSource marioAudioSource;
    private Rigidbody2D marioBody;
    private bool onGroundState = true;
    private float moveHorizontal;
    // public CustomCastEvent onCollected;
    public CastEvent consumePowerup;


    // Start is called before the first frame update
    void Start()
    {
        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        force = gameConstants.playerDefaultForce;

                // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudioSource = GetComponent<AudioSource>();
        // originalPos = marioBody.position;
        // // Debug.log(typeof(marioBody.rotation));
        // //originalRotation = marioBody.rotation;
        // originalRotation = marioBody.rotation;
        // originalEnemyPos = enemyLocation.position;
        // GameManager.OnPlayerDeath  +=  PlayerDiesSequence;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("a")){
            if (!Input.GetKey(KeyCode.D)){
                isADKeyUp = true;
            }
        
      }

      if (Input.GetKeyUp("d")){
            if (!Input.GetKey(KeyCode.A)){
                isADKeyUp = true;
            }
      }
      if(Input.GetKeyUp("space")){
        isSpacebarUp = true;
      }
        // isADKeyUp = true;
        // isSpacebarUp = true;
        // toggle state
      if (Input.GetKeyDown("a")){
        isADKeyUp = false;
        
              //check velocity

        if(faceRightState){
        faceRightState = false;
        marioSprite.flipX = true;
            if (Mathf.Abs(marioBody.velocity.x)>0.05){
          marioAnimator.SetTrigger("onSkid");
        }
        }
            
      }

      if (Input.GetKeyDown("d")){
         isADKeyUp = false;
              //check velocity
        
        if(!faceRightState){
        faceRightState = true;
        marioSprite.flipX = false;
        if (Mathf.Abs(marioBody.velocity.x)>0.05){
          marioAnimator.SetTrigger("onSkid");
        }
        }
      }
      if(Input.GetKeyDown("space")){
        isSpacebarUp = false;
      }

      marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

    if (Input.GetKeyDown("z")){
     AttemptConsumePowerup(KeyCode.Z);
     }

    if (Input.GetKeyDown("x")){
      AttemptConsumePowerup(KeyCode.X);
      }
    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            //check if a or d is pressed currently
            if (!isADKeyUp)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
            }

            if (!isSpacebarUp && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                // countScoreState = true; //check if goomba is underneath
            }
        }
}
  void OnCollisionEnter2D(Collision2D col)
  {
      if (col.gameObject.CompareTag("Ground")) {
          onGroundState = true;
          marioAnimator.SetBool("onGround",onGroundState);
        //   countScoreState = false; // reset score state
        //   scoreText.text = "Score: " + score.ToString();
      };

      if (col.gameObject.CompareTag("Obstacles") && Mathf.Abs(marioBody.velocity.y)<0.01f){
          onGroundState = true;
          marioAnimator.SetBool("onGround",onGroundState);
      }
  }
  void PlayJumpSound(){
marioAudioSource.PlayOneShot(marioAudioSource.clip);
}
public void PlayerDiesSequence()
{
    isDead = true;
    // marioAnimator.SetBool("isDead", true);
    GetComponent<Collider2D>().enabled = false;
    marioBody.AddForce(Vector3.up * 30, ForceMode2D.Impulse);
    marioBody.gravityScale = 30;
    StartCoroutine(dead());
}

IEnumerator dead()
{
    yield return new WaitForSeconds(1.0f);
    marioBody.bodyType = RigidbodyType2D.Static;
}

public void AttemptConsumePowerup(KeyCode k){
  consumePowerup.Raise(k);
}
}

