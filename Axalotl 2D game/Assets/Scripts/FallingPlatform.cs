using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float respawnDelay = 0f;
    private float destroyDelay = 2f;
    public Animator animator;
    private Vector2 initialPosition;
    private bool isFalling = false;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        isFalling = true;
        animator.SetBool("shaking", true);

        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(destroyDelay);
        rb.bodyType = RigidbodyType2D.Static;
        rb.velocity = Vector2.zero;
        transform.position = initialPosition;

        yield return new WaitForSeconds(respawnDelay);
        animator.SetBool("shaking", false);
        isFalling = false;
    }
}
