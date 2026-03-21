using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemy;

    [Header("Timer Settings")]
    public float start_time;
    public float min_time;
    public float shrink_rate;

    private float timer;

    private void Start()
    {
        timer = start_time;
    }

    private void FixedUpdate()
    {
        //code to check for spawn
        if(timer < 0)
        {
            SpawnEnemy();

            //decreasing amount of time each spawn
            if(start_time > min_time)
            {
                start_time -= shrink_rate;
            }

            timer = start_time + Random.Range(-2f, 2f);
        }

        //decrease timer
        timer -= Time.deltaTime;
    }
    private void SpawnEnemy()
    {
        GameObject e = Instantiate(enemy);
        e.transform.position = transform.position;
    }
}
