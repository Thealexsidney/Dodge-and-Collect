using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletspawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin180Up, Spin180Down }


    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private float startDelay = 1f;
    [SerializeField] private float speedIncrease = 0.01f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float timer2 = 0f;
    private float time = 0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        time += Time.deltaTime;
        
        if (timer2 >= 1)
        {
            timer2 = 0f;
            if (firingRate > 0.1f)
            {
                firingRate -= speedIncrease;
            }
        }
        
        
        
        if (time >= startDelay)
        {
            if (spawnerType == SpawnerType.Spin180Up)
            {
                if (transform.eulerAngles.z >= 180)
                {
                    transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
                }



            }
            if (spawnerType == SpawnerType.Spin180Down)
            {
                if (transform.eulerAngles.z < 180)
                {
                    transform.eulerAngles = new Vector3(0f, 0f, 180f);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);

                }



            }
            if (timer >= firingRate)
            {
                Fire();
                timer = 0;
            }
        }
    }

    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<SimpleBullet>().speed = speed;
            spawnedBullet.GetComponent<SimpleBullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }

}
