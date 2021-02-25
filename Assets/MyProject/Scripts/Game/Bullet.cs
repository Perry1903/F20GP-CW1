using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 10f;

    public float lifetime = 1f;
    private float lifeTimer;

    private void OnEnable()
    {
        lifeTimer = lifetime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Projecting bullet forward
        transform.position += transform.forward * speed * Time.deltaTime;

        // Check if bullet has reached end of lifetime/range
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
