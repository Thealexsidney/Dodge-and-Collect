using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float bulletLife = 10f;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public Transform target;
    public GameObject bulletSplit;
    public int splitAmount;

    private float timer = 0f;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("Player");
        if (targetObject != null )
        {
            target = targetObject.transform;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife)
        {
            Destroy(this.gameObject);
            SplitBullet();
        }
        
        
        
        timer += Time.deltaTime;

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction = direction.normalized;

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void SplitBullet()
    {
        float angleStep = 360f / splitAmount;

        for (int i = 0; i < splitAmount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject bullet = Instantiate(bulletSplit, transform.position, rotation);
            
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = rotation * Vector2.up;
            rb.velocity = direction * speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
