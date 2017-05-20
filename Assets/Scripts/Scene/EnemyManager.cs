using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class EnemyManager : MonoBehaviour {

    public List<EnemiesAndWeights> enemiesAndWeights;
    public int spawningTime = 10;
    public int maxSpawnCount = 5;
    private List<Enemy> shuffledEnemyList;
    private GameTimer spawningTimer;
    private int currentSpawnCount = 0;

	// Use this for initialization
	void Start () {
        shuffledEnemyList = new List<Enemy>();

        //Initialize the list from the weights
	    foreach(var enemyAndWeight in enemiesAndWeights)
        {
            for(int i = 0; i != enemyAndWeight.weight; i++)
            {
                shuffledEnemyList.Add(enemyAndWeight.enemy);
            }
        }

        //Suffle the list 
        shuffledEnemyList = shuffledEnemyList.OrderBy(x => new System.Random().Next()).ToList();

        spawningTimer = new GameTimer();
        spawningTimer.ResetAndStartTimer(spawningTime);
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 camPos = Camera.main.transform.position;
        SpawnerBounds bounds = getSpawnerBounds(camPos);

        if (spawningTimer.timerPassed)
        {
            if(currentSpawnCount < maxSpawnCount)
            {
                Enemy newEnemy = Instantiate(shuffledEnemyList[UnityEngine.Random.Range(0, shuffledEnemyList.Count)], new Vector3(bounds.max.x, 0), Quaternion.Euler(0, 0, 90)) as Enemy;
                newEnemy.onDeathDo(() => currentSpawnCount--);
                currentSpawnCount++;
                spawningTimer.ResetTimerNew(spawningTime);
            }
        }

        Debug.DrawLine(camPos, new Vector3(bounds.min.x, camPos.y));
        Debug.DrawLine(camPos, new Vector3(bounds.max.x, camPos.y));
        Debug.DrawLine(camPos, new Vector3(camPos.x, bounds.min.y));
        Debug.DrawLine(camPos, new Vector3(camPos.x, bounds.max.y));
    }

    public SpawnerBounds getSpawnerBounds(Vector3 cameraPosition)
    {
        float distanceIncrease = 1.2f;

        float width = Screen.width * distanceIncrease;
        float height = Screen.height * distanceIncrease;

        return new SpawnerBounds {
             min = new Vector3(cameraPosition.x - width, cameraPosition.y - height),
             max = new Vector3(cameraPosition.x + width, cameraPosition.y + height)
        };
    }
}
