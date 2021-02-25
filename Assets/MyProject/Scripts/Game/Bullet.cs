using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Damage and speed factors of the bullet, how fast it travels before
    // colliding.
    public int damage = 20;
    public float speed = 10f;

    // Setting a lifetime to the bullet so that the object eventually disappears.
    public float lifetime = 1f;
    private float lifeTimer;

    // When the bullet is created, set the timer.
    private void OnEnable()
    {
        lifeTimer = lifetime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Projecting the bullet forward by multiplying its direction by its speed and the current time
    // to factor in faster processing computers.
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        // Check if bullet has reached end of lifetime/range, if so, set to inactive to return to pool.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
