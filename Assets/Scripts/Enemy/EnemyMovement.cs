using UnityEngine;
using System.Timers;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float torque;
    public float movementSpeed;
    [Range(0, 10)]
    public float turningIntervalSeconds = 2;
    public EnemyTurningDir turningDir = EnemyTurningDir.RIGHT;
    public CannonSide currentFiringSide = CannonSide.NONE;
    public EnemyType type;

    GameTimer turningTimer;
    GameObject target;

	// Use this for initialization
	void Start () {
        currentFiringSide = CannonSide.NONE;
        turningTimer = new GameTimer();
        turningTimer.ResetAndStartTimer(turningIntervalSeconds);
    }
	
	// Update is called once per frame
	void Update () {
        //Will be CIRCULAR by default
        if(type.Equals(EnemyType.SLITHER))
        {
            //This timer will swap the turning direction every X seconds provided this is a slither type
            if (turningTimer.timerPassed)
            {
                turningDir = EnemyUtil.swapDirection(turningDir);
                turningTimer.ResetAndStartTimer(Random.Range(turningIntervalSeconds / 2, turningIntervalSeconds));
            }
        }

        if (type.Equals(EnemyType.STRAIGHT))
        {
            //Don't turn
            torque = 0;
        }

        //No target? just move simply
        if (target == null)
        {
            EnemyUtil.simpleMove(GetComponent<Rigidbody2D>(), movementSpeed, torque, turningDir);
        }
        else
        {
            //Target found, seek and find which side the enemy is on
            CannonSide newSide = EnemyUtil.simpleSeekAndFind(target, GetComponent<Rigidbody2D>(), movementSpeed, torque, turningDir);
            updateFiringSide(newSide);
        }
    }

    private void updateFiringSide(CannonSide newSide)
    {
        //The same side, keep firing
        if (newSide == currentFiringSide)
        {
            return;
        }
        else
        {
            //Side has swapped, update
            currentFiringSide = newSide;
        }
    }

    //Keep the target updated
    public void UpdateTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetType(EnemyType type)
    {
        this.type = type;
    }
}
