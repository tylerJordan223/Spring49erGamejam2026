using UnityEngine;

public class CardScript : MonoBehaviour
{
    private bool falling;
    private void Start()
    {
        falling = true;   
    }

    private void FixedUpdate()
    {
        if(falling)
        {
            transform.position += new Vector3(0f, -1f * Time.deltaTime, 0f);
        }
    }

    //simple class to give player a card, drops from enemies


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if(collision.CompareTag("Player"))
        {
            CardManager.instance.AddCard();
            Destroy(this.gameObject);
        }
        if (collision.CompareTag("Ground"))
        {
            falling = false;
        }
    }
}
