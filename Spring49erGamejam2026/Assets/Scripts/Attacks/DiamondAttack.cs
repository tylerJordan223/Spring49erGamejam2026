using System.Collections.Generic;
using UnityEngine;

public class DiamondAttack : MonoBehaviour
{
    public List<GameObject> projectiles = new List<GameObject>();
    private List<float> velocities = new List<float>();
    public float base_velocity;

    private void Start()
    {
        for(int i = 0; i < projectiles.Count; i++)
        {
            velocities.Add(base_velocity * Mathf.Sign(MagicianController.instance.attack_zone.transform.localPosition.y));
        }
    }

    private void FixedUpdate()
    {
        //for each projectile move it based on velocity
        for(int i = 0; i < projectiles.Count; i++)
        {
            //up the velocity by a bit each frame
            velocities[i] += (2f * ((i+1) * 0.05f)) * Mathf.Sign(MagicianController.instance.attack_zone.transform.localPosition.x);

            Vector3 temp = projectiles[i].transform.position;
            temp.x += velocities[i] * Time.deltaTime;
            projectiles[i].transform.position = temp;
        }

        //delete after a certain distance
        if (projectiles[0].transform.position.x < -50 || projectiles[0].transform.position.x > 50)
        {
            Destroy(this.gameObject);
        }
    }

}
