using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsumableTriggerChecker : MonoBehaviour
{
    public Powerup stats;
    public CustomPowerupEvent onCollected;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onCollected.Invoke(stats);
            Destroy(this.gameObject);
        }
    }
}
