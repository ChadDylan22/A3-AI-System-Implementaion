using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
   
    // values for gravity and hight of jump
    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;
    public float speed = 5f;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    // Allows code to make the player jump if space bar is pressed
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
            }

            if (Input.GetButton("left")) {
                direction = Vector3.left * speed;
            }

             if (Input.GetButton("right")) {
                direction = Vector3.right * speed;
            }
        }

        character.Move(direction * Time.deltaTime);
    }
// calls GameOver function from the Game Manager script when the player collides with an obstacle, ending the game.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy")){
            GameManager.Instance.GameOver();
        }
    }
}