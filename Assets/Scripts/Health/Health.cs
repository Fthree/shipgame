using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public GameObject currentHealth;

    int health;

    int maxHealth = 50;

    public void Initialize(int initialHealth)
    {
        if(initialHealth > maxHealth)
        {
            health = maxHealth;
        }
        health = initialHealth;

        RectTransform rect = currentHealth.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x - (maxHealth - initialHealth), rect.sizeDelta.y);
    }

    /*Decreases health by specified amount.
    Returns false if health is less than or equal to 0
    Returns true if health is still in good standing*/
    public bool decreaseHealth(int healthAmount)
    {
        RectTransform rect = currentHealth.GetComponent<RectTransform>();

        if (healthAmount >= health)
        {
            rect.sizeDelta = new Vector2(0, rect.sizeDelta.y);
            return false;
        }
        health -= healthAmount;
        if(health > 0)
        {
            //Decrease the width of the current health panel by the health decrease amount until 0
            rect.sizeDelta = new Vector2(rect.sizeDelta.x - healthAmount, rect.sizeDelta.y);
            return true;
        }
        //If health is 0 or less than 0, leave
        return false;
    }
}
