
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask waterLayer;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool isSwimming;


    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {


        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.rotation = Quaternion.identity;
        else if (horizontalInput < -0.01f)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        if (Input.GetKey(KeyCode.Space) && isGrounded() && !Flying())
        {
            Jump();

        }


    }

    private bool Flying()
    {
        bool moving = Mathf.Abs(body.velocity.y) > 0.05f;
        return moving;
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isSwimming = true;
            Debug.Log("Player has collided with water.");
        }

        else if (collision.gameObject.CompareTag("Ground"))
        {
            isSwimming = false;
            Debug.Log("Player has collided with water.");
        }

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;

    }

}