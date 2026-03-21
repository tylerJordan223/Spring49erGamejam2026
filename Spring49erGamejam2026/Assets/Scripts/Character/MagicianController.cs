using Ginput;
using UnityEngine;

public class MagicianController : MonoBehaviour
{
    [Header("Player Objects")]
    public GameObject player_obj;
    public SpriteRenderer player_sprite;
    public Rigidbody2D rb;

    [Header("Player Values")]
    public float speed;

    private GlobalInput input;

    private void OnEnable()
    {
        input = new GlobalInput();
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 movement = input.Player.Move.ReadValue<Vector2>();

        player_obj.transform.position += new Vector3(movement.x * speed * Time.deltaTime, 0, 0);
    }
}
