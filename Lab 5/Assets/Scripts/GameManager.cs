using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public  TextMeshProUGUI score;
	private  int playerScore =  0;
    public  static  event  gameEvent OnPlayerDeath;
    public  static  event  gameEvent OnEnemyDeath;
    public  delegate  void gameEvent();
	
	public  void  increaseScore(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();
        OnEnemyDeath();
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void  damagePlayer(){
	OnPlayerDeath();
}
}
