using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBehaviour : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward,
            out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
