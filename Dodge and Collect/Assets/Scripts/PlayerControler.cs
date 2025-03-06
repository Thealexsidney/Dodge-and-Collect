using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowMouse : MonoBehaviour
{
    private Camera mainCamera;

    private bool isPaused = false;

    public int Health;
    public int maxHealth;
    public float dodgeChance;
    public TextMeshProUGUI healthText;

    public int Coins;
    public TextMeshProUGUI coinsText;

    public GameObject pauseMenu;
    
    public float timeAlive;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI statsText;
    

    [SerializeField]
    private float maxSpeed = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        healthText.text = "Health: " + Health + "/" + maxHealth;
        coinsText.text = "Coins: " + Coins;
        timeAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FollowMousePosistion(maxSpeed);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        timeAlive =  timeAlive + 1*Time.deltaTime;
        timeText.text = "Time: " + timeAlive.ToString("F2");

        statsText.text = "Max Health: " + maxHealth + "\nMax Speed: " + maxSpeed + "\nDodge: " + dodgeChance;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenu.SetActive(isPaused);
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
            if (!isPaused)
            {
                float RotationZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, RotationZ - 90);
            }
            
        }
                        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (Random.value > dodgeChance)
            {
                Health--;
                                
                healthText.text = "Health: " + Health + "/" + maxHealth;

                if (Health <= 0)
                {
                    SceneManager.LoadScene("GameOver");
                    Health = maxHealth;
                }
            }
            
            Destroy(other.gameObject);

        }
        if (other.CompareTag("Laser"))
        {
            if (Random.value > dodgeChance)
            {
                Health--;

                healthText.text = "Health: " + Health + "/" + maxHealth;

                if (Health <= 0)
                {
                    SceneManager.LoadScene("GameOver");
                    Health = maxHealth;
                }
            }

            

        }
        if (other.CompareTag("Coin"))
        {
            Coins++;
            Destroy(other.gameObject);
            coinsText.text = "Coins: " + Coins;
        }
        if (other.CompareTag("Heal"))
        {
            if (Health < maxHealth)
            {
                Health++;
                Destroy(other.gameObject);
                healthText.text = "Health: " + Health + "/" + maxHealth;
            }
            
        }

    }
    private Vector2 GetWorldPositionFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HealthUp()
    {
        if (maxHealth < 20)
        {
            if (Coins >= 1)
            {
                Coins -= 1;
                maxHealth ++;
                Health++;
                healthText.text = "Health: " + Health + "/" + maxHealth;
                coinsText.text = "Coins: " + Coins;
            }
        }
    }

    public void SpeedUp()
    {
        if(maxSpeed < 10)
        {
            if(Coins >= 1)
            {
                Coins -= 1;
                maxSpeed += 0.5f;
                coinsText.text = "Coins: " + Coins;
            }
        }
                
    }

    public void DogdeUp()
    {
        if (dodgeChance < 0.5)
        {
            if (Coins >= 5)
            {
                Coins -= 5;
                dodgeChance += 0.05f;
                coinsText.text = "Coins: " + Coins;
            }
        }

    }

}
