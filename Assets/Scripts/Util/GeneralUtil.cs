using UnityEngine;
using System.Collections.Generic;

public class GeneralUtil
{
    public static Cannon getCannon(CannonDirections direction, Cannon[] cannons)
    {
        foreach (var cannon in cannons)
        {
            if (cannon.direction.Equals(direction))
            {
                return cannon;
            }
        }

        return null;
    }
}
