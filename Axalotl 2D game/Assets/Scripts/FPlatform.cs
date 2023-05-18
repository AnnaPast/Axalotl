using System.Collections;
using UnityEngine;

public class FPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 2f;      // Задержка перед падением
    [SerializeField] private float respawnDelay = 1f;   // Задержка перед возвращением

    private Vector3 initialPosition;
    private Rigidbody2D rb;

    private void Start()
    {
        initialPosition = transform.position;

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(FallAndRespawn());
    }

    private IEnumerator FallAndRespawn()
    {
        while (true)
        {

            yield return new WaitForSeconds(fallDelay);


            rb.isKinematic = false;

            yield return new WaitForSeconds(1f);

            rb.velocity = Vector2.zero;
            transform.position = initialPosition;

            yield return new WaitForSeconds(respawnDelay);
        }
    }
}
