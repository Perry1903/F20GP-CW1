﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Following variables
    NavMeshAgent enemyAgent;
    public Player player;

    public bool playerAttacked;

    // Enemy info
    public float health = 50;
    public int damage = 20;

    private bool dead = false;
    public bool Dead { get { return dead; } }

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyAgent.SetDestination(player.transform.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Debug.Log("Enemy died.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null
            && health > 0 && !playerAttacked)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        player.Health -= damage;
        Debug.Log("Enemy hit Player!");
        playerAttacked = true;
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1f);
        playerAttacked = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            Debug.Log(gameObject.name);
            Bullet bullet = other.GetComponent<Bullet>();
            health -= bullet.damage;
            bullet.gameObject.SetActive(false);

            if (health <= 0)
            {
                if (!dead)
                {
                    dead = true;
                    Die(); // Method only called once.
                }
            }
        }
    }

    void Die()
    {
        enemyAgent.enabled = false;
        enabled = false;
        transform.localEulerAngles = new Vector3(-10,
            transform.localEulerAngles.y, transform.localEulerAngles.z);

        dead = true;
        player.Money += 100;
    }

}
