using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    ORANGEMUSHROOM = 0,
    REDMUSHROOM = 1
}
public class PowerupManagerEV : MonoBehaviour
{
     // reference of all player stats affected
  public IntVariable marioJumpSpeed;
  public IntVariable marioMaxSpeed;
  public PowerupInventory powerupInventory;
  public List<GameObject> powerupIcons;
  public Powerup goldenMushroom;
public Powerup redMushroom;

  void Start()
  {
      if (!powerupInventory.gameStarted)
      {
          powerupInventory.gameStarted = true;
          powerupInventory.Setup(powerupIcons.Count);
          resetPowerup();
      }
      else
      {
          // re-render the contents of the powerup from the previous time
          for (int i = 0; i < powerupInventory.Items.Count; i++)
          {
              Powerup p = powerupInventory.Get(i);
              if (p != null)
              {
                  AddPowerupUI(i, p.powerupTexture);
              }
          }
      }
  }
    
  public void resetPowerup()
  {
      for (int i = 0; i < powerupIcons.Count; i++)
      {
          powerupIcons[i].SetActive(false);
      }
  }
    
  void AddPowerupUI(int index, Texture t)
  {
      powerupIcons[index].GetComponent<RawImage>().texture = t;
      powerupIcons[index].SetActive(true);
  }

  public void AddPowerup(Powerup p)
  {
      powerupInventory.Add(p, (int)p.index);
      AddPowerupUI((int)p.index, p.powerupTexture);
  }

  public void OnApplicationQuit()
  {
      ResetValues();
  }
  public void ResetValues(){
    powerupInventory.Remove(0);
    powerupInventory.Remove(1);
  }
  public void AttemptConsumePowerup(KeyCode k){
    if (k == KeyCode.Z){
        if (powerupInventory.Get(0) != null){
            marioMaxSpeed.SetValue(marioMaxSpeed.Value+goldenMushroom.aboluteSpeedBooster);
            StartCoroutine(removeSpeedEffect());
            powerupInventory.Remove(0);
            powerupIcons[0].SetActive(false);
        }
    }
    if (k == KeyCode.X){
        if (powerupInventory.Get(1) != null){
            marioJumpSpeed.SetValue(marioJumpSpeed.Value+redMushroom.absoluteJumpBooster);
            StartCoroutine(removeJumpEffect());
            powerupInventory.Remove(1);
            powerupIcons[1].SetActive(false);
        }
    }
  }
  IEnumerator  removeJumpEffect(){
		yield  return  new  WaitForSeconds(5.0f);
		// player.GetComponent<PlayerController>().maxSpeed  /=  2;
        marioJumpSpeed.SetValue(marioJumpSpeed.Value-redMushroom.absoluteJumpBooster);
	}
    IEnumerator  removeSpeedEffect(){
    yield  return  new  WaitForSeconds(5.0f);
    // player.GetComponent<PlayerController>().maxSpeed  /=  2;
    marioMaxSpeed.SetValue(marioMaxSpeed.Value-goldenMushroom.aboluteSpeedBooster);
}
}