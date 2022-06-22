using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    GREENMUSHROOM = 0,
    REDMUSHROOM = 1
}

public class PowerupManagerEV : MonoBehaviour
{
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerupInventory powerupInventory;
    public List<GameObject> powerupIcons;

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
            for (int i = 0; i < powerupInventory.Items.Count; i++)
            {
                Powerup p = powerupInventory.Get(i);
                if (p != null)
                {
                    AddPowerupUI(i, p.powerupTexture);
                }
                else
                {
                    powerupIcons[i].SetActive(false);
                }
            }
        }
    }

    void resetPowerup()
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

    void ResetValues()
    {
        powerupInventory.Clear();
    }

    public void OnApplicationQuit()
    {
        ResetValues();
    }

    public void AttemptConsumePowerup(KeyCode k)
    {
        if (k == KeyCode.Z || k == KeyCode.X)
        {
            int powerupIndex = k == KeyCode.Z ? 0 : 1;
            // Powerup p = powerupInventory.Get(powerupIndex);

            // StartCoroutine(consumePowerup(p));
            powerupIcons[powerupIndex].SetActive(false);
            powerupInventory.Remove(powerupIndex);
        }
        
    }

    // IEnumerator consumePowerup(Powerup p)
    // {
    //     Debug.Log("Start to consume powerup");
    //     int boostedJumpSpeed = marioJumpSpeed.Value;
    //     int boostedMaxSpeed = marioMaxSpeed.Value;

    //     for (int i = 0; i < p.duration; i++)
    //     {
    //         boostedJumpSpeed += p.absoluteJumpBooster;
    //         boostedMaxSpeed += p.absoluteSpeedBooster;
    //         marioJumpSpeed.SetValue(boostedJumpSpeed);
    //         marioMaxSpeed.SetValue(boostedMaxSpeed);
    //         yield return null;
    //     }
        
    //     Debug.Log("Finished consuming powerup");
    //     marioJumpSpeed.SetValue(boostedJumpSpeed - p.absoluteJumpBooster);
    //     marioMaxSpeed.SetValue(boostedMaxSpeed - p.absoluteSpeedBooster);
    //     powerupInventory.Remove((int)p.index);
    //     powerupIcons[(int)p.index].SetActive(false);
    //     yield break;
    // }
}
