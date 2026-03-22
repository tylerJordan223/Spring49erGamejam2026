using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public bool fragile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

            if(fragile)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
