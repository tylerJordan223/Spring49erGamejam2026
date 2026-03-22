using TMPro;
using UnityEngine;

public class HeartAttack : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] TextMeshPro text;
    public float max_fade;
    public float min_fade;
    private int fading = 1;

    public float time_to_health_increase;

    private void Update()
    {
        //fading in and out
        if(sr.color.a < min_fade || sr.color.a > max_fade)
        {
            //need to offbalance so it doesnt get stuck
            sr.color = new Color(sr.color.r, sr.color.b, sr.color.g, sr.color.a + (-fading * 0.05f));
            fading *= -1;
        }

        //fade in either direction necessary
        sr.color = new Color(sr.color.r, sr.color.b, sr.color.g, sr.color.a + (fading * 0.006f));
    }

    private void FixedUpdate()
    {
        //decrease the timer
        time_to_health_increase -= Time.deltaTime;
        text.text = time_to_health_increase.ToString("F2");

        if(time_to_health_increase < 0)
        {
            MagicianController.instance.health += 1;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //failed
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
