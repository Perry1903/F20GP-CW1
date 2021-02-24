using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject weapon;
    public Weapon weaponObject;

    public int initialHealth = 100;
    private int health;

    public int Health {
        get { return health; }
        set { health = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<AmmoCrate>() != null
            && weaponObject.SpareAmmo == 0)
        {
            CollectAmmo(hit);
        }

        if (hit.gameObject.GetComponent<HealthCrate>() != null
            && health <= 100)
        {
            CollectHealth(hit);
        }
    }

    public void CurrentWeapon(string weaponName)
    {
        weapon = GameObject.FindGameObjectWithTag(weaponName);
        weaponObject = weapon.GetComponent<Weapon>();
    }

    private void CollectAmmo(ControllerColliderHit hit)
    {
        weaponObject.SpareAmmo += weaponObject.magazineSize;
        Destroy(hit.gameObject);
    }

    private void CollectHealth(ControllerColliderHit hit)
    {
        health = 100;
        Destroy(hit.gameObject);
    }

}
