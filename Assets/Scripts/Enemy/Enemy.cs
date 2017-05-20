using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float forwardSpeed = 5f;
    public float torque = 20f;
    [Range(0, 5)]
    public float firingIntervalSeconds = 1;

    public HitSplash splashPrefab;
    HitSplash splash;

    EnemyMovement movement;
    
    //Forces time between shots
    GameTimer firingTimer;

    Cannon leftCannon;
    Cannon rightCannon;

    Health health;

    //Current side we're firing on
    CannonSide currentCannonSide;

    GameObject target;
    bool attacking;

    private static string ship = "Enemy";
    private static string player = "Player";
    private static string cannonBall = "CannonBall";

    Cannon[] cannons;

    Action deathCallback;

    void Start()
    {
        //Create some health for the enemy
        health = GetComponent<Health>();
        health.Initialize(UnityEngine.Random.Range(25, 50)); //50hp

        movement = GetComponent<EnemyMovement>();
        cannons = GetComponentsInChildren<Cannon>();
        leftCannon = GeneralUtil.getCannon(CannonDirections.LEFT, cannons);
        rightCannon = GeneralUtil.getCannon(CannonDirections.RIGHT, cannons);

        firingTimer = new GameTimer();
        firingTimer.ResetAndStartTimer(firingIntervalSeconds);
    }

	// Update is called once per frame
	void Update () {
        if(splash != null)
        {
            if(!splash.isAlive())
            {
                Destroy(splash.gameObject);
            }
        }

        //We have a targer to follow
        if(attacking)
        {
            //Our enemy is the player, find and follow it
            target = GameObject.FindGameObjectWithTag(player);
            movement.UpdateTarget(target);
        }
        else
        {
            //no target
            movement.UpdateTarget(null);
        }

        currentCannonSide = movement.currentFiringSide;

        if(firingTimer.timerPassed)
        {
            if (currentCannonSide.Equals(CannonSide.LEFT))
            {
                leftCannon.Fire(transform.position);
            }
            if (currentCannonSide.Equals(CannonSide.RIGHT))
            {
                rightCannon.Fire(transform.position);
            }

            firingTimer.ResetAndStartTimer(UnityEngine.Random.Range(0, firingIntervalSeconds));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
          
        if(isPlayerTag(other.tag))
        {
            attacking = true;
        }

        if (other.gameObject.tag.Equals(cannonBall) && !tag.Equals("Trigger"))
        {
            if (!GeneralUtil.doesBallMatchCannons(other.gameObject, cannons))
            {
                //If not one of ours, we've been hit, remove the cannon ball
                Debug.Log("Enemy shot by player");
                Quaternion splashOrientation = other.transform.rotation;
                //splash = Instantiate(splashPrefab, transform.position, transform.rotation) as HitSplash;
                //splash.transform.Rotate(-other.transform.forward);
                Destroy(other.gameObject);
                if (!health.decreaseHealth(10))
                {
                    Debug.Log("Enemy is dead!!!");
                    deathCallback();
                    Destroy(gameObject);
                }  
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(attacking && isPlayerTag(other.tag))
        {
            attacking = false;
        }
    }

    bool isPlayerTag(string tag)
    {
        return tag.Equals(player);
    }

    public void onDeathDo(Action deathCallback)
    {
        this.deathCallback = deathCallback;
    }
}
