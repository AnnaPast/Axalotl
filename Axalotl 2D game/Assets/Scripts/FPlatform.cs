using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPlatform : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 currentPosiiotn;
    bool moveingBack;
    [SerializeField] private float backPlat = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPosiiotn = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && moveingBack == false)
        {
            Invoke("FallPlatform", 1f);
        }
    }

    void FallPlatform()
    {
        rb.isKinematic = false;
        Invoke("BackPlatform", backPlat);
    }
 
    void BackPlatform()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        moveingBack = true;
    }

    private void Update()
    {
        if(moveingBack == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPosiiotn, 10f * Time.deltaTime);
        }

        if(transform.position.y == currentPosiiotn.y)
        {
            moveingBack = false;
        }
    }
}
