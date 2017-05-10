using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {
    public ShipInput input;

    public float torque = 50f;
    public float speed = 10f;

    void Start()
    {
        input = new ShipInput();
    }

    void Update()
    {
        input.Update();

        Rigidbody2D currentRB = GetComponent<Rigidbody2D>();

        if (input.forward)
        {
            forward(currentRB);
        }

        if(input.reverse)
        {
            reverse(currentRB);
        }

        if (input.left)
        {
            turnLeft(currentRB);
        }

        if (input.right)
        {
            turnRight(currentRB);
        }
    }

    private void turnLeft(Rigidbody2D current)
    {
        current.AddTorque(torque);
    }

    private void turnRight(Rigidbody2D current)
    {
        current.AddTorque(-torque);
    }

    private void reverse(Rigidbody2D current)
    {
        current.AddForce(-transform.up * speed);
    }

    private void forward(Rigidbody2D current)
    {
        current.AddForce(transform.up * speed);
    }

    public Vector2 getPosition()
    {
        return GetComponent<Rigidbody2D>().position;
    }

    public float getRotation()
    {
        return GetComponent<Rigidbody2D>().rotation;
    }
}
