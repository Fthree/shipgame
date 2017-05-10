using UnityEngine;
using System;
using System.Collections.Generic;

public class Cannon : MonoBehaviour {

    //Initial speed of the ball
    public float ballSpeed;

    //Won't go over this velocity, but will try to reach it
    public float minVelocityThreshold;

    public CannonDirections direction;
    public CannonBall cannonBallPrefab;

    //Reference to balls fired by this cannon
    List<CannonBall> balls = new List<CannonBall>();

	public void Fire(Vector2 position) {
        Vector2 initialDirection = new Vector2();

        if(direction.Equals(CannonDirections.RIGHT))
        {
            initialDirection = transform.right;
        }
        if (direction.Equals(CannonDirections.LEFT))
        {
            initialDirection = -transform.right;
        }
        
        CannonBall ball = Instantiate(cannonBallPrefab, new Vector3(position.x, position.y), transform.rotation) as CannonBall;
        ball.Initialize(initialDirection, ballSpeed, minVelocityThreshold);
        //Create a unique name for checking later on
        ball.name = Guid.NewGuid().ToString() + "-cannonball"; 
        balls.Add(ball);
    }

    public void Update()
    {
        List<CannonBall> newList = new List<CannonBall>(balls);

        foreach(var ball in balls)
        {
            //The ball has the ability to delete itself, if it's null, it removed itself
            if (ball == null)
            {
                //Remove self removed balls from the list
                newList.Remove(ball);
            }
        }

        balls = newList;
    }

    public bool doesBallMatch(GameObject currentBall)
    {
        foreach (var ball in balls)
        {
            if (ball != null)
            {
                //Match the name set in initialization
                if(currentBall.name.Equals(ball.name))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public CannonBall findBall(GameObject searchBall)
    {
        foreach(var ball in balls)
        {
            if(ball.name.Equals(searchBall.name))
            {
                return ball;
            }
        }

        return null;
    }
}
