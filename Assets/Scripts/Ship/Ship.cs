using UnityEngine;
using System.Collections.Generic;

public class Ship : MonoBehaviour
{
    Cannon leftCannon;
    Cannon rightCannon;

    Health health;

    private static string cannonBall = "CannonBall";
    Cannon[] cannons;

    void Start()
    {
        //Create some health for the player
        health = GetComponent<Health>();
        health.Initialize(50); //50hp

        //Fetch and store the cannons
        cannons = GetComponentsInChildren<Cannon>();
        leftCannon = GeneralUtil.getCannon(CannonDirections.LEFT, cannons);
        rightCannon = GeneralUtil.getCannon(CannonDirections.RIGHT, cannons);
    }

    void Update()
    {
        //Get the camera and set the positon and rotation at the beginning
        ShipCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShipCamera>();
        camera.setPosition(GetComponentInChildren<ShipMovement>().getPosition());
        camera.setRotation(GetComponentInChildren<ShipMovement>().getRotation());

        //Fire left or right cannons
        if(Input.GetKeyDown(KeyCode.E))
        {
            rightCannon.Fire(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            leftCannon.Fire(transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Has a cannon ball hit the player? (self or others)
        if(other.gameObject.tag.Equals(cannonBall))
        {
            //Ensure it isn't one of our current cannon balls
            if (!GeneralUtil.doesBallMatchCannons(other.gameObject, cannons))
            {
                //If not one of ours, we've been hit
                Debug.Log("Player shot by enemy");
                Destroy(other.gameObject);
                if (!health.decreaseHealth(10))
                {
                    Debug.Log("Player is dead!!!");
                    Destroy(gameObject);
                }
            } 
        }
    }
}
