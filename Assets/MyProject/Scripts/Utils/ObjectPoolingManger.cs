using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManger : MonoBehaviour
{
    private static ObjectPoolingManger instance;
    public static ObjectPoolingManger Instance { get { return instance;  } }

    public GameObject bullet;
    public int bulletAmount = 30;

    public List<GameObject> bullets;

    private void Awake()
    {
        instance = this;

        // Preloading bullets
        bullets = new List<GameObject>(bulletAmount);

        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject bulletInstance = Instantiate(bullet);
            bulletInstance.transform.SetParent(transform);
            bulletInstance.SetActive(false);

            bullets.Add(bulletInstance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBullet()
    {
        // If a bullet object in the pool is not being used, fetch it
        // and instantiate it.
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        // If all bullets are being used, instantiate another
        GameObject bulletInstance = Instantiate(bullet);
        bulletInstance.transform.SetParent(transform);
        bullets.Add(bulletInstance);

        return bulletInstance;
    }
}
