using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private float timer = 0;
    public float laserLife = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > laserLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
    }
}
