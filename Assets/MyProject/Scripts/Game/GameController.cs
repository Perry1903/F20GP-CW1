using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Player player;

    GameObject weapon;
    public Weapon weaponObject;
    public TextMeshProUGUI ammoCounter;

    public TextMeshProUGUI healthCounter;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter.text = "AMMO: " + weaponObject.Ammo + "/" +
            weaponObject.SpareAmmo;

        healthCounter.text = "HEALTH: " + player.Health;
    }

    public void CurrentWeapon(string weaponName)
    {
        weapon = GameObject.FindGameObjectWithTag(weaponName);
        weaponObject = weapon.GetComponent<Weapon>();
        Debug.Log("Current weapon: " + weaponName);
    }

    // Creating separate update functions to prevent updating once per frame
    // If this doesnt work then just stick in Update
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
