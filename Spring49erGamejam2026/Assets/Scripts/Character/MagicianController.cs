using Ginput;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagicianController : MonoBehaviour
{
    //singleton
    public static MagicianController instance;
    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(this.gameObject);
        }
        instance = this;
    }

    [Header("Player Objects")]
    public GameObject player_obj;
    public SpriteRenderer player_sprite;
    public Rigidbody2D rb;

    [Header("Player Values")]
    public float speed;

    private GameInput input;

    private bool jumping;

    private void OnEnable()
    {
        input = new GameInput();
        input.Player.Jump.performed += OnJump;
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void FixedUpdate()
    {
        MovePlayer();

        //checking for fall
        if(rb.linearVelocity.y < 0 && !jumping)
        {
            jumping = true;
        }
    }

    private void MovePlayer()
    {
        Vector2 movement = input.Player.Move.ReadValue<Vector2>();

        player_obj.transform.position += new Vector3(movement.x * speed * Time.deltaTime, 0, 0);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(!jumping)
        {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            jumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            jumping = false;
        }
    }
}
