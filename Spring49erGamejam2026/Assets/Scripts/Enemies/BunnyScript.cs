using UnityEngine;

public class BunnyScript : MonoBehaviour
{
    [SerializeField] GameObject card_drop;

    public float speed;
    private Transform player;
    private bool quitCheck;

    private void Start()
    {
        player = MagicianController.instance.player_obj.transform;
        quitCheck = false;
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
        //check to damage player on contact
        if (collision.CompareTag("Player"))
        {
            MagicianController.instance.DamagePlayer();
        }
    }

    private void OnApplicationQuit()
    {
        quitCheck = true;
    }

    private void OnDestroy()
    {
        if (quitCheck) return;

        //spawn a card and place it at the bunny
        GameManager.instance.SpawnPoof(transform);

        GameObject c = Instantiate(card_drop);
        c.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }
}
