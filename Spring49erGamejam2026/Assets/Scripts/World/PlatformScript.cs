using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public BoxCollider2D col;
    private Transform player;

    private void Start()
    {
        player = MagicianController.instance.player_obj.transform;
    }

    private void FixedUpdate()
    {
        UpdateCollision();
    }

    private void UpdateCollision()
    {
        if(player.position.y > (transform.position.y + 1f))
        {
            col.enabled = true;
        }
        else if(player.position.y < transform.position.y)
        {
            col.enabled = false;
        }
    }
}
