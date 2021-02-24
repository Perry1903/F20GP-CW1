using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    GameObject weapon;
    public Weapon weaponObject;
    public TextMeshProUGUI ammoCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter.text = "AMMO: " + weaponObject.Ammo + "/" + 
            weaponObject.SpareAmmo;
    }

    public void CurrentWeapon (string weaponName)
    {
        weapon = GameObject.FindGameObjectWithTag(weaponName);
        weaponObject = weapon.GetComponent<Weapon>();
        Debug.Log("Current weapon: " + weaponName);
    }
}
