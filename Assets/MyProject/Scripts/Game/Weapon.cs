using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float fireRate = 0.25f;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    private AudioSource gunAudio;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
            gunAudio.Play();
            muzzleFlash.Play();
        }
    }

    void Shoot()
    {
        GameObject bullet = ObjectPoolingManger.Instance.GetBullet();
        bullet.transform.position = fpsCamera.transform.position + 
            fpsCamera.transform.forward;
        bullet.transform.forward = fpsCamera.transform.forward;
    }

}