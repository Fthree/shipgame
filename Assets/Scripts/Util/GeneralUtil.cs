using UnityEngine;
using System.Collections.Generic;

public class GeneralUtil
{
    //Fetch the cannons on the specified side
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

    public static CannonBall getCannonBall(GameObject ballObject, Cannon[] cannons)
    {
        CannonBall returnBall = null;
        foreach(var cannon in cannons)
        {
            returnBall = cannon.findBall(ballObject);
            if(returnBall != null)
            {
                return returnBall;
            }
        }

        return returnBall;
    }

    //Match the ball game object to the current cannons list of balls
    public static bool doesBallMatchCannons(GameObject ball, Cannon[] cannons)
    {
        bool doesMatch = false;
        foreach (var cannon in cannons)
        {
            doesMatch = cannon.doesBallMatch(ball);

            if (doesMatch)
            {
                return true;
            }
        }

        return false;
    }
}
