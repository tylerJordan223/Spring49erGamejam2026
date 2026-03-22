using System.Collections.Generic;
using UnityEngine;

public class ClubsAttack : MonoBehaviour
{
    public List<GameObject> projectiles = new List<GameObject>();
    public float speed;

    private void Start()
    {
        //get the direction of player movement
        Vector2 movement = MagicianController.instance.input.Player.Move.ReadValue<Vector2>();

        if(movement != Vector2.zero)
        {
            //set the direction to where the player is moving
            transform.up = new Vector3(movement.x, movement.y, 0);
        }
        else
        {
            //otherwise do it whatever direction the player is facing
            transform.up = new Vector3(Mathf.Sign(MagicianController.instance.attack_zone.transform.localPosition.x), 0f, 0f);
        }
    }

    private void FixedUpdate()
    {
        foreach(GameObject p in projectiles)
        {
            if(p)
            {
                p.transform.position += p.transform.up.normalized * speed * Time.deltaTime;

                //make sure to destroy this game object so they dont fly infinitely
                if(Mathf.Abs(p.transform.position.x) > 60f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        //increase speed overtime
        speed += 0.4f;
    }
}
