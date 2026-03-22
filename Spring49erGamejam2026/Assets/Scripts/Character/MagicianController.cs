using Ginput;
using System.Collections;
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
    public GameObject attack_zone;

    [Header("Player Values")]
    public float speed;
    public float max_health;
    public float health;

    //booleans
    private bool can_control;
    private bool can_damage;

    //attack values
    public float attack_cooldown;
    private float attack_timer;

    public GameInput input;

    private bool jumping;

    private void OnEnable()
    {
        //initialize input with necessary function
        input = new GameInput();
        input.Player.Jump.performed += OnJump;
        input.Player.Melee.performed += OnAttack;
        input.Player.Throw.performed += OnThrow;
        input.Player.Stash.performed += OnSwap;
        input.Player.Enable();
    }

    private void Start()
    {
        //begin attacktimer
        attack_timer = attack_cooldown;
        health = max_health;

        //initialize bools
        can_damage = true;
        can_control = true;
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Update()
    {
        //refresh cooldowns
        if(attack_cooldown > 0)
        {
            attack_cooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(can_control)
        {
            MovePlayer();
        }

        //checking for fall
        if(rb.linearVelocity.y < -0.5 && !jumping)
        {
            jumping = true;
        }
    }

    private void MovePlayer()
    {
        Vector2 movement = input.Player.Move.ReadValue<Vector2>();

        player_obj.transform.position += new Vector3(movement.x * speed * Time.deltaTime, 0, 0);

        //update the attack zone if moving
        if(movement != Vector2.zero)
        {
            Vector3 pos = player_obj.transform.position;
            pos.x += (player_sprite.bounds.size.x) * Mathf.Sign(movement.x);
            attack_zone.transform.position = pos;
        }
    }

    public void DamagePlayer()
    {
        if(can_damage)
        {
            can_damage = false;

            health -= 1;

            if(health <= 0)
            {
                DisablePlayer();
                GameManager.instance.GameOver();
            }

            //do the cooldown
            StartCoroutine(damageTimeOut());
        }
    }

    //invincibility timeout
    private IEnumerator damageTimeOut()
    {
        player_sprite.color = new Color(player_sprite.color.r, player_sprite.color.r, player_sprite.color.r, 0.5f);
        yield return new WaitForSeconds(1f);
        player_sprite.color = new Color(player_sprite.color.r, player_sprite.color.r, player_sprite.color.r, 1f);
        can_damage = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            //make sure it happened while you were jumping
            if(jumping)
            {
                jumping = false;
            }
        }
    }

    //INPUT CONTROLS//
    private void OnJump(InputAction.CallbackContext context)
    {
        if (!jumping)
        {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            jumping = true;
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (attack_cooldown < 0)
        {
            StartCoroutine(Attack());
        }
    }

    //coroutine to attack while animation is playing (roughly 1 second)
    private IEnumerator Attack()
    {
        attack_zone.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(1f);
        attack_zone.GetComponent<BoxCollider2D>().enabled = false;
        attack_cooldown = 1f;
    }

    //function to throw a card
    private void OnThrow(InputAction.CallbackContext context)
    {
        CardManager.instance.ThrowCard();
    }

    //function to swap a card
    private void OnSwap(InputAction.CallbackContext context)
    {
        CardManager.instance.SwapBackup();
    }

    //debug function to add a card
    private void AddCard(InputAction.CallbackContext context)
    {
        CardManager.instance.AddCard();
    }

    //helper functions
    public void EnablePlayer()
    {
        can_control = true;
        input.Player.Enable();
    }

    public void DisablePlayer()
    {
        can_control = false;
        input.Player.Disable();
    }
}
