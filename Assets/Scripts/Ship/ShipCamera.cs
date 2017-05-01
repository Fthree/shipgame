using UnityEngine;
using System.Collections;

public class ShipCamera : MonoBehaviour {

    public void setPosition(Vector2 position)
    {
        Vector3 newPosition = new Vector3(position.x, position.y, -10f);
        transform.position = newPosition;
    }

    public void setRotation(float rotation)
    {
        
    }
}
