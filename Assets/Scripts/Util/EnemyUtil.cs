using UnityEngine;
using System.Collections;

public class EnemyUtil {
    public static EnemyTurningDir swapDirection(EnemyTurningDir current)
    {
        if(current.Equals(EnemyTurningDir.LEFT))
        {
            return EnemyTurningDir.RIGHT;
        } else
        {
            return EnemyTurningDir.LEFT;
        }
    }

    public static void simpleMove(Rigidbody2D current, float forwardSpeed, float torque, EnemyTurningDir currentDir)
    {
        moveAndTurn(current, torque, forwardSpeed, currentDir);
    }

    public static CannonSide simpleSeekAndFind(GameObject target, Rigidbody2D current, float forwardSpeed, float torque, EnemyTurningDir currentDir)
    {
        RaycastHit2D[] hitsRight = 
            Physics2D.RaycastAll(current.transform.position, current.transform.right, 600);
        RaycastHit2D[] hitsLeft = 
            Physics2D.RaycastAll(current.transform.position, -current.transform.right, 600);

        //is this the ship to the right?
        if (isColliding(hitsRight, target))
        {
            counterTurnUntilStopped(current, torque, currentDir);
            return CannonSide.RIGHT;
        }
        //is this the ship to the left?
        else if (isColliding(hitsLeft, target))
        {
            counterTurnUntilStopped(current, torque, currentDir);
            return CannonSide.LEFT;
        }
        else
        {
            moveAndTurn(current, torque, forwardSpeed, currentDir);
            return CannonSide.NONE;
        }
    }

    private static bool counterTurnUntilStopped(Rigidbody2D current, float torque, EnemyTurningDir turningDir)
    {
        //Is the object still turning?
        if(Mathf.Abs(current.angularVelocity) > 0)
        {
            //Which way were we originally turning?
            if(turningDir.Equals(EnemyTurningDir.LEFT))
            {
                //Counter turn
                turnRight(current, torque); ;
            }
            else if (turningDir.Equals(EnemyTurningDir.RIGHT))
            {
                turnLeft(current, torque);
            }

            //not stopped
            return false;
        }

        //stopped
        return true;
    }

    private static void moveAndTurn(Rigidbody2D current, float torque, float forwardSpeed, EnemyTurningDir turningDir)
    {
        if (turningDir.Equals(EnemyTurningDir.LEFT))
        {
            turnLeft(current, torque);
        }
        else if (turningDir.Equals(EnemyTurningDir.RIGHT))
        {
            turnRight(current, torque);
        }

        move(current, forwardSpeed);
    }

    private static void turnLeft(Rigidbody2D current, float torque)
    {
        current.AddTorque(torque);
    }

    private static void turnRight(Rigidbody2D current, float torque)
    {
        current.AddTorque(-torque);
    }

    private static void move(Rigidbody2D current, float forwardSpeed)
    {
        current.AddForce(current.transform.up * forwardSpeed);
    }

    private static bool isColliding(RaycastHit2D[] hits, GameObject target) 
    {
        foreach(var hit in hits)
        {
            if(hit.collider.gameObject == target)
            {
                return true;
            }
        }

        return false;
    }
}
