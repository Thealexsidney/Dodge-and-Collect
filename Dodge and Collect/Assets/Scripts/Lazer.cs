using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public Vector2 pointA;
    public Vector2 pointB;
    public float width = 0.5f;
    private BoxCollider2D boxCollider;
    void Start()

    {
        boxCollider = GetComponent<BoxCollider2D>();

        StretchBetweenPoints(pointA, pointB);
    }

    public void StretchBetweenPoints(Vector2 a, Vector2 b)

    {
        pointA = a;
        pointB = b;
        

        Vector2 midPoint = (a + b) / 2f;
        transform.position = midPoint;

        float length = Vector2.Distance(a, b);



        transform.right = (b - a).normalized;

        transform.localScale = new Vector3(length, width, 1f);



        // Adjust collider size and offset

        boxCollider.size = new Vector2(length, width);

        boxCollider.offset = Vector2.zero;

    }
        
}
