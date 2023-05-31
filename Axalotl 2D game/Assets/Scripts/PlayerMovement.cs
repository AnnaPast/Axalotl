using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float SwimSpeed;
    [SerializeField] private float GravityInWater;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask waterLayer;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool isSwimming;
    private bool isGrounded;
    public Animator animator;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

    int playerObject, colliderObject;
    bool jumpOffEnable = false;

    private void Start()
    {
        playerObject = LayerMask.NameToLayer("Player");
        colliderObject = LayerMask.NameToLayer("Collide");
    }

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (KBCounter <= 0)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
        else
        {
            if (KnockFromRight == true)
            {
                body.velocity = new Vector2(-KBForce, KBForce);
            }
            if (KnockFromRight == false)
            {
                body.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.rotation = Quaternion.identity;
        else if (horizontalInput < -0.01f)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        if (Input.GetKey(KeyCode.Space) && !isSwimming && !isFlying())
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            StartCoroutine("JumpOff");
        }
    }

    private IEnumerator JumpOff()
    {
        jumpOffEnable = true;
        Physics2D.IgnoreLayerCollision(playerObject, colliderObject, true);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(playerObject, colliderObject, false);
        jumpOffEnable = false;
    }

    private bool isFlying()
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
        if (collision.gameObject.name.Equals ("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Water"))
        {
            isSwimming = true;
            Debug.Log("Player has exited water.");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isSwimming = false;
            Debug.Log("Player has exited water.");
        }
    }

    private void FixedUpdate()
    {
        // Check if player is grounded
        isGrounded = IsGrounded();

        if (isSwimming)
        {
            Swim();
            animator.SetBool("Swim", true);
        }
        else animator.SetBool("Swim", false);
    }

    private void Swim()
    {
        float verticalInput = -GravityInWater;
        if (Input.GetKey(KeyCode.Space))
        {
            verticalInput = 1f;
        }

        body.velocity = new Vector2(horizontalInput * SwimSpeed, verticalInput * SwimSpeed);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}