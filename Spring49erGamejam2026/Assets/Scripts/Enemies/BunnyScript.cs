using UnityEngine;

public class BunnyScript : MonoBehaviour
{
    public float speed;
    private Transform player;

    private void Start()
    {
        player = MagicianController.instance.player_obj.transform;
    }

    private void FixedUpdate()
    {
        MoveBunny();
    }

    private void MoveBunny()
    {
        Vector3 direction = player.position - transform.position;

        if(direction.x < 0)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if its hit by the weapons
        if(collision.CompareTag("Weapon"))
        {
            //kill the bunny
            Destroy(this.gameObject);
        }
    }
}
