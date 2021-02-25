using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject weaponObject;
    Weapon weapon;

    public int initialHealth = 100;
    private int health;

    //public int initialMoney = 0;
    //private int money;

    public int Health {
        get { return health; }
        set { health = value; }
    }
    /*public int Money
    {
        get { return money; }
        set { money = value; }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        //money = initialMoney;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Collecting ammo
        if (hit.gameObject.GetComponent<AmmoCrate>() != null
            && weapon.SpareAmmo == 0)
        {
            CollectAmmo(hit);
        }

        // Collecting health
        if (hit.gameObject.GetComponent<HealthCrate>() != null
            && health <= 100)
        {
            CollectHealth(hit);
        }
    }

    public void CurrentWeapon(string weaponName)
    {
        weaponObject = GameObject.FindGameObjectWithTag(weaponName);
        weapon = weaponObject.GetComponent<Weapon>();
    }

    private void CollectAmmo(ControllerColliderHit hit)
    {
        weapon.SpareAmmo += weapon.magazineSize;
        Destroy(hit.gameObject);
    }

    private void CollectHealth(ControllerColliderHit hit)
    {
        health = 100;
        Destroy(hit.gameObject);
    }

}
