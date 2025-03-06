using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserLife = 5;
    public GameObject laserEmmiter;
    public float flickerSpeed = 5;
    private float flickerTimer = 0;

    private float timer = 0;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    // Start is called before the first frame update
    void Start()
    {

        
        
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        Vector2 pointA = RandomPointOnScreen();
        Vector2 pointB = RandomPointOnScreen();

        lineRenderer.SetPosition(0, pointA);
        lineRenderer.SetPosition(1, pointB);

        Instantiate(laserEmmiter, pointA, Quaternion.identity);
        Instantiate(laserEmmiter, pointB, Quaternion.identity);

        Vector2[] colliderPoints = { pointA, pointB };
        edgeCollider.points = colliderPoints;

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > laserLife) Destroy(this.gameObject);
        if (timer > 1.5)
        {
            gameObject.tag = "Laser";
            lineRenderer.enabled = true;
        }
        
        if (timer < 1.5)
        {
            flickerTimer += Time.deltaTime;
            if (flickerTimer >= flickerSpeed)
            {
                lineRenderer.enabled = !lineRenderer.enabled;
                flickerSpeed -= 0.05f;
                flickerTimer = 0f;
            }
        }
        
        
        
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
