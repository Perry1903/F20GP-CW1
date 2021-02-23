using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBehaviour : MonoBehaviour
{
    public float damage = 10f;
    public float fireRate = 0.25f;
    public float range = 50f;
    public float hitForce = 100f;


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
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward,
            out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                if (enemy.health <= 0f)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
        }
    }

}
