using UnityEngine;

public class BombAttack : MonoBehaviour
{
    public GameObject explosion;

    public Transform start_point;
    public Transform end_point;
    private Vector3 startpos;
    private Vector3 endpos;

    public float duration;
    public float arc_height;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;

        //adjust for direction
        Vector3 ep = end_point.localPosition;
        ep.x *= Mathf.Sign(MagicianController.instance.attack_zone.transform.localPosition.x);
        end_point.localPosition = ep;

        startpos = start_point.position;
        endpos = end_point.position;
    }
    
    private void Update()
    {
        float t = (Time.time - startTime) / duration; //normalizes time to 0-1
        Vector3 pos = new Vector3();
        //just go down until hitting something
        if (t > 1.0f)
        {
            //just move down
            pos = transform.position;
            pos.y -= 15f * Time.deltaTime;
        }
        else
        {
            //arc
            //move overtime to position
            pos = Vector3.Lerp(startpos, endpos, t);
            //apply the arc
            pos.y += arc_height * Mathf.Sin(t * Mathf.PI);
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {
            GameObject go = Instantiate(explosion);
            go.transform.position = transform.position;
            Destroy(this.gameObject);
        }
    }
}
