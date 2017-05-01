using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour
{
    public Splash splashPrefab;
    private Splash splash;

    private float speedThreshold;

    private string owner;

    private string enemy;

    // Use this for initialization
    public void Initialize(Vector2 initialDirection, string owner, string enemy, float speed, float threshold)
    {
        this.owner = owner;
        this.enemy = enemy;
        speedThreshold = threshold;
        GetComponent<Rigidbody2D>().AddForce(initialDirection * speed);
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        if (splash != null)
        {
            if (!splash.isAlive())
            {
                Debug.Log("dead");
                Destroy(splash.gameObject);
                Destroy(gameObject);
            }
        } else
        {
            if (rigidBody.velocity.magnitude < speedThreshold)
            {
                splash = Instantiate(splashPrefab, transform.position, transform.rotation) as Splash;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals(owner))
        {
            return;
        }
        if (other.tag.Equals(enemy))
        {
            if (splash != null)
            {
                //If the splash exists, remove it
                Destroy(splash.gameObject);
            }
            Destroy(other.gameObject); //Destroy the other object or do whatever to it
            Destroy(gameObject); //Destroy the cannon ball
        }
    }
}
