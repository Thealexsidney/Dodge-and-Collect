using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingSpawner : MonoBehaviour
{
    public GameObject GameObject;
    public float shootInterval = 2f;
    public float startDelay = 2f;

    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating(nameof(ShootHomingBullet), startDelay, shootInterval);
    }

    // Update is called once per frame
    void ShootHomingBullet()
    {
        Vector2 spawnPosition = GetRandomEdgePosition();
        
        Instantiate(GameObject, spawnPosition, Quaternion.identity);
    }

    Vector2 GetRandomEdgePosition()
    {
        int edge = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;

        if (mainCamera != null)
        {
            float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

            switch (edge)
            {
                case 0:
                    spawnPosition = new Vector2(Random.Range(screenLeft, screenRight), screenBottom);
                    break;
                case 1:
                    spawnPosition = new Vector2(Random.Range(screenLeft, screenRight), screenTop);
                    break;
                case 2:
                    spawnPosition = new Vector2(screenLeft, Random.Range(screenBottom, screenTop));
                    break;
                case 3:
                    spawnPosition = new Vector2(screenRight, Random.Range(screenBottom, screenTop));
                    break;
            }

        }

        Debug.Log("Pos:" + spawnPosition);
        return spawnPosition;
        
    }
}
