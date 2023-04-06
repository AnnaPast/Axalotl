using UnityEngine;
//test

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool grounded;

    private void Awake()
    {
        //grab references from object
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

    //flip player when moving left-right
       if(horizontalInput>0.01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3 (-1,1,1);

       if (Input.GetKey(KeyCode.Space))
       Jump();
    }

    private void Jump()
    {   body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;

        }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        
    }
}
