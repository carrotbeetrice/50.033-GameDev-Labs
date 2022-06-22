using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Powerup", menuName = "ScriptableObjects/Powerup", order = 5)]
public class Powerup : ScriptableObject
{
    #if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
    #endif
    public PowerupIndex index;
    public Texture powerupTexture;
    public int absoluteSpeedBooster;
    public int absoluteJumpBooster;
    public int duration;

    public List<int> Utilise()
    {
        return new List<int>{absoluteSpeedBooster, absoluteJumpBooster};
    }

    public void Reset()
    {
        absoluteSpeedBooster = 0;
        absoluteJumpBooster = 0;
    }

    public void Enhance(int speedBooster, int jumpBooster)
    {
        absoluteSpeedBooster += speedBooster;
        absoluteJumpBooster += jumpBooster;
    }
}
