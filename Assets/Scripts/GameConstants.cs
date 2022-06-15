using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public float groundSurface = -1;

    // For SpawnManager.cs
    public int enemyPoolSize = 10;

    // Scoring system
    int currentScore;
    int currentPlayerHealth;

    // for EnemyController.cs
    public float maxOffset = 4.0f;
    public float enemyPatrolTime = 2.0f;
    public float flattenTimeStep = 3.0f;

    // for MushroomController
    public float enlargeTimeStep = 1;
    public float enlargeTimeStepper = 0.1f;
    public float shrinkTimeStep = 2;

    // Reset values
    // Vector3 gombaSpawnPointStart = new Vector3(); // hardcoded location

    // for Consume.cs
    public  int consumeTimeStep =  10;
    public  int consumeLargestScale =  4;
    
    // for Debris.cs
    public  int breakTimeStep =  30;
    public  int breakDebrisTorque =  10;
    public  int breakDebrisForce =  10;
    
    // for BreakBrick.cs
    public int spawnNumberOfDebris =  10;
    
    // for Rotator.cs
    public  int rotatorRotateSpeed =  6;
    
    // for testing
    public  int testValue;


    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
