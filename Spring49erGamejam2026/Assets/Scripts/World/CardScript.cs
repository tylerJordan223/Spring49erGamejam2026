using UnityEngine;

public class CardScript : MonoBehaviour
{
    //simple class to give player a card, drops from enemies

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("did it");
            CardManager.instance.AddCard();
            Destroy(this.gameObject);
        }
    }
}
