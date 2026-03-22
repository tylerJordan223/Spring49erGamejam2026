using UnityEngine;

public class EnemyScript: MonoBehaviour
{
    [Header("Enemy Type")]
    public bool bunny;
    public bool dove;

    [SerializeField] GameObject card_drop;

    public float speed;
    private Transform player;
    private void Start()
    {
        player = MagicianController.instance.player_obj.transform;
    }

    private void FixedUpdate()
    {
        if(bunny)
        {
            MoveBunny();
        }else if(dove)
        {
            MoveDove();
        }
    }

    private void MoveDove()
    {
        Vector3 direction = player.position - transform.position;

        transform.position += direction.normalized * Time.deltaTime * speed;
    }

    private void MoveBunny()
    {
        Vector3 direction = player.position - transform.position;

        if (direction.x < 0)
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
        if(collision.CompareTag("Player"))
        {
            MagicianController.instance.DamagePlayer();
        }
    }

    public void KillEnemy()
    {
        //add to the score
        if (bunny)
        {
            GameManager.instance.AddScore(500);
        }
        else if (dove)
        {
            GameManager.instance.AddScore(300);
        }

        //spawn a card and place it at the bunny
        GameObject c = Instantiate(card_drop);
        c.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        GameManager.instance.SpawnPoof(transform);

        Destroy(this.gameObject);
    }
}
