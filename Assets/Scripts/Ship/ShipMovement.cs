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

        if (input.forward)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
        }

        if(input.reverse)
        {
            GetComponent<Rigidbody2D>().AddForce(-transform.up * speed);
        }

        if (input.left)
        {
            GetComponent<Rigidbody2D>().AddTorque(torque);
        }

        if (input.right)
        {
            GetComponent<Rigidbody2D>().AddTorque(-torque);
        }
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
