using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObstacleDamage : MonoBehaviour
{
    [SerializeField] private int aiDamage;
    [SerializeField] private HealthController _healthController;
    public PlayerMovement playerMovement;
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
            Damage();
        }
    }

    void Damage()
    {
        _healthController.playerHealth = _healthController.playerHealth - aiDamage;
        _healthController.UpdateHealth();
    }
}
