using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;
    public TextMeshProUGUI text;
    public void UpdateScore()
    {
        text.text = "Score: " + marioScore.Value.ToString();
    }
    public void Start()
{
    UpdateScore();
}
void OnApplicationQuit(){
	marioScore.SetValue(0);
}
}