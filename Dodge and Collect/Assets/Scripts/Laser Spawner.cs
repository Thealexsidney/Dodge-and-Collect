using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public float shootInterval = 2f;
    public float startDelay = 2f;
    public GameObject GameObject;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(ShootHomingBullet), startDelay, shootInterval);
    }

    void ShootHomingBullet()
    {        
        Instantiate(GameObject,new Vector2(0, 0), Quaternion.identity);
    }
}
