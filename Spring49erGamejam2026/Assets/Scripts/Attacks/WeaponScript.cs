using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public bool fragile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().KillEnemy();

            if(fragile)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
