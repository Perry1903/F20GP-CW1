using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Player player;

    GameObject weaponObject;
    public Weapon weapon;
    public GameObject enemies;

    public TextMeshProUGUI ammoCounter;
    public TextMeshProUGUI healthCounter;
    public TextMeshProUGUI enemyCounter;
    //public TextMeshProUGUI moneyCounter;

    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI defeatText;

    // Start is called before the first frame update
    void Start()
    {
        victoryText.gameObject.SetActive(false);
        defeatText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter.text = "AMMO: " + weapon.Ammo + "/" +
            weapon.SpareAmmo;

        healthCounter.text = "HEALTH: " + player.Health;

        int aliveEnemies = 0;
        foreach (Enemy enemy in enemies.GetComponentsInChildren<Enemy>())
        {
            if (!enemy.Dead) { aliveEnemies++; }
        }
        enemyCounter.text = "ENEMIES REMAINING: " + aliveEnemies;

        if (aliveEnemies == 0)
        {
            victoryText.gameObject.SetActive(true);
            
            victoryText.text = "YOU WIN!";
        }

        //moneyCounter.text = "MONEY: $" + player.Money;

        if (player.Health <= 0)
        {
            foreach (Enemy enemy in enemies.GetComponentsInChildren<Enemy>())
            {
                enemy.enabled = false;
                enemy.GetComponent<CapsuleCollider>().enabled = false;

                player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;

                defeatText.gameObject.SetActive(true);
                defeatText.text = "YOU LOST!";
            }
        }
    }

    public void CurrentWeapon(string weaponName)
    {
        weaponObject = GameObject.FindGameObjectWithTag(weaponName);
        weapon = weaponObject.GetComponent<Weapon>();
        Debug.Log("Current weapon: " + weaponName);
    }

    // Creating separate update functions for optimisation.
    /*public void UpdateAmmo()
    {
        
    }

    public void UpdateHealth()
    {
        if (player.Health <= 30)
        {
            // Change text colour to red - FF0000
            healthCounter.color = new Color(255, 0, 0);
        }
        else
        {
            // Change colour to pink - FF6969
            healthCounter.color = new Color(255, 105, 105);
        }
    }*/
}
