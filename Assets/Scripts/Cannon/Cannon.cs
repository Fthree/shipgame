using UnityEngine;
using System;
using System.Collections.Generic;

public class Cannon : MonoBehaviour {

    public float ballSpeed;

    public float maxVelocity;

    public CannonDirections direction;
    public CannonBall cannonBallPrefab;

    private string owner;
    private string enemy;

    public void Initialize(string owner, string enemy)
    {
        this.owner = owner;
        this.enemy = enemy;
    }

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
        ball.Initialize(initialDirection, owner, enemy, ballSpeed, maxVelocity);
    }
	
	void Update () {
        
	}
}
