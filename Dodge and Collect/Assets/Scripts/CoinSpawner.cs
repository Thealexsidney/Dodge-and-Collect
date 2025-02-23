using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{


    public GameObject coin;

    public float spawnInterval = 2f;
    public float startDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnCoin), startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void SpawnCoin()
    {
        Vector2 randomPoint = RandomPointOnScreen();
        Instantiate(coin, randomPoint, Quaternion.identity);
    }
    Vector3 RandomPointOnScreen()
    {
        float randomX = Random.Range(0, Screen.width);
        float randomY = Random.Range(0, Screen.height);

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(randomX, randomY));

        return worldPosition;
    }


}
