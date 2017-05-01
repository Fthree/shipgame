using UnityEngine;
using System.Timers;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float torque;
    public float movementSpeed;
    public float turningIntervalSeconds = 2;
    public EnemyTurningDir turningDir = EnemyTurningDir.RIGHT;
    public CannonSide currentFiringSide = CannonSide.NONE;

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
        //This timer will just swap the turning direction every X seconds
        if (turningTimer.timerPassed)
        {
            turningDir = EnemyUtil.swapDirection(turningDir);
            turningTimer.ResetAndStartTimer(turningIntervalSeconds);
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
}
