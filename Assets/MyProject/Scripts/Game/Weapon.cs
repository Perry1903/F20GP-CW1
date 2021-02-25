using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public GameController gc;
    public Enemy enemy;
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public AudioSource gunAudio;
    public AudioSource reloadAudio;
    private float nextFire;

    public int magazineSize = 12; // Amount of bullets before reloading
    private int spareAmmo; // Maximum amount of ammo when starting/replenishing
    private int ammo;   // Current ammo amount
    public float damage = 10f;
    public float fireRate = 0.25f;
    public float reloadTime = 2f;

    public int Ammo {
        get { return ammo; }
        set { ammo = value; }
    }
    public int SpareAmmo {
        get { return spareAmmo; }
        set { spareAmmo = value; }
    }

    private void OnEnable()
    {
        gc.CurrentWeapon(gameObject.name);
        player.CurrentWeapon(gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        spareAmmo = magazineSize;
        ammo = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        // If fire button is pressed, current time is greater than next
        // time to fire, and weapon's magazine isn't empty (0).
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && ammo != 0)
        {
            nextFire = Time.time + fireRate;
            Shoot();
            gunAudio.Play();
            muzzleFlash.Play();
        }


        if (Input.GetKeyDown("r") && ammo == 0 && spareAmmo != 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        ammo--;

        GameObject bullet = ObjectPoolingManger.Instance.GetBullet();
        bullet.transform.position = fpsCamera.transform.position + 
            fpsCamera.transform.forward;
        bullet.transform.forward = fpsCamera.transform.forward;
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading.");
        reloadAudio.Play();
        yield return new WaitForSeconds(reloadTime);
        ammo = spareAmmo;
        spareAmmo -= magazineSize;
    }

}