using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Following variables
    //NavMeshAgent enemyAgent;

    // Enemy info
    public float health = 50f;

    // Start is called before the first frame update
    void Start()
    {
        //enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Debug.Log("Enemy died.");
        }
    }
}
