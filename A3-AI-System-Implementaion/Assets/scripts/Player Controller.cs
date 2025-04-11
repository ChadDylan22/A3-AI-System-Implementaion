using UnityEngine;

public class Player : MonoBehaviour
{
    //Properties / Variables
    private new Rigidbody2D rigidbody;

    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }


    private void Awake() //gets the rigidbody component from player
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() //Calls the Circlecast to determine if player is grounded
    {
        HorizontalMovement(); 

        grounded = rigidbody.Raycast(Vector2.down);

        if (grounded){
            GroundedMovement();
        }

          ApplyGravity();
    }

    private void HorizontalMovement() //Gets key inputs from unity's input system
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }

    private void GroundedMovement() //Determins how movement should work if player is grounded / allows jumping only when grounded
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity() //Applies gravifty when player is falling
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate() //Allows the rigidbody to move the player
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        rigidbody.MovePosition(position);   
    }

}
