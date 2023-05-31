using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour

{   [SerializeField] private int aiDamage;
    public float RotateSpeed;
    [SerializeField] private HealthController _healthController;
    public PlayerMovement playerMovement;

    void FixedUpdate()
    {
        float angle = transform.eulerAngles.z;
        transform.Rotate(0, 0, RotateSpeed * 1f * Time.deltaTime);
    }

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
