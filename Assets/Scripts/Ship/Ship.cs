using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    Cannon leftCannon;
    Cannon rightCannon;

    private static string ship = "Player";
    private static string enemy = "Enemy";

    void Start()
    {
        Cannon[] cannons = GetComponentsInChildren<Cannon>();
        leftCannon = ShipUtil.getCannon(CannonDirections.LEFT, cannons);
        rightCannon = ShipUtil.getCannon(CannonDirections.RIGHT, cannons);

        leftCannon.Initialize(ship, enemy);
        rightCannon.Initialize(ship, enemy);
    }

    void Update()
    {
        ShipCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShipCamera>();
        camera.setPosition(GetComponentInChildren<ShipMovement>().getPosition());
        camera.setRotation(GetComponentInChildren<ShipMovement>().getRotation());

        if(Input.GetKeyDown(KeyCode.E))
        {
            rightCannon.Fire(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            leftCannon.Fire(transform.position);
        }
    }
}
