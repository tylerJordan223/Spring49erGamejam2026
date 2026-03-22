using UnityEngine;

public class DoveScript : MonoBehaviour
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
        MoveDove();
    }

    private void MoveDove()
    {
        Vector3 direction = player.position - transform.position;

        transform.position += direction.normalized * Time.deltaTime * speed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //check to damage player on contact
        if(collision.CompareTag("Player"))
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
        GameObject c = Instantiate(card_drop);
        c.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        GameManager.instance.SpawnPoof(transform);
    }
}
