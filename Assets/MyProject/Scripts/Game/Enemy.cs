using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // NavMeshAgent variables - the agent and its target, player
    NavMeshAgent enemyAgent;
    public Player player;

    public AudioSource zombieDead;

    // Enemy info - how much health they have and damage per attack
    public float health = 50;
    public int damage = 20;

    private bool playerAttacked;

    private bool dead = false;
    public bool Dead { get { return dead; } }

    // Start is called before the first frame update
    // Referencing the agent
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    // Set and update the agent's destination to the player's current location 
    void Update()
    {
        enemyAgent.SetDestination(player.transform.position);
    }

    // When the enemy is involved in a collision, check if it was with the player,
    // that the enemy itself isn't dead and isn't in the middle of an attack cooldown.
    // If so, attack the player.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null
            && health > 0 && !playerAttacked)
        {
            AttackPlayer();
        }
    }

    // When the player is attacked, set the player's current health to that
    // subtracted by the enemy's damage value.
    // Once attacked, the enemy must enter a cooldown.
    private void AttackPlayer()
    {
        player.Health -= damage;
        Debug.Log("Enemy hit Player!");
        playerAttacked = true;
        StartCoroutine(AttackCooldown());
    }

    // Couroutine on cooldown function to utilise the delay function.
    // After the time has passed, the enemy can attack again.
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1f);
        playerAttacked = false;
    }

    // When the enemy enables a trigger - it checks if it was a bullet.
    // If so, it gets the damage value of the bullet and subtracts that value
    // from the enemy's health. The bullet is then set to false to return to the object pool.

    // If the enemy's health goes to or below 0, then the enemy dies. The enemy object and agent
    // are deactivated and the variable 'dead' is true. This is to ensure that the enemy can't
    // receive further damage after it dies.
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
                    zombieDead.Play();
                    enemyAgent.enabled = false;
                    enabled = false;

                    //transform.localEulerAngles = new Vector3(-10,
                    // transform.localEulerAngles.y, transform.localEulerAngles.z);

                    //player.Money += 100;

                    StartCoroutine(Die()); // Method only called once.
                }
            }
        }
    }

    // Create a short pause then destroy the object.
    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

}
