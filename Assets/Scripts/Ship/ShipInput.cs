using UnityEngine;
using System.Collections;

//Handles the input as booleans for the player ship
public class ShipInput {

    public bool forward = false, reverse = false, right = false, left = false;

    // Update is called once per frame
    public void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            forward = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            reverse = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            reverse = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            left = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            right = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
        }
    }
}
