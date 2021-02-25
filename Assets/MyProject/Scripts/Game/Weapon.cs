using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Weapon must reference the player and game controller for events related to weapons.
    // The camera is referenced to know the starting position of the bullet and the direction
    // it travels from the camera.
    public Player player;
    public GameController gc;
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public AudioSource gunAudio;
    public AudioSource reloadAudio;
    private float nextFire;

    // These variables reference the current amount of ammo, the magazine size (or the number of bullets
    // that can be fired before reloading), and the amount of spare ammo for reloading.
    public int magazineSize = 12;
    private int spareAmmo;
    private int ammo;
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

    // When a weapon is active, the game controller and player must reference the weapon's name
    // to get the weapon's ammo information when updating the UI and when interacting with an ammo crate.
    private void OnEnable()
    {
        gc.CurrentWeapon(gameObject.name);
        player.CurrentWeapon(gameObject.name);
    }

    // Both current ammo and spare ammo are set to one magazine size of ammo
    // Start is called before the first frame update
    void Start()
    {
        spareAmmo = magazineSize;
        ammo = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        // If fire button is pressed, the current time is greater than next
        // time to fire, and the weapon's magazine isn't empty, then shoot.
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && ammo != 0)
        {
            nextFire = Time.time + fireRate;
            Shoot();
            gunAudio.Play();
            muzzleFlash.Play();
        }

        // If the R key is pressed when the current ammo is empty and there is
        // a spare magazine, then initiate the reload with the set delay.
        if (Input.GetKeyDown("r") && ammo == 0 && spareAmmo != 0)
        {
            StartCoroutine(Reload());
        }
    }

    // When shooting, a bullet is fetched from the object pool of pre-instantiated bullets.
    // For every shot, reduce the ammo amount.
    // The bullet is projected from the camera's position and forward. This means it is
    // aligned with the position of the central crosshair.
    void Shoot()
    {
        ammo--;

        GameObject bullet = ObjectPoolingManger.Instance.GetBullet();
        bullet.transform.position = fpsCamera.transform.position + 
            fpsCamera.transform.forward;
        bullet.transform.forward = fpsCamera.transform.forward;
    }

    // Reload function is called with a delay so the player must wait.
    // The current ammo becomes the spare ammo, and the spare ammo amount is reduced by its magazine size.
    IEnumerator Reload()
    {
        Debug.Log("Reloading.");
        reloadAudio.Play();
        yield return new WaitForSeconds(reloadTime);
        ammo = spareAmmo;
        spareAmmo -= magazineSize;
    }

}