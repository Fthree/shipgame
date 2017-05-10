using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour
{
    public Splash splashPrefab;
    private Splash splash;

    private float speedThreshold;

    GameTimer startTimer;

    public void Initialize(Vector2 initialDirection, float speed, float threshold)
    {
        //speed threshold will define when we destroy the ball
        speedThreshold = threshold;
        //Shoot it off in some direction at some speed initially
        GetComponent<Rigidbody2D>().AddForce(initialDirection * speed);
        startTimer = new GameTimer();
        startTimer.ResetAndStartTimer(2);
    }

    void Update()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        //Is the splash currently active?
        if (splash != null)
        {
            //Has the splash finished with all the particles?
            if (!splash.isAlive())
            {
                Debug.Log("Cannon ball " + name + " missed");
                Destroy(splash.gameObject);
                Destroy(gameObject);
            }
        } else
        {
            //Is the current velocity magnitude under the threshold and has the timer passed?
            if (rigidBody.velocity.magnitude < speedThreshold && startTimer.timerPassed)
            {
                //Create a splash effect and stop rendering the object
                splash = Instantiate(splashPrefab, transform.position, transform.rotation) as Splash;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
    }

    public void Cleanup()
    {
        if(splash != null)
        {
            Destroy(splash.gameObject);
        }

        Destroy(gameObject);
    }
}
