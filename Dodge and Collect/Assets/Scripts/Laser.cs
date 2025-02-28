using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserLife = 5;
    public GameObject laserEmmiter;

    private float timer = 0;
    private LineRenderer lineRenderer;
    
    // Start is called before the first frame update
    void Start()
    {

        
        
        lineRenderer = GetComponent<LineRenderer>();

        Vector2 pointA = RandomPointOnScreen();
        Vector2 pointB = RandomPointOnScreen();

        lineRenderer.SetPosition(0, pointA);
        lineRenderer.SetPosition(1, pointB);

        Instantiate(laserEmmiter, pointA, Quaternion.identity);
        Instantiate(laserEmmiter, pointB, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > laserLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
    }

    Vector3 RandomPointOnScreen()
    {
        float randomX = Random.Range(0, Screen.width);
        float randomY = Random.Range(0, Screen.height);

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(randomX, randomY));

        return worldPosition;
    }
}
