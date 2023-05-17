using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int playerHealth;
    Vector2 checkpointPos;

    [SerializeField] private Image[] hearts;


    private void Start()
    {
        UpdateHealth();
        checkpointPos = transform.position;
    }


    public void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                hearts[i].color = Color.white;
            }    
            else if (i > 0)
            {
                hearts[i].color = Color.black;
            }
            if (playerHealth == 0)
            {
                Die();
            }
        }    
        
    }


    void Die()
    {
       Respawn();
    }


    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }


    void Respawn()
    {
        transform.position = checkpointPos;
        playerHealth = 3;
    }


}
