using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser : MonoBehaviour
{
    public float laserLife = 5;
    public GameObject laserEmmiter;
    public float edgeMargin = 1f;
    private float timer = 0;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    // Start is called before the first frame update
    void Start()
    {

        
        edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();

        

        Vector2 pointA = RandomPointOnScreen();
        Vector2 pointB = RandomPointOnScreen();

        Vector2 localPointA = transform.InverseTransformPoint(pointA);
        Vector2 localPointB = transform.InverseTransformPoint(pointB);

        lineRenderer.SetPosition(0, pointA);
        lineRenderer.SetPosition(1, pointB);

        edgeCollider.points = new Vector2[]
        {
            new Vector2(pointA.x, pointA.y),
            new Vector2(pointB.x, pointB.y)
        };


        

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

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(randomX, randomY, Camera.main.nearClipPlane + 5f));
        worldPosition.z = 0f;
        return worldPosition;
    }
}
