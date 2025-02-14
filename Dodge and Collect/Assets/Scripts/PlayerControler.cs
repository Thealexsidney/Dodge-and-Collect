using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowMouse : MonoBehaviour
{
    private Camera mainCamera;

    public int Health;
    public TextMeshProUGUI healthText;

    [SerializeField]
    private float maxSpeed = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        healthText.text = "Health: " + Health;
    }

    // Update is called once per frame
    void Update()
    {
        FollowMousePosistion(maxSpeed);

        
    }

  

    private void FollowMousePosistion(float maxSpeed)
    {
        
        Vector3 WorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 Difference = WorldPoint - transform.position;
        Difference.Normalize();

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - transform.position;
        
        transform.position = Vector2.MoveTowards(transform.position, GetWorldPositionFromMouse(), maxSpeed * Time.deltaTime);
        
        if (direction.magnitude > 0.00001f)
        {
            float RotationZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, RotationZ - 90);

        }
                        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Health--;

        Destroy(other.gameObject);

        healthText.text = "Health: " + Health;

        if (Health <= 0) 
        {
            SceneManager.LoadScene("GameOver");
            Health = 3;
        }

    }
    private Vector2 GetWorldPositionFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);

    }



}
