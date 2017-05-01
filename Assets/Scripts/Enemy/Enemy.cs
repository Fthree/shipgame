using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float forwardSpeed = 5f;
    public float torque = 20f;
    public float firingIntervalSeconds = 1;

    EnemyMovement movement;
    GameTimer firingTimer;

    Cannon leftCannon;
    Cannon rightCannon;

    CannonSide currentCannonSide;

    GameObject target;
    bool attacking;

    private static string ship = "Enemy";
    private static string enemy = "Player";

    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        Cannon[] cannons = GetComponentsInChildren<Cannon>();
        leftCannon = GeneralUtil.getCannon(CannonDirections.LEFT, cannons);
        rightCannon = GeneralUtil.getCannon(CannonDirections.RIGHT, cannons);

        leftCannon.Initialize(ship, enemy);
        rightCannon.Initialize(ship, enemy);

        firingTimer = new GameTimer();
        firingTimer.ResetAndStartTimer(firingIntervalSeconds);
    }

	// Update is called once per frame
	void Update () {
        if(attacking)
        {
            target = GameObject.FindGameObjectWithTag(enemy);
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

            firingTimer.ResetAndStartTimer(firingIntervalSeconds);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isPlayerTag(other.tag))
        {
            attacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(attacking && isPlayerTag(other.tag))
        {
            attacking = false;
        }
    }

    private bool isPlayerTag(string tag)
    {
        return tag.Equals(enemy);
    }
}
