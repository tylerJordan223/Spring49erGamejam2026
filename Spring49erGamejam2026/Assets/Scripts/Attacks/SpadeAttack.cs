using UnityEngine;

public class SpadeAttack : MonoBehaviour
{
    //spade attack spins, removes spades as they hit enemies

    public float spin_speed;
    public float time_length;

    private void Start()
    {
        //set parent to magician
        transform.SetParent(MagicianController.instance.transform);
    }
    private void Update()
    {
        //shrink timer
        if(time_length > 0)
        {
            time_length -= Time.deltaTime;
        }

        //delete on timeout
        if(time_length < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, 0f, 1f), spin_speed);
    }
}
