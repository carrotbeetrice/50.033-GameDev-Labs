using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManagerEV : MonoBehaviour
{
    public GameConstants gameConstants;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawn Manager EV start");
        for (int i = 0; i < gameConstants.enemyPoolSize; i++)
        {
            spawnFromPooler(ObjectType.gombaEnemy);
        }
    }

    void startSpawn(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < gameConstants.enemyPoolSize; i++)
        {
            spawnFromPooler(ObjectType.gombaEnemy);
        }
    }

    void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);

        if (item != null)
        {
            //set position
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), gameConstants.groundSurface + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
            Debug.Log("new enemy spawned");
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnNewEnemy()
    {
        // ObjectType i = Random.Range(0, 2) == 0 ? ObjectType.gombaEnemy : ObjectType.greenEnemy;
        spawnFromPooler(ObjectType.gombaEnemy);
    }
}
